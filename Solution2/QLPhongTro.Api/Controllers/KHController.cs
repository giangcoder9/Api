using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongTro.Application.Catalog.KH;
using QLPhongTro.Application.Catalog.UnitOfWork;

namespace QLPhongTro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KHController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public KHController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.KhachHang.GetAll());
        }

        [HttpPost]
        public IActionResult Post(KHACHHANG pt)
        {
            unitOfWork.KhachHang.Add(pt);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.KhachHang.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(KHACHHANG pt)
        {
            unitOfWork.KhachHang.Update(pt);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.KhachHang.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string tenKH)
        {
            return Ok(unitOfWork.KhachHang.Search(tenKH));
        }
    }
}
