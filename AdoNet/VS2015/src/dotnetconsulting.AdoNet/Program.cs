// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Xml;
using Microsoft.Data.Sqlite;

namespace dotnetconsulting.AdoNet
{
    public class Program
    {
        static IConfigurationRoot config;

        public static void Main(string[] args)
        {
            SetupConfiguration();

            // Nur SqlClient
            // Kein DataTabel/ DataSet, etc.
            // Kein SqlDependency

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //TestDataTable();
            //TestListAdoNetProvider();
            //TestSqlDependency();
            //TestResultSetSchema();

            //TestSqlConnectionStringBuilder();
            //TestExecuteNonQuery();
            //TestExecuteScalar();
            //TestExecuteReader();
            //TestExecuteReaderBatch();
            //TestExecuteXmlReader();

            //TestScalarParameters(DateTime.Parse("1/1/2017"));

            //TestPreparedScalarParameters(true);
            //TestTableValuedParameters(false);
            //TestBulkCopy();

            //TestErrorHandling();

            //TestTransactions();

            TestSQLite();

            sw.Stop();
            Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds:N0} ms");

            Console.WriteLine("== Fertig ==");
            Console.ReadKey();
        }

        #region Config
        public static void SetupConfiguration()
        {
            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            config = configBuilder.Build();
        }

        public static string MasterConnectionString
        {
            get
            {
                return config["ConnectionStrings:General"];
            }
        }

        public static string AdoNetTestDbConnectionString
        {
            get
            {
                return config["ConnectionStrings:AdoNetTestDb"];
            }
        }

        public static string SQLiteConnection
        {
            get
            {
                return config["ConnectionStrings:SQLite"];
            }
        }
        #endregion

        public static void TestDataTable()
        {
            Console.WriteLine("==  TestDataTable() ==");

            DataTable t;
            // DatatSet s;
        }

        public static void TestListAdoNetProvider()
        {
            Console.WriteLine("==  TestListAdoNetProvider() ==");

            // Nicht möglich mit .NET Core
            // DataTable factories = null;
        }

        public static void TestSqlDependency()
        {
            Console.WriteLine("==  TestSqlDependency() ==");

            // Nicht möglich mit .NET Core
            // SqlDependency dep;
        }

        public static void TestResultSetSchema()
        {
            Console.WriteLine("==  TestResultSetSchema() ==");

            const string sql = @"SELECT * FROM [sys].[databases];";

            using (SqlConnection con = new SqlConnection(MasterConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Methode existiert schlicht nicht
                        // dr.GetSchemaTable();
                        // ...
                    }
                }
            }
        }

        public static void TestSqlConnectionStringBuilder()
        {
            Console.WriteLine("==  TestSqlConnectionStringBuilder() ==");

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(AdoNetTestDbConnectionString);
            Console.WriteLine($"DataSource = {scsb.DataSource}");
            Console.WriteLine($"InitialCatalog = {scsb.InitialCatalog}");
            Console.WriteLine($"IntegratedSecurity = {scsb.IntegratedSecurity}");

            scsb.IntegratedSecurity = false;
            scsb.UserID = "tkansy";
            scsb.Password = "Gehe'm;=\"";

            Console.WriteLine($"ConnectionStrings = {scsb.ConnectionString}");
        }

        #region Execute
        public static void TestExecuteNonQuery()
        {
            Console.WriteLine("==  TestExecuteNonQuery() ==");

            const string SQL = @"TRUNCATE TABLE [dbo].[Names];";

            using (SqlConnection con = new SqlConnection(AdoNetTestDbConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void TestExecuteScalar()
        {
            Console.WriteLine("==  TestExecuteScalar() ==");

            const string SQL = @"SELECT COUNT(*) FROM [sys].[databases];";

            using (SqlConnection con = new SqlConnection(MasterConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    int count = (int)cmd.ExecuteScalar();
                    Console.WriteLine($"count={count:N0}");
                }
            }
        }

        public static void TestExecuteReader()
        {
            Console.WriteLine("==  TestExecuteReader() ==");

            const string SQL = @"SELECT [name], [database_id] FROM [sys].[databases];";

            using (SqlConnection con = new SqlConnection(MasterConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string dbName = dr.GetString(0);
                            int dbId = dr.GetInt32(1);
                            Console.WriteLine($"Name: {dbName}, Id:{dbId}", dbName, dbId);
                        }
                    }
                }
            }
        }

        public static void TestExecuteReaderBatch()
        {
            Console.WriteLine("==  TestExecuteReaderBatch() ==");

            const string SQL = @"SELECT [name], [database_id] FROM [sys].[databases];
                                 SELECT [TABLE_SCHEMA], [TABLE_NAME] FROM [INFORMATION_SCHEMA].[TABLES]
                                 WHERE [TABLE_TYPE] = 'BASE TABLE' ORDER BY [TABLE_NAME];";

            using (SqlConnection con = new SqlConnection(MasterConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                    {
                        do // Alle Ergebnismengen durchlaufen
                        {
                            // Besser zwar wäre ein StringBuilder, doch so ist es einfacher
                            string header = string.Empty;

                            // Namen und Datentypen der Spalten auslesen
                            for (int i = 0; i < dr.VisibleFieldCount; i++)
                                header += $"{ dr.GetName(i)} ({dr.GetFieldType(i).ToString()}) |";

                            // Ausgeben
                            Console.WriteLine(header);
                            Console.WriteLine("".PadLeft(header.Length, '='));
                            while (dr.Read()) // Alle Zeilen durchlaufen
                            {
                                for (int i = 0; i < dr.VisibleFieldCount; i++) // Alle Spalten
                                    if (!dr.IsDBNull(i)) // NULL?
                                                         // Wert ausgeben
                                        Console.Write(dr.GetValue(i).ToString() + '|');
                                    else
                                        Console.Write("<NULL> |");
                                Console.WriteLine(string.Empty); // Zeilenumbruch
                            }
                        } while (dr.NextResult());
                    }
                }
            }
        }

        public static void TestExecuteXmlReader()
        {
            Console.WriteLine("==  TestExecuteXmlReader() ==");

            const string SQL = @"SELECT [name], [database_id] FROM [sys].[databases] FOR XML AUTO;";

            using (SqlConnection con = new SqlConnection(MasterConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    using (XmlReader xmlDr = cmd.ExecuteXmlReader())
                    {
                        if (xmlDr.Read())
                            while (xmlDr.ReadState != ReadState.EndOfFile)
                                Console.WriteLine(xmlDr.ReadOuterXml());
                        else
                            Console.WriteLine("Keine Daten gefunden");
                    }
                }
            }
        }
        #endregion

        public static void TestScalarParameters(DateTime? CreateDate)
        {
            Console.WriteLine("==  TestScalarParameters() ==");

            const string SQL = @"SELECT [name], [database_id] FROM [sys].[databases] " +
                                "WHERE (create_date IS NULL OR [create_date] > @create_date) ORDER BY [create_date];";

            using (SqlConnection con = new SqlConnection(AdoNetTestDbConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    if (CreateDate.HasValue)
                        cmd.Parameters.AddWithValue("create_date", CreateDate);
                    else
                        cmd.Parameters.AddWithValue("create_date", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string dbName = dr.GetString(0);
                            int dbId = dr.GetInt32(1);
                            Console.WriteLine($"Name: {dbName}, Id:{dbId}", dbName, dbId);
                        }
                    }
                }
            }
        }

        public static void TestPreparedScalarParameters(bool UsePrepareCommand)
        {
            Console.WriteLine("==  TestPreparedScalarParameters() ==");

            const string SQL = @"INSERT [dbo].[Names] VALUES(@ID, @Name);";

            // 1000000 Zeilen sollen es sein
            const int rows = 1000000;

            // Stored Procedure aufrufen
            using (SqlConnection con = new SqlConnection(AdoNetTestDbConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;

                    // Genaue Definition für Prepare notwendig
                    cmd.Parameters.Add("ID", SqlDbType.Int, 4);
                    cmd.Parameters.Add("Name", SqlDbType.VarChar, 100);

                    if (UsePrepareCommand)
                        cmd.Prepare();

                    // Alle Zeilen einfügen
                    for (int i = 0; i < rows; i++)
                    {
                        cmd.Parameters["ID"].Value = i;
                        cmd.Parameters["Name"].Value = $"Name {i}";

                        // Ausführen
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void TestTableValuedParameters(bool dumpIDs)
        {
            Console.WriteLine("==  TestTableValuedParameters() ==");

            // Übergabe als SqlDataRecord erstellen
            // 1000000 Zeilen sollen es sein
            const int rows = 1000000;
            IList<SqlDataRecord> tvp = new List<SqlDataRecord>(rows);

            for (int i = 0; i < rows; i++)
            {
                SqlDataRecord record = new SqlDataRecord(
                        new SqlMetaData("Id", SqlDbType.Int),
                        new SqlMetaData("Name", SqlDbType.VarChar, 128));

                record.SetValues(i, $"Name {i}");
                tvp.Add(record);
            }

            // Stored Procedure aufrufen
            using (SqlConnection con = new SqlConnection(AdoNetTestDbConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "dbo.usp_ProcTVP";

                    // Einzigen Parameter als TVP übergeben
                    SqlParameter pList = cmd.Parameters.AddWithValue("list", tvp);
                    pList.SqlDbType = SqlDbType.Structured;
                    pList.TypeName = "dbo.udt_Sample";

                    if (dumpIDs)
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(0);
                                string name = dr.GetString(1);

                                Console.WriteLine($"Id: {id}, Name: {name}");
                            }
                        }
                    }
                    else
                        cmd.ExecuteNonQuery();
                }
            }
        }

        public static void TestBulkCopy()
        {
            Console.WriteLine("==  TestBulkCopy() ==");

            // BulkCopy Objekt ablegen
            SqlBulkCopy sbc = new SqlBulkCopy(AdoNetTestDbConnectionString)
            {
                DestinationTableName = "dbo.Names",
                EnableStreaming = true,
                BatchSize = 100000,
                NotifyAfter = 100000
            };
            sbc.SqlRowsCopied += BulkCopyTest_SqlRowsCopied;

            // Datenquelle initialisieren
            FakeDbDataReader fdr = new FakeDbDataReader(1000000);

            // Auf geht's
            sbc.WriteToServer(fdr);
        }

        private static void BulkCopyTest_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Console.WriteLine($"{e.RowsCopied:N0} rows copied");
        }

        private static void TestErrorHandling()
        {
            Console.WriteLine("==  TestErrorHandling() ==");

            const string SQL = "SELEKT COUNT(*) FROM [sys].[databases];";

            try
            {
                using (SqlConnection con = new SqlConnection(MasterConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQL;

                        // Ausführen und ausgeben
                        Console.WriteLine((int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception!");
                Console.WriteLine($"\t{ex.Message}");

                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine($"\tState = {error.State}, Message = {error.Message}");
                }
            }
        }

        public static void TestTransactions()
        {
            Console.WriteLine("==  TestTransactions() ==");

            const string SQL1 = @"DELETE [dbo].[Names];";
            const string SQL2 = @"SELECT COUNT(*) FROM [dbo].[Names];";

            using (SqlConnection con = new SqlConnection(AdoNetTestDbConnectionString))
            {
                con.Open();

                // Abfrage 1
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQL1;
                        cmd.Transaction = tran;

                        cmd.ExecuteNonQuery();
                    }

                    // Abfrage 2

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQL2;
                        cmd.Transaction = tran;

                        int count = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"count={count:N0}");
                    }

                    tran.Commit();
                }
            }
        }

        #region Andere DbProvider
        public static void TestSQLite()
        {
            Console.WriteLine("==  TestSQLite() ==");

            // Dateinamen ermitteln
            SqliteConnectionStringBuilder scsb = new SqliteConnectionStringBuilder(SQLiteConnection);

            // Ist die Datenbank schon vorhanden?
            // Wenn nicht anlegen
            if (!File.Exists(scsb.DataSource))
            {
                Console.WriteLine("Datenbank und Tabelle anlegen");

                const string SQL1 = "CREATE TABLE Names(ID INT NOT NULL, Name TEXT NOT NULL)";

                // Verbindung aufbauen
                using (SqliteConnection con = new SqliteConnection(SQLiteConnection))
                {
                    con.Open();

                    // Tabelle anlegen
                    using (SqliteCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQL1;

                        cmd.ExecuteNonQuery();
                    }

                    // Daten einfügen
                    // 1000 Zeilen sollen es sein
                    const int rows = 1000;

                    const string SQL2 = "INSERT INTO Names (ID, Name) VALUES (@ID,@Name);";
                    
                    using (SqliteCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQL2;

                        // Parameter anlegen
                        SqliteParameter pID = cmd.Parameters.Add(new SqliteParameter("ID", null));
                        SqliteParameter pName = cmd.Parameters.Add(new SqliteParameter("Name", null));

                        // Prepare scheint mit Sqlite keinen Effekt zu habe
                        // cmd.Prepare();

                        for (int i = 0; i < rows; i++)
                        {
                            pID.Value = i;
                            pName.Value = $"Name {i}";

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Ab hier muss die Datenbank (inkl. Daten) vorhanden sein
            // Also versuchen wir, die Daten abzufragen
            const string SQL3 = "SELECT * FROM Names";

            using (SqliteConnection con = new SqliteConnection(SQLiteConnection))
            {
                con.Open();
                // Tabelle anlegen
                using (SqliteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL3;

                    using (SqliteDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                    {
                        // Besser zwar wäre ein StringBuilder, doch so ist es einfacher
                        string header = string.Empty;

                        // Namen und Datentypen der Spalten auslesen
                        for (int i = 0; i < dr.VisibleFieldCount; i++)
                            header += $"{ dr.GetName(i)} ({dr.GetFieldType(i).ToString()}) |";

                        // Ausgeben
                        Console.WriteLine(header);
                        Console.WriteLine("".PadLeft(header.Length, '='));
                        while (dr.Read()) // Alle Zeilen durchlaufen
                        {
                            for (int i = 0; i < dr.VisibleFieldCount; i++) // Alle Spalten
                                if (!dr.IsDBNull(i)) // NULL?
                                                     // Wert ausgeben
                                    Console.Write(dr.GetValue(i).ToString() + '|');
                                else
                                    Console.Write("<NULL> |");
                            Console.WriteLine(string.Empty); // Zeilenumbruch
                        }
                    }
                }
            }
        }

        public static void PostgreSQLTest()
        {
            Console.WriteLine("==  PostgreSQLTest() ==");

            // Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
            // Nicht sinnvoll nutzbar
            Microsoft.EntityFrameworkCore.Storage.Internal.NpgsqlRelationalConnection a;
        }

        public static void InMemoryTest()
        {
            Console.WriteLine("==  InMemoryTest() ==");

            // Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
            // Nicht sinnvoll nutzbar
            Microsoft.EntityFrameworkCore.Storage.Internal.InMemoryStore a;
        }
        #endregion
    }
}