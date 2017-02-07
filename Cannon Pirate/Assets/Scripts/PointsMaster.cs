using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointsMaster : MonoBehaviour {

	public int lives = 3;

	private int currentBounces = 0;
	private int totalBounces = 0;
	private int bestBounces = 1000000;

	// Use this for initialization
	void Awake () {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBounced += AddBounce;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied += EndBounceRound;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerReachedGoal += ReachedGoal;
		Toolbox.FindRequiredComponent<EventSystem> ().OnStartGame += ResetLives;
	}

	void ResetLives() {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerLivesUpdated (lives);
	}

	public void AddBounce(){
		currentBounces++;
		totalBounces++;
	}

	public void ReachedGoal(){
		if (currentBounces < bestBounces) {
			bestBounces = currentBounces;
		}
	}

	public void EndBounceRound(){
		currentBounces = 0;
		lives--;

		if (lives > 0) {
			Toolbox.FindRequiredComponent<EventSystem> ().OnStartNewRound ();
		} else {
			if (Application.isEditor) {
				lives = 3;
				Toolbox.FindRequiredComponent<EventSystem> ().OnStartNewRound ();
				Debug.LogWarning ("You ran out of lives, but because you're using the editor, the game resets");
			} else {
				Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerRanOutOffLives ();
			}
		}

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerLivesUpdated (lives);
	}
}