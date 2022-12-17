using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using AOC;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;




List<Monkey> parseInput(IEnumerable<string> input, int worryLevelDivider) {

	List<Monkey> monkeys = new List<Monkey>();
 	string monkeyPattern = @"^Monkey (\d+):";  
 	string itemsPattern = @"^  Starting items: (.*)";  
 	string operationPattern = @"^  Operation: new = (.*) (.*) (.*)$";  
 	string testPattern = @"^  Test: divisible by (\d+)$";  
 	string truePattern = @"^    If true: throw to monkey (\d+)$";  
 	string falsePattern = @"^    If false: throw to monkey (\d+)$";  

	// Create a Regex  
	Regex rgMonkey = new Regex(monkeyPattern,RegexOptions.Compiled);
	Regex rgItems = new Regex(itemsPattern,RegexOptions.Compiled);
	Regex rgOperation = new Regex(operationPattern,RegexOptions.Compiled);
	Regex rgTest = new Regex(testPattern,RegexOptions.Compiled);
	Regex rgTrue = new Regex(truePattern,RegexOptions.Compiled);
	Regex rgFalse = new Regex(falsePattern,RegexOptions.Compiled);

	Monkey currentMonkey = new Monkey();; 
	foreach (string line in input) {
		MatchCollection matches = rgMonkey.Matches(line);
		if (matches.Count() > 0) {
			// New Monkey
			currentMonkey = new Monkey {worryLevelDivider = worryLevelDivider };
			monkeys.Add(currentMonkey);
		}

		matches = rgItems.Matches(line); 
		if (matches.Count() > 0) {
			var items = matches[0].Groups[1].Captures[0].ToString();
			foreach (string elt in items.Split(',')) {
				currentMonkey.Items.Enqueue(Int128.Parse(elt));
			}	
		}

		matches = rgOperation.Matches(line); 
		if (matches.Count() > 0) {
			var groups = matches[0].Groups;
			var operand1 = groups[1].ToString();
			var operation = groups[2].ToString();
			var operand2 = groups[3].ToString();

			currentMonkey.Operand1 = operand1;
			currentMonkey.Operand2 = operand2;
			currentMonkey.Operation = operation;
		}

		matches = rgTest.Matches(line); 
		if (matches.Count() > 0) {
			currentMonkey.Dividend = int.Parse(matches[0].Groups[1].ToString());
		}
	

		matches = rgTrue.Matches(line); 
		if (matches.Count() > 0) {
			currentMonkey.ThrowWhenTrue = int.Parse(matches[0].Groups[1].ToString());

		}

		matches = rgFalse.Matches(line); 
		if (matches.Count() > 0) {
			currentMonkey.ThrowWhenFalse = int.Parse(matches[0].Groups[1].ToString());
		}

	}

	return monkeys;


}

Int128 answerPart1(IEnumerable<string> input) {
	Int128 result = 0;

	List<Monkey> monkeys = parseInput(input,3);
	int roundCount = 20;
	Int128 ppcm = 1;
	foreach ( Monkey monkey in monkeys) {
		ppcm *= monkey.Dividend;
	}

	for (int round = 0; round < roundCount; round++ ) {
		int m = 0;
		foreach (Monkey monkey in monkeys) {
			var worryCount = monkey.Items.Count();
			for (int w = 0; w < worryCount; w++) {
				(var worry, var monkeyId) = monkey.PlayTurn();
				//Console.WriteLine($"{round} - {m} - {worry} -> {monkeyId}");
				//monkeys[monkeyId].Items.Enqueue(worry);
				monkeys[monkeyId].Items.Enqueue(worry % ppcm);
			}
			m++;
		}
	}
	
	var scores = (from monkey in monkeys select monkey.InspectCount).ToArray();
	Array.Sort(scores);
	Array.Reverse(scores);
	result = scores[0]*scores[1];
	return result;
}

Int128 answerPart2(IEnumerable<string> input) {
	Int128 result = 0;

	List<Monkey> monkeys = parseInput(input,1);
	int roundCount = 10000;
	Int128 ppcm = 1;

	foreach ( Monkey monkey in monkeys) {
		ppcm *= monkey.Dividend;
	}
	for (int round = 0; round < roundCount; round++ ) {
		int m = 0;
		foreach (Monkey monkey in monkeys) {
			var worryCount = monkey.Items.Count();
			for (int w = 0; w < worryCount; w++) {
				(var worry, var monkeyId) = monkey.PlayTurn();
				//Console.WriteLine($"{round} - {m} - {worry} -> {monkeyId}");
				monkeys[monkeyId].Items.Enqueue(worry % (ppcm) );
			}
			m++;
		}
	}
	
	var scores = (from monkey in monkeys select monkey.InspectCount).ToArray();
	Array.Sort(scores);
	Array.Reverse(scores);
	result = scores[0]*scores[1];
	
	return result;
}



pathTest = @"test.txt";
pathInput = @"input.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 1 - Test  : {answerPart1(contentTest)}");
Console.WriteLine($"Part 1 - Input : {answerPart1(contentInput)}");

contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 2 - Test  : {answerPart2(contentTest)}");
Console.WriteLine($"Part 2 - Input : {answerPart2(contentInput)}");

