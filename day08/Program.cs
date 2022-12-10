using System;
using System.IO;
using System.Linq;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;




int answerPart1(IEnumerable<string> input) {
	int result = 0;

	var height = input.Count();
	var width = input.First().Count();

	var lines = new int[height][];
	var results = new int[height][];
	for (int i = 0; i<height; i++) {
		results[i] = new int[width];
		lines[i] = (from c in input.ElementAt(i) select int.Parse(c.ToString())).ToArray();
	}

	// Borders are visible

	for (int i = 0; i<height; i++) {
		results[i][0]=1;
		results[i][width-1]=1;
	}
	for (int j = 0; j< width; j++) {
		results[0][j] = 1;
		results[height-1][j] = 1;
	}


	// Visible from Left
	
	for (int i = 0; i< height; i++) {
		var maxTreeHeight = lines[i][0];
		for (int j = 1; j< width-1; j++) {
			if ( lines[i][j] > maxTreeHeight ) {
				maxTreeHeight = lines[i][j];
				results[i][j] = 1;
			} else {
				//break;
			}
		}
	}

	// Visile from Right
	for (int i = 0; i< height; i++) {
		var maxTreeHeight = lines[i][width-1];
		for (int j = width-2; j>=1; j--) {
			if ( lines[i][j] > maxTreeHeight ) {
				maxTreeHeight = lines[i][j];
				results[i][j] = 1;
			} else {
				//break;
			}
		}
	}


	// Visible from Top
	
	for (int j = 0; j< width; j++) {
		var maxTreeHeight = lines[0][j];
		for (int i = 1; i< height-1; i++) {
			if ( lines[i][j] > maxTreeHeight ) {
				maxTreeHeight = lines[i][j];
				results[i][j] = 1;
				//result += 1;
			} else {
				//break;
			}
		}
	}

	// Visile from Bottom
	for (int j = 0; j< width; j++) {
		var maxTreeHeight = lines[height-1][j];
		for (int i = height-2; i>=1; i--) {
			if ( lines[i][j] > maxTreeHeight ) {
				maxTreeHeight = lines[i][j];
				results[i][j] = 1;
				//result += 1;
			} else {
				//break;
			}
		}
	}



	for (int i=0; i< height; i++) {
		for (int j = 0; j < width; j++) {
			result += results[i][j];
		}
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	var height = input.Count();
	var width = input.First().Count();

	var lines = new int[height][];
	var results = new int[height][];
	for (int i = 0; i<height; i++) {
		results[i] = new int[width];
		lines[i] = (from c in input.ElementAt(i) select int.Parse(c.ToString())).ToArray();
	}

	for ( int i=0; i < height; i++) {
		for (int j = 0; j < width; j++) {
			// Check right
			var visibleRight = 0;
			for (int k = j+1; k <width; k++) {
				visibleRight += 1;
				if ( lines[i][k] >= lines[i][j]) {
					break;
				}
			}

			// check Left

			var visibleLeft = 0;
			for (int k = j-1; k >=0 ; k--) {
				visibleLeft += 1;
				if ( lines[i][k] >= lines[i][j]) {
					break;
				}
			}

			// Down

			var visibleDown = 0;
			for (int k = i+1; k <height; k++) {
				visibleDown += 1;
				if ( lines[k][j] >= lines[i][j]) {
					break;
				}
			}

			// check Up

			var visibleUp = 0;
			for (int k = i-1; k >=0 ; k--) {
				visibleUp += 1;
				if ( lines[k][j] >= lines[i][j]) {
					break;
				}
			}

			//Console.WriteLine($"({i},{j}) {visibleRight}*{visibleLeft}*{visibleDown}*{visibleUp}");
			if (visibleRight*visibleLeft*visibleUp*visibleDown > result) {
				result = visibleRight*visibleLeft*visibleUp*visibleDown;
			}
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

