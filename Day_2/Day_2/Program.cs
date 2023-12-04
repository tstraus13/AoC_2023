var possibleGameIdSum = 0;
var colorPowerSum = 0;
var lines = ReadDataFile();

foreach (var line in lines)
{
    var gameSplit = line.Split(":");
    var gameId = int.Parse(gameSplit[0].Split(" ")[1]);
    var gamePossible = true;
    
    var redMax = 0;
    var greenMax = 0;
    var blueMax = 0;

    foreach (var roll in gameSplit[1].Split(";"))
    {
        foreach (var result in roll.Split(","))
        {
            var items = result.Trim().Split(" ");
            var count = int.Parse(items[0]);
            var color = items[1].ToLower();

            switch (color)
            {
                case "red":
                    redMax = int.Max(redMax, count);
                    break;
                case "green":
                    greenMax = int.Max(greenMax, count);
                    break;
                case "blue":
                    blueMax = int.Max(blueMax, count);
                    break;
                default:
                    break;
            }

            if (count > ColorMax(color))
                gamePossible = false;
        }
    }

    colorPowerSum = colorPowerSum + (redMax * greenMax * blueMax);

    if (gamePossible)
        possibleGameIdSum = possibleGameIdSum + gameId;
}

Console.WriteLine($"Sum of Possible Games is {possibleGameIdSum}");
Console.WriteLine($"Sum of Color Power is {colorPowerSum}");

int ColorMax(string color)
{
    switch (color.Trim().ToLower())
    {
        case "red":
            return 12;
        case "green":
            return 13;
        case "blue":
            return 14;
        default:
            return -1;
    }
}

List<string> ReadDataFile()
{
    var lines = File.ReadLines("data.txt");

    return lines.ToList();
}