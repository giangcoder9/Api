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
    public class HoaDonController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public HoaDonController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.HoaDon.GetAll());
        }

        [HttpPost]
        public IActionResult Post(HOADON hd)
        {
            if (unitOfWork.HoaDon.Add(hd) == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "error", Massage = "error" });
            }

            return Ok(new Reponse { Status = "succes", Massage = "succes" });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.HoaDon.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(HOADON hd)
        {
            unitOfWork.HoaDon.Update(hd);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.HoaDon.GetIdHD(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenKH)
        {
            return Ok(unitOfWork.HoaDon.Search(tenKH));
        }

        [HttpGet]
        [Route("tong")]
        public IActionResult GetTong(int Id)
        {
            return Ok(unitOfWork.HoaDon.TongTien(Id));
        }
    }
}
