namespace Aoc;

public enum PacketDataType {
	List,
	Int
}
public class PacketData : IComparable {
	
	int IComparable.CompareTo(object? obj) {
		PacketData pd = (PacketData) obj;
		return ComparePackedData(this,pd);
	}
	public static int ComparePackedData(PacketData p1, PacketData p2) {

		if ( ( p1.DataType == PacketDataType.Int ) && (p2.DataType == PacketDataType.Int)) {
			return p1.IntData.CompareTo(p2.IntData);
		}

		if ( ( p1.DataType == PacketDataType.List ) && (p2.DataType == PacketDataType.List)) {
			
			var p1Count = p1.ListData.Count();
			var p2Count = p2.ListData.Count();
			for (int i =  0; i < Math.Min(p1Count,p2Count); i++) {
				var eltCompare = ComparePackedData(p1.ListData[i],p2.ListData[i]);
				if (eltCompare != 0) {
					return eltCompare;
				}
			}
			return p1Count.CompareTo(p2Count);

		}


		if ( p1.DataType == PacketDataType.Int  ) {
			// p2 in then a List
			 	var p1AsList = new PacketData();
				p1AsList.DataType = PacketDataType.List;
				p1AsList.ListData.Add(p1);
				var eltCompare = ComparePackedData(p1AsList,p2);
				if (eltCompare != 0) {
					return eltCompare;
				}

		} else {
			// p1 is a List and p1 and Int

			 	var p2AsList = new PacketData();
				p2AsList.DataType = PacketDataType.List;
				p2AsList.ListData.Add(p2);
				var eltCompare = ComparePackedData(p1,p2AsList);
				if (eltCompare != 0) {
					return eltCompare;
				}
		}

		return 0;

	}
	

	public PacketDataType DataType {get; set;}
	public int IntData {get; set;}
	public List<PacketData> ListData {get; set;}


	public PacketData() {
		ListData = new List<PacketData>();
	}

	public PacketData(string str) {
		
		ListData = new List<PacketData>();
		Stack<PacketData> pds = new Stack<PacketData>();
		string intAsString = "";
		foreach (char c in str) {
			switch(c) {
				case '[':
				  	var pd = new PacketData();
					pd.DataType = PacketDataType.List;
				 	pds.Push(pd);
					intAsString = "";
					break;
				case ']':
				 	var p = pds.Pop();
					if ( intAsString != "" ) {
						var intData = new PacketData();
						intData.DataType = PacketDataType.Int;
						intData.IntData = int.Parse(intAsString);
						p.ListData.Add(intData);
						if ( pds.Count() > 0) {
							var p2 = pds.Pop();
							p2.ListData.Add(p);
							pds.Push(p2);
						} else {
							ListData.Add(p);
						}
					} else {
						if ( pds.Count() > 0) {
							var p2 = pds.Pop();
							p2.ListData.Add(p);
							pds.Push(p2);
						} else {
							ListData.Add(p);
						}
					}
					intAsString = "";
					break;
				case ',':
					if (intAsString != "") {
						var pcomma = pds.Pop();
						var intData = new PacketData();
						intData.DataType = PacketDataType.Int;
						intData.IntData = int.Parse(intAsString);
						pcomma.ListData.Add(intData);
						intAsString = "";
						pds.Push(pcomma);
					}
					break;
				default:
					intAsString += c;
					break;
			}
		}
	}


}