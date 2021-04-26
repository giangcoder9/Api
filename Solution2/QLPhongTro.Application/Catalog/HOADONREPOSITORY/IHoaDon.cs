using Core;
using QLPhongTro.Application.Catalog.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLPhongTro.Application.Catalog.HOADONREPOSITORY
{
    public interface IHoaDon: IGenericRepository<HOADON>
    {
        List<HOADON> GetIdHD(int maHD);
        double TongTien(int maHD);
    }
}
