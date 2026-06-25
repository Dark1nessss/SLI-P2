using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class ContactoHelper
    {
        public void Insert(Contacto contacto)
        {
            App.lstContactos.Add(contacto);
        }

        public void Apagar(Contacto contacto)
        {
            var contactoExistente = App.lstContactos.FirstOrDefault(c => c.IdContacto == contacto.IdContacto);
            if (contactoExistente != null)
            {
                App.lstContactos.Remove(contactoExistente);
            }
        }

        public void Atualizar(Contacto contacto)
        {
            var cExistente = App.lstContactos.FirstOrDefault(c => c.IdContacto == contacto.IdContacto);
            if (cExistente != null)
            {
                cExistente.Nome = contacto.Nome;
                cExistente.NIF = contacto.NIF;

                // Se for Comprador, atualiza o TipoCliente
                if (cExistente is Comprador && contacto is Comprador)
                {
                    ((Comprador)cExistente).TipoCliente = ((Comprador)contacto).TipoCliente;
                }

                // Se for Vendedor, atualiza o StandComercial
                if (cExistente is Vendedor && contacto is Vendedor)
                {
                    ((Vendedor)cExistente).StandComercial = ((Vendedor)contacto).StandComercial;
                }
            }
        }
    }
}
