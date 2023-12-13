using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }

        public List<object> Productos { get; set; }
    }
}
