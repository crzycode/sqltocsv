using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqltocsv
{
    internal class Ajio_json
    {
        public string id { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string URL_image { get; set; }
        public string Category_by_gender { get; set; }

        public int Discount { get; set; }
        public int OriginalPrice { get; set; }
        public string Color { get; set; }
        public string product_name { get; set; }
        public int offer_price { get; set; }

        public int total_rating { get; set; }
        public int total_reviews { get; set; }
        public int rating { get; set; }
        public string Type { get; set; }
    }
}
