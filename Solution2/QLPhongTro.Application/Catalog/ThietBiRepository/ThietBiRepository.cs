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

namespace QLPhongTro.Application.Catalog.ThietBiRepository
{
    public class ThietBiRepository:GenericRepositories,IThietBi
    {
        public ThietBiRepository()
        {

        }

        public ThietBiRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<THIETBI> GetAll()
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<THIETBI>)connection.Query<THIETBI>("SELECT * From THIETBI");
            }

        }

        public int Add(THIETBI tb)
        {
            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();

                x = connection.Query<PHONGTRO>($"SELECT UPPER(TenThietBi) From THIETBI Where Replace(dbo.TRIM1(UPPER(dbo.ufn_removeMark(TenThietBi))),' ','')=REPLACE(dbo.TRIM1(UPPER(dbo.ufn_removeMark(@TenThietBi))), ' ', '')", tb, commandType: CommandType.Text).Count();
                if (x <= 0)
                {
                    connection.Execute("insert THIETBI values(@TenThietBi)", tb);
                    return 1;
                }
                return 0;
            }
            
        }

        public int Delete(int maTB)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from THIETBI Where MaThietBi ={maTB}");
            }
            return count;
        }

        public int Update(THIETBI TB)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE THIETBI SET TenThietBi= @TenThietBi WHERE MaThietBi =@MaThietBi", TB);
            }
            return count;
        }

        public THIETBI GetId(int maTB)
        {
            THIETBI tb = new THIETBI();
            using (SqlConnection connection = con())
            {
                tb = (THIETBI)connection.Query<THIETBI>("select TOP 1 * from THIETBI where MaThietBi=" + maTB).FirstOrDefault();
                return tb;
            }

        }

        public List<THIETBI> Search(string TenTB)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<THIETBI>)connection.Query<THIETBI>($"SELECT *FROM THIETBI WHERE dbo.ufn_removeMark(TenThietBi) LIKE '%{TenTB}%' or TenThietBi LIKE N'%{TenTB}%' ", commandType: CommandType.Text);
            }

        }
    }
}
