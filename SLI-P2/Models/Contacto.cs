using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Contacto
    {
        private Guid _idContacto;
        private string _nome;
        private string _nif;
        private string _isComprador;

        public Guid IdContacto => _idContacto;

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value.Trim();
                if (_nome.Length == 0)
                {
                    _nome = "Contacto Geral";
                }
            }
        }

        public string NIF
        {
            get { return _nif; }
            set
            {
                _nif = value.Trim();
                if (_nif.Length == 0)
                {
                    _nif = "999999999";
                }
            }
        }

        public string TipoExibicao
        {
            get
            {
                if (_isComprador == "Comprador")
                {
                    return "Comprador";
                }
                else
                {
                    return "Vendedor";
                }
            }
        }

        public Contacto()
        {
            _idContacto = Guid.NewGuid();
            Nome = "";
            NIF = "";
        }
    }
}
