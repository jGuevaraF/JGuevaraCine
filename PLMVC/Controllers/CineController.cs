using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Cine objCine = new ML.Cine();
            objCine.Zona = new ML.Zona();

            using (var cine = new HttpClient())
            {
                string URLapi = "http://localhost:5027/";
                cine.BaseAddress = new Uri(URLapi);

                var respuesta = cine.GetAsync("cine");
                respuesta.Wait();

                var resultGetAll = respuesta.Result;

                if (resultGetAll.IsSuccessStatusCode)
                {
                    result.Objects = new List<object>();
                    var leerPeliculas = resultGetAll.Content.ReadAsAsync<ML.Result>();
                    leerPeliculas.Wait();

                    foreach (var cineObtenido in leerPeliculas.Result.Objects)
                    {
                        ML.Cine resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(cineObtenido.ToString());
                        result.Objects.Add(resultados);
                    }
                    objCine.Cines = result.Objects;
                    return View(objCine);
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult Form(int? idCine, int? idZona)
        {
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            ML.Result resultZonas = BL.Zona.GetAll();
            cine.Zona.Zonas = resultZonas.Objects;

            if (idCine == null)
            {
                return View(cine);
            }
            else // update
            {
                using (var updateCine = new HttpClient())
                {
                    string URLapi = "http://localhost:5027/";
                    updateCine.BaseAddress = new Uri(URLapi);
                    var respuesta = updateCine.GetAsync("cine/" + idCine + "/" + idZona);
                    respuesta.Wait();

                    var resultUpdate = respuesta.Result;

                    if (resultUpdate.IsSuccessStatusCode)
                    {
                        var leerRegistro = resultUpdate.Content.ReadAsAsync<ML.Result>();
                        leerRegistro.Wait();
                        ML.Cine resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(leerRegistro.Result.Object.ToString());
                        cine = resultItemList;
                        cine.Zona.Zonas = resultZonas.Objects;

                        return View(cine);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Form(ML.Cine cine)
        {
            ML.Result resultZonas = BL.Zona.GetAll();
            cine.Zona.Zonas = resultZonas.Objects;

            string URLapi = "http://localhost:5027/";

            if (cine.IdCine == null) // agregar
            {
                using (var add = new HttpClient())
                {

                    add.BaseAddress = new Uri(URLapi);
                    var responseTaks = add.PostAsJsonAsync<ML.Cine>("cine", cine);
                    responseTaks.Wait();
                    var result = responseTaks.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Cine dado de alta correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR ";
                    }

                    return PartialView("Modal");
                }
            }
            else
            {
                using (var update = new HttpClient())
                {
                    update.BaseAddress = new Uri(URLapi);
                    var responseTaks = update.PutAsJsonAsync<ML.Cine>("cine", cine);
                    responseTaks.Wait();
                    var result = responseTaks.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Cine se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR ";
                    }

                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int? idCine)
        {
            string URLapi = "http://localhost:5027/";

            using (var delete = new HttpClient())
            {

                delete.BaseAddress = new Uri(URLapi);

                var respuesta = delete.DeleteAsync("cine/" + idCine);
                var resultDelete = respuesta.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El Cine se ha borrado correctamente.";
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult PuntosCine()
        {

            ML.Result result = new ML.Result();
            ML.Cine objCine = new ML.Cine();
            objCine.Zona = new ML.Zona();
            ML.Result resultZonas = BL.Zona.GetAll();


            using (var cine = new HttpClient())
            {
                string URLapi = "http://localhost:5027/";
                cine.BaseAddress = new Uri(URLapi);

                var respuesta = cine.GetAsync("cine");
                respuesta.Wait();

                var resultGetAll = respuesta.Result;

                if (resultGetAll.IsSuccessStatusCode)
                {
                    result.Objects = new List<object>();
                    var leerPeliculas = resultGetAll.Content.ReadAsAsync<ML.Result>();
                    leerPeliculas.Wait();

                    foreach (var cineObtenido in leerPeliculas.Result.Objects)
                    {
                        ML.Cine resultados = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(cineObtenido.ToString());
                        result.Objects.Add(resultados);
                    }
                    objCine.Cines = result.Objects;
                    objCine.Zona.Zonas = resultZonas.Objects;
                    return View(objCine);
                }

            }

            return View();
        }

    }
}
