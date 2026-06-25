using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class VeiculoEletrico : Veiculo
    {
        private double _kwhBateria;

        public double KwhBateria
        {
            get { return _kwhBateria; }
            set
            {
                _kwhBateria = value;
                if (_kwhBateria < 0) { 
                    _kwhBateria = 0; 
                }
            }
        }

        public VeiculoEletrico() : base()
        {
            KwhBateria = 0;
            TipoCombustivel = "Elétrico";
        }

        // Reescreve os métodos da mãe usando OVERRIDE
        public override decimal CalcularISV()
        {
            return 0.00m;
        }

        public override string ObterDescricaoTipo()
        {
            return "100% Elétrico (EV)";
        }
    }
}