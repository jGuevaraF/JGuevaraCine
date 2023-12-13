using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [ApiController]
    [Route("cine")]
    public class CineController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet]
        [Route("{idCine}/{idZona}")]
        public IActionResult GetById(int idCine, int idZona)
        {
            ML.Result result = BL.Cine.GetById(idZona, idCine);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(ML.Cine cine)
        {
            ML.Result result = BL.Cine.Add(cine);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("{idCine}")]
        public IActionResult Delete(int idCine)
        {
            ML.Result result = BL.Cine.Delete(idCine);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(ML.Cine cine)
        {
            ML.Result result = BL.Cine.Update(cine);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
