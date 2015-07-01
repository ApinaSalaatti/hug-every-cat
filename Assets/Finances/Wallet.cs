using UnityEngine;
using System.Collections;

public class Wallet : MonoBehaviour {
	private int money;
	public int Money {
		get { return money; }
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
}
