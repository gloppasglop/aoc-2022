using System;
using System.IO;
using System.Collections;
using System.Numerics;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;

int answerPart1(IEnumerable<string> input, int rowY) {
	int result = 0;

	List<(Vector2,Vector2)> sensors = new List<(Vector2, Vector2)>();

	foreach (string line in input) {
		var splitBySpace = line.Split(' ');
		var sensorX = splitBySpace[2].Remove(splitBySpace[2].Length-1).Split('=')[1];
		var sensorY = splitBySpace[3].Remove(splitBySpace[3].Length-1).Split('=')[1];
		var beaconX = splitBySpace[8].Remove(splitBySpace[8].Length-1).Split('=')[1];
		var beaconY = splitBySpace[9].Split('=')[1];
		sensors.Add((
			new Vector2(int.Parse(sensorX),int.Parse(sensorY)),
			new Vector2(int.Parse(beaconX),int.Parse(beaconY))));
	}

	var rowIntersections = new List<Vector2>();
	foreach ((Vector2,Vector2) sensor in sensors) {
		// Get the distance from each beacon to the row  
		var distToRow = Math.Abs(rowY-sensor.Item1.Y);
		
		// check if greater than distance to beacon
		var distToBeacon = Math.Abs(sensor.Item1.X-sensor.Item2.X)+Math.Abs(sensor.Item1.Y-sensor.Item2.Y);

		if (distToRow <= distToBeacon) {
			// There is an intersection
			var minX = sensor.Item1.X-distToBeacon+distToRow;
			var maxX = sensor.Item1.X+distToBeacon-distToRow;
			rowIntersections.Add(new Vector2(minX,maxX));
		}

	}

	// Sort by interval start
	rowIntersections.Sort(delegate(Vector2 A, Vector2 B)
	{
		return A.X.CompareTo(B.X);
	});


	if (rowIntersections.Count() == 0) {
		return 0;
	}
	var firstInterval = rowIntersections[0];
	var intervalUnion = new Stack<Vector2>();
	intervalUnion.Push(firstInterval);

	for( int i = 1; i < rowIntersections.Count(); i++) {
		firstInterval = intervalUnion.Pop();
		var I = rowIntersections[i];
		if (I.X <= firstInterval.Y) {
			firstInterval.Y = Math.Max(firstInterval.Y,I.Y);
			intervalUnion.Push(firstInterval);
		} else {
			intervalUnion.Push(firstInterval);
			intervalUnion.Push(I);
		}
	}
	
	foreach (Vector2 I in intervalUnion.ToList()) {
		result += (int) (I.Y-I.X);
	}

	return result;
}

Int128 answerPart2(IEnumerable<string> input, int maxRow) {
	Int128 result = 0;

	List<(Vector2,Vector2)> sensors = new List<(Vector2, Vector2)>();

	foreach (string line in input) {
		var splitBySpace = line.Split(' ');
		var sensorX = splitBySpace[2].Remove(splitBySpace[2].Length-1).Split('=')[1];
		var sensorY = splitBySpace[3].Remove(splitBySpace[3].Length-1).Split('=')[1];
		var beaconX = splitBySpace[8].Remove(splitBySpace[8].Length-1).Split('=')[1];
		var beaconY = splitBySpace[9].Split('=')[1];
		sensors.Add((
			new Vector2(int.Parse(sensorX),int.Parse(sensorY)),
			new Vector2(int.Parse(beaconX),int.Parse(beaconY))));
	}

	for (int rowY = 0; rowY < maxRow; rowY++) {

		var rowIntersections = new List<Vector2>();
		foreach ((Vector2,Vector2) sensor in sensors) {
			// Get the distance from each beacon to the row  
			var distToRow = Math.Abs(rowY-sensor.Item1.Y);
		
			// check if greater than distance to beacon
			var distToBeacon = Math.Abs(sensor.Item1.X-sensor.Item2.X)+Math.Abs(sensor.Item1.Y-sensor.Item2.Y);

			if (distToRow <= distToBeacon) {
				// There is an intersection
				var minX = sensor.Item1.X-distToBeacon+distToRow;
				var maxX = sensor.Item1.X+distToBeacon-distToRow;
				rowIntersections.Add(new Vector2(minX,maxX));
			}

		}

		// Sort by interval start
		rowIntersections.Sort(delegate(Vector2 A, Vector2 B)
		{
			return A.X.CompareTo(B.X);
		});


		if (rowIntersections.Count() == 0) {
		 	continue;
		}

		var firstInterval = rowIntersections[0];
		var intervalUnion = new Stack<Vector2>();
		intervalUnion.Push(firstInterval);

		for( int i = 1; i < rowIntersections.Count(); i++) {
			firstInterval = intervalUnion.Pop();
			var I = rowIntersections[i];
			if (I.X <= firstInterval.Y) {
				firstInterval.Y = Math.Max(firstInterval.Y,I.Y);
				intervalUnion.Push(firstInterval);
			} else {
				intervalUnion.Push(firstInterval);
				intervalUnion.Push(I);
			}
		}
	
		//foreach (Vector2 I in intervalUnion.ToList()) {
		//	result += (int) (I.Y-I.X);
		//}

		if ( intervalUnion.Count() > 1 ) {
			var tmp = intervalUnion.ToList();
			tmp.Sort(delegate(Vector2 A, Vector2 B)
			{
				return A.X.CompareTo(B.X);
			});
			result = 4_000_000*((Int128) tmp[1].X-1) + (Int128) rowY;
			break;
			//return result;

		}
	}
	return result;
}



pathTest = @"test.txt";
pathInput = @"input.txt";
contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 1 - Test  : {answerPart1(contentTest,10)}");
Console.WriteLine($"Part 1 - Input : {answerPart1(contentInput,2000000)}");

contentTest = File.ReadLines(pathTest);
contentInput = File.ReadLines(pathInput);
Console.WriteLine($"Part 2 - Test  : {answerPart2(contentTest,20)}");
Console.WriteLine($"Part 2 - Input : {answerPart2(contentInput,4000000)}");

