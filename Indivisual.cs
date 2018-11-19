using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indivisual : MonoBehaviour {
	public static int codon_size = 3;
	public static int gene_size = 11;
	public static int genes_size = 10;


	private Vector3 start_pos;
	public float[] codon_upper_limit = new float[3]{0f, -Mathf.PI, -180f};
	public float[] codon_lower_limit = new float[3]{Mathf.PI/2, Mathf.PI, 180f};
	public float period_upper_limit = 5f;
	public float period_lower_limit = 0.01f;
	public float power_upper_limit = 100000000f;
	public float power_lower_limit = 100f;


	public float[,] genes = new float[genes_size, gene_size];
	public List<JointController> joint_controllers;

	public float Calc_fittness() {
		return joint_controllers[0].transform.position.z - start_pos.z; 
	}

	public void Init() {

		for (int i = 0; i < transform.childCount; ++i) {
			GameObject child = transform.GetChild(i).gameObject;
			if (child.GetComponent<JointCollector>() != null) {
				child.GetComponent<JointCollector>().Collector(this);
			}
		}

		start_pos = joint_controllers[0].transform.position;
	}

	public void Genes_init() {
		for (int i = 0; i < genes_size; ++i) {
			for (int j = 0; j < gene_size; ++j) {
					genes[i,j] = Random.Range(0f, 1f);
			}
		}
	}
}