using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

	public Transform LookAt;
    protected Vector3 offset = new Vector3 (0, 0, -10);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// make the camera to look at player
		transform.position = LookAt.transform.position + offset;
	}
}
