using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLPhongTro.Application.Catalog.ChiTietThietBiRepisitory;
using QLPhongTro.Application.Catalog.UnitOfWork;

namespace QLPhongTro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ChiTietController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.ChiTiet.GetAll());
        }

        [HttpPost]
        public IActionResult Post(CHITETTHIETBI CT)
        {
            unitOfWork.ChiTiet.Add(CT);
            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            unitOfWork.ChiTiet.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(CHITETTHIETBI CT)
        {
            unitOfWork.ChiTiet.Update(CT);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int Id)
        {
            return Ok(unitOfWork.ChiTiet.GetId(Id));
        }

        [HttpPost]
        [Route("search")]
        public IActionResult PostSearch(string TenPT)
        {
            return Ok(unitOfWork.ChiTiet.Search(TenPT));
        }

    }
}
