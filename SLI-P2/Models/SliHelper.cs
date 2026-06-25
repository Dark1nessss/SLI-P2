using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class SliHelper
    {
        public void Insert(Veiculo veiculo)
        {
            App.lstVeiculos.Add(veiculo);
        }

        public void Apagar(Veiculo veiculo)
        {
            var veiculoExistente = App.lstVeiculos.FirstOrDefault(v => v.IdVeiculo == veiculo.IdVeiculo);
            if (veiculoExistente != null)
            {
                App.lstVeiculos.Remove(veiculoExistente);
            }
        }

        public void Atualizar(Veiculo veiculo)
        {
            var vExistente = App.lstVeiculos.FirstOrDefault(v => v.IdVeiculo == veiculo.IdVeiculo);
            if (vExistente != null)
            {
                // Atualiza os dados comuns da classe mãe
                vExistente.Vin = veiculo.Vin;
                vExistente.Marca = veiculo.Marca;
                vExistente.Modelo = veiculo.Modelo;
                vExistente.Ano = veiculo.Ano;
                vExistente.PrecoBase = veiculo.PrecoBase;
                vExistente.CustosTransporte = veiculo.CustosTransporte;
                vExistente.IsImportacaoUe = veiculo.IsImportacaoUe;
                vExistente.TipoCombustivel = veiculo.TipoCombustivel;

                // Se for a combustão, atualiza os campos específicos com cast seguro
                if (vExistente is VeiculoCombustao && veiculo is VeiculoCombustao)
                {
                    ((VeiculoCombustao)vExistente).Cilindrada = ((VeiculoCombustao)veiculo).Cilindrada;
                    ((VeiculoCombustao)vExistente).EmissoesCO2 = ((VeiculoCombustao)veiculo).EmissoesCO2;
                    ((VeiculoCombustao)vExistente).Particulas = ((VeiculoCombustao)veiculo).Particulas;
                }
                // Se for híbrido plug-in, atualiza a autonomia elétrica
                if (vExistente is VeiculoHibridoPlugin && veiculo is VeiculoHibridoPlugin)
                {
                    ((VeiculoHibridoPlugin)vExistente).AutonomiaEletrica = ((VeiculoHibridoPlugin)veiculo).AutonomiaEletrica;
                }
                // Se for elétrico, atualiza os kWh da bateria
                if (vExistente is VeiculoEletrico && veiculo is VeiculoEletrico)
                {
                    ((VeiculoEletrico)vExistente).KwhBateria = ((VeiculoEletrico)veiculo).KwhBateria;
                }
                if (vExistente is VeiculoMotociclo && veiculo is VeiculoMotociclo)
                {
                    ((VeiculoMotociclo)vExistente).Cilindrada = ((VeiculoMotociclo)veiculo).Cilindrada;
                }
            }
        }

        // --- Métricas para o painel de Backoffice ---
        public decimal ObterMediaCustosLegalizacao()
        {
            if (App.lstVeiculos.Count == 0) return 0m;
            decimal somaTotal = App.lstVeiculos.Sum(v => v.CalcularISV() + v.CalcularIVA());
            return somaTotal / App.lstVeiculos.Count;
        }

        public double ObterMediaEmissoesCO2()
        {
            var veiculosComCO2 = App.lstVeiculos.Where(v => v is VeiculoCombustao).ToList();
            if (veiculosComCO2.Count == 0) return 0.0;
            double somaCO2 = veiculosComCO2.Sum(v => ((VeiculoCombustao)v).EmissoesCO2);
            return somaCO2 / veiculosComCO2.Count;
        }

        public int ContarVeiculosPorTipo<T>() where T : Veiculo
        {
            return App.lstVeiculos.OfType<T>().Count();
        }
    }
}