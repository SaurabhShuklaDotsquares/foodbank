using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class RecipeController : BaseController
    {
        private readonly IFoodService foodService;
        private readonly IFoodbankService foodbankService;
        private readonly IRecipeService recipeService;
        private readonly IFamilyParcelService familyParcelService;
        public RecipeController(IFoodService _foodService, IFoodbankService _foodbankService, IRecipeService _recipeService, IFamilyParcelService _familyParcelService)
        {
            foodService = _foodService;
            foodbankService = _foodbankService;
            recipeService = _recipeService;
            familyParcelService = _familyParcelService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddRecipe(int Id = 0)
        {
            FoodbankRecipeDto model = new FoodbankRecipeDto();
            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var listItems = foodService.GetAllFoodListForParcel(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            listItems.Insert(0, selectvaule);
            ViewBag.FoodItemList = listItems;

            if (Id > 0)
            {
                var recipe = recipeService.GetRecipeById(Id);
                if(recipe != null)
                {
                    model.Id = recipe.Id;
                    model.FoodbankId = recipe.FoodbankId;
                    model.RecipeTitle = recipe.RecipeTitle;
                    model.Quntity = recipe.Quntity;
                    model.IsRecipeNeedToCook = recipe.IsRecipeNeedToCook == true ? true : false;
                    model.CookingInstructions = recipe.CookingInstructions;
                    model.RecipeNumber = recipe.RecipeNumber;
                    
                    ViewBag.FoodItemList = foodService.GetAllFoodList().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = (recipe.Ingredients != null && (recipe.Ingredients.Split(',').Where(x => !string.IsNullOrEmpty(x))?.Select(x => Convert.ToInt32(x)).ToArray() ?? null).Contains(x.Id)) ? true : false
                    }).ToList();
                }
            }
            else
            {
               
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddRecipe(FoodbankRecipeDto model)
        {
            if (ModelState.IsValid)
            {
         
                var obj = recipeService.GetRecipeFoodItemById(model.Id);
                FoodbankRecipeFoodItem parcelFoodItem = !obj.Select(x => x.FoodId).Contains(model.FoodItemId) ? new FoodbankRecipeFoodItem() : null;
                if (parcelFoodItem != null)
                {
                    if (model.Id == 0)
                    {
                        FoodbankRecipe entity = new FoodbankRecipe();
                        entity.Id = model.Id;
                        entity.FoodbankId = CurrentUser.FoodbankId;
                        entity.RecipeTitle = model.RecipeTitle;
                    
                        entity.IsRecipeNeedToCook = model.IsRecipeNeedToCook;
                        entity.CookingInstructions = model.CookingInstructions;
                        entity.RecipeNumber = Extensions.RandomString(8);
                        entity.AddedDate = DateTime.Now;
                        entity.IsDelete = false;

                        parcelFoodItem.FoodId = model.FoodItemId;
                        parcelFoodItem.Quantity = Convert.ToInt32(model.Quantity);
                        entity.FoodbankRecipeFoodItem.Add(parcelFoodItem);
                        bool result = recipeService.Save(entity);
                        if (result)
                        {
                            ShowSuccessMessage("Success!", "Record has been added successfully.", false);
                        }
                        else
                        {
                            ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                        }
                        return RedirectToAction("AddRecipe", "Recipe", new { id = model.Id });
                    }
                    else
                    {
                        FoodbankRecipe entity =  recipeService.GetRecipeById(model.Id);
                        entity.IsRecipeNeedToCook = model.IsRecipeNeedToCook;
                        entity.CookingInstructions = model.CookingInstructions;
                        recipeService.Save(entity);

                        parcelFoodItem.FoodId = model.FoodItemId;
                        parcelFoodItem.FoodbankRecipeId = model.Id;
                        parcelFoodItem.Quantity = Convert.ToInt32(model.Quantity);
                        bool result = recipeService.SaveRecipeFoodItem(parcelFoodItem);
                        if (result)
                        {
                            ShowSuccessMessage("Success!", "Record has been added successfully.", false);
                        }
                        else
                        {
                            ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                        }
                        return RedirectToAction("AddRecipe", "Recipe", new { id = model.Id });
                    }
                }
                else
                {
                    ShowErrorMessage("Error!", "Food Item is already exists. Please add another food item or delete already exist item.", false);
                    return RedirectToAction("AddRecipe", "Recipe", new { id = model.Id });
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult RecipeList(DataTableServerSide model)
        {
            KeyValuePair<int, List<FoodbankRecipeDto>> recipelist = new KeyValuePair<int, List<FoodbankRecipeDto>>();
            recipelist = recipeService.GetRecipeList(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = recipelist.Key,
                recordsFiltered = recipelist.Key,
                data = recipelist.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.RecipeTitle+" ["+c.RecipeNumber+']',
                    c.Ingredientsids,
                    "<a href=" + Url.Action("RecipeDetail", "Recipe", new { Id = c.Id })
                    + " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a><a href=" + Url.Action("AddRecipe", "Recipe", new { Id = c.Id })
                    + " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a><a  data-toggle='modal' data-target='#modal-delete-recipe' href=" + Url.Action("DeleteRecipe", "Recipe", new { Id = c.Id })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                   })
            });
        }

        public IActionResult RecipeDetail(int Id)
        {
            FoodbankRecipeDto model = new FoodbankRecipeDto();

            if (Id > 0)
            {
                var recipe = recipeService.GetRecipeById(Id);
                if (recipe != null)
                {
                    model.Id = recipe.Id;
                    model.FoodbankId = recipe.FoodbankId;
                    model.RecipeTitle = recipe.RecipeTitle;
                    model.Quntity = recipe.FoodbankRecipeFoodItem.Count;
                    model.IsRecipeNeedToCook = recipe.IsRecipeNeedToCook == true ? true : false;
                    model.CookingInstructions = recipe.CookingInstructions;
                    model.RecipeNumber = recipe.RecipeNumber;
                    //ViewBag.FoodItemList = foodService.GetAllFoodList().Select(x => new SelectListItem
                    //{
                    //    Text = x.Name,
                    //    Value = x.Id.ToString(),
                    //    Selected = (recipe.Ingredients != null && (recipe.Ingredients.Split(',').Where(x => !string.IsNullOrEmpty(x))?.Select(x => Convert.ToInt32(x)).ToArray() ?? null).Contains(x.Id)) ? true : false
                    //}).ToList();
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteRecipe(int Id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Recipe?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Recipe" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string DeleteRecipe(int Id, IFormCollection Fc)
        {
            string message = string.Empty;
            try
            {
                if (Id > 0)
                {
                    var recipe = recipeService.GetRecipeById(Id);
                    if (recipe != null)
                    {
                        var parcel = familyParcelService.GetParcelsByRecipeid(Id);
                        if (parcel.Count > 0)
                        {
                            return   message = "This recipe associated with parcel, they can not delete";

                        }
                        recipe.IsDelete = true;
                        foreach (var item in recipe.FoodbankRecipeFoodItem.ToList())
                        {
                            recipeService.DeleteRecipeFoodItem(item.Id);
                        }
                        recipeService.DeleteRecipe(Id);
                        //if (recipeService.Save(recipe))
                        //{
                        //    message = "Success";
                        //   // ShowSuccessMessage("Success!", "Recipe has been deleted successfully.", true);
                        //}
                        //else
                        //{
                        //  //  ShowSuccessMessage("Error!", message, false);
                        //    message = "Error";
                        //}
                         message = "Success";
                        return message;
                    }
                }
                
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                //ShowSuccessMessage("Error!", message, false);
            }
            return message;
        }
        [HttpGet]
        public IActionResult GetFoodItemUnit(int foodId)
        {
            var result = foodService.GetFoodUnit(foodId);
            return NewtonSoftJsonResult(new RequestOutcome<Stock> { Data = result });
        }

        [HttpPost]
        public IActionResult BindRecipeTypeFoodItemList(DataTableServerSide model, int RecipeTypeId)
        {
            KeyValuePair<int, List<FoodbankRecipeFoodItem>> parcelType = new KeyValuePair<int, List<FoodbankRecipeFoodItem>>();
            parcelType = recipeService.GetRecipeFoodItemList(model, RecipeTypeId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    $"{c.Food.Name}",
                    $"{c.Quantity} {(c.Food.Stock.Count >0 ?c.Food.Stock.FirstOrDefault().Unit:"") }",

                    "<a data-toggle='modal' data-target='#modal-delete-parceltype-fooditem' href='" + Url.Action("DeleteRecipeFoodItem", "Recipe", new { id = c.Id })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    ,
                })
            });
        }

        #region Delete ParcelType Section
        [HttpGet]
        public IActionResult DeleteRecipeFoodItem(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this food item?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Food Item" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the parcel type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteRecipeFoodItem(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var foodItem = recipeService.GetRecipeFoodItem(id);
                if (foodItem != null)
                {

                    recipeService.DeleteRecipeFoodItem(id);
                }
                
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Record has been deleted successfully." });
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

               
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = message });
            }
        }
        #endregion
    }
}
