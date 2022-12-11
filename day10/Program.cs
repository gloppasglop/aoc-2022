using System;
using System.IO;
using System.Linq;
using Computer;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;




int answerPart1(IEnumerable<string> input) {
	int result = 0;
	
	Cpu computer = new Cpu(input.ToArray());

	List<int> cyclesToCheck = new List<int> { 20, 60, 100, 140, 180, 220 };
	while (computer.PC < computer.Mem.Count()-1) {
		computer.Tick();
		//computer.printState();
		if (cyclesToCheck.Contains(computer.Cycle)) {
			result += computer.Cycle*computer.X;
		}

	}

	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	Cpu computer = new Cpu(input.ToArray());

	while (computer.PC < computer.Mem.Count()-1) {
		computer.Tick();
		//computer.DisplayScreen();
	}
	computer.DisplayScreen();


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

