
const string FILE = "data.txt";

var lines = await ReadData();
var times = lines[0]
    .Split(":")[1]
    .Trim()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(s => int.Parse(s))
    .ToList();
var distances = lines[1]
    .Split(":")[1]
    .Trim()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(s => int.Parse(s))
    .ToList();

//var winningOptions = new int[times.Count];

// for (int i = 0; i < times.Count; i++)
// {
//     var time = times[i];
//     var recordDistance = distances[i];
//     var lastDistance = Int32.MinValue;
//     var winningOption = 0;
//
//     for (int j = 2; j < time; j++)
//     {
//         var remaingingTime = time - j;
//         var distance = j * remaingingTime;
//
//         if (distance < lastDistance && distance < recordDistance)
//             break;
//         
//         if (distance > recordDistance)
//             winningOption++;
//             
//         lastDistance = distance;
//     }
//
//     winningOptions[i] = winningOption;
// }

var winningOptions = 0;

ulong recordDistance = 298118510661181;
ulong time = 49787980;
var lastDistance = Int128.MinValue;

for (ulong j = 2; j < time; j++)
{
    var remaingingTime = time - j;
    ulong distance = j * remaingingTime;

    if (distance < lastDistance && distance < recordDistance)
        break;
    
    if (distance > recordDistance)
        winningOptions++;
        
    lastDistance = distance;
    //Console.WriteLine($"DISTANCE: {distance}\t\tRECORD_DISTANCE: {recordDistance}");
}


Console.WriteLine(winningOptions);

async Task<List<string>> ReadData()
{
    var lines = await File.ReadAllLinesAsync(FILE);

    return lines.ToList();
}