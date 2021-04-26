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
    public class UnitOfWork:IUnitOfWork
    {

        public UnitOfWork(IKhachHang KH, IPhongTro PT, IDichVu dv, IChiTietThietBi ct, IThuePhong tp, IHoaDon hd, IDienNuoc dn, IThietBi tb)
        {
            KhachHang = KH;
            PhongTro = PT;
            DichVu = dv;
            ChiTiet = ct;
            ThuePhong = tp;
            HoaDon = hd;
            DienNuoc = dn;
            ThietBi = tb;
        }
        public IKhachHang KhachHang { get; }
        public IPhongTro PhongTro { get; }
        public IDichVu DichVu { get; }
        public IChiTietThietBi ChiTiet { get; }
        public IThuePhong ThuePhong { get; }
        public IHoaDon HoaDon { get; }
        public IDienNuoc DienNuoc { get; }
        public IThietBi ThietBi { get; }
    }
}
