using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.ModalMapper
{
    public class FoodbankRecipeDtoMapper
    {
        public static List<FoodbankRecipeDto> MapList(List<FoodbankRecipe> recipeListItems, List<Food> foodListItems)
        {
            var recipeList = new List<FoodbankRecipeDto>();
            foreach (var item in recipeListItems)
            {
                recipeList.Add(new FoodbankRecipeDto
                {
                    Id = item.Id,
                    FoodbankId = item.FoodbankId,
                    RecipeTitle = item.RecipeTitle,
                    Ingredients = item.Ingredients,
                    RecipeNumber = item.RecipeNumber,
                   Ingredientsids = GetFoodName(item.FoodbankRecipeFoodItem.ToList(), foodListItems),
                }); 
            }
            return recipeList;
        }
        public static string GetFoodName(List<FoodbankRecipeFoodItem> Ingredients, List<Food> foodListItems)
        {
            if(Ingredients==null)
            {
                return "";
            }
            var foodarray = Ingredients.Select(x => (x.FoodId ?? 0).ToString()).ToArray();
            var returnobj = "";
            var fooditem= foodListItems.Where(m => foodarray.Select(x => x).Contains(m.Id.ToString())).ToList();
            for (int j = 0; j < fooditem.Count; j++)
            {
                if (j == fooditem.Count - 1)
                { returnobj = returnobj + fooditem[j].Name; }
                else
                { returnobj = returnobj + fooditem[j].Name+", "; }
            }
            return returnobj;
        }
    }
}
