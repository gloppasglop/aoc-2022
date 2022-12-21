using System;
using System.IO;
using System.Collections;
using System.Linq;
using Aoc;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;


int answerPart1(IEnumerable<string> input) {
	int result = 0;

	//var content = input.ToArray();

	string firstAsString = "";
	string secondAsString = "";
	//PacketData first = new PacketData();;
	//PacketData second = new PacketData();


	List<(PacketData firt, PacketData second)> packetPairs = new List<(PacketData firt, PacketData second)>();
	bool newPair = true;

	foreach (string line in input) {
		if ( line == "") {
			newPair = true;
			continue;
		}

		if (newPair) {
			firstAsString = line;
			newPair = false;
		} else {
			secondAsString = line;
			packetPairs.Add((new PacketData(firstAsString),new PacketData(secondAsString)));
		}

	}
	
	var i = 1;
	foreach ( (PacketData, PacketData) pair in packetPairs) {
		var compare = PacketData.ComparePackedData(pair.Item1,pair.Item2);
		if ( compare < 0) {
			result += i;
		}
		i++;
	}
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;

	List<PacketData> packets = new List<PacketData>();
	foreach (string line in input) {
		if ( line == "") {
			continue;
		}
		packets.Add(new PacketData(line));

	}

	var dividerpacket1 = new PacketData("[[2]]");
	var dividerpacket2 = new PacketData("[[6]]");
	packets.Add(dividerpacket1);
	packets.Add(dividerpacket2);

	packets.Sort();

	int i = 1;
	result = 1;

	foreach ( PacketData pd in packets) {
		if ( PacketData.ComparePackedData(dividerpacket1,pd) == 0) {
			result *= i;
		}
		if ( PacketData.ComparePackedData(dividerpacket2,pd) == 0) {
			result *= i;
		}
		i++;
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

