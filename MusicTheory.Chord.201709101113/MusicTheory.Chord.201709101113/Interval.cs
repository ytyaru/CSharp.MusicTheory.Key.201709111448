using System;
using System.Collections.Generic;
namespace MusicTheory.Chord
{
	/// <summary>
	/// 音程.
	/// </summary>
	public class Interval
	{
		// P1,M2,m3,a4,d5
		public static readonly List<int> Perfects = new List<int>(){1,4,5};
		public static readonly List<int> Majors = new List<int>(){2,3,6,7};
		public static readonly List<char> Prefixes = new List<char>(){'P','M','m','a','d'};
		public static readonly Dictionary<char, int[]> Types = new Dictionary<char, int[]>(){{'P', new int[]{1,4,5}}, {'M', new int[]{2,3,6,7}}, {'m', new int[]{2,3,6,7}}, {'a', new int[]{1,2,3,4,5,6,7}}, {'d', new int[]{1,2,3,4,5,6,7}}};

		/// <summary>音程名</summary>
		public string Name { get; private set; }
		/// <summary>ピッチクラス(半音数)</summary>
		public int Pitch { get; private set; }

		public Interval (string name)
		{
			if (null == name || name.Trim ().Length < 2) {this.Pitch = 0; this.Name = "";return;}
			char prefix = name [0];
			int degree = int.Parse(name.Substring (1));
			// throw new Exception ("無効な文字列です。[prefix][degree]の文字列を入力してください。M1,P2,などprefixとdegreeには無効な組合せが存在する点に注意してください。prefix=P,M,m,a,d, degree=1〜14。P=1,4,5(8,11,12), M|m=2,3,6,7(9,10,13,14), a|d=1〜14");
			if (this.Valid (prefix, degree)) {
				this.Pitch = this.GetPitch (prefix, degree);
				this.Name = name;
			}
		}
		private bool Valid(char prefix, int degree)
		{
			if (!Interval.Prefixes.Contains(prefix)) {return false;}//throw new Exception ("prefixはP,M,m,a,dのいずれかを入力してください。");
			if (14 < degree) {return false;}//throw new Exception ("数は1〜14までの自然数にしてください。");
			foreach (KeyValuePair<char, int[]> kv in Interval.Types) {
				if (prefix != kv.Key) {	continue; }
				foreach (int d in kv.Value) {if (d == degree || d + 7 == degree) {return true;}}
			}
			return false;
		}
		/// <summary>第一音からの半音数を取得する。</summary>
		/// <returns>The pitch.</returns>
		/// <param name="prefix">Prefix.</param>
		/// <param name="degree">Degree.</param>
		private int GetPitch(char prefix, int degree)
		{
			int singleDegree = (degree < 8) ? degree : degree - 7;//単音程にする
			int octave = (degree < 8) ? 0 : 12;
			return Degree.Pitchs [singleDegree] + octave + this.GetTypePitch(prefix, singleDegree);
		}
		/// <summary>prefixにおける変化量を半音数で返す。</summary>
		/// <returns>変化量(半音数)</returns>
		/// <param name="prefix">Prefix.</param>
		/// <param name="singleDegree">Single degree(単音程(1〜7)).</param>
		private int GetTypePitch(char prefix, int singleDegree)
		{
			if ('P' == prefix || 'M' == prefix) {return 0;} 
			else if ('m' == prefix) {return -1;} 
			else if ('a' == prefix) {return +1;} 
			else if ('d' == prefix) {
				if (Interval.Perfects.Contains (singleDegree)) {return -1;} 
				else if (Interval.Majors.Contains (singleDegree)) {return -2;}
				else {throw new Exception ("singleDegreeは1〜7までの自然数を入力してください。");}
			} else {throw new Exception ("prefixはP,M,m,a,dのいずれかを入力してください。");}
		}
	}
}
