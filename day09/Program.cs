using System;
using System.IO;
using System.Numerics;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

Vector2 left  = new Vector2 { X = -1, Y =  0 };
Vector2 right = new Vector2 { X = +1, Y =  0 };
Vector2 down  = new Vector2 { X =  0, Y = -1 };
Vector2 up    = new Vector2 { X =  0, Y = +1 };

Vector2 moveLeft  = new Vector2 { X = -2, Y =  0 };
Vector2 moveRight = new Vector2 { X = +2, Y =  0 };
Vector2 moveDown  = new Vector2 { X =  0, Y = -2 };
Vector2 MoveUp    = new Vector2 { X =  0, Y = +2 };

Vector2 moveLeftUp    = new Vector2 { X = -2, Y = +1 };
Vector2 moveRightUp   = new Vector2 { X = +2, Y = +1 };
Vector2 moveLeftDown  = new Vector2 { X =  0, Y = -2 };
Vector2 MoveRightUp   = new Vector2 { X =  0, Y = +2 };

Dictionary<string,Vector2> directions = new Dictionary<string, Vector2> {
	{ "L", left },
	{ "R", right },
	{ "D", down },
	{ "U", up },
};


Vector2 moveKnot(Vector2 head,Vector2 tail) {
	//
	var diff = head - tail;
	//Console.WriteLine($"Diff {diff}");

	if ( Math.Abs(diff.X) == 1 && Math.Abs(diff.Y) == 1) {
		// Touching, do nothing
	} else if ( diff.X == 2 && diff.Y == 0) {
		// Tail should move right
		tail += right;
	} else if  ( diff.X == -2 && diff.Y == 0 ) {
		// Tail should move left
		tail += left;
	} else if  ( diff.X == 0 && diff.Y == 2 ) {
		// Tail should move up
		tail += up;
	} else if  ( diff.X == 0 && diff.Y == -2 ) {
		// Tail should move down
		tail += down;
	} else if  ( diff.X > 0 && diff.Y > 0 ) {
		// Tail should move up,right
		tail += right;
		tail += up;
	} else if  ( diff.X < 0 && diff.Y < 0 ) {
		// Tail should move down,left
		tail += down;
		tail += left;
	} else if  ( diff.X > 0 && diff.Y < 0 ) {
		// Tail should move down,right
		tail += down;
		tail += right;
	} else if  ( diff.X < 0 && diff.Y > 0 ) {
		// Tail should move up,left
		tail += up;
		tail += left;
	}
	//Console.WriteLine($"New Tail : {currentTailPosition}");
	return tail;
}

int answerPart1(IEnumerable<string> input) {
	int result = 0;

	HashSet<Vector2> visitedPositions = new HashSet<Vector2>();

	Vector2 currentHeadPosition = new Vector2(0,0);
	Vector2 currentTailPosition = new Vector2(0,0);
	visitedPositions.Add(currentTailPosition);

	foreach (string line in input) {
		var direction = line.Split(' ')[0];
		var steps = int.Parse(line.Split(' ')[1].ToString());

		for ( int i = 0; i< steps; i++) {
			//Console.WriteLine();
			//Console.WriteLine($"Current Head : {currentHeadPosition}");
			//Console.WriteLine($"Current Tail : {currentTailPosition}");
			currentHeadPosition += directions[direction];
			currentTailPosition = moveKnot(currentHeadPosition,currentTailPosition);
			visitedPositions.Add(currentTailPosition);
			//Console.WriteLine($"New Head : {currentHeadPosition}");
			//Console.WriteLine($"New Tail : {currentTailPosition}");
		}
	}
	result = visitedPositions.Count();
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	HashSet<Vector2> visitedPositions = new HashSet<Vector2>();

	Vector2[] knots = new Vector2[10];

	for ( int i = 0; i < 10; i++) {
		knots[i] = new Vector2(0,0);
	}

	visitedPositions.Add(knots[9]);

	foreach (string line in input) {
		var direction = line.Split(' ')[0];
		var steps = int.Parse(line.Split(' ')[1].ToString());

		for ( int i = 0; i< steps; i++) {
			knots[0] += directions[direction];
			for ( int k=0; k<9; k++) {
				var currentHeadPosition = knots[k];
				var currentTailPosition = knots[k+1];
				currentTailPosition = moveKnot(currentHeadPosition,currentTailPosition);
				knots[k] = currentHeadPosition;
				knots[k+1] = currentTailPosition;
			}
			visitedPositions.Add(knots[9]);
		}
	}
	result = visitedPositions.Count();
	return result;
}



pathTest = @"test.txt";
pathInput = @"input.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 1 - Test  : {answerPart1(contentTest)}");
Console.WriteLine($"Part 1 - Input : {answerPart1(contentInput)}");

pathTest = @"test2.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 2 - Test  : {answerPart2(contentTest)}");
Console.WriteLine($"Part 2 - Input : {answerPart2(contentInput)}");

