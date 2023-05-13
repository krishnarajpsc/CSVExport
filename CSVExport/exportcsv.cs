using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace CSVExport
{
    static class Exportcsv
    {
        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(';');
            }
            sw.Close();
        }
        public static DataTable CSVtoDataTable(string strFilePath, char csvDelimiter)
        {
            DataTable dt = new DataTable();
            if (File.Exists(strFilePath))
            {
                using (StreamReader sr = new StreamReader(strFilePath))
                {

                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        try
                        {
                            dt.Columns.Add(header);
                        }
                        catch { }
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(csvDelimiter);
                        foreach (string rowStr in rows) {
                            if (rowStr != "")
                            {
                                DataRow dr = dt.NewRow();
                                string[] columns = rowStr.Split(',');
                                for (int i = 0; i < columns.Length; i++)
                                {
                                    dr[i] = columns[i];
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }

                }
            }
            return dt;
        }
    }
}
