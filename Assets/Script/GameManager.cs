using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance;

	public int collectedGems;
	public int collectedCherrys;
	public int lifes;
	public bool playerIsPoped;
	public bool levelComplited;

	void Awake(){
		Singleton ();
	}

	private void Singleton(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

}
