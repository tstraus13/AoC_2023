var numberStrings = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "zero" };

var lines = ReadFile();
var sum = ParseData(lines);

Console.WriteLine($"Sum is {sum}");

int ParseData(List<string> lines)
{
    var sum = 0;

    foreach (var line in lines)
    {
        var digitFirst = -1;
        var digitFirstPosition = -1;
        var digitLast = -1;
        var digitLastPosition = -1;

        var textFirst = -1;
        var textFirstPosition = -1;
        var textLast = -1;
        var textLastPosition = -1;

        for (int position = 0; position < line.Length; position++)
        {
            var c = line[position];
            
            if (char.IsDigit(c))
            {
                if (digitFirst < 0)
                {
                    digitFirst = c - '0';
                    digitFirstPosition = position;
                    digitLast = digitFirst;
                    digitLastPosition = position;
                }
                else
                {
                    digitLast = c - '0';
                    digitLastPosition = position;
                }
            }
        }

        if (numberStrings.Any(n => line.Contains(n)))
        {
            foreach (var numberString in numberStrings)
            {
                if (!line.Contains(numberString))
                    continue;

                var firstPosition = line.IndexOf(numberString);
                var lastPosition = line.LastIndexOf(numberString);
                
                if (textFirst < 0 || firstPosition < textFirstPosition)
                {
                    textFirst = NumberStringToInt(numberString);
                    textFirstPosition = line.IndexOf(numberString);
                }

                if (textLast < 0 || lastPosition > textLastPosition)
                {
                    textLast = NumberStringToInt(numberString);
                    textLastPosition = line.LastIndexOf(numberString);
                }
            }
        }

        int first = 0;
        int last = 0;

        if (digitFirstPosition < 0)
            digitFirstPosition = textFirstPosition + 1;
        else if (textFirstPosition < 0)
            textFirstPosition = digitFirstPosition + 1;

        if (digitLastPosition < 0)
            digitLastPosition = textLastPosition - 1;
        else if (textLastPosition < 0)
            textLastPosition = digitLastPosition - 1;
        
        if (digitFirstPosition < textFirstPosition)
            first = digitFirst;
        else
            first = textFirst;

        if (digitLastPosition > textLastPosition)
            last = digitLast;
        else
            last = textLast;

        var lineNumber = (first * 10) + last;
        sum = sum + lineNumber;
    }
    
    return sum;
}    

List<string> ReadFile()
{
    var lines = File.ReadAllLines("data.txt");

    return lines.ToList();
}

int NumberStringToInt(string number)
{
    switch (number.ToLower())
    {
        case "zero":
            return 0;
        case "one":
            return 1;
        case "two":
            return 2;
        case "three":
            return 3;
        case "four":
            return 4;
        case "five":
            return 5;
        case "six":
            return 6;
        case "seven":
            return 7;
        case "eight":
            return 8;
        case "nine":
            return 9;
        default:
            return -1;
    }
}