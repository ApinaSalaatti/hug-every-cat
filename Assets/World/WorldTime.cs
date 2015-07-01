using UnityEngine;
using System.Collections;

public class WorldTime : MonoBehaviour {
	private int day = 0;
	private int hour = 0;
	private int minute = 0;

	private float minuteLength = 1f;
	private float minuteTimer = 0f;

	// Use this for initialization
	void Start () {
	
	}

	void WorldUpdate (float deltaTime) {
		minuteTimer += deltaTime;
		if(minuteTimer >= minuteLength) {
			minuteTimer = 0f;
			minute++;
			if(minute == 60) {
				minute = 0;
				hour++;
				if(hour == 24) {
					hour = 0;
					day++;
				}
			}
		}
	}

	public string GetTimeString(string format = "h:m") {
		string ret = format;
		ret = ret.Replace("h", hour.ToString("00"));
		ret = ret.Replace("m", minute.ToString("00"));
		ret = ret.Replace("d", day.ToString());
		return ret;
	}
}
