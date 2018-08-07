using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySwitcher : MonoBehaviour {

	[SerializeField]
    protected GameObject switchOn;
	[SerializeField]
    protected GameObject switchOff;
    protected bool isOn;
	[SerializeField]
    protected GameObject target;
    protected Animator switchAnimator;
    protected bool onSwitch;

	// Use this for initialization
	void Start () {
		switchAnimator = GetComponent<Animator>();
		isOn = false;
	}

	void Update() {
		if (onSwitch == true && Input.GetKeyDown (KeyCode.E)) {
			Use();
		}
	}

	void SwitchOn(){
		if (isOn == false) {
			SwitcherState(true);
			switchAnimator.Play ("Switcher_on");
		}
	}

	void SwitchOff(){
		if (isOn == true) {
			SwitcherState(false);
			switchAnimator.Play ("Switcher_off");
		}
	}

	void Toggle () {
		if (isOn == true) {
			SwitchOff();
		} else if (isOn == false) {
			SwitchOn();
		}
	}

	void SwitcherState (bool on) {
		isOn = on;
	}

	void Use(){
		Toggle ();
		target.GetComponent<Door> ().Toggle();
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag ("Player")) {
			onSwitch = true;
			}
		}
	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag ("Player")) {
			onSwitch = false;
		}
	}
}
