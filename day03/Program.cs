using System;
using System.IO;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

int answerPart1(IEnumerable<string> input) {
	int result = 0;
	foreach (string line in input) {
		HashSet<char> comp1 = new HashSet<char>();
		// Check first compartment
		for (int i = 0; i < line.Count()/2; i++) {
			comp1.Add(line[i]);

		}
		HashSet<char> comp2 = new HashSet<char>();
		// Check second compartment
		for (int i = line.Count()/2; i < line.Count(); i++) {
			comp2.Add(line[i]);
		}
		comp1.IntersectWith(comp2);
		foreach (var elt in comp1) {
			// No boundary checks
			if ( (int)elt >= (int)'a' ) {
				// Lowercase
				//Console.WriteLine(1+(int)elt-(int)'a');
				result += 1+(int)elt-(int)'a';
			} else {
				// Uppercase
				//Console.WriteLine(27+(int)elt(int)'A');
				result += 27+(int)elt-(int)'A';
			}
		}
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;
	var content = input.ToList();
	for (int group = 0; group < content.Count()/3; group ++) {
		HashSet<char> comp1 = new HashSet<char>();
		var line = content[3*group];
		for (int i = 0; i < line.Count(); i++) {
			comp1.Add(line[i]);

		}

		HashSet<char> comp2 = new HashSet<char>();
		line = content[3*group+1];
		for (int i = 0; i < line.Count(); i++) {
			comp2.Add(line[i]);

		}
		HashSet<char> comp3 = new HashSet<char>();
		line = content[3*group+2];
		for (int i = 0; i < line.Count(); i++) {
			comp3.Add(line[i]);

		}

		comp1.IntersectWith(comp2);
		comp1.IntersectWith(comp3);
		foreach (var elt in comp1) {
			// No boundary checks
			if ( (int)elt >= (int)'a' ) {
				// Lowercase
				//Console.WriteLine(1+(int)elt-(int)'a');
				result += 1+(int)elt-(int)'a';
			} else {
				// Uppercase
				//Console.WriteLine(27+(int)elt(int)'A');
				result += 27+(int)elt-(int)'A';
			}
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

