using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncableObject : MonoBehaviour {

	[Range(-100,100)]
	public float bounceForce;
	[Range(0,100)]
	public float upwardsForce;

	public Transform nextTarget;

	public enum BouncableObjectTypes {
		REGULAR,
		CUTABLE,
		SHOOTABLE,
		ONE_TIME_USE,
		MULTIPLE_DIRECTIONS
	}

	public BouncableObjectTypes myType;

	public bool isReflectingPlayer = true;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("Player")) {
			
			Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();

			CannonBallAbilities.PlayerStates currentState = col.gameObject.GetComponent<CannonBallAbilities> ().myState;

			switch (myType) {
			case BouncableObjectTypes.REGULAR:
				if (currentState != CannonBallAbilities.PlayerStates.NEUTRAL && currentState != CannonBallAbilities.PlayerStates.FARTING) {
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
				} else {
					if (isReflectingPlayer) {
						rb.velocity = (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
						rb.transform.LookAt (new Vector3 (nextTarget.position.x, rb.transform.position.y, nextTarget.position.z));
					} else {
						rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					}
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced ();
				}
				break;
			case BouncableObjectTypes.CUTABLE:
				switch(currentState){
				case CannonBallAbilities.PlayerStates.NEUTRAL:
					rb.velocity = (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					rb.transform.LookAt (new Vector3 (nextTarget.position.x, rb.transform.position.y, nextTarget.position.z));
					break;
				case CannonBallAbilities.PlayerStates.SWORD:
					rb.velocity += new Vector3 (0, upwardsForce, 0);
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced ();
					//Cut this Object and proceed
					break;
				case CannonBallAbilities.PlayerStates.FLINTLOCK:
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
					//End movement
					break;
				case CannonBallAbilities.PlayerStates.FARTING:
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
					//End movement
					break;
				}
				break;
			case BouncableObjectTypes.SHOOTABLE:
				switch(currentState){
				case CannonBallAbilities.PlayerStates.NEUTRAL:
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
					break;
				case CannonBallAbilities.PlayerStates.SWORD:
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
					break;
				case CannonBallAbilities.PlayerStates.FLINTLOCK:
					//rb.velocity += new Vector3 (0, upwardsForce, 0);
					rb.velocity = (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					rb.transform.LookAt (new Vector3 (nextTarget.position.x, rb.transform.position.y, nextTarget.position.z));
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced ();
					break;
				case CannonBallAbilities.PlayerStates.FARTING:
					col.gameObject.GetComponent<CannonBallAbilities> ().Kill ();
					break;
				}
				break;
			case BouncableObjectTypes.ONE_TIME_USE:
				if (isReflectingPlayer) {
					rb.velocity = (nextTarget.position - transform.position).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					rb.transform.LookAt (new Vector3 (nextTarget.position.x, rb.transform.position.y, nextTarget.position.z));
				} else {
					rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
				}
				Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced ();

				Debug.LogWarning ("The Object was destroyed rather than playing an animation");
				Destroy (transform.parent.gameObject);
				break;
			case BouncableObjectTypes.MULTIPLE_DIRECTIONS:
				switch(currentState){
				case CannonBallAbilities.PlayerStates.NEUTRAL:
					rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					break;
				case CannonBallAbilities.PlayerStates.SWORD:
					rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0) + rb.transform.right * bounceForce;
					rb.transform.LookAt (rb.transform.position + new Vector3(rb.velocity.x, 0, rb.velocity.z));
					break;
				case CannonBallAbilities.PlayerStates.FLINTLOCK:
					rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0) + rb.transform.right * -1 * bounceForce;
					rb.transform.LookAt (rb.transform.position + new Vector3(rb.velocity.x, 0, rb.velocity.z));
					break;
				case CannonBallAbilities.PlayerStates.FARTING:
					rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z).normalized * bounceForce + new Vector3 (0, upwardsForce, 0);
					break;
				}
				break;
			}
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

		if (myType == BouncableObjectTypes.MULTIPLE_DIRECTIONS) {
			
		}
	}
}