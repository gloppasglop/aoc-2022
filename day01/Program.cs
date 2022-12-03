using System;
using System.IO;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;


int answerPart1(IEnumerable<string> input) {
	int maxCalories = 0;
	int calories = 0;
	int elfCalories = 0;

	foreach( string line in input) {
		// Check if empty line
		if ( line == "" ) {
			// New Elf
			if (elfCalories > maxCalories) {
				maxCalories = elfCalories;
			}
			elfCalories = 0;
		} else {
			// Parse line as int
			if (int.TryParse(line, out calories)) {
				elfCalories += calories;
			}
		}
	}

	return maxCalories;
}


int answerPart2(IEnumerable<string> input) {
	int result = 0;
	int calories = 0;
	int elfCalories = 0;
	List<int> elves = new List<int>();

	int index = 0;
	elves.Add(0);
	foreach( string line in input) {
		// Check if empty line
		if ( line == "" ) {
			// New Elf
			elves.Add(0);
			index++;
		} else {
			// Parse line as int
			if (int.TryParse(line, out calories)) {
				elves[index] += calories;
			}
		}
	}
	
	elves.Sort();
	elves.Reverse();
	result = elves[0]+elves[1]+elves[2];
	return result;
}

pathTest = @"test_part1.txt";
pathInput = @"input_part1.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 1 - Test  : {answerPart1(contentTest)}");
Console.WriteLine($"Part 1 - Input : {answerPart1(contentInput)}");

pathTest = @"test_part2.txt";
pathInput = @"input_part2.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 2 - Test  : {answerPart2(contentTest)}");
Console.WriteLine($"Part 2 - Input : {answerPart2(contentInput)}");

