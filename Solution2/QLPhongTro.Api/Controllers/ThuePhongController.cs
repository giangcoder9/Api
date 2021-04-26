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
    public class ThuePhongController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ThuePhongController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.ThuePhong.GetAll());
        }

        [HttpPost]
        public IActionResult Post(THUEPHONG tp)
        {
            if (unitOfWork.ThuePhong.Add(tp) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.ThuePhong.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(THUEPHONG tp)
        {
            unitOfWork.ThuePhong.Update(tp);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.ThuePhong.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenKH)
        {
            return Ok(unitOfWork.ThuePhong.Search(tenKH));
        }
    }
}
