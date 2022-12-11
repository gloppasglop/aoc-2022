using System.Collections;

namespace AOC;

public class Monkey {

	public Queue<Int128> Items {get;set;} = new Queue<Int128>();
	public string Operand1 {get;set;} = "";
	public string Operand2 {get;set;} = "";
	public string Operation {get;set;} = "";
	public Int128 Dividend {get;set;}
	public int ThrowWhenFalse {get;set;}
	public int ThrowWhenTrue {get;set;}
	public Int128 worryLevelDivider {get;set;}

	public Int128 InspectCount {get;set;} = 0;
	public (Int128,int) PlayTurn() {
		var worryLevel = Items.Dequeue();
		Int128 operand1;
		Int128 operand2;
		Int128 newWorry = 0;

		InspectCount++;

		if (Operand1 == "old") {
			operand1 = worryLevel;
		} else {
			operand1 = Int128.Parse(Operand1);
		}

		if (Operand2 == "old") {
			operand2 = worryLevel;
		} else {
			operand2 = Int128.Parse(Operand2);
		}

		switch(Operation) {
			case "+":
				newWorry = operand1 + operand2;
				break;
			case "*":
				newWorry = operand1 * operand2;
				break;
			default:
				Console.WriteLine("Unhandled operation : "+Operation);
				break;
		}

		newWorry = newWorry / worryLevelDivider;

		if ( newWorry % Dividend == 0 ) {
			return (newWorry , ThrowWhenTrue);
		} else {
			return (newWorry , ThrowWhenFalse);
		}


	}
}