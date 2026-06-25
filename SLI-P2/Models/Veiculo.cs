using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SLI_P2.Models
{
    public class Veiculo
    {
        private Guid _idVeiculo;
        private string _vin;
        private string _marca;
        private string _modelo;
        private int _ano;
        private string _tipoCombustivel;
        private decimal _precoBase;
        private decimal _custosTransporte;
        private bool _isImportacaoUe;
        private string _tipoVeiculo;
        private List<Documento> _documentos;

        public Guid IdVeiculo { get { return _idVeiculo; } }

        public string Vin
        {
            get { return _vin; }
            set
            {
                _vin = value.Trim().ToUpper();
                if (_vin.Length == 0) { 
                    _vin = "SEM_VIN"; 
                }
            }
        }

        public string Marca
        {
            get { return _marca; }
            set
            {
                _marca = value.Trim();
                if (_marca.Length == 0) { 
                    _marca = "Marca Geral"; 
                }
            }
        }

        public string Modelo
        {
            get { return _modelo; }
            set
            {
                _modelo = value.Trim();
                if (_modelo.Length == 0) { 
                    _modelo = "Modelo Geral"; 
                }
            }
        }

        public int Ano
        {
            get { return _ano; }
            set
            {
                _ano = value;
                if (_ano < 1900 || _ano > 2026) 
                { 
                    _ano = 2026; 
                }
            }
        }

        public string TipoCombustivel
        {
            get { return _tipoCombustivel; }
            set
            {
                _tipoCombustivel = value.Trim();
                if (_tipoCombustivel.Length == 0) { 
                    _tipoCombustivel = "Gasolina"; 
                }
            }
        }

        public decimal PrecoBase
        {
            get { return _precoBase; }
            set
            {
                _precoBase = value;
                if (_precoBase < 0) { 
                    _precoBase = 0; 
                }
            }
        }

        public decimal CustosTransporte
        {
            get { return _custosTransporte; }
            set
            {
                _custosTransporte = value;
                if (_custosTransporte < 0) { 
                    _custosTransporte = 0; 
                }
            }
        }

        public bool IsImportacaoUe
        {
            get { return _isImportacaoUe; }
            set { _isImportacaoUe = value; }
        }

        public string TipoVeiculo
        {
            get { return _tipoVeiculo; }
            set { _tipoVeiculo = value; }
        }

        public List<Documento> Documentos
        {
            get { return _documentos; }
        }

        public virtual decimal CalcularISV()
        {
            return 0m;
        }

        public virtual string ObterDescricaoTipo()
        {
            return "Veículo Geral";
        }

        public decimal CalcularIVA()
        {
            // Se for da União Europeia, não paga IVA em Portugal (já foi pago na origem)
            if (IsImportacaoUe)
            {
                return 0m;
            }

            // Se for Fora da UE (ex: Suíça): Aplica os 10% de Direitos Aduaneiros primeiro
            decimal direitosAduaneiros = PrecoBase * 0.10m;

            // A base do IVA além do carro e transporte, inclui o ISV e a Alfândega
            decimal baseCalculo = PrecoBase + CalcularISV() + CustosTransporte + direitosAduaneiros;

            return baseCalculo * 0.23m;
        }

        public decimal ObterFatorMultiplicadorIdade()
        {
            int idade = DateTime.Now.Year - Ano;

            if (idade <= 1) return 0.90m;
            if (idade == 2) return 0.80m;
            if (idade == 3) return 0.72m;
            if (idade == 4) return 0.65m;
            if (idade == 5) return 0.57m;
            if (idade >= 6 && idade <= 7) return 0.48m;
            if (idade >= 8 && idade <= 10) return 0.40m;

            return 0.20m;
        }

        public decimal ValorISV()
        { 
            return CalcularISV(); 
        }
        public decimal ValorIVA()
        { 
            return CalcularIVA(); 
        }
        public string DescricaoTipo()
        { 
            return ObterDescricaoTipo(); 
        }
        public decimal DireitosAduaneiros()
        {
            if (IsImportacaoUe)
            {
                return 0m;
            }
            return PrecoBase * 0.10m;
        }
        public decimal CustoTotal() => PrecoBase + CustosTransporte + ValorISV() + ValorIVA() + DireitosAduaneiros();
        public Veiculo()
        {
            _idVeiculo = Guid.NewGuid();
            _vin = "";
            _marca = "";
            _modelo = "";
            _ano = 2026;
            _tipoCombustivel = "";
            _precoBase = 0;
            _custosTransporte = 0;
            _isImportacaoUe = true;
            _tipoVeiculo = "";
            _documentos = new List<Documento>();
        }
    }
}