using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour {
	private const int pop_size = 30;
	private const float elite_rate = 0.3f;
	private const float mutate_p = 0.02f;
	private const float cross_p = 0.1f;
	private const int generation_size = 100;
	private float elapsed_time = 0f;
	private float peirod = 6f;
	public float[,] most_efficient_genes = new float[Indivisual.genes_size, Indivisual.gene_size];
	public float most_efficient_fittness = float.MinValue;

	public GameObject softInidivisual;
	public GameObject[] population = new GameObject[pop_size];
	public float[,,] next_genes_list = new float[pop_size, Indivisual.genes_size, Indivisual.gene_size];
	public float[] fittnesses = new float[pop_size];
	public float[] accumulated_fittnesses = new float[pop_size];
	public float accumulated_max_fittness = 0f;
	public float max_fittness = float.MinValue;
	public float min_fittness = float.MaxValue;
	public float avarage_fittness = 0f;
	public int generation_count = 1; 

	public
	void Start () {
		initialze_population();
	}
	
	void FixedUpdate () {
		elapsed_time += Time.fixedDeltaTime;

		if (elapsed_time > peirod) {
			set_next_genes_list();
			create_next_generation();
			elapsed_time = 0f;
			++generation_count;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			create_most_efficient_indivisual();
		}
	}

	void initialze_population() {
		Random.InitState((int)Time.time);
		for (int i  = 0; i < pop_size; ++i) {
			population[i] = GameObject.Instantiate(softInidivisual, Vector3.zero + Vector3.right*i*4 , Quaternion.Euler(0f, 0f, 0f)); 
			population[i].GetComponent<Indivisual>().Genes_init();
			population[i].GetComponent<Indivisual>().Init();
		}
	}

	private void set_next_genes_list() {
		max_fittness = float.MinValue;
		min_fittness = float.MaxValue;
		avarage_fittness = 0f;
		for (int i = 0; i < pop_size; ++i) {
			fittnesses[i] = population[i].GetComponent<Indivisual>().Calc_fittness();
			if (fittnesses[i] > max_fittness) max_fittness = fittnesses[i];
			if (fittnesses[i] < min_fittness) min_fittness = fittnesses[i];
			avarage_fittness += fittnesses[i];
		}
		avarage_fittness /= pop_size;

		for (int i = 0; i < pop_size; ++i) {
			fittnesses[i] -= min_fittness;
		}
		List<KeyValuePair<float, int>> sorted_fittness = new List<KeyValuePair<float, int>>();
		for (int i = 0; i < pop_size; ++i) {
			sorted_fittness.Add(new KeyValuePair<float, int>(fittnesses[i], i));
		}
		MyComparer mc = new MyComparer();
		sorted_fittness.Sort(mc);

		accumulated_fittnesses[0] = fittnesses[0];
		for (int i = 1; i < pop_size; ++i) accumulated_fittnesses[i] = accumulated_fittnesses[i-1] + fittnesses[i];
		accumulated_max_fittness = accumulated_fittnesses[pop_size-1];

		int elite_size = (int)(pop_size*elite_rate);
		for (int i = 0; i < elite_size; ++i) {
			int ind_idx = sorted_fittness[i].Value;
			float[,] old_genes = population[ind_idx].GetComponent<Indivisual>().genes;
			copy_genes(next_genes_list, old_genes, i);
		}

		if ((pop_size-elite_size)%2 == 1) {
			int ind_idx = sorted_fittness[elite_size].Value;
			float[,] old_genes = population[ind_idx].GetComponent<Indivisual>().genes;
			copy_genes(next_genes_list, old_genes, elite_size);
			++elite_size;
		}

		for (int i = elite_size; i < pop_size; i += 2) {
			float[,] child_genes1 = new float[Indivisual.genes_size, Indivisual.gene_size];
			float[,] child_genes2 = new float[Indivisual.genes_size, Indivisual.gene_size];
			int parent_idx1 = select_parent_roulette();
			int parent_idx2 = select_parent_roulette();
			mk_child_genes(population[parent_idx1].GetComponent<Indivisual>().genes, 
							population[parent_idx2].GetComponent<Indivisual>().genes,
							child_genes1, child_genes2);
			copy_genes(next_genes_list, child_genes1, i);
			copy_genes(next_genes_list, child_genes2, i+1);
		}

		if (most_efficient_fittness < max_fittness) {
			most_efficient_fittness = max_fittness;
			copy_genes(most_efficient_genes, next_genes_list, 0);
		}

		return;
	}

	private void copy_genes(float[,,] destination_genes, float[,] source_genes, int idx) {
		for (int i = 0; i < Indivisual.genes_size; ++i) {
			for (int j = 0; j < Indivisual.gene_size; ++j) {
				destination_genes[idx, i, j] = source_genes[i, j];
			}
		}
		return;
	}
	private void copy_genes(float[,] destination_genes, float[,,] source_genes, int idx) {
		for (int i = 0; i < Indivisual.genes_size; ++i) {
			for (int j = 0; j < Indivisual.gene_size; ++j) {
				destination_genes[i, j] = source_genes[idx, i, j];
			}
		}
		return;
	}

	private void mutate_genes(float[,] child_genes) {
		for (int i = 0; i < Indivisual.genes_size; ++i) {
			for (int j = 0; j < Indivisual.gene_size; ++j) {
				if (Random.Range(0f, 1f) < mutate_p) child_genes[i, j] = Random.Range(0f, 1f);
			}
		}
		return;
	}

	private void cross_genes(float[,] parent_genes1, float[,] parent_genes2, float[,] child_genes1, float[,] child_genes2) {
		for (int i = 0; i < Indivisual.genes_size; ++i) {
			for (int j = 0; j < Indivisual.gene_size; ++j) {
				if (Random.Range(0f, 1f) < cross_p) {
					child_genes1[i, j] = parent_genes2[i, j];
					child_genes2[i, j] = parent_genes1[i, j];
				} else {
					child_genes1[i, j] = parent_genes1[i, j];
					child_genes2[i, j] = parent_genes2[i, j];
				}
			}
		}
		return;
	} 

	private void mk_child_genes(float[,] parent_genes1, float[,] parent_genes2, float[,] child_genes1, float[,] child_genes2) {
		cross_genes(parent_genes1, parent_genes2, child_genes1, child_genes2);
		mutate_genes(child_genes1);
		mutate_genes(child_genes2);
	}

	private int select_parent_roulette() {
		float roulette = Random.Range(0f, accumulated_max_fittness);  
		for (int i = 0; i < pop_size; ++i) {
			if (roulette < accumulated_fittnesses[i]) return i;
		}
		return pop_size-1;
	}

	private void create_next_generation() {
		for (int i = 0; i < pop_size; ++i) {
			Destroy(population[i]);
		}
		for (int i = 0; i < pop_size; ++i) {
			population[i] = GameObject.Instantiate(softInidivisual, Vector3.zero + Vector3.right*i*4 , Quaternion.Euler(0f, 0f, 0f)); 
			float[,] new_genes = population[i].GetComponent<Indivisual>().genes;
			copy_genes(new_genes, next_genes_list, i);
			population[i].GetComponent<Indivisual>().Init();
		}
	}


	private void create_most_efficient_indivisual() {
		GameObject indivisual_asset = GameObject.Instantiate(softInidivisual);
		float[,] new_genes = indivisual_asset.GetComponent<Indivisual>().genes;
		for (int i = 0; i < Indivisual.genes_size; ++i) {
			for (int j = 0; j < Indivisual.gene_size; ++j) {
				new_genes[i, j] = most_efficient_genes[i, j];
			}
		}
		indivisual_asset.GetComponent<Indivisual>().Init();
		UnityEditor.PrefabUtility.CreatePrefab("Assets/MyAssets/MostEfficientIndivisual.prefab", indivisual_asset);
		Debug.Log("created new prefab");
		GameObject.DestroyImmediate(indivisual_asset);
	}
} 