using System;
using System.Collections.Generic;
namespace MusicTheory.Chord
{
	/// <summary>音度.</summary>
	public class Degree
	{
		/// <summary>完全系</summary>
		public static readonly List<int> Perfects = new List<int> () {1, 4, 5};
		/// <summary>長短系</summary>
		public static readonly List<int> Majors = new List<int> () {2, 3, 6, 7};
		/// <summary>完全、長短系のピッチクラス</summary>
		public static readonly Dictionary<int, int> Pitchs = new Dictionary<int, int>()
		{{1, 0},{2, 2},{3, 4},{4, 5},{5, 7},{6, 9},{7, 11}};
		/// <summary>音度名</summary>
		public string Name { get; private set; }
		/// <summary>ピッチクラス</summary>
		public int Pitch { get; private set; }
		/// <summary>音度名からPitchClassを算出する。</summary>
		/// <param name="name">音度名(1,#2,b3,+4,-5,♯6,♭7,##8等)</param>
		public Degree(string name){ this.SetPitch(name); }
		private void SetPitch(string name) {
			// [変化記号]?[音度]
			// 1,-2,+3,--4
			System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match (name, @"(?<accidential>[+|\-|#|b|♯|♭]*)(?<degree>[1-9][0-9]?)");
			int d = int.Parse (match.Groups ["degree"].Value);
			if (d < 1 || 14 < d) { throw new Exception ("度数は1〜14の自然数にしてください。"); }
			int octavePitch = 0;
			if (7 < d) { octavePitch = 12; }
			int a = Accidental.GetPitch(match.Groups ["accidential"].Value);
			this.Pitch = Degree.Pitchs [(d < 8) ? d : d - 7] + a + octavePitch;
			this.Name = name;
		}
	}
}

