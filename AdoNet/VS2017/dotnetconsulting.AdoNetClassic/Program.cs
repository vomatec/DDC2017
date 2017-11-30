// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu
using System;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace dotnetconsulting.AdoNetClassic
{
    class Program
    {
        static void Main(string[] args)
        {
            ListAdoNetProvider();

            Console.WriteLine("== Fertig ==");
            Console.ReadKey();
        }

        public static void ListAdoNetProvider()
        {
            DataTable factories = DbProviderFactories.GetFactoryClasses();

            foreach (DataRow row in factories.Rows)
            {
                // ...
            }
        }

        public static void ResultSetSchema()
        {
            const string conString = @"server=.;database=master;integrated security=true;";
            const string sql = @"select * from sys.databases";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable table = dr.GetSchemaTable();
                        // ...
                    }
                }
            }
        }

        public static void BulkCopyTest()
        {
            const string conString = @"server=.;database=master;integrated security=true;";

            SqlBulkCopy sbc = new SqlBulkCopy(conString)
            {
                EnableStreaming = true,
            };

            sbc.NotifyAfter = 5000;
            sbc.SqlRowsCopied += Sbc_SqlRowsCopied;
            // sbc.WriteToServer(...);
        }

        private static void Sbc_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void TestSqlConnectionStringBuilder()
        {
            SqlConnectionStringBuilder a;

        }
    }
}
