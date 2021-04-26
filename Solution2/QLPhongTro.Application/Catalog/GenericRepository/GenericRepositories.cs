using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace QLPhongTro.Application.Catalog.GenericRepository
{
    public class GenericRepositories
    {

        //protected static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLPhongTro;Integrated Security=True";// ??????
        protected string connectionString;// ??????
        private readonly IConfiguration configuration;

        protected SqlConnection con()
        {
            return new SqlConnection(connectionString);
        }

        public GenericRepositories()
        {

        }

        public GenericRepositories(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("QLPhongTro");
        }
    }
}
