using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectiveLevelData", menuName = "Ka-Pirate/CollectiveLevelData", order = 0)]
public class CollectiveLevelData : ScriptableObject{
	[SerializeField]
	public List<LevelDataScritableObject> allLevels = new List<LevelDataScritableObject> ();

}
