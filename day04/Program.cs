using System;
using System.IO;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

UInt128 convertToInt(int start, int end) {
	UInt128 result = 0;
	for (int i = start-1; i< end; i++) {
		result |= ((UInt128) 1 << i) ;
	}
	return result;
}
int answerPart1(IEnumerable<string> input) {
	int result = 0;
	foreach (string line in input) {
		var first = line.Split(',')[0];
		var second = line.Split(',')[1];
		int start = 0;
		int end = 0;

		int.TryParse(first.Split('-')[0], out start);
		int.TryParse(first.Split('-')[1], out end);
		var firstAsInt = convertToInt(start, end);
		
		int.TryParse(second.Split('-')[0], out start);
		int.TryParse(second.Split('-')[1], out end);
		var secondAsInt = convertToInt(start, end);

		if ( ((firstAsInt & secondAsInt) == secondAsInt) || ((firstAsInt & secondAsInt) == firstAsInt)) {
			// Console.WriteLine($"{first}({firstAsInt}) {second}({secondAsInt}) ");
			result += 1;
		}
		
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;
	foreach (string line in input) {
		var first = line.Split(',')[0];
		var second = line.Split(',')[1];
		int start = 0;
		int end = 0;

		int.TryParse(first.Split('-')[0], out start);
		int.TryParse(first.Split('-')[1], out end);
		var firstAsInt = convertToInt(start, end);
		
		int.TryParse(second.Split('-')[0], out start);
		int.TryParse(second.Split('-')[1], out end);
		var secondAsInt = convertToInt(start, end);

		if ( ((firstAsInt & secondAsInt) != 0)) {
			// Console.WriteLine($"{first}({firstAsInt}) {second}({secondAsInt}) ");
			result += 1;
		}
		
	}
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

