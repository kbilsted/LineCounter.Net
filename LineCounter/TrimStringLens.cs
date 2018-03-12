using System;
using System.Runtime.CompilerServices;

namespace KbgSoft.LineCounter {
	public class TrimStringLens {
		/// <summary>
		///     points to the end index +1 of the _value - OR - at the index after the last char if the whole input is trimmed
		/// </summary>
		private int _end;

		/// <summary>
		///     points to the start index of the _value - OR - at the index after the last char if the whole input is trimmed
		/// </summary>
		private int _start;

		private string _value;

		public TrimStringLens() {
		}

		public TrimStringLens(string nonNullString) {
			_value = nonNullString;

			Prepare();
		}

		public int Length => _end - _start;

		public TrimStringLens SetValue(string newString) {
			_value = newString;
			Prepare();
			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Prepare() {
			var len = _value.Length;
			for (_start = 0; _start < len; _start++)
				if(!char.IsWhiteSpace(_value, _start))
					break;

			_end = Math.Max(_start, _value.Length);
			if (_start < _value.Length) {
				for (var i = _value.Length - 1; i >= _start; i--) {
					if (!char.IsWhiteSpace(_value, i))
						break;
					_end--;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool StartsWithOrdinal(string s) {
			var count = s.Length;
			var endSearch = _start + count;
			if (endSearch > _end)
				return false;

			var sIndex = 0;
			for (var i = _start; i < endSearch;) {
				if (_value[i] != s[sIndex])
					return false;
				i++;
				sIndex++;
			}

			return true;


			//if (s.Length > Length)
			//	return false;
			//return 0 == string.CompareOrdinal(_value, _start, s, 0, s.Length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsWhitespace() {
			return Length == 0;
		}

		public static bool operator ==(TrimStringLens lens, string s) {
			if (lens._start == lens._value.Length) return s == "";

			if (lens.Length != s.Length)
				return false;

			var sIndex = 0;
			for (var lensIndex = lens._start; sIndex < s.Length;) {
				if (s[sIndex] != lens._value[lensIndex])
					return false;
				lensIndex++;
				sIndex++;
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(TrimStringLens lens, string s) {
			return !(lens == s);
		}

		public override bool Equals(object obj) {
			var otherLens = (TrimStringLens) obj;
			return _value == otherLens._value && _start == otherLens._start && _end == otherLens._end;
		}

		public override string ToString() {
			return _value.Substring(_start, Length);
		}
	}
}