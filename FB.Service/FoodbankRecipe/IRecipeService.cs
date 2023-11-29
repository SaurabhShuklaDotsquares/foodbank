using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IRecipeService
    {
        public bool Save(FoodbankRecipe foodbankRecipe);
        public KeyValuePair<int, List<FoodbankRecipeDto>> GetRecipeList(DataTableServerSide searchModel);
        public FoodbankRecipe GetRecipeById(int Id);
        List<FoodbankRecipe> GetRecipeList(int foodbankId);
        void DeleteRecipeFoodItem(int id);
        FoodbankRecipeFoodItem GetRecipeFoodItem(int id);
        KeyValuePair<int, List<FoodbankRecipeFoodItem>> GetRecipeFoodItemList(DataTableServerSide searchModel, int RecipeTypeId);
        List<FoodbankRecipeFoodItem> GetRecipeFoodItemById(int ReceipeId);
        bool SaveRecipeFoodItem(FoodbankRecipeFoodItem parcelFoodItem);
        void DeleteRecipe(int id);
    }
}
