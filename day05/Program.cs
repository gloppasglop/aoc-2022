using System;
using System.IO;
using System.Collections;
using System.Linq;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

string answerPart1(IEnumerable<string> input) {
	string result = "";
	int startMove = 0;
	int numberOfStacks = 0;
	var stacks = new List<Stack<char>>();
	foreach (string line in input) {
		startMove++;
		if (line[1] == '1') {
			numberOfStacks = (line.Length+1)/4;
			break;
		}

	}
	for ( int i = 0; i< numberOfStacks; i++) {
		stacks.Add(new Stack<char>());
	}

	var l = input.Count();

	for (int index = startMove-2; index >=0; index--) {
		for (int i = 0; i < numberOfStacks; i++) {
			var line = input.ElementAt(index);
			var c = line[1+4*i];
			if ( c != ' ' ) {
				stacks[i].Push(c);
			}
		}
	}

	for (int index = startMove+1; index < l; index++) {
		//Console.WriteLine(input.ElementAt(index));
		var instructions = input.ElementAt(index).Split(' ');
		int qty,src,dest;

		int.TryParse(instructions[1], out qty);
		int.TryParse(instructions[3], out src);
		src--;
		int.TryParse(instructions[5], out dest);
		dest--;

		for (int c = 0; c < qty; c++) {
			stacks[dest].Push(stacks[src].Pop());
		}
	}


	for ( int i = 0; i< numberOfStacks; i++) {
		result += stacks[i].Pop();
	}
	return result;
}

string answerPart2(IEnumerable<string> input) {
	string result = "";
	int startMove = 0;
	int numberOfStacks = 0;
	var stacks = new List<Stack<char>>();
	foreach (string line in input) {
		startMove++;
		if (line[1] == '1') {
			numberOfStacks = (line.Length+1)/4;
			break;
		}

	}
	for ( int i = 0; i< numberOfStacks; i++) {
		stacks.Add(new Stack<char>());
	}

	var l = input.Count();

	for (int index = startMove-2; index >=0; index--) {
		for (int i = 0; i < numberOfStacks; i++) {
			var line = input.ElementAt(index);
			var c = line[1+4*i];
			if ( c != ' ' ) {
				stacks[i].Push(c);
			}
		}
	}

	for (int index = startMove+1; index < l; index++) {
		//Console.WriteLine(input.ElementAt(index));
		var instructions = input.ElementAt(index).Split(' ');
		int qty,src,dest;

		int.TryParse(instructions[1], out qty);
		int.TryParse(instructions[3], out src);
		src--;
		int.TryParse(instructions[5], out dest);
		dest--;

		var tmp = new char[qty];
		for (int c = 0; c < qty; c++) {
			tmp[c]=stacks[src].Pop();
		}
		for (int c = 0; c < qty; c++) {
			stacks[dest].Push(tmp[qty-1-c]);
		}
	}


	for ( int i = 0; i< numberOfStacks; i++) {
		result += stacks[i].Pop();
	}
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

