using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isMobilePlatform) {

		} else {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn ();
				}
			}
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton ();
				}
			}
			if (Input.GetMouseButtonDown (1) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton ();
				}
			}
		}
	}
}
