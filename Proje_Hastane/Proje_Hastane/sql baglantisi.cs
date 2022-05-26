using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    class sql_baglantisi
    {
        //burası ilk adımımız bi sınıf çağırdık ve adını sql_baglantisi yaptık bu sayede her yerden bağlanabiliriz
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=EFE\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
