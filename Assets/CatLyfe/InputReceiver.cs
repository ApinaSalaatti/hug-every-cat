using UnityEngine;
using System.Collections;

// An interface that a class must implement if it is to receive input from the player
public interface InputReceiver {
	bool ReceiveInput();
}
