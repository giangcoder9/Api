using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongTro.Application.Catalog.DichVuRepository;
using QLPhongTro.Application.Catalog.UnitOfWork;

namespace QLPhongTro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public DichVuController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.DichVu.GetAll());
        }

        [HttpPost]
        public IActionResult Post(DichVu dv)
        {
            if (unitOfWork.DichVu.Add(dv) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.DichVu.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(DichVu pt)
        {
            unitOfWork.DichVu.Update(pt);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.DichVu.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenDV)
        {
            return Ok(unitOfWork.DichVu.Search(tenDV));
        }
    }
}
