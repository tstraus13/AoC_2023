using System.Numerics;

const string FILE_NAME = "data.txt";

// Part 2
var hands = new List<Hand>();
var lines = ReadData();
var sum = 0;

foreach (var line in lines)
    hands.Add(new Hand(line));

hands.Sort();

for (int i = 0; i < hands.Count; i++)
{
    sum += (hands[i].Bid * (i + 1));
}

Console.WriteLine($"Total Winnings is {sum}");

string[] ReadData()
{
    var lines = File.ReadAllLines(FILE_NAME);

    return lines;
}


struct Hand : IComparable<Hand>
{
    private const int PAIR_MULTIPLIER = 100;
    
    public int Bid { get; private set; }
    public Card[] Cards { get; private set; }
    
    public Hand(string handAndBidLine)
    {
        var split = handAndBidLine.Split(" ");
        var hand = split[0];
        var bid = split[1];
        
        Cards = hand.Select(c => new Card(c)).ToArray();
        Bid = int.Parse(bid);
    }

    public BigInteger HandValue()
    {
        var duplicates = Cards
            .GroupBy(c => c.Type, c => c)
            .ToArray();
        
        switch (duplicates.Count())
        {
            case 1:
                return BigInteger.Pow(PAIR_MULTIPLIER, 5);
            case 2:
                if (duplicates.Any(d => d.Count() == 4) && duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 5);
                if (duplicates.Any(d => d.Count() == 4))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 4);
                if (duplicates.Any(d => d.Count() == 3) && duplicates.Any(d => d.Count() == 2) && duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 5);
                return BigInteger.Pow(PAIR_MULTIPLIER, 3) + BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 3:
                if (duplicates.Any(d => d.Count() == 3) && duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 4);
                if (duplicates.Any(d => d.Count() == 3))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 3);
                if (duplicates.Any(d => d.Count() == 2 && d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 4);
                if (duplicates.Any(d => d.Count() == 2) && duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 3) + BigInteger.Pow(PAIR_MULTIPLIER, 2);
                return BigInteger.Pow(PAIR_MULTIPLIER, 2) + BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 4:
                if (duplicates.Any(d => d.Count() == 2) && duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 3);
                return BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 5:
                if (duplicates.Any(d => d.Key == CardTypes.Joker))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 2);
                return PAIR_MULTIPLIER;
            default:
                throw new Exception("Invalid Hand!");
        }
    }
    
    public int CompareTo(Hand other)
    {
        var a = HandValue();
        var b = other.HandValue();

        if (a > b)
            return 1;
        if (a < b)
            return -1;

        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].Value > other.Cards[i].Value)
                return 1;
            if (Cards[i].Value < other.Cards[i].Value)
                return -1;
        }

        return 0;
    }
}

struct Card : IComparable<Card>
{
    public CardTypes Type { get; private set; }
    public int Value { get; private set; }
    
    public Card(char cardChar)
    {
        switch (cardChar)
        {
            case 'J':
                Type = CardTypes.Joker;
                Value = 1;
                break;
            case '2':
                Type = CardTypes.Two;
                Value = 2;
                break;
            case '3':
                Type = CardTypes.Three;
                Value = 3;
                break;
            case '4':
                Type = CardTypes.Four;
                Value = 4;
                break;
            case '5':
                Type = CardTypes.Five;
                Value = 5;
                break;
            case '6':
                Type = CardTypes.Six;
                Value = 6;
                break;
            case '7':
                Type = CardTypes.Seven;
                Value = 7;
                break;
            case '8':
                Type = CardTypes.Eight;
                Value = 8;
                break;
            case '9':
                Type = CardTypes.Nine;
                Value = 9;
                break;
            case 'T':
                Type = CardTypes.Ten;
                Value = 10;
                break;
            case 'Q':
                Type = CardTypes.Queen;
                Value = 11;
                break;
            case 'K':
                Type = CardTypes.King;
                Value = 12;
                break;
            case 'A':
                Type = CardTypes.Ace;
                Value = 13;
                break;
            default:
                throw new Exception("Invalid Card!");
        }
    }
    
    public int CompareTo(Card other)
    {
        return Value.CompareTo(other.Value);
    }
}

enum CardTypes
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Joker,
    Queen,
    King,
    Ace
}

/*
// Part 1
var hands = new List<Hand>();
var lines = ReadData();
var sum = 0;

foreach (var line in lines)
    hands.Add(new Hand(line));

hands.Sort();

for (int i = 0; i < hands.Count; i++)
{
    sum += (hands[i].Bid * (i + 1));
}

Console.WriteLine($"Total Winnings is {sum}");

string[] ReadData()
{
    var lines = File.ReadAllLines(FILE_NAME);

    return lines;
}


struct Hand : IComparable<Hand>
{
    private const int PAIR_MULTIPLIER = 100;

    public int Bid { get; private set; }
    public Card[] Cards { get; private set; }

    public Hand(string handAndBidLine)
    {
        var split = handAndBidLine.Split(" ");
        var hand = split[0];
        var bid = split[1];

        Cards = hand.Select(c => new Card(c)).ToArray();
        Bid = int.Parse(bid);
    }

    public BigInteger HandValue()
    {
        var duplicates = Cards
            .GroupBy(c => c, c => c.Type)
            .ToArray();

        switch (duplicates.Count())
        {
            case 1:
                return BigInteger.Pow(PAIR_MULTIPLIER, 5);
            case 2:
                if (duplicates.Any(d => d.Count() == 4))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 4);
                return BigInteger.Pow(PAIR_MULTIPLIER, 3) + BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 3:
                if (duplicates.Any(d => d.Count() == 3))
                    return BigInteger.Pow(PAIR_MULTIPLIER, 3);
                return BigInteger.Pow(PAIR_MULTIPLIER, 2) + BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 4:
                return BigInteger.Pow(PAIR_MULTIPLIER, 2);
            case 5:
                return PAIR_MULTIPLIER;
            default:
                throw new Exception("Invalid Hand!");
        }
    }

    public int CompareTo(Hand other)
    {
        var a = HandValue();
        var b = other.HandValue();

        if (a > b)
            return 1;
        if (a < b)
            return -1;

        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].Value > other.Cards[i].Value)
                return 1;
            if (Cards[i].Value < other.Cards[i].Value)
                return -1;
        }

        return 0;
    }
}

struct Card : IComparable<Card>
{
    public CardTypes Type { get; private set; }
    public int Value { get; private set; }

    public Card(char cardChar)
    {
        switch (cardChar)
        {
            case '2':
                Type = CardTypes.Two;
                Value = 2;
                break;
            case '3':
                Type = CardTypes.Three;
                Value = 3;
                break;
            case '4':
                Type = CardTypes.Four;
                Value = 4;
                break;
            case '5':
                Type = CardTypes.Five;
                Value = 5;
                break;
            case '6':
                Type = CardTypes.Six;
                Value = 6;
                break;
            case '7':
                Type = CardTypes.Seven;
                Value = 8;
                break;
            case '8':
                Type = CardTypes.Eight;
                Value = 9;
                break;
            case '9':
                Type = CardTypes.Nine;
                Value = 10;
                break;
            case 'T':
                Type = CardTypes.Ten;
                Value = 11;
                break;
            case 'J':
                Type = CardTypes.Jack;
                Value = 12;
                break;
            case 'Q':
                Type = CardTypes.Queen;
                Value = 13;
                break;
            case 'K':
                Type = CardTypes.King;
                Value = 14;
                break;
            case 'A':
                Type = CardTypes.Ace;
                Value = 15;
                break;
            default:
                throw new Exception("Invalid Card!");
        }
    }

    public int CompareTo(Card other)
    {
        return Value.CompareTo(other.Value);
    }
}

enum CardTypes
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}
*/