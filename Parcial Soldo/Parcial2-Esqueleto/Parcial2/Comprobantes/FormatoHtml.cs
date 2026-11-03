using Parcial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Parcial2.Comprobantes
{
    public class FormatoHtml : IFormatoComprobante
    {
        private const string NombreTaller = "Taller mecanico";

        public string Extension => "html";

        public void Encabezado(OrdenTrabajo orden)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><body>");
            sb.AppendLine($"<h1>{WebUtility.HtmlEncode(NombreTaller)}");
            sb.AppendLine($"<p>Cliente:{WebUtility.HtmlEncode(orden.Cliente)}|" +
                $"Patente:{WebUtility.HtmlEncode(orden.Patente)}|"+
                $"Forma de pago:{WebUtility.HtmlEncode(orden.FormaPago)}");
            sb.AppendLine("");
                          
        }

        public void Item(ItemOrden item)
        {
            throw new NotImplementedException();
        }

        public string Resultado()
        {
            throw new NotImplementedException();
        }

        public void Totales(decimal subtotal, decimal ajuste, decimal total)
        {
            throw new NotImplementedException();
        }
    }
}
