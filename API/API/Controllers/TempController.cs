using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : Controller
    {

        private ITempRepository _tempRepository;
        public TempController(ITempRepository tempRepository)
        {
            _tempRepository = tempRepository;
        }

        //api/temp
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ICollection<Temp>>))]
        public IActionResult GetAllTemp()
        {
            var temps = _tempRepository.GetAllTemps().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(temps);
        }

        //api/temp/id
        [HttpGet("{id}", Name = "GetTempByID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Temp>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTempByID(int id)
        {
            if (!_tempRepository.hasTempId(id))
            {
                return NotFound();
            }
            var temps = _tempRepository.GetTempByID(id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(temps);
        }


        //api/temp/yyyy,mm,dd
        [HttpGet("{yyyy},{mm},{dd}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Temp>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetTempByDateTime(int yyyy, int mm, int dd)
        {

            var temps = _tempRepository.GetTempByTime(new DateTime(yyyy, mm, dd));
            if (temps.Count == 0)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(temps);
        }

        //api/temp
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Temp))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateTemp([FromBody]Temp newTemp)
        {
            if (newTemp == null)
            {
                return BadRequest(ModelState);
            }
            var temp = _tempRepository.GetTempByTime(newTemp.time);
            if (temp.Count != 0)
            {
                ModelState.AddModelError("", "New temp has time exists");
                //not processable
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_tempRepository.CreateTemp(newTemp))
            {
                ModelState.AddModelError("", "Save Temp Error");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTempByID", new { id = newTemp.id }, newTemp);
        }


        //api/temp/id
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public IActionResult UpdateTemp(int id, Temp newTemp)
        {
            if (newTemp == null)
            {
                return BadRequest(ModelState);
            }

            if (id != newTemp.id)
            {
                return BadRequest(ModelState);
            }

            if (!_tempRepository.hasTempId(id))
            {
                ModelState.AddModelError("", "No such id");
                return NotFound(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tempRepository.updaeteTemp(newTemp))
            {
                ModelState.AddModelError("", "update temp Error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        //api/temp/id
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public IActionResult DeleteTemp(int id)
        {
            if (!_tempRepository.hasTempId(id))
            {
                return NotFound();
            }
            Temp target = _tempRepository.GetTempByID(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (target == null)
            {
                return NotFound();
            }

            if (!_tempRepository.DeleteTemp(target))
            {
                ModelState.AddModelError("", "delete temp Error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
