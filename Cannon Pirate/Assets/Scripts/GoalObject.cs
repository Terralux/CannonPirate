using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {
	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("Player")) {
			if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerReachedGoal != null) {
				Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerReachedGoal ();
			}
		}
	}
}