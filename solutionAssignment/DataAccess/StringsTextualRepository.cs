namespace solutionAssignment.DataAccess;

public class StringsTextualRepository : StringsRepository
{
    private static readonly string Separator = Environment.NewLine;
    protected override string? StringsToText(List<string> allRecipes)
    {
        return string.Join(Separator, allRecipes);
    }

    protected override List<string> TextToStrings(string fileContents)
    {
        return fileContents.Split(Separator).ToList();
    }
}
