using UnityEngine;
using System.Collections;

public class Wallet : MonoBehaviour, ISaveable {
	private int money;
	public int Money {
		get { return money; }
	}

	void Start() {
		GameSaveLoad.Instance.AddSaveable(this);
	}

	public void AddMoney(int amount) {
		money += amount;
	}
	public void TakeMoney(int amount) {
		if(money >= amount) {
			money -= amount;
		}
		else {
			money = 0;
		}
	}

	public void StartNewGame() {
		AddMoney(100);
	}

	public void LoadGame() {

	}

	public void SaveGame() {

	}
}
