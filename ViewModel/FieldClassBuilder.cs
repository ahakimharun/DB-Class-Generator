
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace DB_Class_Generator.ViewModel
{
    public class FieldClassBuilder
    {
        public StringBuilder ClassString = new StringBuilder();

        public FieldClassBuilder(string dbName, string dbTable, IEnumerable<TableFields> tableFields)
        {
            ClassString.AppendLine($"// Generated class for {dbTable} from database: {dbName}");
            ClassString.AppendLine($"public class {dbTable}\n");

            foreach(TableFields tablefield in tableFields)
            {
                var TransformedFieldtype = TransformFieldtype(tablefield);
                ClassString.AppendLine($"\tpublic {TransformedFieldtype} {tablefield.Field} {{ get; set; }}");
            }
            ClassString.AppendLine("}");
        }

        public string TransformFieldtype(TableFields tablefield)
        {

            var nullable = tablefield.Null == "YES" ? "?" : string.Empty;
            // Match any number within brackets '(' ')'
            var bracketednumber = Regex.Matches(tablefield.Type, @"\(\d+\)");

            string typesize = "";
            foreach(var match in bracketednumber)
            {
                typesize = Regex.Replace(match.ToString(), @"\D", "");
            }
            
            // var typesize = new String(tablefield.Type.Where(Char.IsDigit).ToArray());
            var cleantypetext = Regex.Replace(tablefield.Type, @"\(\d+\)", "");
            return cleantypetext switch
            {
                "binary" => $"byte[{typesize}]{nullable }",
                "bit" => $"bool{nullable}",
                "tinyint" => $"Int16{nullable}",
                "int" => $"int{nullable}",
                "bigint" => $"long{nullable}",
                "smallmoney" => $"decimal{nullable}",
                "money" => $"decimal{nullable}",
                "decimal" => $"decimal{nullable}",
                "double" => $"double{nullable}",
                "numeric" => $"decimal{nullable}",
                "char" => $"string",
                "nchar" => $"string",
                "varchar" => $"string",
                "nvarchar" => $"string",
                "text" => $"string",
                "ntext" => $"string",
                "xml" => $"System.Xml.Linq.XElement",
                "smalldatetime" => $"DateTime{nullable}",
                "datetime" => $"DateTime{nullable}",
                "datetime2" => $"DateTime{nullable}",
                "date" => $"DateTime{nullable}",
                "datetimeoffset" => $"DateTimeOffset{nullable}",
                "time" => $"TimeSpan{nullable}",
                "uniqueidentifier" => $"Guid{nullable}",
                "sql_variant" => $"object{nullable}",
                "timestamp" => $"byte[]",
                _ => $"<{tablefield.Type}>",

            };
        }
    }

//    public partial class DatabaseType : ObservableObject
//    {
//        public DatabaseType()
//        {
//            _databasetypes = new ObservableCollection<string>
//        {
//            "MySql",
//            "SQL Server"
//        };
//        }

//        private ObservableCollection<string> _databasetypes;
//        public ObservableCollection<string> Databasetypes { get { return _databasetypes; } }
//    }
}
