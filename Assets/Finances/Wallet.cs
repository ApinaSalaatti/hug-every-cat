using UnityEngine;
using System.Collections;

public class Wallet : MonoBehaviour, ISaveable {
	private int money;
	public int Money {
		get { return money; }
	}

	void Awake() {
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
		Debug.Log("Creating new wallet...");
		AddMoney(100);
	}

	public void SaveGame() {
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("money", money);

		using(System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.SaveFolder + "wallet")) {
			file.WriteLine(json.Print());
		}
	}

	public void LoadGame() {
		string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "wallet");
		JSONObject json = new JSONObject(lines[0]);
		money = (int)json.GetField("money").n;
	}
}
