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
    public class DienNuocController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public DienNuocController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.DienNuoc.GetAll());
        }

        [HttpPost]
        public IActionResult Post(DienNuoc dn)
        {
            if (unitOfWork.DienNuoc.Add(dn) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.DienNuoc.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(DienNuoc dn)
        {
            unitOfWork.DienNuoc.Update(dn);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.DienNuoc.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenPhong)
        {
            return Ok(unitOfWork.DienNuoc.Search(tenPhong));
        }
    }
}
