using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class VeiculoHibridoPlugin : VeiculoCombustao
    {
        private int _autonomiaEletrica;

        public int AutonomiaEletrica
        {
            get { return _autonomiaEletrica; }
            set 
            {
                _autonomiaEletrica = value;
                if (_autonomiaEletrica < 0)
                {
                    _autonomiaEletrica = 0;
                }
            }
        }

        public VeiculoHibridoPlugin() : base()
        {
            AutonomiaEletrica = 0;
            TipoCombustivel = "Híbrido Plug-In";
        }

        public override string ObterDescricaoTipo()
        {
            return "Híbrido Plug-In (PHEV)";
        }

        public override decimal CalcularISV()
        {
            // Pega no cálculo base da tabela de combustão (Reutilização DRY)
            decimal isvBase = base.CalcularISV();

            // Regra especial Euro 6e-bis
            if (AutonomiaEletrica >= 50 && EmissoesCO2 <= 80)
            {
                return isvBase * 0.25m; // Desconto de 75% (paga apenas 25%)
            }

            return isvBase; // Perde o benefício, paga o ISV por inteiro
        }
    }
}
