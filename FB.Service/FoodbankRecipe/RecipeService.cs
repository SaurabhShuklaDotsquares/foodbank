using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class RecipeService : IRecipeService
    {
        private IRepository<FoodbankRecipe> repoRecipe;
        private IRepository<FoodbankRecipeFoodItem> repoFoodbankRecipeFoodItem;
        private readonly IFoodService foodService;
        public RecipeService(IRepository<FoodbankRecipe> _repoRecipe, IFoodService _foodService, IRepository<FoodbankRecipeFoodItem> _repoFoodbankRecipeFoodItem)
        {
            repoRecipe = _repoRecipe; 
            foodService = _foodService;
            repoFoodbankRecipeFoodItem = _repoFoodbankRecipeFoodItem;
        }

        public bool Save(FoodbankRecipe foodbankRecipe)
        {
            try
            {
                if (foodbankRecipe.Id > 0)
                {
                    repoRecipe.Update(foodbankRecipe);
                    repoRecipe.SaveChanges();
                    return true;
                }
                else
                {
                    repoRecipe.Insert(foodbankRecipe);
                    repoRecipe.SaveChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        public List<FoodbankRecipe> GetRecipeList(int foodbankId)
        {
            return repoRecipe.Query().Filter(x => x.FoodbankId == foodbankId && x.IsDelete==false).Get().ToList();
        }

        public FoodbankRecipe GetRecipeById(int Id)
        {
            return repoRecipe.Query().Include(x=>x.FoodbankRecipeFoodItem).Filter(x => x.Id == Id).Get().FirstOrDefault();
        }
        public void DeleteRecipeFoodItem(int id)
        {
            repoFoodbankRecipeFoodItem.Delete(id);
        }
        public void DeleteRecipe(int id)
        {
            repoRecipe.Delete(id);
        }
        public FoodbankRecipeFoodItem GetRecipeFoodItem(int id)
        {
            return repoFoodbankRecipeFoodItem.Query().Filter(x => x.Id == id).Get().FirstOrDefault();
        }
        public KeyValuePair<int, List<FoodbankRecipeFoodItem>> GetRecipeFoodItemList(DataTableServerSide searchModel, int RecipeTypeId)
        {
            var predicate = PredicateBuilder.True<FoodbankRecipeFoodItem>();
            predicate = CustomPredicate.BuildPredicate<FoodbankRecipeFoodItem>(searchModel, new Type[] { typeof(FoodbankRecipeFoodItem) });
            predicate = predicate.And(m => m.FoodbankRecipeId == RecipeTypeId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var foodItemList = repoFoodbankRecipeFoodItem
            .Query()
            .Include(x => x.Food)
            .Include(x => x.Food.Stock)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(FoodbankRecipeFoodItem), typeof(Food) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            KeyValuePair<int, List<FoodbankRecipeFoodItem>> resultResponse = new KeyValuePair<int, List<FoodbankRecipeFoodItem>>(totalCount, foodItemList);

            return resultResponse;
        }
        public bool SaveRecipeFoodItem(FoodbankRecipeFoodItem parcelFoodItem)
        {
            try
            {
                if (parcelFoodItem.Id > 0)
                {
                    repoFoodbankRecipeFoodItem.Update(parcelFoodItem);
                    return true;
                }
                else
                {
                    repoFoodbankRecipeFoodItem.Insert(parcelFoodItem);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<FoodbankRecipeFoodItem> GetRecipeFoodItemById(int ReceipeId)
        {
            return repoFoodbankRecipeFoodItem.Query().Filter(x => x.FoodbankRecipeId == ReceipeId).Include(x => x.Food).Get().ToList();
        }
        public KeyValuePair<int, List<FoodbankRecipeDto>> GetRecipeList(DataTableServerSide searchModel)
        {
            var predicate = PredicateBuilder.True<FoodbankRecipe>();
            predicate = CustomPredicate.BuildPredicate<FoodbankRecipe>(searchModel, new Type[] { typeof(FoodbankRecipe), typeof(Foodbank),typeof(Food) });
            predicate=predicate.And(x => x.IsDelete == false);
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var recipeList = repoRecipe
                .Query().Include(x=>x.FoodbankRecipeFoodItem)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(FoodbankRecipe), typeof(Foodbank), typeof(Food) } ))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = FoodbankRecipeDtoMapper.MapList(recipeList, foodService.GetAllFoodList());

            KeyValuePair<int, List<FoodbankRecipeDto>> resultResponse = new KeyValuePair<int, List<FoodbankRecipeDto>>(totalCount, results);

            return resultResponse;

        }

        public void Dispose()
        {
            if (repoRecipe != null)
            {
                repoRecipe.Dispose();
                repoRecipe = null;
            }
            if (foodService != null)
            {
                foodService.Dispose();
                
            }

        }

    }
}
