using UnityEngine;
using System.Collections;

public class PauseGUI : MonoBehaviour {
	public void Resume() {
		GlobalInput.Instance.Unpause();
	}

	public void ExitGame() {
		GameState.Instance.ExitGame();
	}
}
