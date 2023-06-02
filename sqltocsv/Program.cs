using Newtonsoft.Json;
using sqltocsv;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Chilkat;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using Aspose.Cells;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Cells.Drawing;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Amazon.Runtime.Internal.Util;

class programm
{
    static void Main(string[] args)
    {
/*-----------------------------------*/
        /*singleton obj1 = singleton.createobj();
        obj1.method1();*/
/*------------single ton */
        programm p = new programm();

        p.pure();

    }
    public void convert()
    {
        DataTable table = new DataTable();
        SqlConnection sqlCon = new SqlConnection("Data Source = DESKTOP-83C4JL9\\KALI; Initial Catalog = shopyway; Integrated Security = True;");

        string fileName = "D:\\Data\\Electronics.csv";
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "select * from products";
        sqlCmd.Connection = sqlCon;
        sqlCon.Open();


        using (var CommandText = new SqlCommand("select * from products"))
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

    public void csvtojson()
    {

        var workbook = new Workbook("D:\\Data\\Electronics.csv");
        /*  File.Create("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.json").Dispose();*/
        workbook.Save("D:\\Data\\Electronics.json");

        /*string path = "D:\\Downloads\\ajio_data.csv";
        var csv = new List<string[]>();
        var lines = File.ReadAllLines(path);

        foreach (string line in lines)
            csv.Add(line.Split(','));

        var properties = lines[0].Split(',');

        var listObjResult = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            var objResult = new Dictionary<string, string>();
            for (int j = 0; j < properties.Length; j++)
                objResult.Add(properties[j], csv[i][j]);

            listObjResult.Add(objResult);
        }
        File.Create("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.json").Dispose();

       
        var json = JsonConvert.SerializeObject(listObjResult, Formatting.Indented);

       

        File.WriteAllText(@"D:\Data\mangal.json", json);
*/

    }

    public async void download_image()
    {
        var data = File.ReadAllText("D:\\Data\\mangal.json");


        dynamic json = JsonConvert.DeserializeObject<List<ajio>>(data);


        for (int i = 0; i < json.Count; i++)
        {
            var value = File.ReadAllLines("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.txt").ToList();
            var j = Convert.ToInt32(value[0]);

            /*  string path = json[j].URL_image;
              using (WebClient client = new WebClient())
              {
                  client.DownloadFile(path, $@"D:\Image_server\Clothes\ajio\{json[j].id}.jpg");
                  value.Insert(0, Convert.ToString(j + 1));
                  File.WriteAllText("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.txt", value[0]);
                  Console.WriteLine("Downloaded " + json[j].id);
              }*/

            WebClient client = new WebClient();
            var img = json[j].URL_image;
            byte[] dataArr = client.DownloadData(json[j].URL_image);
            //save file to local
            System.IO.File.WriteAllBytes($@"D:\Image_server\Clothes\ajio\{json[j].id}.jpg", dataArr);

            value.Insert(0, Convert.ToString(j + 1));
            File.WriteAllText("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.txt", value[0]);
            Console.WriteLine("Downloaded " + json[j].id);
        }
    }

    public void checknetwork()
    {
        Boolean pin;
        var retryCount = 0;
        while (true)
        {
            try
            {
                PingNetwork("www.google.com");
                download_image();
                break;
            }
            catch (TimeoutException tex)
            {
                if (++retryCount < 3) continue;

                throw; //or handle error and break/return
            }
        }
    }
    public bool PingNetwork(string hostNameOrAddress)
    {
        bool pingStatus = false;

        using (Ping p = new Ping())
        {
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            try
            {
                PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                pingStatus = (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                pingStatus = false;
            }
        }

        return pingStatus;
    }
    public void pure()
    {
        var data = File.ReadAllText("D:\\Data\\mangal.json");

        int j = 0;
        dynamic json = JsonConvert.DeserializeObject<List<ajio>>(data);
        var women_sql = File.ReadAllLines("D:\\Data\\Women.sql").ToList();
        var men_sql = File.ReadAllLines("D:\\Data\\Men.sql").ToList();

        string[] category_arra_men = { "Short", "Shirt", "T-shirt", "Jean", "Trouser", "Chino", "Jogger", "Trunk", "Pant", "Suit", "Pyjama"
        ,"Kurta","Brief","Jacket","Boxer","Sweatshirt","Vest","Hoodie","Bermuda","Pullover","Henley","Oversized","Sweater","Shawl","Tracksuit"
        ,"Cardigan","Sherwani","Blazer","tie","Muffler","Waistcoat","hoodies","Jersey","Dhoti","Coat","Sweatpant","Waist","Gillet","Mask","Bandana"
        };
        string[] category_arra_women = { "Kurta","Legging","T-shirt","Jogger","Jean","Pant","Jegging","Top","Trouser","Dress",
            "Camisol","Palazzo","Sweatshirt","Saree","Trackpant","Shirt","Pullover","Short","Bikini","Panties","Henley",
            "Bodysuit","Lehenga","Pyjama","Hoodie","Kurti","kurta","Bra","Nightwear","Jumpsuit","Nighties","Capri","Culottes","Nighti","Jacket",
            "Tunic","Sweater","hoodies","Blouse","Blouson","Nightdress","Swimsuit","Lingerie","Tshirt","dresses","shirt","Nightshirt","Suit",
            "Panty","Anarkali","Tracksuit","Sandal","Playsuit","Bottom","Dungaree","Loungewear","chino","Skirt","Shrug","Short","Peacoat",
            "Blazer","Tregging","Cardigan","Nightgown","Babydoll","Brief","Boyshort","Trackpants","Muffler","Mask","Shawl","Gillet","Gilet",
            "Hipster","Dupatta","Salwar","Patiala","Scarf","Waistband","Pallu","Gown","Coat","Lace","piece","Accent","Hemline","Lehenga",
        };
        for (int i = 0; i < json.Count; i++)
        {
            

            if (json[i].Category_by_gender == "Men")
            {
                if (File.Exists($@"D:\\Image_server\\Clothes\\Shopyway\\{json[i].id}.jpg"))
                {
                    var jsondat = JsonConvert.SerializeObject(json[i], Formatting.Indented);
                    var jsondata = JsonConvert.DeserializeObject<dynamic>(jsondat);
                   
                    Random price = new Random();
                    int original_price = price.Next(300, 1000);
                    Random dis = new Random();
                    int discount = dis.Next(10, 40);

                    var discounted_price = original_price * discount / 100;

                    var offer_price = original_price - discounted_price;


                    string[] type_array = new string[1];
                    var discription_data = Convert.ToString(jsondata.Description);
                    string[] discription = discription_data.Split(" ");

                    for (int x = 0; x < discription.Length; x++)
                    {
                       
                        var discription_ = discription[x].ToLower();

                        for (int y = 0; y < category_arra_men.Length; y++)
                        {
                            
                            var category_arra_men_ = category_arra_men[y].ToLower();


                            
                            if (discription_.StartsWith(category_arra_men_))
                            {
                               
                                    type_array[0] = category_arra_men_;


                                
                                break;
                            }

                        }

                    }
                    var count = discription.Length - 1;



                    jsondata.Discount = discount;
                    jsondata.OriginalPrice = original_price;
                    jsondata.Add("product_name", jsondata.Description);
                    
                    jsondata.Add("Category", $"{jsondata.Category_by_gender}");
                    jsondata.Remove("Category_by_gender");
                    jsondata.Add("offer_price", offer_price);
                    jsondata.Add("total_rating", 0);
                    jsondata.Add("total_reviews", 0);
                    jsondata.Add("rating", 0);
                    
                    jsondata.Remove("URL_image");
                    jsondata.Add("URL_image", $"Clothes/Shopyway/{jsondata.id}.jpg");
                    if (type_array[0] != null)
                    {
                        jsondata.Add("type", type_array[0]);

                        string U_removableChars = Regex.Escape(@"'");
                        string U_pattern = "[" + U_removableChars + "]";
                        string descriptions = Regex.Replace(json[i].Description, U_pattern, "''");
                        var isert_command = $"insert into ajiomen(File_name, Product_name, category)values('{json[i].id}', '{descriptions}', '{type_array[0]}')";
                        men_sql.Add(isert_command);
                        File.WriteAllLines("D:\\Data\\Men.sql", men_sql);
                    }
                    else
                    {
                        jsondata.Add("type", "Others");

                        string U_removableChars = Regex.Escape(@"'");
                        string U_pattern = "[" + U_removableChars + "]";
                        string descriptions = Regex.Replace(json[i].Description, U_pattern, "''");
                        var isert_command = $"insert into ajiomen(File_name, Product_name, category)values('{json[i].id}', '{descriptions}', '{discription[count]}')";
                        men_sql.Add(isert_command);
                        File.WriteAllLines("D:\\Data\\Men.sql", men_sql);
                    }





                    var d = JsonConvert.SerializeObject(jsondata);

                    if (!Directory.Exists($@"D:\Data\Men\{jsondata.type}"))
                    {
                        Directory.CreateDirectory($@"D:\Data\Men\{jsondata.type}");
                        File.Create($@"D:\Data\Men\{jsondata.type}\{jsondata.id}.json").Dispose();
                        File.WriteAllText($@"D:\Data\Men\{jsondata.type}\{jsondata.id}.json", d);
                    }
                    else
                    {
                        File.WriteAllText($@"D:\Data\Men\{jsondata.type}\{jsondata.id}.json", d);
                    }

                    

                    var counter = $"{j = j + 1}";

                    File.WriteAllText("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.txt", counter);
                    Console.WriteLine(counter);
                }
                else
                {
                    json.Remove(json[i]);
                }
                   
            }
            if (json[i].Category_by_gender == "Women")
            {
                if (File.Exists($@"D:\\Image_server\\Clothes\\Shopyway\\{json[i].id}.jpg"))
                {
                    var jsondat = JsonConvert.SerializeObject(json[i], Formatting.Indented);
                    var jsondata = JsonConvert.DeserializeObject<dynamic>(jsondat);
                   
                    Random price = new Random();
                    int original_price = price.Next(300, 1000);
                    Random dis = new Random();
                    int discount = dis.Next(10, 40);

                    var discounted_price = original_price * discount / 100;

                    var offer_price = original_price - discounted_price;


                    string[] type_array = new string[1];
                    var discription_data = Convert.ToString(jsondata.Description);
                    string[] discription = discription_data.Split(" ");

                    for (int x = 0; x < discription.Length; x++)
                    {
                        for (int y = 0; y < category_arra_women.Length; y++)
                        {
                            var discription_ = discription[x].ToLower();
                            var category_arra_women_ = category_arra_women[y].ToLower();
                            if (discription_.StartsWith(category_arra_women_))
                            {

                                type_array[0] = category_arra_women_;



                                break;
                            }

                        }

                    }
                    var count = discription.Length - 1;



                    jsondata.Discount = discount;
                    jsondata.OriginalPrice = original_price;
                    jsondata.Add("product_name", jsondata.Description);
                  
                    jsondata.Remove("URL_image");
                    jsondata.Add("Category", $"{jsondata.Category_by_gender}");
                    jsondata.Remove("Category_by_gender");
                    jsondata.Add("offer_price", offer_price);
                    jsondata.Add("total_rating", 0);
                    jsondata.Add("total_reviews", 0);
                    jsondata.Add("rating", 0);
                    jsondata.Add("URL_image", $"Clothes/Shopyway/{jsondata.id}.jpg");
                    if (type_array[0] != null)
                    {
                        string U_removableChars = Regex.Escape(@"'");
                        string U_pattern = "[" + U_removableChars + "]";
                        string descriptions = Regex.Replace(json[i].Description, U_pattern, "''");
                        jsondata.Add("type", type_array[0]);
                        var isert_command = $"insert into ajiomen(File_name, Product_name, category)values('{json[i].id}', '{descriptions}', '{type_array[0]}')";
                        women_sql.Add(isert_command);
                        File.WriteAllLines("D:\\Data\\Women.sql", women_sql);
                    }
                    else
                    {
                        jsondata.Add("type", "Others");

                        string U_removableChars = Regex.Escape(@"'");
                        string U_pattern = "[" + U_removableChars + "]";
                        string descriptions = Regex.Replace(json[i].Description, U_pattern, "''");
                        var isert_command = $"insert into ajiomen(File_name, Product_name, category)values('{json[i].id}', '{descriptions}', '{discription[count]}')";
                        women_sql.Add(isert_command);
                        File.WriteAllLines("D:\\Data\\Women.sql", women_sql);
                    }






                    var d = JsonConvert.SerializeObject(jsondata);

                    if (!Directory.Exists($@"D:\Data\Women\{jsondata.type}"))
                    {
                        Directory.CreateDirectory($@"D:\Data\Women\{jsondata.type}");
                        File.Create($@"D:\Data\Women\{jsondata.type}\{jsondata.id}.json").Dispose();
                        File.WriteAllText($@"D:\Data\Women\{jsondata.type}\{jsondata.id}.json", d);
                    }
                    else
                    {
                        File.WriteAllText($@"D:\Data\Women\{jsondata.type}\{jsondata.id}.json", d);
                    }

                   
                    var counter = $"{j = j + 1}";

                    File.WriteAllText("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.txt", counter);
                    Console.WriteLine(counter);


                }
                else
                {
                    json.Remove(json[i]);
                }




            }


        }


        var serialize = JsonConvert.SerializeObject(json,Formatting.Indented);
        File.WriteAllText("D:\\Data\\mangal.json", serialize);
        Console.WriteLine(json.id);

    }



    public void convertsql()
    {
        var data = File.ReadAllText("D:\\Data\\mangal.json");
        var women_sql = File.ReadAllLines("D:\\Data\\Men.sql").ToList();

        var json = JsonConvert.DeserializeObject<List<ajio>>(data);
        for (int i = 0; i < json.Count; i++)
        {
            if (File.Exists($@"D:\\Image_server\\Clothes\\Shopyway\\{json[i].id}.jpg"))
            {
                if (!File.Exists("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.sql"))
                {
                    File.Create("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.sql").Dispose();
                }
                else
                {
                   
                   var d = $"insert into ajiomen(File_name, Product_name, category)values('{json[i].id}', '{json[i].Description}', 'mangal')";
                    women_sql.Insert(0,d) ;
                    File.WriteAllLines("D:\\VisualStudioProject\\sqltocsv\\sqltocsv\\mangal.sql", women_sql);
                }
            }
            else
            {

            }
        }

     
        

    }
    public void electronics_data()
    {
        var data = File.ReadAllText("D:\\Data\\Electronics.json");

        int j = 0;
        dynamic json = JsonConvert.DeserializeObject<List<Electronics>>(data);

        for (int i = 0; i < json.Count; i++)
        {
            var jsondat = JsonConvert.SerializeObject(json[i], Formatting.Indented);
            var jsondata = JsonConvert.DeserializeObject<dynamic>(jsondat);
            
            


            if (File.Exists($@"D:\Image_server\Electronics\{jsondata.id}.jpg"))
            {
                jsondata.Add("URL_image", $"shopimg/Electronics/{jsondata.id}.jpg");

              
                var d = JsonConvert.SerializeObject(jsondata, Formatting.Indented);

                if (!Directory.Exists($@"D:\Data\Electronics\{jsondata.type}"))
                {
                    Directory.CreateDirectory($@"D:\Data\Electronics\{jsondata.type}");
                    File.Create($@"D:\Data\Electronics\{jsondata.type}\{jsondata.id}.json").Dispose();
                    File.WriteAllText($@"D:\Data\Electronics\{jsondata.type}\{jsondata.id}.json", d);
                }
                else
                {
                    File.Create($@"D:\Data\Electronics\{jsondata.type}\{jsondata.id}.json").Dispose();
                    File.WriteAllText($@"D:\Data\Electronics\{jsondata.type}\{jsondata.id}.json", d);
                }

                Console.WriteLine($"{jsondata.id}");
            }

            

            

           
          

        }


    }

    public void Update_json_data()
    {
        dynamic path = $"D:\\Data\\Women\\";

        var rand = new Random();
        var files = Directory.GetFiles(path, "*.json",SearchOption.AllDirectories);
        
        for (int i = 0; i < files.Length; i++)
        {
            var data = File.ReadAllText(files[i]);
           /* var serialize = JsonConvert.SerializeObject(, Formatting.Indented);*/
             dynamic jsondata = JsonConvert.DeserializeObject<dynamic>(data);



            jsondata.Remove("URL_image");
            jsondata.Add("URL_image", $"Clothes/Shopyway/{jsondata.id}.jpg");
              

                

           
            
            
            /*            jsondata.URL_image = $"Clothes/Shopyway/{jsondata.URL_image}";*/
            
            
            

            var d = JsonConvert.SerializeObject(jsondata, Formatting.Indented);
            File.WriteAllText($@"D:\Data\Women\{jsondata.type}\{jsondata.id}.json", d);
            Console.WriteLine(jsondata.id);

        }
    }



}

sealed class singleton
{
    private singleton()
    {

    }

    public static singleton getinstance = null;
    public static singleton createobj()
    {
        if(getinstance == null)
        {
            return new singleton();
        }
        else
        {
            return getinstance;
        }
    }
    public void method1()
    {

    }
}
public static class MyStringExtensions
{
    public static bool Like(this string toSearch, string toFind)
    {
        return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
    }
}