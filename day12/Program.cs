using System;
using System.IO;
using System.Collections;
using System.Linq;

using System.Numerics;
using System.Text;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

Vector2 UP = new Vector2(0,-1);
Vector2 DOWN = new Vector2(0,1);
Vector2 RIGHT = new Vector2(1,0);
Vector2 LEFT = new Vector2(-1,0);

Vector2[] MOVES = { UP, DOWN, RIGHT, LEFT};


int getMinSteps(List<List<int>> board, Vector2 start, Vector2 end) {
	int result = 0;

	var maxX = board[0].Count();
	var maxY = board.Count();
	var size = new Vector2(maxX,maxY);

	int[,] boardDist = new int[maxY, maxX];

	for (int y = 0; y< size.Y; y++) {
		for (int x = 0; x <size.X; x++) {
			boardDist[y,x] = int.MaxValue;
		}
	}

	boardDist[(int) start.Y,(int)start.X] = 0;


	bool moreWork = true;

	var paths= new List<List<Vector2>>();

	paths.Add(new List<Vector2> { start });
	while (moreWork) {
		moreWork = false;

		var newPaths= new List<List<Vector2>>();
		foreach (List<Vector2> path in paths) {
			var validPos = new List<Vector2>();
			var last =path.Last();

			// Get Possible moves
			foreach (Vector2 move in MOVES) {
				var newPos = last+move;
				if ( (newPos.X < 0) || (newPos.X > size.X-1  ) || (newPos.Y < 0) || ( newPos.Y > size.Y-1)) {
					continue;
				}
			
				if (path.Contains(newPos)) {
					continue;
				}

				if ( board[(int) newPos.Y][(int) newPos.X] <= board[(int) last.Y][(int) last.X]+1 ) {
					validPos.Add(newPos);
				} 
			}

			// Check if destination distance is smaller than previous distances
			// for the destination
			foreach( Vector2 newPos in validPos) {
				var currentMinDist = boardDist[(int) newPos.Y,(int) newPos.X];
				if ( path.Count() >= currentMinDist) {
					continue;
				}
				if ( newPos == end) {
					result = path.Count();
					continue;
				}



				var newPath = path.ToList();
				newPath.Add(newPos);
				newPaths.Add(newPath);
				boardDist[(int) newPos.Y,(int) newPos.X] = newPath.Count()-1;

				moreWork = true;


			}




		}
		paths = newPaths.ToList();

	}
	
	return result;

}
int answerPart1(IEnumerable<string> input) {
	int result = 0;
	Vector2 start = new Vector2(0,0);
	Vector2 end = new Vector2(0,0);
	int x = 0;
	int y = 0;
	List<List<int>> board = new List<List<int>>();
	List<List<int>> boardDist = new List<List<int>>();

	foreach (string line in input) {
		var row = new List<int>();
		var rowDist = new List<int>();
		x = 0;
		foreach (char c in line) {
			var height = (int) c;
			var distance = int.MaxValue;

			if (height == (int) 'S') {
				height = (int) 'a';
				start = new Vector2(x,y);
				distance = 0;
			}
			if (height == (int) 'E') {
				height = (int) 'z';
				end = new Vector2(x,y);
			}
			row.Add(height);
			rowDist.Add(distance);
			x++;
		}
		board.Add(row);
		boardDist.Add(rowDist);
		y++;

	}

	result = getMinSteps(board, start, end);

	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	Vector2 end = new Vector2(0,0);
	int x = 0;
	int y = 0;
	List<List<int>> board = new List<List<int>>();
	List<List<int>> boardDist = new List<List<int>>();

	List<Vector2> starts = new List<Vector2>();
	foreach (string line in input) {
		var row = new List<int>();
		var rowDist = new List<int>();
		x = 0;
		foreach (char c in line) {
			var height = (int) c;
			var distance = int.MaxValue;

			if (height == (int) 'S') {
				height = (int) 'a';
				distance = 0;
			}
			if (height == (int) 'E') {
				height = (int) 'z';
				end = new Vector2(x,y);
			}

			if (height == (int) 'a') {
				starts.Add(new Vector2(x,y));
			}
			row.Add(height);
			rowDist.Add(distance);
			x++;
		}
		board.Add(row);
		boardDist.Add(rowDist);
		y++;

	}

	result = int.MaxValue;

	foreach (Vector2 start in starts) {
		var steps = getMinSteps(board, start, end);
		if (steps > 0 && steps < result) {
			result = steps;
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

