using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace dotnetconsulting.Samples.EFContext.SqlResources
{
    public static class MigrationBuilderExtentions
    {
        public static void SqlResource(this MigrationBuilder migrationBuilder, Assembly Assembly, string ResourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
                using (StreamReader reader = new StreamReader(stream))
                    migrationBuilder.Sql(reader.ReadToEnd());
        }

        public static void SqlResource(this MigrationBuilder migrationBuilder, string ResourceName)
        {
            migrationBuilder.SqlResource(Assembly.GetExecutingAssembly(), ResourceName);
        }
    }
}