using QLPhongTro.Application.Catalog.ChiTietThietBiRepisitory;
using QLPhongTro.Application.Catalog.DichVuRepository;
using QLPhongTro.Application.Catalog.DienNuocRepository;
using QLPhongTro.Application.Catalog.HOADONREPOSITORY;
using QLPhongTro.Application.Catalog.KH;
using QLPhongTro.Application.Catalog.PhongTro;
using QLPhongTro.Application.Catalog.ThietBiRepository;
using QLPhongTro.Application.Catalog.ThuePhongRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLPhongTro.Application.Catalog.UnitOfWork
{
    public interface IUnitOfWork
    {
        IKhachHang KhachHang{ get; }
        IPhongTro PhongTro { get; }
        IDichVu DichVu { get; }
        IChiTietThietBi ChiTiet { get; }
        IThuePhong ThuePhong { get; }
        IHoaDon HoaDon { get; }
        IDienNuoc DienNuoc { get; }
        IThietBi ThietBi { get; }
    }
}
