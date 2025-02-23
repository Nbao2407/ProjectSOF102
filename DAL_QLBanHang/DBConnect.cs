using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLBanHang
{
    public class DBConnect
    {
        protected SqlConnection _conn = new SqlConnection(@"Data Source=NGÔGIABẢO\SQLEXPRESS;Initial Catalog=QLBanHang#;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //static string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["abc"].ConnectionString;
        //protected  SqlConnection _conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLBanHang1.mdf;Integrated Security=True"); //OK
        //protected SqlConnection _conn = new SqlConnection(strConnection);        
        //Test git chạy duoc chua lan 2
        //Sua lan 3
        //Sua lan 4b
    } 
}
