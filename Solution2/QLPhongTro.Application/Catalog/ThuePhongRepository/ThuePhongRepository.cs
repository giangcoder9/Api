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

namespace QLPhongTro.Application.Catalog.ThuePhongRepository
{
    public class ThuePhongRepository : GenericRepositories, IThuePhong
    {
        public ThuePhongRepository()
        {

        }

        public ThuePhongRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<THUEPHONG> GetAll()
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<THUEPHONG>)connection.Query<THUEPHONG>("select PHONGTRO.GiaPhong, MaSoThue ,KHACHHANG.maKH,KHACHHANG.TenKH,PHONGTRO.TenPhongTro,NgayThue,THUEPHONG.trangThai,PHONGTRO.MaPhongTro,TienCoc from PHONGTRO,THUEPHONG,KHACHHANG where PHONGTRO.MaPhongTro=THUEPHONG.MaPhongTro and KHACHHANG.maKH=THUEPHONG.maKH");
            }

        }

        public int Add(THUEPHONG tp)
        {
            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();

                x = connection.Query<THUEPHONG>($"SELECT MaSoThue From THUEPHONG Where maKH=@maKH and MaPhongTro=@MaPhongTro", tp, commandType: CommandType.Text).Count();
                if (x <= 0)
                {
                    connection.Execute("insert THUEPHONG values(@maKH ,@NgayThue,@trangThai,@MaPhongTro,@TienCoc)", tp);
                    return 1;
                }
                return 0;
            }
            
        }

        public int Delete(int maTP)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from THUEPHONG Where MaSoThue={maTP}");
            }
            return count;
        }

        public int Update(THUEPHONG tp)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE THUEPHONG SET maKH =@maKH, NgayThue= @NgayThue, trangThai=@trangThai,MaPhongTro=@MaPhongTro,TienCoc=@TienCoc WHERE MaSoThue=@MaSoThue", tp);
            }
            return count;
        }

        public THUEPHONG GetId(int maTP)
        {
            THUEPHONG tp = new THUEPHONG();
            using (SqlConnection connection = con())
            {
                tp= (THUEPHONG)connection.Query<THUEPHONG>($"SELECT MaSoThue,KHACHHANG.maKH,PHONGTRO.MaPhongTro,KHACHHANG.TenKH,NgayThue,THUEPHONG.trangThai,PHONGTRO.TenPhongTro,TienCoc,PHONGTRO.GiaPhong From THUEPHONG,PHONGTRO,KHACHHANG where THUEPHONG.MaPhongTro=PHONGTRO.MaPhongTro and THUEPHONG.maKH=KHACHHANG.maKH and MaSoThue={maTP}" ).FirstOrDefault();
                return tp;
            }

        }

        public List<THUEPHONG> Search(string KH)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<THUEPHONG>)connection.Query<THUEPHONG>($"SELECT MaSoThue,KHACHHANG.TenKH,NgayThue,THUEPHONG.trangThai,PHONGTRO.TenPhongTro,TienCoc,PHONGTRO.GiaPhong From THUEPHONG,PHONGTRO,KHACHHANG where THUEPHONG.MaPhongTro=PHONGTRO.MaPhongTro and THUEPHONG.maKH=KHACHHANG.maKH and (dbo.ufn_removeMark(TenKH) LIKE '%{KH}%' or TenKH like N'%{KH}%') ", commandType: CommandType.Text);
            }

        }
        
    }
}
