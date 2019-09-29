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
    public class PpgController : Controller
    {
        private IPpgRepository _ppgRepository;

        public PpgController(IPpgRepository ppgRepository)
        {
            _ppgRepository = ppgRepository;
        }
        //api/ppg
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ICollection<Ppg>>))]
        public IActionResult GetAllPpgs()
        {
            var ppgs = _ppgRepository.GetAllPpgs().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(ppgs);
        }

        //api/ppg/id
        [HttpGet("{id}", Name = "GetPpgByID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ppg>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPpgByID(int id)
        {
            if (!_ppgRepository.hasPpgId(id))
            {
                return NotFound();
            }
            var ppgs = _ppgRepository.GetPpgByID(id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(ppgs);
        }


        //api/ppg/yyyy,mm,dd
        [HttpGet("{yyyy},{mm},{dd}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ppg>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetPpgByDateTime(int yyyy, int mm, int dd)
        {
            var ppg = _ppgRepository.GetPpgByTime(new DateTime(yyyy, mm, dd));
            if (ppg.Count == 0)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(ppg);
        }

        //api/ppg
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Ppg))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreatePpg([FromBody]Ppg newPpg)
        {
            if (newPpg == null)
            {
                return BadRequest();
            }
            var ppgs = _ppgRepository.GetPpgByTime(newPpg.time);
            if (ppgs.Count != 0)
            {
                ModelState.AddModelError("", "New ppg has time exists");
                //not processable
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_ppgRepository.CreatePpg(newPpg))
            {
                ModelState.AddModelError("", "Save Ppg Error");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPpgByID", new { id = newPpg.id }, newPpg);
        }

        //api/ppg/id
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public IActionResult UpdatePpg(int id,[FromBody]Ppg newPpg)
        {
            if (newPpg == null)
            {
                return BadRequest(ModelState);
            }

            if (id != newPpg.id)
            {
                return BadRequest(ModelState);
            }
            if (!_ppgRepository.hasPpgId(id))
            {
                ModelState.AddModelError("", "No such id");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ppgRepository.updaetePpg(newPpg))
            {
                ModelState.AddModelError("", "update Ppg Error");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/ppg/id
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public IActionResult DeletePpg(int id)
        {
            if (!_ppgRepository.hasPpgId(id))
            {
                return NotFound();
            }
            Ppg target = _ppgRepository.GetPpgByID(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (target == null)
            {
                return NotFound();
            }

            if (!_ppgRepository.DeletePpg(target))
            {
                ModelState.AddModelError("", "delete ppg Error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
