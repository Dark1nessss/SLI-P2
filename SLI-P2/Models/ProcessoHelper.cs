using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLI_P2.Models
{
    public class ProcessoHelper
    {
        public void Insert(Processo processo)
        {
            App.lstProcessos.Add(processo);
        }

        public void Apagar(Processo processo)
        {
            var procExistente = App.lstProcessos.FirstOrDefault(p => p.IdProcesso == processo.IdProcesso);
            if (procExistente != null)
            {
                App.lstProcessos.Remove(procExistente);
            }
        }

        public void Atualizar(Processo processo)
        {
            var procExistente = App.lstProcessos.FirstOrDefault(p => p.IdProcesso == processo.IdProcesso);
            if (procExistente != null)
            {
                procExistente.Numero = processo.Numero;
                procExistente.Estado = processo.Estado;
                procExistente.VeiculoAssociado = processo.VeiculoAssociado;
                procExistente.AlfandegaDestino = processo.AlfandegaDestino;
                procExistente.CompradorAssociado = processo.CompradorAssociado;
                procExistente.VendedorAssociado = processo.VendedorAssociado;
            }
        }
    }
}