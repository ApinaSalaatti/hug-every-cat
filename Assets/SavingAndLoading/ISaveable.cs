using UnityEngine;
using System.Collections;

// An interface for objects that know how to save/load themselves into/from JSON
public interface ISaveable {
	void LoadGame();
	void SaveGame();
	void StartNewGame();
}
