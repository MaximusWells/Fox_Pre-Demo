using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGameButton : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
		GameManager.instance.playerIsPoped = false;
		SceneManager.LoadScene (sceneIndex);
	}
}