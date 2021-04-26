using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongTro.Application.Catalog.UnitOfWork;

namespace QLPhongTro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThietBiController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ThietBiController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.ThietBi.GetAll());
        }

        [HttpPost]
        public IActionResult Post(THIETBI tb)
        {
            if (unitOfWork.ThietBi.Add(tb) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.ThietBi.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(THIETBI tb)
        {
            unitOfWork.ThietBi.Update(tb);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.ThietBi.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenTB)
        {
            return Ok(unitOfWork.ThietBi.Search(tenTB));
        }
    }
}
