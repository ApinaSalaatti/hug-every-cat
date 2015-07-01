using UnityEngine;
using System.Collections;

public enum AIMessageType {
	TERMINATE_CURRENT_GOAL, NEW_INTERESTING_OBJECT, NEED_LOW, NEED_CRITICALLY_LOW
}

public class AIMessage {
	private AIMessageType type;
	public AIMessageType Type { get { return type; } }

	private System.Object data;
	public System.Object Data { get { return data; } }

	public AIMessage(AIMessageType t, System.Object d) {
		type = t;
		data = d;
	}
}
