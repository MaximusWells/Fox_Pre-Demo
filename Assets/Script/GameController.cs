using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance;

	protected int collectedGems;
    protected int collectedCherrys;
    protected int lifes;
	[SerializeField]
    protected Text haveGems;
	[SerializeField]
    protected Text haveCherrys;
	[SerializeField]
    protected Text haveLifes;
    protected Scene thisScene;


	void Awake(){
		Instancenate ();
		CalculateItems ();
		thisScene = SceneManager.GetActiveScene ();
	}


	void OnEnable(){
		SceneManager.sceneLoaded += LevelIsLoaded;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= LevelIsLoaded;
	}
	void LevelIsLoaded(Scene scene, LoadSceneMode sceneMode)
    {
		if (scene.name == thisScene.name)
        {
			if (!GameManager.instance.playerIsPoped && !GameManager.instance.levelComplited) {
				NewGameValues ();
			} else {
				ContinueGameValues ();
			}
			CalculateItems ();
			if (!GameManager.instance.playerIsPoped && GameManager.instance.levelComplited == true) {
				ContinueGameValues ();
			}
		}
    }

    public void NewGameValues()
    {
        collectedGems = 0;
        collectedCherrys = 0;
        lifes = 3;
    }

    public void ContinueGameValues()
    {
        collectedGems = GameManager.instance.collectedGems;
        collectedCherrys = GameManager.instance.collectedCherrys;
        lifes = GameManager.instance.lifes;
    }

	public void LevelComplited(bool yes)
	{
		GameManager.instance.levelComplited = yes;
		GameManager.instance.collectedGems = collectedGems;
		GameManager.instance.collectedCherrys = collectedCherrys;
		GameManager.instance.lifes = lifes;
	}

    public void GameOver(){
		if (lifes <= 0){
				SceneManager.LoadScene("EndGame");
				GameManager.instance.levelComplited = false;
		}else if(lifes > 0){
			SceneManager.LoadScene(thisScene.name);
			GameManager.instance.playerIsPoped = true;
			GameManager.instance.collectedGems = collectedGems;
			GameManager.instance.collectedCherrys = collectedCherrys;
			GameManager.instance.lifes = lifes;
		}
	}

	public void LifeUp(){
		if (collectedCherrys >= 100) {
			lifes++;
			collectedCherrys = 0;
			CalculateItems ();
		}
	}

	private void Instancenate(){
		if (instance == null) {
			instance = this;
		}
	}

	public void CalculateItems (){
		haveGems.text = "" + collectedGems.ToString ();
		haveCherrys.text = "" + collectedCherrys.ToString ();
		haveLifes.text = "Lifes = " + lifes.ToString ();
	}

	public void AddGem(){
		collectedGems++;
		CalculateItems ();
	}

	public void AddCherry(){
		collectedCherrys++;
		CalculateItems ();
		LifeUp ();
	}

	public void DebuffGem(){
		collectedGems--;
		CalculateItems ();
        if(collectedGems <= 0)
        {
            collectedGems = 0;
            CalculateItems();
        }
	}

	public void GetHurt(){
		lifes--;
		CalculateItems ();
		GameOver ();
	}
}
