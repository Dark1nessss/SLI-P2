using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SLI_P2.Models;

namespace SLI_P2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SliHelper _helper = new SliHelper();
        private Veiculo _veiculoSelecionado = null;
        private string _caminhoDavSelecionado = string.Empty;
        private ContactoHelper _contactoHelper = new ContactoHelper();
        private Contacto _contactoSelecionado = null;

        public MainWindow()
        {
            InitializeComponent();
            cbTipoVeiculo.SelectedIndex = 0;
            cbAtributoEspecifico.IsEnabled = false;
            AtualizarInterface();
        }

        private void cbTipoVeiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbTipoCombustivel.Items.Clear();
            switch (cbTipoVeiculo.SelectedIndex)
            {
                case 0: // Combustão
                    cbTipoCombustivel.Items.Add("Gasolina");
                    cbTipoCombustivel.Items.Add("Diesel");
                    break;
                case 1: // EV
                    cbTipoCombustivel.Items.Add("Elétrico");
                    break;
                case 2: // PHEV
                    cbTipoCombustivel.Items.Add("Híbrido Gasolina");
                    cbTipoCombustivel.Items.Add("Híbrido Diesel");
                    cbTipoCombustivel.Items.Add("Plug-in Gasolina");
                    cbTipoCombustivel.Items.Add("Plug-in Diesel");
                    break;
                case 3: // Mota
                    cbTipoCombustivel.Items.Add("Gasolina");
                    break;
            }
            cbTipoCombustivel.SelectedIndex = 0;

            if (cbTipoVeiculo.SelectedIndex == 1)
            {
                panelCilindrada.Visibility = Visibility.Collapsed;
                panelEletrico.Visibility = Visibility.Visible;
            }
            else
            {
                panelCilindrada.Visibility = Visibility.Visible;
                panelEletrico.Visibility = Visibility.Collapsed;
            }

            if (cbTipoVeiculo.SelectedIndex == 0 || cbTipoVeiculo.SelectedIndex == 2)
            {
                panelAmbiental.Visibility = Visibility.Visible;
            }
            else
            {
                panelAmbiental.Visibility = Visibility.Collapsed;
            }

            if (cbTipoVeiculo.SelectedIndex == 2)
            {
                panelHibrido.Visibility = Visibility.Visible;
            }
            else
            {
                panelHibrido.Visibility = Visibility.Collapsed;
            }
        }

        // Método para o botão de selecionar o ficheiro PDF (Anexar DAV)
        private void btnAnexarDav_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog janelaFicheiro = new Microsoft.Win32.OpenFileDialog();
            janelaFicheiro.Filter = "Documentos PDF (*.pdf)|*.pdf";
            janelaFicheiro.Title = "Selecionar PDF da DAV";

            if (janelaFicheiro.ShowDialog() == true)
            {
                _caminhoDavSelecionado = janelaFicheiro.FileName;
                lblCaminhoDav.Text = janelaFicheiro.SafeFileName;
                btnVerPdf.IsEnabled = true;
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMarca.Text) || string.IsNullOrWhiteSpace(txtModelo.Text) || string.IsNullOrWhiteSpace(txtVin.Text))
                {
                    throw new ArgumentException("Preencha a Marca, Modelo e VIN do veículo.");
                }

                if (!int.TryParse(txtAno.Text, out int ano) || ano < 1900 || ano > 2026)
                {
                    throw new FormatException("Ano inválido (insira um valor entre 1900 e 2026).");
                }

                decimal.TryParse(txtPrecoBase.Text, out decimal precoBase);
                decimal.TryParse(txtCustosTransporte.Text, out decimal transporte);

                Veiculo novoVeiculo;

                if (_veiculoSelecionado == null)
                {
                    switch (cbTipoVeiculo.SelectedIndex)
                    {
                        case 0:
                            novoVeiculo = new VeiculoCombustao();
                            break;
                        case 1:
                            novoVeiculo = new VeiculoEletrico();
                            break;
                        case 2:
                            novoVeiculo = new VeiculoHibridoPlugin();
                            break;
                        default:
                            novoVeiculo = new VeiculoMotociclo();
                            break;
                    }
                }
                else
                {
                    novoVeiculo = _veiculoSelecionado;
                }

                novoVeiculo.Marca = txtMarca.Text;
                novoVeiculo.Modelo = txtModelo.Text;
                novoVeiculo.Vin = txtVin.Text;
                novoVeiculo.Ano = ano;

                if (cbTipoCombustivel.SelectedItem != null)
                {
                    novoVeiculo.TipoCombustivel = cbTipoCombustivel.SelectedItem.ToString();
                }
                else
                {
                    novoVeiculo.TipoCombustivel = "Gasolina";
                }

                novoVeiculo.PrecoBase = precoBase;
                novoVeiculo.CustosTransporte = transporte;

                if (chkIsImportacaoUe.IsChecked != null)
                {
                    novoVeiculo.IsImportacaoUe = chkIsImportacaoUe.IsChecked.Value;
                }
                else
                {
                    novoVeiculo.IsImportacaoUe = true;
                }

                // Configuração de campos por tipo (Cast clássico)
                if (novoVeiculo is VeiculoMotociclo)
                {
                    VeiculoMotociclo mota = (VeiculoMotociclo)novoVeiculo;
                    int.TryParse(txtCilindrada.Text, out int cc);
                    mota.Cilindrada = cc;
                }
                else if (novoVeiculo is VeiculoCombustao)
                {
                    VeiculoCombustao combustao = (VeiculoCombustao)novoVeiculo;
                    int.TryParse(txtCilindrada.Text, out int cc);
                    int.TryParse(txtCO2.Text, out int co2);
                    double.TryParse(txtParticulas.Text, out double part);

                    combustao.Cilindrada = cc;
                    combustao.EmissoesCO2 = co2;
                    combustao.Particulas = part;

                    if (novoVeiculo is VeiculoHibridoPlugin)
                    {
                        VeiculoHibridoPlugin hibrido = (VeiculoHibridoPlugin)novoVeiculo;
                        int.TryParse(txtAutonomia.Text, out int aut);
                        hibrido.AutonomiaEletrica = aut;
                    }
                }
                else if (novoVeiculo is VeiculoEletrico)
                {
                    VeiculoEletrico eletrico = (VeiculoEletrico)novoVeiculo;
                    double.TryParse(txtKwhBateria.Text, out double bat);
                    eletrico.KwhBateria = bat;
                }

                // Associa o Documento DAV se um ficheiro tiver sido selecionado
                if (string.IsNullOrEmpty(_caminhoDavSelecionado) == false)
                {
                    Documento dav = new Documento();
                    dav.TipoDocumento = "Declaração Aduaneira de Veículos (DAV)";
                    dav.DataEmissao = DateTime.Now;
                    dav.CaminhoFicheiro = _caminhoDavSelecionado;

                    novoVeiculo.Documentos.Clear(); // Evita acumular duplicados na edição
                    novoVeiculo.Documentos.Add(dav);
                }

                if (_veiculoSelecionado == null)
                {
                    _helper.Insert(novoVeiculo);
                }
                else
                {
                    _helper.Atualizar(novoVeiculo);
                }

                LimparFormulario();
                AtualizarInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lvVeiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _veiculoSelecionado = lvVeiculos.SelectedItem as Veiculo;
            if (_veiculoSelecionado == null) return;

            txtMarca.Text = _veiculoSelecionado.Marca;
            txtModelo.Text = _veiculoSelecionado.Modelo;
            txtVin.Text = _veiculoSelecionado.Vin;
            txtAno.Text = _veiculoSelecionado.Ano.ToString();
            txtPrecoBase.Text = _veiculoSelecionado.PrecoBase.ToString("F2");
            txtCustosTransporte.Text = _veiculoSelecionado.CustosTransporte.ToString("F2");
            chkIsImportacaoUe.IsChecked = _veiculoSelecionado.IsImportacaoUe;

            // Carrega o documento existente de forma clássica se houver
            if (_veiculoSelecionado.Documentos.Count > 0)
            {
                Documento docExistente = _veiculoSelecionado.Documentos[0];
                _caminhoDavSelecionado = docExistente.CaminhoFicheiro;
                lblCaminhoDav.Text = "DAV Anexada";
                btnVerPdf.IsEnabled = true;
            }
            else
            {
                _caminhoDavSelecionado = "";
                lblCaminhoDav.Text = "Nenhum ficheiro selecionado";
                btnVerPdf.IsEnabled = false;
            }

            if (_veiculoSelecionado is VeiculoMotociclo)
            {
                VeiculoMotociclo mota = (VeiculoMotociclo)_veiculoSelecionado;
                cbTipoVeiculo.SelectedIndex = 3;
                txtCilindrada.Text = mota.Cilindrada.ToString();
            }
            else if (_veiculoSelecionado is VeiculoHibridoPlugin)
            {
                VeiculoHibridoPlugin hibrido = (VeiculoHibridoPlugin)_veiculoSelecionado;
                cbTipoVeiculo.SelectedIndex = 2;
                txtCilindrada.Text = hibrido.Cilindrada.ToString();
                txtCO2.Text = hibrido.EmissoesCO2.ToString();
                txtParticulas.Text = hibrido.Particulas.ToString();
                txtAutonomia.Text = hibrido.AutonomiaEletrica.ToString();
            }
            else if (_veiculoSelecionado is VeiculoCombustao)
            {
                VeiculoCombustao combustao = (VeiculoCombustao)_veiculoSelecionado;
                cbTipoVeiculo.SelectedIndex = 0;
                txtCilindrada.Text = combustao.Cilindrada.ToString();
                txtCO2.Text = combustao.EmissoesCO2.ToString();
                txtParticulas.Text = combustao.Particulas.ToString();
            }
            else if (_veiculoSelecionado is VeiculoEletrico)
            {
                VeiculoEletrico eletrico = (VeiculoEletrico)_veiculoSelecionado;
                cbTipoVeiculo.SelectedIndex = 1;
                txtKwhBateria.Text = eletrico.KwhBateria.ToString();
            }

            cbTipoCombustivel.SelectedItem = _veiculoSelecionado.TipoCombustivel;

            lblAvisoVeiculo.Text = _veiculoSelecionado.Marca + " " + _veiculoSelecionado.Modelo;
            panelAvisoEdicao.Visibility = Visibility.Visible;
            btnAdicionar.Content = "Atualizar Dados";
            btnEliminar.IsEnabled = true;
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (_veiculoSelecionado != null)
            {
                MessageBoxResult res = MessageBox.Show("Cancelar as alterações atuais?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No) return;
            }
            LimparFormulario();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (_veiculoSelecionado != null)
            {
                MessageBoxResult res = MessageBox.Show("Remover este veículo da lista?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    _helper.Apagar(_veiculoSelecionado);
                    LimparFormulario();
                    AtualizarInterface();
                }
            }
        }

        private void AtualizarInterface()
        {
            lvVeiculos.ItemsSource = null;
            lvVeiculos.ItemsSource = App.lstVeiculos;

            lblMediaCustos.Text = _helper.ObterMediaCustosLegalizacao().ToString("N2") + " €";
            lblMediaCO2.Text = _helper.ObterMediaEmissoesCO2().ToString("F1") + " g/km";

            int carros = 0;
            int motas = 0;

            foreach (Veiculo v in App.lstVeiculos)
            {
                if (v is VeiculoCombustao || v is VeiculoEletrico)
                {
                    carros++;
                }
                else if (v is VeiculoMotociclo)
                {
                    motas++;
                }
            }

            lblContadorTipos.Text = "Carros: " + carros + " | Motas: " + motas;
        }

        private void btnVerPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_caminhoDavSelecionado))
                {
                    System.Diagnostics.ProcessStartInfo informacaoJanela = new System.Diagnostics.ProcessStartInfo();
                    informacaoJanela.FileName = _caminhoDavSelecionado;
                    informacaoJanela.UseShellExecute = true;

                    System.Diagnostics.Process.Start(informacaoJanela);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir o PDF: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimparFormulario()
        {
            _veiculoSelecionado = null;
            _caminhoDavSelecionado = string.Empty;

            if (panelAvisoEdicao != null) panelAvisoEdicao.Visibility = Visibility.Collapsed;
            if (lblCaminhoDav != null) lblCaminhoDav.Text = "Nenhum ficheiro selecionado";
            if (btnVerPdf != null) btnVerPdf.IsEnabled = false;

            txtMarca.Clear(); txtModelo.Clear(); txtVin.Clear();
            txtAno.Text = "2026";
            txtPrecoBase.Clear(); txtCustosTransporte.Clear();
            txtCilindrada.Clear(); txtCO2.Clear(); txtParticulas.Text = "0";
            txtAutonomia.Clear(); txtKwhBateria.Clear();
            cbTipoVeiculo.SelectedIndex = 0;
            btnAdicionar.Content = "Gravar";
            btnEliminar.IsEnabled = false;
            lvVeiculos.SelectedItem = null;
        }

        private void btnPresetCarro_Click(object sender, RoutedEventArgs e)
        {
            cbTipoVeiculo.SelectedIndex = 0;
            cbTipoCombustivel.SelectedItem = "Diesel";
            txtMarca.Text = "Audi"; txtModelo.Text = "A4 Avant"; txtVin.Text = "WAUZZZ8KZA123456";
            txtAno.Text = "2021"; txtPrecoBase.Text = "18500"; txtCustosTransporte.Text = "450";
            chkIsImportacaoUe.IsChecked = true; txtCilindrada.Text = "1968"; txtCO2.Text = "118"; txtParticulas.Text = "0.001";
        }

        private void btnPresetMota_Click(object sender, RoutedEventArgs e)
        {
            cbTipoVeiculo.SelectedIndex = 3;
            cbTipoCombustivel.SelectedItem = "Gasolina";
            txtMarca.Text = "Yamaha"; txtModelo.Text = "XVS 950 Midnight Star"; txtVin.Text = "JYAVN02100001234";
            txtAno.Text = "2015"; txtPrecoBase.Text = "6200"; txtCustosTransporte.Text = "300";
            chkIsImportacaoUe.IsChecked = true; txtCilindrada.Text = "942";
        }

        // -------------------------- Contacto ----------------------------------------

        private void cbTipoContacto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipoContacto.SelectedIndex != -1)
            {

                cbAtributoEspecifico.IsEnabled = true;
                cbAtributoEspecifico.Items.Clear();

                if (cbTipoContacto.SelectedIndex == 0) // Comprador
                {
                    lblAtributoEspecifico.Text = "Tipo de Cliente:";
                    cbAtributoEspecifico.Items.Add("Particular");
                    cbAtributoEspecifico.Items.Add("Empresa");
                    cbAtributoEspecifico.Items.Add("Stand");
                }
                else // Vendedor
                {
                    lblAtributoEspecifico.Text = "Stand Comercial:";
                    cbAtributoEspecifico.Items.Add("Stand Local");
                    cbAtributoEspecifico.Items.Add("Concessionário");
                    cbAtributoEspecifico.Items.Add("Importador Direto");
                }
                cbAtributoEspecifico.SelectedIndex = 0;
            }
            else
            {
                cbAtributoEspecifico.IsEnabled = false;
            }
        }

        private void btnGravarContacto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeContacto.Text) || string.IsNullOrWhiteSpace(txtNifContacto.Text))
            {
                MessageBox.Show("Por favor, preencha o Nome e o NIF do contacto.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isNovo = (_contactoSelecionado == null);
            Contacto contactoTratado;

            if (isNovo)
            {
                if (cbTipoContacto.SelectedIndex == 0)
                {
                    contactoTratado = new Comprador();
                }
                else
                {
                    contactoTratado = new Vendedor();
                }
            }
            else
            {
                contactoTratado = _contactoSelecionado;
            }

            contactoTratado.Nome = txtNomeContacto.Text;
            contactoTratado.NIF = txtNifContacto.Text;

            if (contactoTratado is Comprador comprador)
            {
                comprador.TipoCliente = cbAtributoEspecifico.Text;
            }
            else if (contactoTratado is Vendedor vendedor)
            {
                vendedor.StandComercial = cbAtributoEspecifico.Text;
            }

            if (isNovo)
            {
                _contactoHelper.Insert(contactoTratado);
                MessageBox.Show("Contacto registado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _contactoHelper.Atualizar(contactoTratado);
                MessageBox.Show("Contacto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            AtualizarInterfaceContactos();
            LimparFormularioContacto();
        }

        private void lvContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _contactoSelecionado = lvContactos.SelectedItem as Contacto;

            if (_contactoSelecionado != null)
            {
                txtNomeContacto.Text = _contactoSelecionado.Nome;
                txtNifContacto.Text = _contactoSelecionado.NIF;

                if (_contactoSelecionado is Comprador comprador)
                {
                    cbTipoContacto.SelectedIndex = 0;
                    cbAtributoEspecifico.Text = comprador.TipoCliente;
                }
                else if (_contactoSelecionado is Vendedor vendedor)
                {
                    cbTipoContacto.SelectedIndex = 1;
                    cbAtributoEspecifico.Text = vendedor.StandComercial;
                }

                btnGravarContacto.Content = "Atualizar";
                btnEliminarContacto.IsEnabled = true;
                cbTipoContacto.IsEnabled = false;
            }
        }

        private void btnEliminarContacto_Click(object sender, RoutedEventArgs e)
        {
            if (_contactoSelecionado != null)
            {
                var confirmacao = MessageBox.Show($"Tem a certeza que deseja eliminar o contacto {_contactoSelecionado.Nome}?",
                    "Confirmar Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmacao == MessageBoxResult.Yes)
                {
                    _contactoHelper.Apagar(_contactoSelecionado);
                    AtualizarInterfaceContactos();
                    LimparFormularioContacto();
                }
            }
        }

        private void btnLimparContacto_Click(object sender, RoutedEventArgs e)
        {
            LimparFormularioContacto();
        }

        private void AtualizarInterfaceContactos()
        {
            lvContactos.ItemsSource = null;
            lvContactos.ItemsSource = App.lstContactos;
        }

        private void LimparFormularioContacto()
        {
            txtNomeContacto.Clear();
            txtNifContacto.Clear();
            cbAtributoEspecifico.Text = "";
            cbTipoContacto.SelectedIndex = 0;
            cbTipoContacto.IsEnabled = true;

            btnGravarContacto.Content = "Gravar";
            btnEliminarContacto.IsEnabled = false;
            _contactoSelecionado = null;
            lvContactos.SelectedItem = null;
        }

        // -------------------------- Processos ----------------------------------------

        private void CarregarProcessos()
        {
            cbProcVeiculo.ItemsSource = App.lstVeiculos;
            cbProcContacto.ItemsSource = App.lstContactos;
            cbProcAlfandega.ItemsSource = App.lstAlfandegas;

            dgProcessos.ItemsSource = null;
            dgProcessos.ItemsSource = App.lstProcessos;
        }

        // Evento do botão
        private void btnCriarProcesso_Click(object sender, RoutedEventArgs e)
        {
            if (cbProcVeiculo.SelectedItem == null || cbProcContacto.SelectedItem == null)
            {
                MessageBox.Show("Selecione veículo e contacto!");
                return;
            }

            Processo p = new Processo();
            p.VeiculoAssociado = (Veiculo)cbProcVeiculo.SelectedItem;
            p.ContactoAssociado = (Contacto)cbProcContacto.SelectedItem;
            p.AlfandegaDestino = (Alfandega)cbProcAlfandega.SelectedItem;
            p.Estado = "Pendente";

            App.lstProcessos.Add(p);
            CarregarProcessos();
        }
    }
}