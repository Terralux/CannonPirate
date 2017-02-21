using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Usecase example for the Toolbox behaviour.
/// The most important aspect of using the Toolbox is to be aware of race conditions
/// I prefer to assign every Component in Awake and make my calls in Start or later
/// </summary>
public class UsecaseExample : MonoBehaviour {

	[Range(1,255)]
	public int randomValueLimit;

	private int maxHealth = 9999;
	[HideInInspector]
	public int curHealth;

	// I use Awake to register my component to avoid race conditions
	void Awake(){
		Toolbox.RegisterComponent<UsecaseExample> (this);

		curHealth = maxHealth;
	}

	// Next I find my component in Start and reset my health
	void Start () {
		Toolbox.FindRequiredComponent<UsecaseExample> ().ResetHealth ();

		//Next thing i do is start a timer dealing damage every 3 seconds
		StartCoroutine (WaitForNextDamage ());
	}

	// This method is called through the toolbox as seen in start
	public void ResetHealth() {
		curHealth = maxHealth;
	}

	// This Method is also called through the toolbox as seen in WaitForNextDamage
	public bool AdjustHealth(int adjustmentValue){
		curHealth += adjustmentValue;

		curHealth = curHealth > maxHealth ? maxHealth : curHealth;

		if (curHealth <= 0) {
			curHealth = 0;
			return true;
		}
		return false;
	}

	// This method is called regularly
	public int GetRandomNumber (){
		return Random.Range (-randomValueLimit, randomValueLimit);
	}

	IEnumerator WaitForNextDamage(){
		yield return new WaitForSeconds (3f);
		bool playerIsDead = Toolbox.FindRequiredComponent<UsecaseExample> ().AdjustHealth (GetRandomNumber ());
		if (!playerIsDead) {
			StartCoroutine (WaitForNextDamage ());
		}
	}
}