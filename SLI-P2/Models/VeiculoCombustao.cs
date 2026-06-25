using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class VeiculoCombustao : Veiculo
    {
        private int _cilindrada;
        private int _emissoesCO2;
        private double _particulas;

        public int Cilindrada
        {
            get { return _cilindrada; }
            set { _cilindrada = value < 0 ? 0 : value; }
        }

        public int EmissoesCO2  
        {
            get { return _emissoesCO2; }
            set { _emissoesCO2 = value < 0 ? 0 : value; }
        }

        public double Particulas
        {
            get { return _particulas; }
            set { _particulas = value < 0 ? 0 : value; }
        }

        public VeiculoCombustao() : base()
        {
            Cilindrada = 0;
            EmissoesCO2 = 0;
            Particulas = 0;
        }

        public override string ObterDescricaoTipo()
        {
            return $"Combustão ({TipoCombustivel})";
        }

        public override decimal CalcularISV()
        {
            // 1. Componente Cilindrada
            decimal taxaCC = 4.90m;
            decimal abaterCC = 5643.20m;

            if (Cilindrada <= 1250)
            {
                taxaCC = 0.99m;
                abaterCC = 751.35m;
            }
            decimal compCilindrada = (Cilindrada * taxaCC) - abaterCC;
            if (compCilindrada < 0) compCilindrada = 0;

            // 2. Componente Ambiental (CO2)
            decimal taxaCO2 = 0m;
            decimal abaterCO2 = 0m;

            bool isDiesel = TipoCombustivel.Trim().ToLower().Contains("diesel") ||
                            TipoCombustivel.Trim().ToLower().Contains("gasóleo");

            if (isDiesel)
            {
                // Tabela Diesel
                if (EmissoesCO2 <= 79) { taxaCO2 = 5.20m; abaterCO2 = 415.10m; }
                else if (EmissoesCO2 <= 95) { taxaCO2 = 22.40m; abaterCO2 = 1775.30m; }
                else if (EmissoesCO2 <= 120) { taxaCO2 = 69.10m; abaterCO2 = 6210.50m; }
                else if (EmissoesCO2 <= 145) { taxaCO2 = 81.30m; abaterCO2 = 7670.10m; }
                else { taxaCO2 = 178.40m; abaterCO2 = 21750.80m; }
            }
            else
            {
                // Tabela Geral (Gasolina e outros)
                if (EmissoesCO2 <= 95) { taxaCO2 = 4.15m; abaterCO2 = 370.20m; }
                else if (EmissoesCO2 <= 115) { taxaCO2 = 7.30m; abaterCO2 = 670.50m; }
                else if (EmissoesCO2 <= 145) { taxaCO2 = 47.10m; abaterCO2 = 5250.10m; }
                else if (EmissoesCO2 <= 175) { taxaCO2 = 57.30m; abaterCO2 = 6730.40m; }
                else { taxaCO2 = 147.20m; abaterCO2 = 22460.50m; }
            }
            decimal compAmbiental = (EmissoesCO2 * taxaCO2) - abaterCO2;
            if (compAmbiental < 0) compAmbiental = 0;

            // 3. Aplicar Desconto de Idade nas duas componentes
            decimal fatorIdade = ObterFatorMultiplicadorIdade();
            decimal isvAjustado = (compCilindrada * fatorIdade) + (compAmbiental * fatorIdade);

            // 4. Agravamento Diesel (Filtro de Partículas)
            if (isDiesel && Particulas > 0.001)
            {
                isvAjustado += 500.00m;
            }

            return isvAjustado < 0 ? 0m : isvAjustado;
        }
    }
}
