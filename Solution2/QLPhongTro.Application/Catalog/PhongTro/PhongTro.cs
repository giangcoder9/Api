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

namespace QLPhongTro.Application.Catalog.PhongTro
{
    public class PhongTro : GenericRepositories, IPhongTro
    {
        public PhongTro()
        {

        }

        public PhongTro(IConfiguration configuration) : base(configuration)
        {

        }

        public List<PHONGTRO> GetAll()
        {
            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<PHONGTRO>)connection.Query<PHONGTRO>($"SELECT * From PHONGTRO");
            }

        }

        public int Add(PHONGTRO phongTro)
        {

            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();
                
                x = connection.Query<PHONGTRO>($"SELECT UPPER(TenPhongTro) From PHONGTRO Where Replace(dbo.TRIM1(UPPER(dbo.ufn_removeMark(TenPhongTro))),' ','')=REPLACE(dbo.TRIM1(UPPER(dbo.ufn_removeMark(@TenPhongTro))), ' ', '')",phongTro, commandType: CommandType.Text).Count();
                if ( x <= 0)
                {
                     connection.Execute("insert PHONGTRO values(@TenPhongTro,@DienTich,@LoaiPhong,@GiaPhongTro,@trangThai)", phongTro);
                    return 1;
                }
                return 0;
            } 
        }

        public int Delete(int MaPhongTro)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from PHONGTRO Where MaPhongTro={MaPhongTro}");
            }
            return count;
        }

        public int Update(PHONGTRO phongTro)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE PHONGTRO SET TenPhongTro= @TenPhongTro, DienTich= @DienTich, LoaiPhong=@LoaiPhong,GiaPhong=@GiaPhongTro,trangThai=@trangThai WHERE MaPhongTro=@MaPhongTro", phongTro);
            }
            return count;
        }

        public PHONGTRO GetId(int MaPhongTro)
        {
            PHONGTRO pt = new PHONGTRO();
            using (SqlConnection connection = con())
            {
                pt = (PHONGTRO)connection.Query<PHONGTRO>("select TOP 1 * from PHONGTRO where MaPhongTro=" + MaPhongTro).FirstOrDefault();
                return pt;
            }

        }

        public List<PHONGTRO> Search(string TenPhong)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<PHONGTRO>)connection.Query<PHONGTRO>($"SELECT * FROM PHONGTRO WHERE dbo.ufn_removeMark(TenPhongTro) LIKE '%{TenPhong}%' or TenPhongTro LIKE N'%{TenPhong}%' ", commandType: CommandType.Text);
            }

        }
    }
}
