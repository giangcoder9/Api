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

namespace QLPhongTro.Application.Catalog.ChiTietThietBiRepisitory
{
    public class ChiTietThietBiRepository: GenericRepositories, IChiTietThietBi
    {
        public ChiTietThietBiRepository()
    {

    }

    public ChiTietThietBiRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public List<CHITETTHIETBI> GetAll()
    {

        using (SqlConnection connection = con())
        {
            connection.Open();
            return (List<CHITETTHIETBI>)connection.Query<CHITETTHIETBI>("select MaCTTB,THIETBI.TenThietBi,THIETBI.MaThietBi,soLuong,maphong,TenPhongTro from THIETBI,CHITETTHIETBI,PHONGTRO where THIETBI.MaThietBi=CHITETTHIETBI.MaThietBi and PHONGTRO.MaPhongTro=CHITETTHIETBI.maphong ");
        }

    }

    public int Add(CHITETTHIETBI ct)
    {
        int count = 0;
        using (SqlConnection connection = con())
        {
            count = connection.Execute("insert CHITETTHIETBI values(@MaThietBi,@soLuong,@maphong)", ct);
        }
        return count;
    }

    public int Delete(int maCT)
    {
        int count = 0;
        using (SqlConnection connection = con())
        {
            count = connection.Execute($"delete from CHITETTHIETBI Where MaCTTB={maCT}");
        }
        return count;
    }

        public int Update(CHITETTHIETBI ct)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE CHITETTHIETBI SET MaThietBi=@MaThietBi,maphong=@maphong,soLuong=@soLuong where MaCTTB= @MaCTTB", ct);
            }
            return count;
        }

        public CHITETTHIETBI GetId(int maCT)
    {
            CHITETTHIETBI ct = new CHITETTHIETBI();
        using (SqlConnection connection = con())
        {
            ct= (CHITETTHIETBI)connection.Query<CHITETTHIETBI>("select MaCTTB,THIETBI.TenThietBi,THIETBI.MaThietBi,soLuong,maphong,TenPhongTro from THIETBI,CHITETTHIETBI,PHONGTRO where THIETBI.MaThietBi=CHITETTHIETBI.MaThietBi and PHONGTRO.MaPhongTro=CHITETTHIETBI.maphong and MaCTTB =" + maCT).FirstOrDefault();
            return ct;
        }

    }

    public List<CHITETTHIETBI> Search(string TenPT)
    {

        using (SqlConnection connection = con())
        {
            connection.Open();
            return (List<CHITETTHIETBI>)connection.Query<CHITETTHIETBI>($"select MaCTTB,THIETBI.TenThietBi,THIETBI.MaThietBi,soLuong,maphong,TenPhongTro from THIETBI,CHITETTHIETBI,PHONGTRO where THIETBI.MaThietBi=CHITETTHIETBI.MaCTTB and PHONGTRO.MaPhongTro=CHITETTHIETBI.maphong and (dbo.ufn_removeMark(TenPhongTro) LIKE '%{TenPT}%' or TenPhongTro LIKE N'%{TenPT}%')", commandType: CommandType.Text);
        }
           

    }
      
    }
}
