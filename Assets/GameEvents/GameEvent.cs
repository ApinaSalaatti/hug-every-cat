using UnityEngine;
using System.Collections;

public class GameEvent {
	// All possible event types. There's probably some really cool and beautiful way to do this.
	public static int CAT_CREATED = 1;
	public static int HOUSE_ITEM_ADDED = 2;
	public static int HOUSE_ITEM_REMOVED = 3;

	private int gameEventType;
	public int GameEventType {
		get { return gameEventType; }
	}

	private System.Object gameEventData;
	public System.Object GameEventData {
		get { return gameEventData; }
	}

	public GameEvent(int type, System.Object data) {
		gameEventType = type;
		gameEventData = data;
	}
}
