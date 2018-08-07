using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public static Door instance;

    [SerializeField]
    protected GameObject doorOpen;
	[SerializeField]
    protected GameObject doorClose;

	public bool isOpened;

	// Use this for initialization
	void Start () {

		DoorSet(false);
	}

	void Update() {
		if (isOpened == true) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = doorOpen.GetComponent<SpriteRenderer> ().sprite;
		} else if (isOpened == false) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = doorClose.GetComponent<SpriteRenderer> ().sprite;
		}
	}

	public void Open(){
		if (isOpened == false) {
			DoorSet(true);
		}
	}

	public void Close(){
		if (isOpened == true) {
			DoorSet(false);
		}
	}

	public void Toggle(){
		if (isOpened == true) {
			Close();
		} else {
			Open();
		}
	}
	void DoorSet(bool open){
		isOpened = open;
    }
}
