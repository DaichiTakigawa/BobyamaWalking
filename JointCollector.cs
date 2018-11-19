using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCollector : MonoBehaviour {
	public void Collector(Indivisual indivisual) {
		for (int i = 0; i < transform.childCount; ++i) {
			GameObject child = transform.GetChild(i).gameObject;
			if (child.GetComponent<JointController>() != null) {
				indivisual.joint_controllers.Add(child.GetComponent<JointController>());
				child.GetComponent<JointController>().Init(indivisual, indivisual.joint_controllers.Count-1);
			}
			if (child.GetComponent<JointCollector>() != null) {
				child.GetComponent<JointCollector>().Collector(indivisual);	
			}
		}
	}

}
