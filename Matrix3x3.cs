using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix3x3 {
	private float[,] matrix = new float[3, 3];

	public Matrix3x3(Vector3 x, Vector3 y, Vector3 z) {
		for (int i = 0; i < 3; ++i) {
			matrix[i,0] = x[i]; matrix[i,1] = y[i]; matrix[i,2] = z[i];
		}
	}

	private float Calc_det(List<float> row1, List<float> row2) {
		return row1[0]*row2[1] - row1[1]*row2[0];
	}
	
	public float[,] Inverse() {
		float[,] inverse = new float[3,3];
		for (int i = 0; i < 3; ++i) inverse[i,i] = 1f;

		float det = matrix[0,0]*matrix[1,1]*matrix[2,2] + matrix[0,1]*matrix[1,2]*matrix[2,0] + matrix[0,2]*matrix[1,0]*matrix[2,1];
		det -= matrix[0,2]*matrix[1,1]*matrix[2,0] + matrix[0,1]*matrix[1,0]*matrix[2,2] + matrix[0,0]*matrix[1,2]*matrix[2,1];
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				List<float> row1 = new List<float>(2);
				List<float> row2 = new List<float>(2);
				for (int x = 0; x < 3; ++x) {
						if (x == i) {continue;}
					for (int y = 0; y < 3; ++y) {
						if (y == j) {continue;}
						if (row1.Count != 2) row1.Add(matrix[x,y]);
						else row2.Add(matrix[x,y]);
					}
				}
				inverse[i,j] = Calc_det(row1, row2) / det * ((i+j)%2 == 0 ? 1f : -1f); 
			}
		}

		return inverse;
	}
}
