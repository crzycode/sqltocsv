using System.Data;
using System.Data.SqlClient;

class programm
{
    static void Main(string[] args)
    {
        programm p = new programm();
        p.convert();
    }
    public void convert()
    {
        DataTable table = new DataTable();
        SqlConnection sqlCon = new SqlConnection("Data Source = DESKTOP-U7FFVU6\\KALI; Initial Catalog = Medical_shop; Integrated Security = True;");
       
        string fileName = "D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\Brand.csv";
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "select * from brand";
        sqlCmd.Connection = sqlCon;
        sqlCon.Open();
     

        using (var CommandText = new SqlCommand("select * from brand"))
        using (var reader = sqlCmd.ExecuteReader())
        using (var outFile = File.CreateText(fileName))
        {
            

            string[] columnNames = GetColumnNames(reader).ToArray();
            int numFields = columnNames.Length;
            outFile.WriteLine(string.Join(",", columnNames));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] columnValues =
                        Enumerable.Range(0, numFields)
                                  .Select(i => reader.GetValue(i).ToString())
                                  .Select(field => string.Concat("\"", field.Replace("\"", "\"\""), "\""))
                                  .ToArray();
                    outFile.WriteLine(string.Join(",", columnValues));
                }
            }
        }
    }
    private IEnumerable<string> GetColumnNames(IDataReader reader)
    {
        foreach (DataRow row in reader.GetSchemaTable().Rows)
        {
            yield return (string)row["ColumnName"];
        }
    }
}
