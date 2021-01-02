using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace RehberKyt
{
   public class veritabani
    {
        public MySqlConnection baglanti()
        {
            MySqlConnection baglan = new MySqlConnection(@"Server = sunucuadresi; Database = veritabani; Uid = kullaniciad; Pwd = 'sifre'; Encrypt = false; AllowUserVariables = True; UseCompression = True");
           baglan.Open();
            return baglan;
        }
      
    }
}
