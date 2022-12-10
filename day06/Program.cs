using System;
using System.IO;
using System.Collections;
using System.Linq;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

int answerPart1(IEnumerable<string> input) {
	int result = 0;
	var buffer = "";
	string line  = input.First();
	int len = 4;
	for (int i = 0;i < line.Length - len ; i++) {
		buffer = line[i..(i+len)];
		var hs = new HashSet<char>(buffer);
		//Console.WriteLine($"{buffer} - {hs.Count()}");
		if (hs.Count() == len) {
			result = i+len;
			break;
		}
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;
	var buffer = "";
	string line  = input.First();
	int len = 14;
	for (int i = 0;i < line.Length - len; i++) {
		buffer = line[i..(i+len)];
		var hs = new HashSet<char>(buffer);
		//Console.WriteLine($"{buffer} - {hs.Count()}");
		if (hs.Count() == len) {
			result = i+len;
			break;
		}
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

