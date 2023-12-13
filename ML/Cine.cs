
namespace ML
{
    public class Cine
    {
        public int? IdCine { get; set; }
        public string? CineNombre { get; set; }
        public string? Direccion { get; set; }
        public Decimal? Ventas { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public List<object>? Cines { get; set; }
        public ML.Zona? Zona { get; set; }
    }
}
