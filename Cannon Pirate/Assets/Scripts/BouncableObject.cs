using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncableObject : MonoBehaviour {

	[Range(-10,10)]
	public float bounceForce;
	[Range(0,10)]
	public float upwardsForce;

	public Transform nextTarget;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("Player")) {
			Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();
			rb.velocity = (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
			rb.transform.LookAt (new Vector3(nextTarget.position.x, rb.transform.position.y, nextTarget.position.z));
		}
	}

	private Transform playerTransform;

	void OnDrawGizmos(){
		if (playerTransform == null) {
			playerTransform = GameObject.FindGameObjectWithTag ("CannonPosition").transform;
		}

		if (playerTransform == null) {
			return;
		}

		if (CompareTag ("CannonTarget")) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (playerTransform.position, transform.position);
		}

		if (nextTarget != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (transform.position, (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0) + transform.position);
		}
	}
}