﻿using solutionAssignment.DataAccess;
using solutionAssignment.Recipes.Ingredients;

namespace solutionAssignment.Recipes;

public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegister _ingredientsRegister;
    private const string Separator = ",";

    public RecipesRepository(IStringsRepository stringsRepository, IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    public List<Recipe> Read(string filePath)
    {
        //return new List<Recipe>
        //{
        //    new Recipe(new List<Ingredient>
        //    {
        //        new WheatFlour(),
        //        new Butter(),
        //        new Sugar(),
        //    }),
        //    new Recipe(new List<Ingredient>
        //    {
        //        new CocoaPowder(),
        //        new SpeltFlour(),
        //        new Cinnamon(),
        //    })
        //};
        List<string> recipesFromFile = _stringsRepository.Read(filePath);
        var recipes = new List<Recipe>();
        foreach (var recipeFromFile in recipesFromFile)
        {
            var recipe = RecipesFromFile(recipeFromFile);
            recipes.Add(recipe);
        }
        return recipes;
    }

    private Recipe RecipesFromFile(string recipeFromFile)
    {
        var textualIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();
        foreach (var textualId in textualIds)
        {
            var id = int.Parse(textualId);
            var ingredidient = _ingredientsRegister.GetById(id);
            ingredients.Add(ingredidient);
        }
        return new Recipe(ingredients);
    }

    public void Write(string filePath, List<Recipe> allRecipes)
    {
        var recipeAsStrings = new List<string>();
        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipeAsStrings.Add(string.Join(Separator, allIds));
        }
        _stringsRepository.Write(filePath, recipeAsStrings);
    }
}
