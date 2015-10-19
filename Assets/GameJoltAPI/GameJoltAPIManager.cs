using UnityEngine;
using System.Collections;

public delegate void OnVerifyUserCallback(bool success);
public delegate void OnGetScoresCallback(GJScore[] scores);
public delegate void OnAddScoreCallback(bool success);

public class GameJoltAPIManager : MonoBehaviour {
	private static int gameID = 94651;
	private static string privateKey = "";
	private static string username;
	private static string userToken;
	
	private static bool initiated;
	
	public static string verifiedUsername;
	
	public static OnVerifyUserCallback VerifyUserCallback;
	public static OnGetScoresCallback GetScoresCallback;
	public static OnAddScoreCallback AddScoreCallback;
	
	void Awake() {
		if(!initiated) {
			initiated = true;
			GJAPI.Init(gameID, privateKey);
			GJAPI.Users.VerifyCallback += OnUserVerified;
			GJAPI.Scores.AddCallback += OnScoreAdded;
			GJAPI.Scores.GetMultipleCallback += OnGetScores;
		}
	}
	
	public static void SendScore(uint score, string name) {
		//Debug.Log("SCORE TO SEND: " + score);
		if(UserIsVerified()) {
			GJAPI.Scores.Add(score + "pts", score, 0, "");
		}
		else {
			if(name != null && name != "") {
				GJAPI.Scores.AddForGuest(score + "pts", score, name, 0, "");
			}
			else {
				OnScoreAdded(false);
			}
		}
		
		Debug.Log("LOL SENDING SCORE");
	}
	
	public static bool UserIsVerified() {
		return verifiedUsername != null && verifiedUsername != "";
	}
	public static void VerifyUser(string name, string token) {
		GJAPI.Users.Verify(name, token);
		username = name;
		userToken = token;
	}
	public static void Logout() {
		verifiedUsername = "";
	}
	
	public static void GetScores() {
		GJAPI.Scores.Get();
	}
	
	private static void OnScoreAdded(bool success) {
		Debug.Log("LOL SCORE ADDED");
		AddScoreCallback(success);
	}
	
	private static void OnGetScores(GJScore[] scores) {
		//Score[] s = new Score[scores.Length];
		//for(int i = 0; i < s.Length; i++) {
		//	s[i] = new Score(scores[i].Name, scores[i].Sort);
		//}
		
		GetScoresCallback(scores);
	}
	
	public static void OnUserVerified(bool success) {
		if(success) {
			verifiedUsername = username;
		}
		else {
			Debug.Log("Error while verifying user :(");
		}
		
		VerifyUserCallback(success);
	}
}
