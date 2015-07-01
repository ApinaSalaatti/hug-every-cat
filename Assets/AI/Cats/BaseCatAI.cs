using UnityEngine;
using System.Collections;

// An abstract class that different AI implementations (if I actually ever make many) can inherit from
public abstract class BaseCatAI {
	private CatBrain brain;
	public CatBrain Brain { get { return brain; } }

	public BaseCatAI(CatBrain b) {
		brain = b;
	}

	public abstract void AIUpdate(float deltaTime);
	public abstract bool HandleMessage(AIMessage msg);
}
