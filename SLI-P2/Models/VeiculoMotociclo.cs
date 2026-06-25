using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class VeiculoMotociclo : Veiculo
    {
        private int _cilindrada;

        public int Cilindrada
        {
            get { return _cilindrada; }
            set { _cilindrada = value < 0 ? 0 : value; }
        }

        public VeiculoMotociclo() : base()
        {
            Cilindrada = 0;
            TipoCombustivel = "Gasolina (Mota)";
        }

        public override string ObterDescricaoTipo()
        {
            return "Motociclo (Mota)";
        }

        public override decimal CalcularISV()
        {
            // Escalões de ISV de Motociclos em Portugal
            if (Cilindrada <= 120) return 0m;
            if (Cilindrada <= 250) return 62.00m;
            if (Cilindrada <= 350) return 83.00m;
            if (Cilindrada <= 500) return 104.00m;
            if (Cilindrada <= 750) return 166.00m;
            return 245.00m; // > 750cc (Bate a 100% com o teu valor de 245€!)
        }
    }
}
