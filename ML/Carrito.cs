using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Carrito
    {
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public List<object> CarritoProductos { get; set; }
    }
}
