namespace Computer;

public class Cpu {
	
	public int X { get; set;}
	public int PC { get; set;}

	public string[] Mem {get;set;}

	public int Cycle { get; set; }
	private int _opcycle { get; set; }
	private string _currentInstruction {get;set;}

	public char[] Screen { get; set;}

	public void DisplayScreen() {
		Console.WriteLine();
		for (int i = 0; i < 6; i++) {
			var line = Screen[(40*i)..(40*(i+1))];
			Console.Write($"{i:D2} ");
			Console.WriteLine(line);
		}
		Console.WriteLine();

	}

	public void printState() {
		Console.WriteLine($"{Cycle} - {PC} {_currentInstruction} - {_opcycle} : {X}");
	}

	public void Tick() {
		// Read Instruction
		_currentInstruction = Mem[PC];

		switch(_currentInstruction.Split(' ')[0]) {
			case "noop":
				if (_opcycle == 1) {
					_opcycle = 0;
					PC++;
				}
				break;
			case "addx":
				if ( _opcycle == 2) {
					_opcycle = 0;
					X += int.Parse(_currentInstruction.Split(' ')[1]);
					PC++;
				}
				break;
			default:
				Console.WriteLine("ERROR");
				break;

		}
		
		// Draw Screen
		// Sprite is 3 wide
		// X is the position of the middle of the sprite
		
		var pixelPos = Cycle % (6*40);
		var linePos = pixelPos % 40;
		if ( (linePos == X) || (linePos == X-1) || (linePos == X+1) ) {
			Screen[Cycle % (6*40)] = '#';
		} else {
			Screen[Cycle % (6*40)] = '.';

		}


		Cycle++;
		_opcycle++;




	}

	public Cpu(string[] mem) {
		X = 1;
		PC = 0;
		_opcycle = 0;
		Mem = mem;
		Screen = new char[6*40];
		for (int i = 0; i< 6*40; i++) {
			Screen[i] = ' ';
		}

	}
}