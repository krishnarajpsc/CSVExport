using System;
using System.Data;
using System.Data;
using System.IO;

namespace CSVExport
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string importPath = Path.Combine(path, "import.csv");
            string exportPath = Path.Combine(path, "export.csv");
            DatabaseConnection databaseConnection = new DatabaseConnection();
            DataTable saveDataTable=Exportcsv.CSVtoDataTable(importPath, ';');
            if (saveDataTable != null)
            {
                databaseConnection.SaveData(saveDataTable);
            }
            Exportcsv.ToCSV(databaseConnection.GetEmployeeData(), exportPath);
        }
       
    }
}
