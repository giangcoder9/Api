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

namespace QLPhongTro.Application.Catalog.DichVuRepository
{
    public class DichVuRepository : GenericRepositories, IDichVu
    {
        public DichVuRepository()
        {

        }

        public DichVuRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<DichVu> GetAll()
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<DichVu>)connection.Query<DichVu>("SELECT * From DichVu");
            }

        }

        public int Add(DichVu dv)
        {
            int x;
            using (SqlConnection connection = con())
            {
                connection.Open();

                x = connection.Query<DichVu>($"SELECT UPPER(MaDV) From DichVu Where Replace(dbo.TRIM1(UPPER(dbo.ufn_removeMark(TenDV))),' ','')=REPLACE(dbo.TRIM1(UPPER(dbo.ufn_removeMark(@TenDV))), ' ', '')", dv, commandType: CommandType.Text).Count();
                if (x <= 0)
                {
                    connection.Execute("insert DichVu values(@TenDV ,@Gia)", dv);
                    return 1;
                }
                return 0;
            }
            
        }

        public int Delete(int maDV)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute($"delete from DichVu Where MaDV={maDV}");
            }
            return count;
        }

        public int Update(DichVu dv)
        {
            int count = 0;
            using (SqlConnection connection = con())
            {
                count = connection.Execute("UPDATE DichVu SET TenDV = @TenDV , Gia= @Gia where MaDV=@MaDV" , dv);
            }
            return count;
        }

        public DichVu GetId(int maDV)
        {
            DichVu dv = new DichVu();
            using (SqlConnection connection = con())
            {
                dv = (DichVu)connection.Query<DichVu>("select TOP 1 * from DichVu where MaDV =" + maDV ).FirstOrDefault();
                return dv;
            }

        }

        public List<DichVu> Search(string TenDV)
        {

            using (SqlConnection connection = con())
            {
                connection.Open();
                return (List<DichVu>)connection.Query<DichVu>($"SELECT *FROM DichVu WHERE dbo.ufn_removeMark(TenDV) LIKE '%{TenDV}%' or TenDV LIKE N'%{TenDV}%'", commandType: CommandType.Text);
            }

        }
    }
}
