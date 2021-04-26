using Core;
using Dapper;
using Microsoft.Extensions.Configuration;
using QLPhongTro.Application.Catalog.GenericRepository;
using QLPhongTro.Application.Catalog.KH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QLPhongTro.Application.Catalog.KH
{
    public class KhachHang:GenericRepositories,IKhachHang
    {

        public KhachHang()
        {

        }

        public KhachHang(IConfiguration configuration) : base(configuration)
        {

        }
        public List<KHACHHANG> GetAll()
        {
        
            using (SqlConnection connection = con())
            {
                connection.Open();
                string className = typeof(KHACHHANG).Name;
    
                return (List<KHACHHANG>)connection.Query<KHACHHANG>($"SELECT {className} From {className}");
            }

        }

        public int Add(KHACHHANG kh)
        {
           
            using (SqlConnection connection = con())
            {
                connection.Open();
                connection.Execute("insert KHACHHANG values(@TenKH ,@GioiTinh,@NgaySinh,@DiaChi,@SDT)", kh);
                return 0;
            }
           
        }

        public int Delete(int maKH)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from KHACHHANG Where MaKH={maKH}");
            }
            return count;
        }

        public int Update(KHACHHANG kh)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE KHACHHANG SET TenKH= @TenKH, GioiTinh= @GioiTinh, NgaySinh=@NgaySinh,DiaChi=@DiaChi,SDT=@SDT WHERE maKH=@maKH", kh);
            }
            return count;
        }

        public KHACHHANG GetId(int maKH)
        {
            KHACHHANG kh = new KHACHHANG();
            using (SqlConnection connection = con())
            {
                kh = (KHACHHANG)connection.Query<KHACHHANG>("select TOP 1 * from KHACHHANG where maKH=" + maKH).FirstOrDefault();
                return kh;
            }

        }

        public List<KHACHHANG> Search(string TenKH)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<KHACHHANG>)connection.Query<KHACHHANG>($"SELECT *FROM KHACHHANG WHERE dbo.ufn_removeMark(TenKH) LIKE '%{TenKH}%' or TenKH like N'%{TenKH}%'", commandType: CommandType.Text);
            }

        }

    }
}
