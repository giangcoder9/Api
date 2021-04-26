using Core;
using Dapper;
using Microsoft.Extensions.Configuration;
using QLPhongTro.Application.Catalog.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QLPhongTro.Application.Catalog.HOADONREPOSITORY
{
    public class HoaDonRepository : GenericRepositories, IHoaDon
    {
        public HoaDonRepository()
        {

        }

        public HoaDonRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<HOADON> GetAll()
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<HOADON>)connection.Query<HOADON>("select HOADON.SoHD,KHACHHANG.maKH,KHACHHANG.TenKH,NgayLap,HOADON.TrangThai,PHONGTRO.MaPhongTro,PHONGTRO.TenPhongTro,PHONGTRO.GiaPhong from PHONGTRO,HOADON,KHACHHANG where PHONGTRO.MaPhongTro=HOADON.MaPhongTro and HOADON.maKH=KHACHHANG.maKH");
            }

        }

        public int Add(HOADON hd)
        {
            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();

                x = connection.Query<HOADON>($"SELECT SoHD From HOADON Where maKH=@maKH and MaPhongTro=@MaPhongTro", hd, commandType: CommandType.Text).Count(); 
                if(x<=0)
                {
                    connection.Execute("insert HOADON values(@maKH,@NgayLap,@TrangThai,@MaPhongTro)", hd);
                    return 1;
                }
                return 0;
            }
            
        }

        public int Delete(int maHD)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from HOADON Where SoHD={maHD}");
            }
            return count;
        }

        public int Update(HOADON hd)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE HOADON SET  maKH= @maKH, NgayLap= @NgayLap, TrangThai=@TrangThai,MaPhongTro=@MaPhongTro,SoHD=@SoHD", hd);
            }
            return count;
        }

        public HOADON GetId(int maHD)
        {
            HOADON hd = new HOADON();
            using (SqlConnection connection = con())
            {
                hd = (HOADON)connection.Query<HOADON>($"select SoHD,KHACHHANG.maKH,KHACHHANG.TenKH,NgayLap,HOADON.TrangThai,PHONGTRO.MaPhongTro,PHONGTRO.TenPhongTro,PHONGTRO.GiaPhong from PHONGTRO,HOADON,KHACHHANG where PHONGTRO.MaPhongTro=HOADON.MaPhongTro and KHACHHANG.maKH=HOADON.MaPhongTro and SoHD={maHD}").FirstOrDefault();
                return hd;
            }

        }

        public List<HOADON> GetIdHD(int maHD)
        {
            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<HOADON>)connection.Query<HOADON>($"select sum((DienNuoc.SoMoi-DienNuoc.SoCu)*DichVu.Gia) as TongGiaDV, TenDV,sum(DienNuoc.SoMoi-DienNuoc.SoCu)as SoDV,HOADON.SoHD,KHACHHANG.maKH,KHACHHANG.TenKH,NgayLap,HOADON.TrangThai,PHONGTRO.MaPhongTro,PHONGTRO.TenPhongTro,PHONGTRO.GiaPhong from PHONGTRO,HOADON,KHACHHANG,DienNuoc,DichVu where PHONGTRO.MaPhongTro=HOADON.MaPhongTro and KHACHHANG.maKH=HOADON.MaPhongTro and DienNuoc.MaDV = DichVu.MaDV and DienNuoc.MaPhongTro=PHONGTRO.MaPhongTro and SoHD={maHD} group by HOADON.SoHD, KHACHHANG.maKH, KHACHHANG.TenKH, HOADON.NgayLap, HOADON.TrangThai, PHONGTRO.MaPhongTro, PHONGTRO.TenPhongTro, PHONGTRO.GiaPhong, DichVu.TenDV, DienNuoc.SoMoi, DienNuoc.SoCu");
            }
        }

        public List<HOADON> Search(string TenKH)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<HOADON>)connection.Query<HOADON>($"select sum((DienNuoc.SoMoi-DienNuoc.SoCu)*DichVu.Gia) as TongGiaDV, TenDV,sum(DienNuoc.SoMoi-DienNuoc.SoCu)as SoDV,HOADON.SoHD,KHACHHANG.maKH,KHACHHANG.TenKH,NgayLap,HOADON.TrangThai,PHONGTRO.MaPhongTro,PHONGTRO.TenPhongTro,PHONGTRO.GiaPhong from PHONGTRO,HOADON,KHACHHANG,DienNuoc,DichVu where PHONGTRO.MaPhongTro=HOADON.MaPhongTro and KHACHHANG.maKH=HOADON.MaPhongTro and DienNuoc.MaDV = DichVu.MaDV and DienNuoc.MaPhongTro=PHONGTRO.MaPhongTro and (( dbo.ufn_removeMark(TenKH) LIKE '%{TenKH}%' or TenKH LIKE N'%{TenKH}%')) group by HOADON.SoHD, KHACHHANG.maKH, KHACHHANG.TenKH, HOADON.NgayLap, HOADON.TrangThai, PHONGTRO.MaPhongTro, PHONGTRO.TenPhongTro, PHONGTRO.GiaPhong, DichVu.TenDV, DienNuoc.SoMoi, DienNuoc.SoCu", commandType: CommandType.Text);
            }

        }
        public double TongTien(int maHD)
        {
            var hd = new List<HOADON>();
            double s = 0;
            double giaPhong;
            double tienCoc;
            using (SqlConnection connection = con())
            {
                connection.Open();
                hd = (List<HOADON>)connection.Query<HOADON>($"select THUEPHONG.TienCoc , DichVu.TenDV,DichVu.Gia,sum((DienNuoc.SoMoi-DienNuoc.SoCu))as SoDV, KHACHHANG.maKH,KHACHHANG.TenKH,NgayLap,PHONGTRO.MaPhongTro,PHONGTRO.TenPhongTro,PHONGTRO.GiaPhong,sum((DienNuoc.SoMoi-DienNuoc.SoCu)*DichVu.Gia) as TongGiaDV from THUEPHONG, PHONGTRO,HOADON,KHACHHANG,DichVu,DienNuoc where PHONGTRO.MaPhongTro=HOADON.MaPhongTro and KHACHHANG.maKH=HOADON.MaPhongTro and DienNuoc.MaPhongTro = PHONGTRO.MaPhongTro and DichVu.MaDV=DienNuoc.MaDV and PhongTro.MaPhongTro=ThuePhong.MaPhongTro and SoHD={maHD} group by PHONGTRO.GiaPhong, DichVu.Gia, PHONGTRO.TenPhongTro, DichVu.TenDV, KHACHHANG.maKH, KHACHHANG.TenKH, HOADON.NgayLap, PHONGTRO.MaPhongTro,THUEPHONG.TienCoc");
                giaPhong = hd.FirstOrDefault().GiaPhong;
                tienCoc = hd.FirstOrDefault().TienCoc;
                s += giaPhong-tienCoc;
                foreach(var x in hd)
                {
                    s +=x.TongGiaDV;
                }
                return s;
            }
        }
    }
}
