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

namespace QLPhongTro.Application.Catalog.DienNuocRepository
{
    public class DienNuocRepository : GenericRepositories, IDienNuoc
    {
        public DienNuocRepository()
        {

        }

        public DienNuocRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<DienNuoc> GetAll()
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<DienNuoc>)connection.Query<DienNuoc>("SELECT MaDN,DichVu.MaDV,SoCu,SoMoi,ngayChot,PHONGTRO.MaPhongTro,TenDV,TenPhongTro From DienNuoc,DichVu,PHONGTRO where DienNuoc.MaDV=DichVu.MaDV and PHONGTRO.MaPhongTro = DienNuoc.MaPhongTro");
            }

        }

        public int Add(DienNuoc dn)
        {

            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();

                x = connection.Query<DienNuoc>($"SELECT MaDN From DienNuoc Where MaDV=@MaDV and MaPhongTro=@MaPhongTro", dn, commandType: CommandType.Text).Count();
                if (x <= 0)
                {
                    connection.Execute("insert DienNuoc values(@MaDV,@SoCu,@SoMoi,@ngayChot,@MaPhongTro)", dn);
                    return 1;
                }
                return 0;
            }
            
        }

        public int Delete(int maDN)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from DienNuoc Where MaDN={maDN}");
            }
            return count;
        }

        public int Update(DienNuoc dn)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE DienNuoc SET  MaDV= @MaDV, SoCu= @SoCu, SoMoi=@SoMoi,ngayChot=@ngayChot,MaPhongTro=@MaPhongTro WHERE MaDN=@MaDN", dn);
            }
            return count;
        }

        public DienNuoc GetId(int maDN)
        {
            DienNuoc dn = new DienNuoc();
            using (SqlConnection connection = con())
            {
                dn = (DienNuoc)connection.Query<DienNuoc>("SELECT MaDN,DichVu.MaDV,SoCu,SoMoi,ngayChot,PHONGTRO.MaPhongTro,TenDV,TenPhongTro From DienNuoc,DichVu,PHONGTRO where DienNuoc.MaDV=DichVu.MaDV and PHONGTRO.MaPhongTro = DienNuoc.MaPhongTro and maDN=" + maDN).FirstOrDefault();
                return dn;
            }

        }

        public List<DienNuoc> Search(string TenPhong)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<DienNuoc>)connection.Query<DienNuoc>($"SELECT MaDN,DichVu.MaDV,SoCu,SoMoi,ngayChot,PHONGTRO.MaPhongTro,TenDV,TenPhongTro From DienNuoc,DichVu,PHONGTRO where DienNuoc.MaDV=DichVu.MaDV and PHONGTRO.MaPhongTro = DienNuoc.MaPhongTro and (dbo.ufn_removeMark(TenPhongTro) LIKE '%{TenPhong}%' or TenPhongTro LIKE N'%{TenPhong}%')", commandType: CommandType.Text);
            }

        }
    }
}
