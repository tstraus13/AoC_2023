
const string DATA_FILE = "data.txt";

var lines = ReadData();
var card_copies = new int[lines.Count()];
Array.Fill(card_copies, 1);

var sum = 0;
var card_sum = 0;

for (var i = 0; i < lines.Count(); i++)
{
    var score = 0;
    
    var card = lines[i].Split(":", StringSplitOptions.RemoveEmptyEntries);
    var numbers = card[1].Split("|", StringSplitOptions.RemoveEmptyEntries);
    var win_numbers = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var player_numbers = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

    var matches = player_numbers.Where(p => win_numbers.Contains(p));
    
    switch (matches.Count())
    {
        case 0:
            score = 0;
            break;
        case 1:
            score = 1;
            break;
        default:
            score = (int)Math.Pow(2, matches.Count() - 1);
            break;
    }
    
    sum += score;

    for (int j = 0; j < card_copies[i]; j++)
    {
        for (int k = 1; k <= matches.Count(); k++)
        {
            card_copies[(i + k) >= card_copies.Length ? card_copies.Length - 1 : i + k] += 1;
        }
    }

    card_sum += card_copies[i];
}

Console.WriteLine($"Total Score is {sum}");
Console.WriteLine($"Total Number of Card Copies is {card_sum}"); // 8063216

List<string> ReadData()
{
    var data = File.ReadAllLines(DATA_FILE).ToList();

    return data;
}