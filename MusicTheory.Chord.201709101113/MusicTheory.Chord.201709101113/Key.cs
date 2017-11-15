using System;
using System.Collections.Generic;
namespace MusicTheory.Chord
{
	/// <summary>音名.</summary>
	public class Key
	{
		/// <summary>音名ごとのPitchClass.</summary>
		public static readonly Dictionary<char,int> Keys = new Dictionary<char,int>(){{'C',0},{'D',2},{'E',4},{'F',5},{'G',7},{'A',9},{'B',11}};

		/// <summary>音程名</summary>
		public string Name { get; private set; }
		/// <summary>ピッチクラス(半音数)</summary>
		public int Pitch { get; private set; }

		public Key (string name)
		{
			if (null == name || name.Trim ().Length < 1) {this.Pitch = 0; this.Name = "";return;}
			char key = name [0];
			string accidental = name.Substring (1);
			// throw new Exception ("音名はC,D,E,F,G,A,Bのいずれかと変化記号を指定してください。");
			if (Key.Keys.ContainsKey(key)) {
				this.Pitch = (Key.Keys [key] + Accidental.GetPitch (accidental)) % 12;//-11〜0〜11。オクターブ無視
				if (this.Pitch < 0) {this.Pitch += 12;}//負数なら正数にする。
				this.Name = name;
			}
		}
	}
}

