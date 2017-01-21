using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			Destroy (col.gameObject);

			if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied != null) {
				Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied ();
			} else {
				Debug.LogWarning ("No Listeners found for the Event OnPlayerDied");
			}
		}
	}

}