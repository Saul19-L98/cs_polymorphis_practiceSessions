using System;
using System.Collections.Generic;
using Newtonsoft.Json;

List<Ingredients> ingredients = new()
{
    new Ingredients()
    {
        Id = 1,
        Name = "Flour",
        Instruction = "This is the base of your cookie. It gives the structure to the baked goods",
    },
    new Ingredients()
    {
        Id = 2,
        Name = "Sugar",
        Instruction = " This adds sweetness and also aids in browning and the spread of the cookies",
    },
    new Ingredients()
    {
        Id = 3,
        Name = "Butter",
        Instruction = "This is the base of your cookie. It gives the structure to the baked goods.",
    },
    new Ingredients()
    {
        Id = 4,
        Name = "Eggs",
        Instruction = "They bind the ingredients together and also add moisture",
    },
    new Ingredients()
    {
        Id = 5,
        Name = "Vanilla",
        Instruction = "It enhances the flavors of the other ingredients",
    },
    new Ingredients()
    {
        Id = 6,
        Name = "Salt",
        Instruction = "It balances out the sweetness and enhances the overall flavor",
    },
    new Ingredients()
    {
        Id = 7,
        Name = "Baking Powder",
        Instruction = "They help the cookies to rise and create a certain texture",
    }
};

var recipes = new Recipe();

List<int[]> AllRecipes;

var jsonFile = new JsonFile("recipes.json");
AllRecipes = jsonFile.JsonDeserialize();

Console.WriteLine("Existing recipes are: ");
foreach(var element in AllRecipes)
{
    int index = AllRecipes.IndexOf(element);
    Console.WriteLine($"*****{index + 1}*****");
    for (int i = 0; i < element.Length; i++)
    {
        var ingredient = ingredients[element[i]];
        Console.WriteLine($"{ingredient.Name}. {ingredient.Instruction}");
    }
}

Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
foreach(var ingrediment in ingredients)
{
    Console.WriteLine($"{ingrediment.Id}. {ingrediment.Name}");
}
Tuple<bool,int> userOption;
while (true)
{

    userOption = ConsoleReader.IsAnInteger("Add an ingredient by it's Id or type anything else if finish: ",ingredients
        .Count);
    if(userOption.Item1)
    {
        recipes.AddIngredient(ingredients[userOption.Item2 - 1]);
    }
    else

    {
        break;
    }
}

int[] recipesToSave = new int[recipes.Ingredients.Count];
for(int i = 0; i < recipes.Ingredients.Count; i++)
{
    recipesToSave[i] = recipes.Ingredients[i].Id;
}

if(recipesToSave.Length > 0)
{
    AllRecipes.Add(recipesToSave);
    Console.WriteLine("Recipe Added:");
    foreach (var recipe in recipes.Ingredients)
    {
        Console.WriteLine($"{recipe.Name}. {recipe.Instruction}");
    }
}
else
{
    Console.WriteLine("No recipe added");
}

// Convert the whole list to JSON and overwrite the file
jsonFile.JsonSerialized(AllRecipes);

Console.ReadKey();

public class Ingredients
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instruction { get; set; }
}

public static class ConsoleReader
{
    public static Tuple<bool,int> IsAnInteger(string message, int numberOfRecipes)
    {
        bool validation;
        Console.WriteLine($"{message}");
        var input = Console.ReadLine();
        bool evaluationToInt = int.TryParse(input, out int userOption);
        if(evaluationToInt)
        {
            validation = userOption > 0 && userOption < numberOfRecipes;
        }
        else
        {
            validation = false;
        }
        return new Tuple<bool,int>(validation, userOption);
    }
}

public class JsonFile
{
    public string Name { get; }
    public JsonFile(string name)
    {
        Name = name;
    }
    public List<int[]> JsonDeserialize()
    {
        // If the file exists, read it and deserialize the JSON to get the current list of recipes
        if (File.Exists(Name))
        {
            string existingJson = File.ReadAllText(Name);
            return JsonConvert.DeserializeObject<List<int[]>>(existingJson);
        }
        else
        {
            // If the file doesn't exist yet, start with an empty list
            return new List<int[]>();
        }
    } 
    public void JsonSerialized(List<int[]> allRecipes)
    {
        string json = JsonConvert.SerializeObject(allRecipes, Formatting.Indented);
        File.WriteAllText(Name, json);
    }
}

public class Recipe
{
    public List<Ingredients> Ingredients { get; set; } = new List<Ingredients>();
    public void AddIngredient(Ingredients ingredient)
    {
        Ingredients.Add(ingredient);
    }
}

public class RecipeBook
{
    private List<Recipe> Recipes { get; set; } = new List<Recipe>();
    private List<int[]> AllRecipes { get; set; } = new List<int[]>();
    private JsonFile jsonFile;

    public RecipeBook(JsonFile jsonFile,List<Ingredients> ingredients)
    {
        this.jsonFile = jsonFile;
        LoadRecipes(ingredients);
    }

    public void AddRecipe(Recipe recipe, List<Ingredients> ingredients,Recipe recipes)
    {
        Tuple<bool, int> userOption;
        while (true)
        {
            userOption = ConsoleReader.IsAnInteger("Add an ingredient by it's Id or type anything else if finish: ", ingredients.Count);
            if (userOption.Item1)
            {
                recipes.AddIngredient(ingredients[userOption.Item2 - 1]);
            }
            else

            {
                break;
            }
        }
    }

    private void LoadRecipes(List<Ingredients> ingredients)
    {
        Console.WriteLine("Existing recipes are: ");
        foreach (var element in AllRecipes)
        {
            int index = AllRecipes.IndexOf(element);
            Console.WriteLine($"*****{index + 1}*****");
            for (int i = 0; i < element.Length; i++)
            {
                var ingredient = ingredients[element[i]];
                Console.WriteLine($"{ingredient.Name}. {ingredient.Instruction}");
            }
        }
    }

    private void SaveRecipes()
    {
        jsonFile.JsonSerialized(AllRecipes);
    }
}