using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyIndicator : MonoBehaviour {
	[SerializeField]
	private Wallet wallet;

	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "$" + wallet.Money.ToString();
	}
}
