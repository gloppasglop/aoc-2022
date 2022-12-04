using System;
using System.IO;
using PRS;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

int answerPart1(IEnumerable<string> input) {
	int totalScore = 0;
	int score = 0;
	foreach (string line in input) {
		string[] items = line.Split(' ');
		string opponent = items[0];
		string me = items[1];
		switch(opponent) {
			case "A":
				// Rock
				switch (me) {
					case "X":
						// Rocks, Draw
						score = 1+3;
						break;
					case "Y":
					 	// Paper, Win
						score = 2+6;
						break;
					case "Z":
						// Scisors, Loose
						score = 3+0;
						break;
				}
				break;
			case "B":
				// Paper
				switch (me) {
					case "X":
						// Rocks, Loose
						score = 1+0;
						break;
					case "Y":
					 	// Paper, Draw
						score = 2+3;
						break;
					case "Z":
						// Scisors, Win
						score = 3+6;
						break;
				}
				break;
			case "C":
				// Scisors
				switch (me) {
					case "X":
						// Rocks, Win
						score = 1+6;
						break;
					case "Y":
					 	// Paper, Loose
						score = 2+0;
						break;
					case "Z":
						// Scisors, Draw
						score = 3+3;
						break;
				}
				break;
		}
		totalScore += score;
	}
	return totalScore;
}

int answerPart2(IEnumerable<string> input) {
	int totalScore = 0;
	int score = 0;
	foreach (string line in input) {
		string[] items = line.Split(' ');
		string opponent = items[0];
		string me = items[1];
		switch(opponent) {
			case "A":
				// Rock
				switch (me) {
					case "X":
						// I need to loose must play scisors
						score = 3+0;
						break;
					case "Y":
					 	// I need a draw so play Rock
						score = 1+3;
						break;
					case "Z":
						// I need to win so play Paper
						score = 2+6;
						break;
				}
				break;
			case "B":
				// Paper
				switch (me) {
					case "X":
						// I need to loose must play Rock
						score = 1+0;
						break;
					case "Y":
					 	// I need a draw so play Paper
						score = 2+3;
						break;
					case "Z":
						// I need to win so play Scisors
						score = 3+6;
						break;
				}
				break;
			case "C":
				// Scisors
				switch (me) {
					case "X":
						// I need to loose must play Paper
						score = 2+0;
						break;
					case "Y":
					 	// I need a draw so play Scisors
						score = 3+3;
						break;
					case "Z":
						// I need to win so play Rock
						score = 1+6;
						break;
				}
				break;
		}
		totalScore += score;
	}
	return totalScore;
}

Hand rock = new("Rock",1);
Hand paper = new("Paper",2);
Hand scisors = new("Scisors",3);

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

