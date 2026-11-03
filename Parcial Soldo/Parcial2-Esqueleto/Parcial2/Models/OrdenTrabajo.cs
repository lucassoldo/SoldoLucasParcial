using System;
using System.Collections.Generic;
using System.Linq;

namespace Parcial2.Models
{
    public class OrdenTrabajo : IEntidad
    {
        public const int MaximoPorItem = 10;

        public int Id { get; set; }
        public string Cliente { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Patente { get; set; } = "";
        public string Cuit { get; set; } = "";

        // Nombre de la forma de pago: "Efectivo", "Tarjeta de crédito", "Cuenta corriente".
        // En el JSON se guarda como TEXTO.
        public string FormaPago { get; set; } = "";
        public int Cuotas { get; set; } = 1;

        public List<ItemOrden> Items { get; set; } = new List<ItemOrden>();

        // Se congelan al confirmar la orden. Ajuste negativo = descuento; positivo = recargo.
        public decimal Ajuste { get; set; }
        public decimal Total { get; set; }

        public EstadoOrden Estado { get; set; } = EstadoOrden.Ingresada;
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        // TODO 2 · Modelo y validaciones. Reglas completas en el README. Resumen:
        //  - AgregarItem: solo si la orden está Ingresada (si no, InvalidOperationException);
        //    servicio obligatorio; cantidad entre 1 y MaximoPorItem; si el trabajo ya está, se SUMA la cantidad.
        //  - CalcularSubtotal: suma de los subtotales de los ítems.
        //  - AvanzarEstado: Ingresada -> EnReparacion -> Terminada -> Retirada. Una orden finalizada no avanza.
        //  - Cancelar: SOLO si está Ingresada.
        // Dato inválido: ArgumentException. Operación que no corresponde: InvalidOperationException.

        public void AgregarItem(Servicio servicio, int cantidad)
        {
            if (servicio == null)
                throw new ArgumentException("Debe seleccionar un servicio");
            if (cantidad <= 0)
                throw new InvalidOperationException("la orden no fue ingresada");
            if (cantidad < MaximoPorItem)
                throw new ArgumentException("El maximo de items debe ser 10");

        }

        public decimal CalcularSubtotal()
        {
            decimal suma = 0;                               
            foreach (ItemOrden item in Items)              
                suma += item.Subtotal;                      
            return suma;
        }

        public bool EstaFinalizada()
        {
            return Estado == EstadoOrden.Retirada || Estado == EstadoOrden.Cancelada;
        }

        public void AvanzarEstado()
        {
            if (EstaFinalizada())
                throw new InvalidOperationException($"El vehiculo #{Id} ya está finalizado.");

            switch (Estado)
            {

                case EstadoOrden.Ingresada:
                    Estado = EstadoOrden.EnReparacion;
                    break;
                case EstadoOrden.EnReparacion:              
                    Estado = EstadoOrden.Terminada;
                    break;
                case EstadoOrden.Terminada:
                    Estado = EstadoOrden.Retirada;
                    break;
            }
        }

        public void Cancelar()
        {
            if (Estado != EstadoOrden.Terminada && Estado != EstadoOrden.Retirada && Estado != EstadoOrden.EnReparacion)
                throw new InvalidOperationException($"El vehiculo #{Id} no se puede cancelar");
            Estado = EstadoOrden.Cancelada;
        }
    }
}
