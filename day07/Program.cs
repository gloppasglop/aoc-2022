using System;
using System.IO;
using Filesystem;

string pathTest;
string pathInput;

IEnumerable<string> contentTest;
IEnumerable<string> contentInput;



int getSize(DirNode dir ) {
	int totalSize = 0;
	// Get the size of all files
	foreach (String name in dir.Files.Keys) {
		totalSize += dir.Files[name].Size;
	}

	// Recurse to children directories
	foreach (KeyValuePair<string, DirNode> entry in dir.Dirs) {
		var size = getSize(entry.Value);
		totalSize += size;
	}

	return totalSize;
}

List<DirNode> getAllDirs(DirNode dir ) {

	List<DirNode> dirNodes = new List<DirNode>();
	dirNodes.Add(dir);
	// Recurse to children directories
	foreach (KeyValuePair<string, DirNode> entry in dir.Dirs) {
		dirNodes.AddRange(getAllDirs(entry.Value));
	}

	return dirNodes;
}

DirNode parseInput(IEnumerable<string> input){
	DirNode root = new ("/",null, new Dictionary<string, DirNode>(), new Dictionary<string,FileNode>());
	var cmd = "";
	DirNode currentNode = root;
	foreach (string line in input) {
		// check for command
		if (line[0] == '$') {
			var cmdline = line.Split(' ');
			//Console.WriteLine(line);
			switch(cmdline[1])
			{
				case "cd":
				 	// get second argument
					cmd = "cd";
					switch(cmdline[2])
					{
						case "..":
							currentNode = currentNode.Parent;
							break;
						case "/":
							currentNode = root;
							break;
						default:
							currentNode = currentNode.Dirs[cmdline[2]];
							break;
					}
					break;
				case "ls":
				 	cmd = "ls";
					break;
			}
		} else {
			var output = line.Split(' ');	
			if (output[0] == "dir") {
				// Directory
				var dirName = output[1];
				DirNode newDir = new(dirName,currentNode,new Dictionary<string, DirNode>(),new Dictionary<string, FileNode>());
				currentNode.Dirs[dirName] = newDir;

			} else {
				// File
				int size = 0;
				int.TryParse(output[0], out size);
				var fileName = output[1];
				FileNode newFile = new(fileName,size);
				currentNode.Files[fileName] = newFile;

			}
		}
	}

	return root;

}

int answerPart1(IEnumerable<string> input) {
	int result = 0;
	var root = parseInput(input);
	//result = getSize(root,int.MaxValue);
	var alldirs = getAllDirs(root);
	foreach( DirNode dir in alldirs) {
		var size = getSize(dir);
		if ( size < 100000) {
			result += size;
		}
	}
	//result = getSize(root);
	return result;
}

int answerPart2(IEnumerable<string> input) {
	int result = 0;
	var root = parseInput(input);
	var alldirs = getAllDirs(root);
	var freeSpace = 70000000-getSize(root);

	result = int.MaxValue;
	foreach( DirNode dir in alldirs) {
		var size = getSize(dir);
		if (freeSpace + size >= 30000000 ) {
			if ( size < result) {
				result = size;
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

