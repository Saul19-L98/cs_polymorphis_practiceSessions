var number = new List<int> { 1,4,6,-1,12,44,-8,-19 };

bool shallAddPositiveOnly = false;
int sum;
if (shallAddPositiveOnly)
{
    sum = new PositiveNumbersSumCalculator().Calculate(number);
}
else
{
    sum = new NumbersSumCalculator().Calculate(number);
}
Console.WriteLine("Sum is:" + sum);
Console.ReadKey();

public class NumbersSumCalculator
{
    public int Calculate(List<int> numbers)
    {
        int sum = 0;
        foreach(var number in numbers)
        {
            if (shallBeAdded(number))
            {
                sum += number;
            }
        }
        return sum;
    }

    protected virtual bool shallBeAdded(int number)
    {
        return true;
    }
}

public class PositiveNumbersSumCalculator : NumbersSumCalculator
{
    protected override bool shallBeAdded(int number)
    {
        return number > 0;
    }
    //public int Calculate(List<int> numbers)
    //{
    //    int sum = 0;
    //    foreach (var number in numbers)
    //    {
    //        if(number > 0){
    //            sum += number;
    //        }
    //    }
    //    return sum;
    //}
}