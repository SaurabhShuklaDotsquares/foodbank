using FB.Web.Areas.FoodBank.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FB.Web
{
    public class OpenFoodApi
    {
        public static List<SelectListItem> GetFoodItemCatogoriesList()
        {
            #region Configuration
            string endPointUrl = "https://uk.openfoodfacts.org/categories.json";
            #endregion


            #region Bind food item list using open food api
            try
            {
                using (var wc = new WebClient())
                {

                    try
                    {
                        OpenFoodApiCategoryModel apimodel = new OpenFoodApiCategoryModel();
                        var result = wc.DownloadData(endPointUrl);
                        var encodedList = Encoding.UTF8.GetString(result);
                        apimodel = JsonConvert.DeserializeObject<OpenFoodApiCategoryModel>(encodedList);
                        var foodItemCatgoriesList = apimodel.Tags.Select(c => new SelectListItem
                        {
                            Text = c.name,
                            Value = c.id.Replace("en:", " ").Trim().ToString()
                        }).ToList();

                        return foodItemCatgoriesList.Where(x => !string.IsNullOrWhiteSpace(x.Text)).ToList();
                    }
                    catch (Exception ex)
                    {
                        return new List<SelectListItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
            #endregion 
        }
        public static List<SelectListItem> GetFoodItemList(string categoryId)
        {
            #region Configuration
            string endPointUrl = "https://uk.openfoodfacts.org/category/" + categoryId + ".json";
            #endregion


            #region Bind food item list using open food api
            try
            {
                using (var wc = new WebClient())
                {

                    try  //Add  
                    {
                        OpenFoodApiModel apimodel = new OpenFoodApiModel();
                        var result = wc.DownloadData(endPointUrl);
                        var test = Encoding.UTF8.GetString(result);
                        apimodel = JsonConvert.DeserializeObject<OpenFoodApiModel>(test);
                        var foodItemList = apimodel.products.Select(c => new SelectListItem
                        {
                            Text = c.product_name,
                            Value = c._id.ToString()
                        }).ToList();

                        return foodItemList.Where(x => !string.IsNullOrWhiteSpace(x.Text)).ToList();
                    }
                    catch (Exception ex)
                    {
                        return new List<SelectListItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
            #endregion 
        }

        public static List<SelectListItem> GetFoodItemAllergyList(string foodItemId)
        {
            #region Configuration
            string endPointUrl = "https://uk.openfoodfacts.org/api/v0/product/" + foodItemId + ".json";
            #endregion


            #region Bind food item list using open food api
            try
            {
                using (var wc = new WebClient())
                {

                    try
                    {
                        OpenFoodApiAllergyModel apimodel = new OpenFoodApiAllergyModel();
                        var result = wc.DownloadData(endPointUrl);
                        var encodedList = Encoding.UTF8.GetString(result);
                        apimodel = JsonConvert.DeserializeObject<OpenFoodApiAllergyModel>(encodedList);
                        var foodItemCatgoriesList = apimodel.product.Select(c => new SelectListItem
                        {
                            Text = c.product_name,
                            Value = c._id.Replace("en:", " ").Trim().ToString()
                        }).ToList();

                        return foodItemCatgoriesList.Where(x => !string.IsNullOrWhiteSpace(x.Text)).ToList();
                    }
                    catch (Exception ex)
                    {
                        return new List<SelectListItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
            #endregion 
        }
    }
}
