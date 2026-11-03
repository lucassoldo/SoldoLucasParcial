using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2.Pagos
{
    public class PagoCuentaCorriente : IPagoStrategy
    {
        public string Nombre => "Cuenta Corriente";

        public bool PermiteCuotas => false;

        public bool RequiereCuit => true;
      

        public decimal CalcularAjuste(decimal subtotal, int cuotas)
        {
            if (subtotal > 600000m)
                throw new InvalidOperationException("el pago corriente no admite pagos de mas de $600.000");
            return 3000m + subtotal * 0.05m;

        }
    }
}
