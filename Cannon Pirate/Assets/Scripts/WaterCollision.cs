using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour {
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			Destroy (col.gameObject);
			col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
		}
	}
}