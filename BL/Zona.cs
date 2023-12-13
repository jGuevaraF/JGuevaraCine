using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Zona zona = new ML.Zona();
                            zona.IdZona = obj.IdZona;
                            zona.ZonaNombre = obj.Nombre;

                            result.Objects.Add(zona);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener las zonas.";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex?.InnerException?.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
