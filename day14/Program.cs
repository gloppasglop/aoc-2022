using System;
using System.IO;
using System.Collections;
using System.Numerics;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

int answerPart1(IEnumerable<string> input) {
	int result = 0;

	int maxX = 0;
	int maxY = 0;
	int minX = int.MaxValue;
	int minY = int.MaxValue;

	HashSet<Vector2> occupiedSlots = new HashSet<Vector2>();
	foreach (string line in input) {
		var coords = line.Split(" -> ");
		bool firstCoord = true;
		int prevX = 0;
		int prevY = 0;

		foreach ( string coord in coords) {
			var x = int.Parse(coord.Split(',')[0]);
			var y = int.Parse(coord.Split(',')[1]);
			
			if ( x  < minX ) {
				minX = x;
			}
			if ( y  < minY ) {
				minY = y;
			}

			if ( x  > maxX ) {
				maxX = x;
			}
			if ( y  > maxY ) {
				maxY = y;
			}

			if ( firstCoord ) {
				firstCoord = false;
			} else {

				if( prevX == x) {
					// Check for vertical lines
					var start = Math.Min(prevY,y);
					var end = Math.Max(prevY,y);
					for (int i = start; i <= end; i++) {
						occupiedSlots.Add(new Vector2(x,i));
					}

				} else if ( prevY == y ) {
					// Check for horizontal lines

					var start = Math.Min(prevX,x);
					var end = Math.Max(prevX,x);
					for (int i = start; i <= end; i++) {
						occupiedSlots.Add(new Vector2(i,y));
					}

				} else {
					Console.WriteLine("Unreachable");
				}
 


			}
			prevX = x;
			prevY = y;
		}
	}

	bool moreWork = true;
	var dropPos = new Vector2(500,0);
	var sandPos = dropPos;
	
	var DOWN = new Vector2(0,1);
	var DOWN_LEFT = new Vector2(-1,1);
	var DOWN_RIGHT = new Vector2(1,1);

	var possible_moves = new Vector2[3] {DOWN,DOWN_LEFT,DOWN_RIGHT};

	while ( moreWork) {
		moreWork = false;

		// Check if possible moves

		bool canMove = true;
		while ( canMove) {
			// Check if sand is below last horizontal wall
			if (sandPos.Y > maxY) {
				break;
			}		
			canMove = false;
			foreach ( Vector2 move in possible_moves) {
				var newSandPos = sandPos+move;
				if (! occupiedSlots.Contains(newSandPos)) {
					sandPos = newSandPos;
					canMove = true;
					break;
				}
			}

			if (! canMove) {
				occupiedSlots.Add(sandPos);
				//Console.WriteLine($"({sandPos.X},{sandPos.Y})");
				sandPos = dropPos;
				result += 1;
			} else {
				moreWork = true;
			}
		
		}
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	int maxX = 0;
	int maxY = 0;
	int minX = int.MaxValue;
	int minY = int.MaxValue;

	HashSet<Vector2> occupiedSlots = new HashSet<Vector2>();
	foreach (string line in input) {
		var coords = line.Split(" -> ");
		bool firstCoord = true;
		int prevX = 0;
		int prevY = 0;

		foreach ( string coord in coords) {
			var x = int.Parse(coord.Split(',')[0]);
			var y = int.Parse(coord.Split(',')[1]);
			
			if ( x  < minX ) {
				minX = x;
			}
			if ( y  < minY ) {
				minY = y;
			}

			if ( x  > maxX ) {
				maxX = x;
			}
			if ( y  > maxY ) {
				maxY = y;
			}

			if ( firstCoord ) {
				firstCoord = false;
			} else {

				if( prevX == x) {
					// Check for vertical lines
					var start = Math.Min(prevY,y);
					var end = Math.Max(prevY,y);
					for (int i = start; i <= end; i++) {
						occupiedSlots.Add(new Vector2(x,i));
					}

				} else if ( prevY == y ) {
					// Check for horizontal lines

					var start = Math.Min(prevX,x);
					var end = Math.Max(prevX,x);
					for (int i = start; i <= end; i++) {
						occupiedSlots.Add(new Vector2(i,y));
					}

				} else {
					Console.WriteLine("Unreachable");
				}
 


			}
			prevX = x;
			prevY = y;
		}
	}

	bool moreWork = true;
	var dropPos = new Vector2(500,0);
	var sandPos = dropPos;
	
	var DOWN = new Vector2(0,1);
	var DOWN_LEFT = new Vector2(-1,1);
	var DOWN_RIGHT = new Vector2(1,1);

	var possible_moves = new Vector2[3] {DOWN,DOWN_LEFT,DOWN_RIGHT};

	while ( moreWork) {
		moreWork = false;

		// Check if possible moves

		bool canMove = true;
		while ( canMove) {
			canMove = false;
			foreach ( Vector2 move in possible_moves) {
				var newSandPos = sandPos+move;
				if (! (occupiedSlots.Contains(newSandPos) || newSandPos.Y == maxY+2)) {
					sandPos = newSandPos;
					canMove = true;
					break;
				}
			}

			if (! canMove) {
				occupiedSlots.Add(sandPos);
				//Console.WriteLine($"({sandPos.X},{sandPos.Y})");
				result += 1;
				if ( sandPos == dropPos) {
					moreWork = false;
					break;
				}
				sandPos = dropPos;
			} else {
				moreWork = true;
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

