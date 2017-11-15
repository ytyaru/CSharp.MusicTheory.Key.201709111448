using System;
using System.Collections.Generic;
namespace MusicTheory.Chord
{
	public class Accidental
	{
		public static readonly AccidentalType Flat = new AccidentalType(-1, '♭','b','-',new Dictionary<string,string>(){{"en","flat"},{"ja","変"}});
		public static readonly AccidentalType Sharp = new AccidentalType(+1, '♯','#','+',new Dictionary<string,string>(){{"en","sharp"},{"ja","嬰"}});
		public static int GetPitch(string accidental) {
			if (!Accidental.IsSameChars (accidental)) {throw new Exception ("変化記号は異なる記号を併用せず同じ記号だけを使用してください。");}
			try {return Flat.GetValue (accidental);}
			catch (Exception e) {}
			try {return Sharp.GetValue (accidental);}
			catch (Exception e) {}
			return 0;
		}
		private static bool IsSameChars(string accidental)
		{
			foreach (char c in accidental) {if (c != accidental[0]) {return false;}}
			return true;
		}
		private Accidental () {}
	}
	public class AccidentalType
	{
		public readonly int Value;
		public readonly char Unicode;
		public readonly char Ascii;
		public readonly char Operator;
		public readonly Dictionary<string, string> Names;
		public AccidentalType(int value, char unicode, char ascii, char ope, Dictionary<string, string> names)
		{
			this.Value = value;
			this.Unicode = unicode;
			this.Ascii = ascii;
			this.Operator = ope;
			this.Names = names;
		}
		public bool Equals(char accidental) {
			if (accidental == this.Unicode || accidental == this.Ascii || accidental == this.Operator) {
				return true;
			} else {
				return false;
			}
		}
		public bool Equals(string accidental) {
			foreach (char c in accidental) { if (this.Equals(c)) { return true; } }
			return false;
		}
		public int GetValue(string accidental) {
			int count = 0;
			for (; count < accidental.Length; count++) {
				if (!this.Equals(accidental[count])) { throw new Exception("対象外の記号が使われています。"); }
			}
			return this.Value * count;
		}
	}
}
