using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Alfandega
    {
        private Guid _idAlfandega;
        private string _nome;
        private string _localizacao;

        public Guid IdAlfandega
        {
            get { return _idAlfandega; }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value.Trim();
                if (_nome.Length == 0)
                {
                    _nome = "Alfândega Geral";
                }
            }
        }

        public string Localizacao
        {
            get { return _localizacao; }
            set
            {
                _localizacao = value.Trim();
                if (_localizacao.Length == 0)
                {
                    _localizacao = "Não Definida";
                }
            }
        }

        public Alfandega()
        {
            _idAlfandega = Guid.NewGuid();
            Nome = "";
            Localizacao = "";
        }
    }
}
