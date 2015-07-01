using UnityEngine;
using System.Collections;

public enum Gender { MALE, FEMALE, UNKNOWN }
public enum BodyType { SKINNY, MEDIUM, FAT }

public class CatStats : MonoBehaviour {
	// Basic info
	public string Name {
		get; set;
	}
	public BodyType BodyType {
		get; set;
	}
	public Gender Gender {
		get; set;
	}

	void Awake() {

	}

	public void Save(JSONObject json) {
		json.AddField("name", Name);
		json.AddField("bodyType", BodyType.ToString());
		json.AddField("gender", Gender.ToString());
	}

	public void Load(JSONObject json) {

	}
}
