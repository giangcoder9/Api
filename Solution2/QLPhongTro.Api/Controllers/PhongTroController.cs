using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongTro.Application.Catalog.PhongTro;
using QLPhongTro.Application.Catalog.UnitOfWork;

namespace QLPhongTro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhongTroController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public PhongTroController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(unitOfWork.PhongTro.GetAll());
        }
        [HttpPost]
        public IActionResult Post(PHONGTRO pt)
        {
            if (unitOfWork.PhongTro.Add(pt) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }  
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.PhongTro.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(PHONGTRO pt)
        {
            unitOfWork.PhongTro.Update(pt);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.PhongTro.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string PhongTro)
        {
            return Ok(unitOfWork.PhongTro.Search(PhongTro));
        }
    }
}
