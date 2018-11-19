using System.Collections;
using System.Collections.Generic;

public class MyComparer : IComparer<KeyValuePair<float, int>> {
	public int Compare(KeyValuePair<float, int> x, KeyValuePair<float, int> y) {
		if (x.Key > y.Key) return -1;
		else if (x.Key < y.Key) return 1;
		else {
			if (x.Value > y.Value) return -1;
			else if (x.Value < y.Value) return 1;
		}
		return 0;
	}
}
