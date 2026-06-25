using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Vendedor : Contacto
    {
        private string _standComercial;

        public string StandComercial
        {
            get { return _standComercial; }
            set
            {
                _standComercial = value.Trim();
                if (_standComercial.Length == 0)
                {
                    _standComercial = "Stand Particular";
                }
            }
        }

        public Vendedor() : base()
        {
            _standComercial = "";
        }
    }
}
