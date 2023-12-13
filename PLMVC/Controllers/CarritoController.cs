using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ML;
using System.Reflection.Metadata.Ecma335;

namespace PLMVC.Controllers
{
    public class CarritoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Carrito carrito = new ML.Carrito();
            carrito.CarritoProductos = new List<object>();
            if (HttpContext.Session.GetString("Carrito") == null)
            {
                return View(carrito);
            }
            else
            {
                GetCarrito(carrito);
                return View(carrito);
            }
        }

        [HttpGet]
        public IActionResult Add(int idProducto, string? bandera)
        {
            bool existe = false;
            ML.Carrito carrito = new ML.Carrito();
            carrito.CarritoProductos = new List<object>();
            ML.Result result = BL.Producto.GetById(idProducto);

            if (HttpContext.Session.GetString("Carrito") == null) //validamos si es que en el carrito existen mas productos, si la sesion existe es porque hay mas productos
            {

                if (result.Correct)
                {
                    ML.Producto producto = (ML.Producto)result.Object; 
                    producto.Cantidad = 1; //cantidad es una propiedad de producto en el ML pero no en la BD
                    carrito.CarritoProductos.Add(producto); //agregamos el producto a la lista del carrito
                    //serializar carrito, esto porque en las sesiones de .NET Core solo permiten guardar datos primitivos, y al serializarlo, lo esta guardando como un JSON (string)
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.CarritoProductos)); //se crea la sesion con el nombre Carrito, y le guardamos el objeto como json
                }

            }
            else //la sesion existe, y hay productos en el carrito
            {

                ML.Producto producto = (ML.Producto)result.Object;
                GetCarrito(carrito); //le envio el carrito vacio, para que me devuelva el carrito lleno de objetos de tipo producto, que estan guardados en la sesion
                foreach (ML.Producto producto1 in carrito.CarritoProductos)
                {
                    if (producto.Id == producto1.Id)
                    {
                        producto1.Cantidad += 1;
                        existe = true;
                        break;
                    }
                    else
                    {
                        existe = false;
                    }
                }
                if (existe == true) 
                {
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.CarritoProductos));
                }
                else
                {
                    producto.Cantidad = 1;
                    carrito.Total += producto.Precio;
                    carrito.CarritoProductos.Add(producto);
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.CarritoProductos));
                }

            }
            if (bandera.Equals("agregar"))
            {
                return RedirectToAction("GetAll");
                
            } else
            {
                return RedirectToAction("GetAll", "Dulceria");
            }
        }

        public ML.Carrito GetCarrito(ML.Carrito carrito)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito")); //obtengo la informacion de la sesion, es decir, obtengo todos los productos del carrito

            foreach (var obj in ventaSession)
            {
                ML.Producto objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString()); //lo deserealizo para poder trabajar con la clase

                objMateria.SubTotal = (decimal)(objMateria.Precio * objMateria.Cantidad);
                carrito.Total += objMateria.Precio * objMateria.Cantidad;
                carrito.CarritoProductos.Add(objMateria);
            }
            return carrito;
        }

        [HttpGet]
        public IActionResult Quitar(int idProducto, string bandera)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito")); // obtengo la información de la sesión, es decir, todos los productos del carrito

            for (int i = 0; i < ventaSession.Count; i++)
            {
                ML.Producto producto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(ventaSession[i].ToString()); // deserializo el objeto para poder trabajar con él
               
                if (producto.Id == idProducto)
                {
                    if (bandera.Equals("quitar"))
                    {
                        producto.Cantidad--;
                        if (producto.Cantidad == 0)
                        {
                            ventaSession.RemoveAt(i);
                        }
                        else
                        {
                            ventaSession[i] = Newtonsoft.Json.JsonConvert.SerializeObject(producto); // actualizo el objeto modificado en la lista, osea que guarda el objeto con la cantidad - 1
                        }

                        break;
                    } else
                    {
                        ventaSession.RemoveAt(i);
                    }
                   
                }
            }

            // Actualiza la sesión con la informacion modificada que se realizo anteriormente
            HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(ventaSession));

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Vaciar()
        {
            var productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito")); // obtengo la información de la sesión, es decir, todos los productos del carrito

            int cont = 0;
           while(productos.Count > 0)
            {
                productos.RemoveAt(cont);
            }

            // Actualiza la sesión con la informacion modificada que se realizo anteriormente
            HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(productos));

            return RedirectToAction("GetAll");

        }
    }
}
