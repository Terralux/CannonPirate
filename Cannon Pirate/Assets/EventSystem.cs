using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour{

	public delegate void voidEvent();

	public voidEvent OnStartGame;
	public voidEvent OnPlayerDied;
	public voidEvent OnPlayerReachedGoal;
	public voidEvent OnPlayerRanOutOffLives;
	public voidEvent OnPlayerFiredCannon;

	public voidEvent OnPlayerSelectedALevel;
	public voidEvent OnPlayerSelectedWatchAnAdd;

	public voidEvent OnLoadedLevel;
	public voidEvent OnLoadingNextLevel;
}