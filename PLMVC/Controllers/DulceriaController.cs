using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class DulceriaController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();
            producto.Productos = result.Objects;
            return View(producto);
        }
    }
}
