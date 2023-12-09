
using System.Collections.Concurrent;

Console.WriteLine("START");

var text = await ReadData();

var seeds = text
    .Split(":")[1]
    .Trim()
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(s => long.Parse(s))
    .ToArray();

var mappingsText = text
    .Split(":");


var mappings = new Mapping[7];

var first = mappingsText[2]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[0] = new Mapping(first);

var second = mappingsText[3]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[1] = new Mapping(second);

var third = mappingsText[4]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[2] = new Mapping(third);

var fourth = mappingsText[5]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[3] = new Mapping(fourth);

var fifth = mappingsText[6]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[4] = new Mapping(fifth);

var sixth = mappingsText[7]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[5] = new Mapping(sixth);

var seventh = mappingsText[8]
    .Split($"{Environment.NewLine}{Environment.NewLine}")[0]
    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
mappings[6] = new Mapping(seventh);


var lowest = long.MinValue;
foreach (var seed in seeds)
{
    var result = mappings[6]
        .MapSource(mappings[5]
            .MapSource(mappings[4]
                .MapSource(mappings[3]
                    .MapSource(mappings[2]
                        .MapSource(mappings[1]
                            .MapSource(mappings[0]
                                .MapSource(seed)
                            )
                        )
                    )
                )
            )
        );

    if (lowest == long.MinValue || result < lowest)
        lowest = result;
}

Console.WriteLine($"Lowest Location of Seeds is {lowest}");

var values = new ConcurrentStack<long>();
var tasks = new List<Task>();
lowest = 0;

for (int i = 0; i < seeds.Length; i+=2)
{

    Action<object?> action = new Action<object?>((t) =>
    {
        var index = (int)t;
        
        for (long j = seeds[index]; j < seeds[index] + (seeds[index + 1]); j++)
        {
            var result = mappings[6]
                .MapSource(mappings[5]
                    .MapSource(mappings[4]
                        .MapSource(mappings[3]
                            .MapSource(mappings[2]
                                .MapSource(mappings[1]
                                    .MapSource(mappings[0]
                                        .MapSource(j)
                                    )
                                )
                            )
                        )
                    )
                );
    
            values.Push(result);

            if (values.Count() >= 50000)
            {
                var l = values.Min();
                values.Clear();
                values.Push(l);
            }
        }
    });
    
    tasks.Add(new TaskFactory().StartNew(action, i));

}

await Task.WhenAll(tasks);

lowest = values.Min();

Console.WriteLine($"Lowest Location of Seed Ranges is {lowest}");

Console.WriteLine("END");



async Task<string> ReadData()
{
    var data = await File.ReadAllTextAsync("data.txt");

    return data;
}

struct Mapping
{
    public Mapping(string[] maps)
    {
        Maps = new Map[maps.Length];

        for (int i = 0; i < maps.Length; i++)
        {
            var items = maps[i]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();

            Maps[i] = new Map(items);
        }
    }
    
    public Map[] Maps;

    public long MapSource(long source)
    {
        foreach (var map in Maps)
        {
            if (source >= map.SourceStart && source <= (map.SourceStart + (map.Range - 1)))
            {
                var sourceDiff = Math.Abs(map.SourceStart - source);
                return map.DestinationStart + sourceDiff;
            }
        }

        return source;
    }
}

struct Map
{
    public Map(long[] items)
    {
        DestinationStart = items[0];
        SourceStart = items[1];
        Range = items[2];
    }
    
    public long SourceStart;
    public long DestinationStart;
    public long Range;
}