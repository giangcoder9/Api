using Microsoft.Extensions.DependencyInjection;
using QLPhongTro.Application.Catalog.ChiTietThietBiRepisitory;
using QLPhongTro.Application.Catalog.DichVuRepository;
using QLPhongTro.Application.Catalog.DienNuocRepository;
using QLPhongTro.Application.Catalog.HOADONREPOSITORY;
using QLPhongTro.Application.Catalog.KH;
using QLPhongTro.Application.Catalog.PhongTro;
using QLPhongTro.Application.Catalog.ThietBiRepository;
using QLPhongTro.Application.Catalog.ThuePhongRepository;
using QLPhongTro.Application.Catalog.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IKhachHang, KhachHang>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPhongTro, PhongTro>();
            services.AddTransient<IDichVu, DichVuRepository>();
            services.AddTransient<IChiTietThietBi, ChiTietThietBiRepository>();
            services.AddTransient<IThuePhong, ThuePhongRepository>();
            services.AddTransient<IHoaDon, HoaDonRepository>();
            services.AddTransient<IDienNuoc, DienNuocRepository>();
            services.AddTransient<IThietBi, ThietBiRepository>();
        }
    }
}
