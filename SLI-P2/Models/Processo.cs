using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class Processo
    {
        private Guid _idProcesso;
        private int _numero;
        private string _estado;
        private Veiculo _veiculoAssociado;
        private Alfandega _alfandegaDestino;
        private Contacto _contactoAssociado;
        private Comprador _compradorAssociado;
        private Vendedor _vendedorAssociado;

        public Guid IdProcesso
        {
            get { return _idProcesso; }
        }

        public int Numero
        {
            get { return _numero; }
            set 
            { 
                _numero = value;
                if (_numero < 0)
                {
                    _numero = 0;
                }
            }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                _estado = value.Trim();
                if (_estado.Length == 0)
                {
                    _estado = "Pendente";
                }
            }
        }

        public Veiculo VeiculoAssociado
        {
            get { return _veiculoAssociado; }
            set { _veiculoAssociado = value; }
        }

        public Alfandega AlfandegaDestino
        {
            get { return _alfandegaDestino; }
            set { _alfandegaDestino = value; }
        }

        public Contacto ContactoAssociado
        {
            get { return _contactoAssociado; }
            set { _contactoAssociado = value; }
        }

        public Comprador CompradorAssociado
        {
            get { return _compradorAssociado; }
            set { _compradorAssociado = value; }
        }

        public Vendedor VendedorAssociado
        {
            get { return _vendedorAssociado; }
            set { _vendedorAssociado = value; }
        }

        public Processo()
        {
            _idProcesso = Guid.NewGuid();
            _numero = 0;
            _estado = "";
            _veiculoAssociado = new Veiculo();
            _alfandegaDestino = new Alfandega();
            _contactoAssociado = new Contacto();
            _compradorAssociado = new Comprador();
            _vendedorAssociado = new Vendedor();
        }
    }
}
