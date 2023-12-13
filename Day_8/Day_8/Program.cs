const string FILE_NAME = "data.txt";
const string BEGIN = "AAA";
const string END = "ZZZ";


await PartTwo();
await PartOne();

async Task PartTwo()
{
	var moves = 0;
	var lines = await ReadData();
	var nodes = new Dictionary<string, Node>();
	var directions = lines[0].ToArray();

	foreach (var line in lines.Skip(2))
	{
		var parsedLine = line.Split(" = ");
		var key = parsedLine[0];
		var node = parsedLine[1].Replace("(", "").Replace(")", "").Split(", ");
		var left = node[0];
		var right = node[1];
	
		nodes.Add(key, new Node(left, right));
	}

	TraverseNodes();

	Console.WriteLine($"Number of Moves needed is {moves}");

	void TraverseNodes()
	{
		var startingNodes = new string[nodes.Count(n => n.Key.EndsWith('A'))];
		startingNodes = nodes
			.Where(n => n.Key.EndsWith('A'))
			.Select(n => n.Key)
			.ToArray();
		
		//var nodeKey = BEGIN;
		var direction = directions[0] == 'L' ? 0 : 1;
		var tempMoves = 0;
	
		while (true)
		{
			Parallel.For(0, startingNodes.Length, i =>
			{
				startingNodes[i] = nodes[startingNodes[i]].LeftRight[direction];
			});
				
			moves++;
			tempMoves++;
		
			if (startingNodes.All(n => n.EndsWith('Z')))
				break;

			if (tempMoves >= directions.Length)
				tempMoves = 0;
			
			direction = directions[tempMoves] == 'L' ? 0 : 1;

		}
	}

	async Task<string[]> ReadData()
	{
		var lines = await File.ReadAllLinesAsync(FILE_NAME);

		return lines;
	}
}

async Task PartOne()
{
	var moves = 0;
	var lines = await ReadData();
	var nodes = new Dictionary<string, Node>();
	var directions = lines[0].ToArray();

	foreach (var line in lines.Skip(2))
	{
		var parsedLine = line.Split(" = ");
		var key = parsedLine[0];
		var node = parsedLine[1].Replace("(", "").Replace(")", "").Split(", ");
		var left = node[0];
		var right = node[1];
	
		nodes.Add(key, new Node(left, right));
	}

	TraverseNodes();

	Console.WriteLine($"Number of Moves needed is {moves}");

	void TraverseNodes()
	{
		var nodeKey = BEGIN;
		var direction = directions[0] == 'L' ? 0 : 1;
	
		while (true)
		{
			var node = nodes[nodeKey];

			moves++;
		
			if (node.LeftRight[direction] == END)
				break;

			nodeKey = node.LeftRight[direction];
			direction = directions[moves % directions.Length] == 'L' ? 0 : 1;
		}
	}

	async Task<string[]> ReadData()
	{
		var lines = await File.ReadAllLinesAsync(FILE_NAME);

		return lines;
	}
}



struct Node
{
	public Node(string left, string right)
	{
		LeftRight = new string[2] { left, right };
	}
	
	public string[] LeftRight { get; private set; }
}