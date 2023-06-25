using solutionAssignment.App;
using solutionAssignment.DataAccess;
using solutionAssignment.FileAccess;
using solutionAssignment.Recipes;
using solutionAssignment.Recipes.Ingredients;


const FileFormat Format = FileFormat.Json;

IStringsRepository stringsRepository = Format == FileFormat.Json ? new StringsJsonRepository() : new StringsTextualRepository();

const string FileName = "recipes";
var fileMetadata = new FileMetadata(FileName, Format);

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(stringsRepository, ingredientsRegister),
    new RecipesConsoleUserInteraction(ingredientsRegister));
cookiesRecipesApp.Run(fileMetadata.ToPath());

