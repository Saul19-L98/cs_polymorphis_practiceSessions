List<string> strings = new List<string> { "bobcat", "wolverine", "grizzly" };

var test = new Exercise();
var testing = test.ProcessAll(strings);

foreach(var item in testing)
{
    Console.WriteLine(item);
}

Console.ReadKey();
public class Exercise
{
    public List<string> ProcessAll(List<string> words)
    {
        var stringsProcessors = new List<StringsProcessor>
                {
                    new StringsTrimmingProcessor(),
                    new StringsUppercaseProcessor()
                };

        List<string> result = words;
        foreach (var stringsProcessor in stringsProcessors)
        {
            result = stringsProcessor.Process(result);
        }
        return result;
    }
}

public  class StringsProcessor
{
    public virtual List<string> Process(List<string> words)
    {
        List<string> result = words;
        return result;
    }
}
public class StringsTrimmingProcessor : StringsProcessor
{
    public override List<string> Process(List<string> words)
    {
        var result = new List<string>();
        foreach (var word in words)
        {
            double middleLength = (double)word.Length / 2;
            int middleIndex = (int)Math.Floor(middleLength);
            result.Add(word.Substring(0,middleIndex));
        }
        return result;
    }
}
public class StringsUppercaseProcessor : StringsProcessor
{
    public override List<string> Process(List<string> words)
    {
        var result = new List<string>();
        foreach (var word in words)
        {
            string upperWord = word.ToUpper();
            result.Add(upperWord);
        }
        return result;
    }
}