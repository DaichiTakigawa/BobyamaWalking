using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
	private Text generation_count;
	private Text max_fittness;
	private Text min_fittness;
	private Text avarage_fittness;

	private Generation generation;

	void Start() {
		generation = GameObject.FindGameObjectWithTag("Generation").GetComponent<Generation>();
		generation_count = GameObject.Find("GenerationCount").GetComponent<Text>();
		max_fittness = GameObject.Find("MaxFittness").GetComponent<Text>();
		min_fittness = GameObject.Find("MinFittness").GetComponent<Text>();
		avarage_fittness = GameObject.Find("AvarageFittness").GetComponent<Text>();
	} 

	 void Update() {
		 generation_count.text = "第 " + generation.generation_count + " 世代";
		 max_fittness.text = "最大移動距離: " + generation.max_fittness + "m";
		 min_fittness.text = "最小移動距離: " + generation.min_fittness + "m";
		 avarage_fittness.text = "平均移動距離: " + generation.avarage_fittness + "m";
		 if (generation.generation_count == 1) {
			max_fittness.text = "最大移動距離: " + 0 + "m";
			min_fittness.text = "最小移動距離: " + 0 + "m";
			avarage_fittness.text = "平均移動距離: " + 0 + "m";
			 
		 }
	 }

}
