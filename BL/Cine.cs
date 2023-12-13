using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();
                            cine.IdCine = obj.IdCine;
                            cine.CineNombre = obj.CineNombre;
                            cine.Direccion = obj.Direccion;
                            cine.Ventas = obj.Ventas;
                            cine.Latitud = obj.Latitud;
                            cine.Longitud = obj.Longitud;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona;
                            cine.Zona.ZonaNombre = obj.ZonaNombre;

                            result.Objects.Add(cine);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener los cines";
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

        public static ML.Result GetById(int idZona, int idCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {idZona}, {idCine}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = query.IdCine;
                        cine.CineNombre = query.CineNombre;
                        cine.Direccion = query.Direccion;
                        cine.Ventas = query.Ventas;
                        cine.Latitud = query.Latitud;
                        cine.Longitud = query.Longitud;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona;
                        cine.Zona.ZonaNombre = query.ZonaNombre;

                        result.Object = cine;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener el cine";
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

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineAdd '{cine.CineNombre}', '{cine.Direccion}', {cine.Ventas}, {cine.Zona?.IdZona}, '{cine.Latitud}', '{cine.Longitud}'");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException?.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineDelete {idCine}");
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el cine";
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

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JguevaraCineContext context = new DL.JguevaraCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.CineNombre}', '{cine.Direccion}', {cine.Ventas},'{cine.Latitud}', '{cine.Longitud}', {cine.Zona?.IdZona}");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException?.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}