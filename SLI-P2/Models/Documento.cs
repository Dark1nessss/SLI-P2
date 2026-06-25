using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Documento
    {
        private Guid _idDocumento;
        private string _tipoDocumento;
        private DateTime _dataEmissao;

        public Guid IdDocumento
        {
            get { return _idDocumento; }
        }

        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set
            {
                _tipoDocumento = value.Trim();
                if (_tipoDocumento.Length == 0) { 
                    _tipoDocumento = "Declaração Aduaneira (DAV)"; 
                }
            }
        }

        public DateTime DataEmissao
        {
            get { return _dataEmissao; }
            set { _dataEmissao = value; }
        }

        public Documento()
        {
            _idDocumento = Guid.NewGuid();
            _tipoDocumento = "";
            _dataEmissao = DateTime.Now;
        }
    }
}