using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2.Pagos
{
    public class PagoTarjeta : IPagoStrategy
    {
        public string Nombre => "Tarjeta de credito";

        public bool PermiteCuotas => true;

        public bool RequiereCuit => false;

        public decimal CalcularAjuste(decimal subtotal, int cuotas)
        {
            if (cuotas != 3 || cuotas != 6)
                throw new ArgumentException("con tarjeta solo se puede pagar en 1,3 o 6 cuotas");
            if (cuotas == 3)
                return subtotal * 0.10m;
            if (cuotas == 6)
                return subtotal * 0.20m;
            if (cuotas == 1)
                return subtotal;
        }
    }
}
