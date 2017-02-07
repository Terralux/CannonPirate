using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallAbilities : MonoBehaviour {
	
	public float fartForce = 300f;

	public int maxFartCharges = 3;
	private int currentFartCharges = 3;

	public int maxBullets = 3;
	private int currentBullets = 3;

	public enum PlayerStates{
		NEUTRAL,
		SWORD,
		FLINTLOCK,
		FARTING
	}

	public PlayerStates myState = PlayerStates.NEUTRAL;

	void Awake () {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton += ActivateFlintlock;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton += ActivateSword;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn += ActivateFart;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied += Reset;

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced += ResetState;
	}

	void Start(){
		Reset ();
	}

	public void ActivateSword(){
		myState = PlayerStates.SWORD;
	}

	public void ActivateFlintlock(){
		if (currentBullets > 0) {
			myState = PlayerStates.FLINTLOCK;
			currentBullets--;
			Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBulletsUpdated (currentBullets);
		}
	}

	public void ActivateFart(){
		if (currentFartCharges > 0) {
			myState = PlayerStates.FARTING;
			Rigidbody rb = GetComponent<Rigidbody> ();
			rb.velocity = rb.velocity + new Vector3 (0, fartForce, 0);
			currentFartCharges--;
			Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFartsUpdated (currentFartCharges);
		}
	}

	public void Reset(){
		currentBullets = maxBullets;
		currentFartCharges = maxFartCharges;

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBulletsUpdated (currentBullets);
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFartsUpdated (currentFartCharges);
	}

	public void ResetState(){
		myState = PlayerStates.NEUTRAL;
	}

	public void Kill(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied ();

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton -= ActivateFlintlock;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton -= ActivateSword;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn -= ActivateFart;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied -= Reset;

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced -= ResetState;

		Destroy (gameObject);
	}

	void OnDrawGizmos(){
		switch (myState) {
		case PlayerStates.NEUTRAL:
			Gizmos.color = Color.white;
			break;
		case PlayerStates.SWORD:
			Gizmos.color = Color.green;
			break;
		case PlayerStates.FLINTLOCK:
			Gizmos.color = Color.red;
			break;
		case PlayerStates.FARTING:
			Gizmos.color = Color.yellow;
			break;
		}

		Gizmos.DrawWireSphere (transform.position, 1.3f);
	}
}