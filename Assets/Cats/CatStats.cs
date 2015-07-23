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
	
	void Save(JSONObject json) {
		JSONObject stats = new JSONObject(JSONObject.Type.OBJECT);
		stats.AddField("name", Name);
		stats.AddField("bodyType", BodyType.ToString());
		stats.AddField("gender", Gender.ToString());
		
		json.AddField("stats", stats);
	}
	
	void Load(JSONObject json) {
		JSONObject stats = json.GetField("stats");
		Name = name = stats.GetField("name").str; // Set the gameobject's name also
		string btString = stats.GetField("bodyType").str;
		string gString = stats.GetField("gender").str;
		
		if(btString.Equals(BodyType.SKINNY.ToString()))
			BodyType = BodyType.SKINNY;
		else if(btString.Equals(BodyType.MEDIUM.ToString()))
			BodyType = BodyType.MEDIUM;
		else if(btString.Equals(BodyType.FAT.ToString()))
			BodyType = BodyType.FAT;
		
		if(gString.Equals(Gender.FEMALE.ToString()))
			Gender = Gender.FEMALE;
		else if(gString.Equals(Gender.MALE.ToString()))
			Gender = Gender.MALE;
		else
			Gender = Gender.UNKNOWN;
	}
}
