const char EMPTY_SPACE = '.';

var lines = ReadData();
var numberMap = ParseNumberPositions(lines);

var sum = 0;
var ratio = 0;

for (int y = 0; y < lines.Count; y++)
{
	for (int x = 0; x < lines[y].Length; x++)
	{
		var chr = lines[y][x];

		if (chr != EMPTY_SPACE && !char.IsDigit(chr))
		{
			var adjacentValues = numberMap
				.Where(
					np =>
						(np.Y == y || np.Y == (y - 1) || np.Y == (y + 1))
						&&
						((np.X[0] <= x && np.X[1] > x)
						 || np.X[1] == x
						 || np.X[0] == (x + 1))
				).ToList();

			sum += adjacentValues.Sum(a => a.Value);

			if (chr == '*' && adjacentValues.Count() == 2)
				ratio += (adjacentValues[0].Value * adjacentValues[1].Value);
		}
	}
}

Console.WriteLine($"Sum of Adjacent Values is {sum}");
Console.WriteLine($"Gear Ratio is {ratio}");


List<string> ReadData()
{
	var result = File.ReadAllLines("data.txt");

	return result.ToList();
}

List<NumberPosition> ParseNumberPositions(List<string> lines)
{
	var result = new List<NumberPosition>();
	var digits = new List<char>();

	for (int y = 0; y < lines.Count; y++)
	{
		if (string.IsNullOrEmpty(lines[y]))
			continue;

		for (int x = 0; x < lines[y].Length; x++)
		{
			if (lines[y][x] == EMPTY_SPACE || !char.IsDigit(lines[y][x]))
			{
				if (digits.Any())
				{
					result.Add(new NumberPosition(new[] { x - digits.Count, x }, y, int.Parse(new string(digits.ToArray()))));
					digits.Clear();
				}
			}

			else if (char.IsDigit(lines[y][x]))
			{
				digits.Add(lines[y][x]);
			}
		}

		if (digits.Any())
			result.Add(new NumberPosition(new[] { lines[y].Length - digits.Count, lines[y].Length }, y, int.Parse(new string(digits.ToArray()))));
		
		digits.Clear();
	}
	
	return result;
}

struct NumberPosition
{
	public NumberPosition(int[] x, int y, int value)
	{
		X = x;
		Y = y;
		Value = value;
	}

	public int[] X { get; }

	public int Y { get; }

	public int Value { get; }
}
