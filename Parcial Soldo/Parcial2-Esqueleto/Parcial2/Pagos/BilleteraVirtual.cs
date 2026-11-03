using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2.Pagos
{
    public class BilleteraVirtual : IPagoStrategy
    {
        public string Nombre => "Billetera Virtual";

        public bool PermiteCuotas => false;

        public bool RequiereCuit => false;

        public decimal CalcularAjuste(decimal subtotal, int cuotas)
        {
            return subtotal * 0.95m;
        }
    }
}
