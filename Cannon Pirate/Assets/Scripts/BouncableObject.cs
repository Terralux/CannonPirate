using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncableObject : MonoBehaviour {

	[Range(-10,10)]
	public float bounceForce;
	[Range(0,10)]
	public float upwardsForce;

	//public float forceModifier = 100;

	private Vector3 reflectedAngle;

	public Transform nextTarget;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("CannonBall")) {
			Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();
			rb.velocity = reflectedAngle * bounceForce + new Vector3 (0, upwardsForce, 0);
		}
	}

	private Transform playerTransform;

	void OnDrawGizmos(){
		if (playerTransform == null) {
			playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		if (playerTransform == null) {
			return;
		}

		Gizmos.color = Color.blue;
		Gizmos.DrawLine (playerTransform.position, transform.position);
		Gizmos.color = Color.red;
		reflectedAngle = Vector3.Reflect (transform.position - playerTransform.position, transform.forward);
		Gizmos.DrawLine (transform.position, transform.position + reflectedAngle * bounceForce + new Vector3 (0, upwardsForce, 0));
	}

}