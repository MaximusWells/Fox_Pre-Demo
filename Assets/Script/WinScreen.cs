using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {

	[SerializeField]
    protected GameObject salute;
    protected Vector2 randomPos;

	// Use this for initialization
	void Start () {
		StartCoroutine (coroutine());
	}

	IEnumerator coroutine () {
		while (true) {
			Instantiate (salute, randomPos, Quaternion.identity);
			yield return new WaitForSeconds (0.3f);
		}
	}

	// Update is called once per frame
	void Update () {
		randomPos = new Vector2 (Random.Range (-70, 70), Random.Range (-50, 50));

	}
}
