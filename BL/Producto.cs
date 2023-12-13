using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    var query = context.Productos.FromSqlRaw("GetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.Id = item.Id;
                            producto.Nombre = item.Nombre;
                            producto.Precio = item.Precio;
                            producto.Foto = item.Foto;

                            result.Objects.Add(producto);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay productos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    var query = context.Productos.FromSqlRaw($"GetById {idProducto}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {

                        ML.Producto producto = new ML.Producto();
                        producto.Id = query.Id;
                        producto.Nombre = query.Nombre;
                        producto.Precio = query.Precio;
                        producto.Foto = query.Foto;

                        result.Object = producto;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el producto";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }
    }

}

