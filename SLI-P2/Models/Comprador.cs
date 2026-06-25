using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Comprador : Contacto
    {
        private string _tipoCliente;

        public string TipoCliente
        {
            get { return _tipoCliente; }
            set
            {
                _tipoCliente = value.Trim();
                if (_tipoCliente.Length == 0)
                {
                    _tipoCliente = "Particular";
                }
            }
        }

        public Comprador() : base()
        {
            _tipoCliente = "";
        }
    }
}