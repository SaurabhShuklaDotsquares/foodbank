using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Models
{
    public class OpenFoodApiModel
    {
        public List<Product> products { get; set; }
    }
    public class Product
    {
        public string _id { get; set; }
        public string product_name { get; set; }
        public string additives_tags { get; set; }
    }


    public class OpenFoodApiCategoryModel
    {
        public List<Tag> Tags { get; set; }
    }
    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class OpenFoodApiAllergyModel
    {
        public List<Product> product { get; set; }
    }
    public class Allergy
    {
        public string _id { get; set; }
        public string product_name { get; set; }
    }
}
