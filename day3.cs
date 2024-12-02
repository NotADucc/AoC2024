using System.Text.RegularExpressions;

public class Program
{
    private static void Main(string[] args)
    {
        const string file_name = @"";
        Regex rgx = new Regex("mul[(]\\d{1,3},\\d{1,3}[)]|do[(][)]|don't[(][)]", RegexOptions.Compiled);
        bool mul = true;

        var res = File.ReadAllLines(file_name)
            .Select(line =>
                rgx.Matches(line)
                    .Aggregate((0L, 0L), (curr, next) =>
                    {
                        var split = next.Value.Split(',');
                        if (split.Length == 2)
                        {
                            long res = long.Parse(split[0].Substring(4)) * int.Parse(split[1].Substring(0, split[1].Length - 1));
                            curr.Item1 += res;
                            curr.Item2 += mul ? res : 0L;
                        }
                        else
                        {
                            mul = next.Value.Equals("do()");
                        }
                        return curr;
                    })
            ).ToArray();

        Console.WriteLine($"Res 1 : {res.Sum(x => x.Item1)}");
        Console.WriteLine($"Res 2 : {res.Sum(x => x.Item2)}");
    }
}
