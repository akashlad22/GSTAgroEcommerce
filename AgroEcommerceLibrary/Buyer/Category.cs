using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Buyer
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }

        public int StatusId { get; set; }
        public float Commission { get; set; }
        public string RejectionReason { get; set; }

        ///SubCategory
        public int SubCategory1Id { get; set; }
        public string SubCategory1Name { get; set; }
        public string SubCategory1Code { get; set; }

        public int SubCategory2Id { get; set; }
        public string SubCategory2Name { get; set; }
        public string SubCategory2Code { get; set; }

        public int SubCategory3Id { get; set; }
        public string SubCategory3Name { get; set; }
        public string SubCategory3Code { get; set; }

        public List<Category> categories { get; set; }

    }
}
