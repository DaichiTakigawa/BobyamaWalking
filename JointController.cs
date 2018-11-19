using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JointController : MonoBehaviour {
	private Rigidbody rb;
	private float elapsed_time = 0f;
	private int idx = 0;
	private static int pose_size = 3; 

	public Vector3 default_local_y; 
	public Vector3[] aim_local_ys = new Vector3[pose_size];
	public float period = 1f;
	public float power = 10000f;

	public void Start() {
		rb = GetComponent<Rigidbody>();
		elapsed_time = 0f;
	}

	public void FixedUpdate () {
		elapsed_time += Time.fixedDeltaTime;
		idx = (int)(elapsed_time / period) % pose_size;
		Vector3 aimVec = aim_local_ys[idx].x * transform.parent.right 
						+ aim_local_ys[idx].y * transform.parent.up 
						+ aim_local_ys[idx].z * transform.parent.forward;
		Vector3 nowVec = transform.up;
		aimVec = aimVec.normalized;
		Vector3 torqueVec = Calc_torqueVec(nowVec, aimVec);
		rb.AddTorque(torqueVec, ForceMode.VelocityChange);
	}

	private Vector3 Calc_torqueVec(Vector3 nowVec, Vector3 aimVec) {
		return Vector3.Cross(nowVec, aimVec).normalized * power;
	}

	public void Init(Indivisual indivisual, int gene_idx) {
		rb = GetComponent<Rigidbody>();
		elapsed_time = 0f;
		Vector3 default_world_y = transform.rotation * Vector3.up;
		Matrix3x3 matrix = new Matrix3x3(transform.parent.right, transform.parent.up, transform.parent.forward);
		float[,] inverse_matrix = matrix.Inverse();
		default_local_y.x = inverse_matrix[0,0]*default_world_y.x + inverse_matrix[0,1]*default_world_y.y + inverse_matrix[0,2]*default_world_y.z;
		default_local_y.y = inverse_matrix[1,0]*default_world_y.x + inverse_matrix[1,1]*default_world_y.y + inverse_matrix[1,2]*default_world_y.z;
		default_local_y.z = inverse_matrix[2,0]*default_world_y.x + inverse_matrix[2,1]*default_world_y.y + inverse_matrix[2,2]*default_world_y.z;
		default_local_y = default_local_y.normalized;

		power = Decode_gene(indivisual.genes[gene_idx, Indivisual.gene_size-2], indivisual.power_lower_limit, indivisual.power_upper_limit);
		period = Decode_gene(indivisual.genes[gene_idx, Indivisual.gene_size-1], indivisual.period_lower_limit, indivisual.period_upper_limit);
		for (int i = 0; i < pose_size; ++i) {
			float theta = Decode_gene(indivisual.genes[gene_idx, i*pose_size + 0], indivisual.codon_lower_limit[0], indivisual.codon_upper_limit[0]);
			float phi = Decode_gene(indivisual.genes[gene_idx, i*pose_size + 1], indivisual.codon_lower_limit[1], indivisual.codon_upper_limit[1]);
			float angle = Decode_gene(indivisual.genes[gene_idx, i*pose_size + 2], indivisual.codon_lower_limit[2], indivisual.codon_upper_limit[2]);
			Vector3 axis = new Vector3(Mathf.Sin(theta)*Mathf.Cos(phi), Mathf.Sin(theta)*Mathf.Sin(phi), Mathf.Cos(theta));
			aim_local_ys[i] = Quaternion.AngleAxis(angle, axis) * default_local_y;
		}
	}

	private float Decode_gene(float nucleotido, float lb, float ub) {
		return lb + nucleotido * (ub -lb); 
	}

}
