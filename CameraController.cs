using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float move_speed = 10f;
	public float rotation_speed = 10f;

	public GameObject[] my_camera = new GameObject[3];

	void Start () {
		
	}
	
	void Update () {
		/*
		Vector3 move_vec = new Vector3();
		if (Input.GetKey(KeyCode.W)) { move_vec += transform.forward; }
		if (Input.GetKey(KeyCode.S)) { move_vec += -transform.forward; }
		if (Input.GetKey(KeyCode.D)) { move_vec += transform.right; }
		if (Input.GetKey(KeyCode.A)) { move_vec += -transform.right; }
		move_vec = move_vec.normalized;

		Vector3 rotation_vec = new Vector3();
		if (Input.GetKey(KeyCode.UpArrow)) { rotation_vec += -Vector3.right; }
		if (Input.GetKey(KeyCode.DownArrow)) { rotation_vec += Vector3.right; }
		if (Input.GetKey(KeyCode.RightArrow)) { rotation_vec += Vector3.up; }
		if (Input.GetKey(KeyCode.LeftArrow)) { rotation_vec += -Vector3.up; }
		rotation_vec = rotation_vec.normalized;

		transform.position += move_vec * move_speed * Time.deltaTime;
		transform.rotation *= Quaternion.AngleAxis(rotation_speed * Time.deltaTime, rotation_vec);
		*/

		if (Input.GetKeyDown(KeyCode.Alpha1)) { Change_camera(0); }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { Change_camera(1); }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { Change_camera(2); }

	}

	void Change_camera(int n) {
		for (int i = 0; i < 3; ++i) {
			if (i == n) my_camera[i].GetComponent<Camera>().enabled = true;
			else my_camera[i].GetComponent<Camera>().enabled = false;
		}
	}
}
