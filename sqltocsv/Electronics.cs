using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqltocsv
{
    internal class Electronics
    {
        public int Product_id { get; set; }
        public string id { get; set; }
        public string Product_name { get; set; }
        public string type { get; set; }
        public int offer_price { get; set; }
        public int original_price { get; set; }
        public string off_now { get; set; }

        public int total_rating { get; set; }
        public int total_reviews { get; set; }
        public double rating { get; set; }
        public string description { get; set; }

    }
}
