using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameOverMode { SUCCESS, FAIL, FORFEIT }

public class GameState : MonoBehaviour {
	private static GameState instance;
	public static GameState Instance { get { return instance; } }

	private string nextLevel; // The level that will be unlocked when this level is cleared. Needed for the next level button :)

	[SerializeField]
	private GameObject playerCat;

	[SerializeField]
	private InputField nameField;

	[SerializeField]
	private GlobalInput globalbInput;
	[SerializeField]
	private GameObject mainGUI;
	[SerializeField]
	private GameOverScreen gameOverScreen;
	[SerializeField]
	private GameObject pauseMenu;

	private float gameTime = 0f;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	void Start() {
		CatExportImportData cat = CatFactory.Instance.CreateFromFile("startingCat");

		//Sprite s = cat.GetComponent<CatSpriteManager>().GetSprite();
		Renderer r = playerCat.GetComponentInChildren<Renderer>();
		r.sharedMaterial.mainTexture = cat.texture;

		CatStats s = playerCat.GetComponent<CatStats>();
		s.Name = cat.name;
		s.Gender = cat.gender;

		//mainGUI.transform.parent.BroadcastMessage("SetPlayer", playerCat);

		//globalbInput.Init(pauseMenu, mainGUI);

		// TODO: maybe make something that actually makes sense rather than this hacky shit
		// NOTE TO FUTURE SELF WHO DOESN'T REMEMBER WHAT THIS IS:
		// It's the input field that's attached to the Catstagram send GUI because it requires an input field...
		nameField.text = cat.name;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
	}

	public void GameOver(GameOverMode mode = GameOverMode.FAIL, string nextLvl = "") {
		mainGUI.SetActive(false);
		globalbInput.enabled = false;
		playerCat.GetComponent<PlayerMover>().Enable(false);

		if(mode == GameOverMode.SUCCESS) {
			//LevelManager.Instance.UnlockLevel(nextLevel);
			int score = playerCat.GetComponent<PlayerScore>().Score;

			nextLevel = nextLvl;
			LevelManager.Instance.LevelCleared(MainSceneLoader.currentLevelName, score, nextLevel);
			gameOverScreen.Show(GameOverMode.SUCCESS);
		}
		else {
			gameOverScreen.Show(mode);
		}
	}

	public void NextLevel() {
		LevelManager.Instance.OpenLevel(nextLevel);
	}

	public void Restart() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void ExitGame() {
		ConfirmDialog.Instance.Show("Are you really sure you wish to quit, meow?", DoExit);
	}
	
	private void DoExit() {
		globalbInput.Unpause();
		Application.LoadLevel(7); // Load level select screen
	}
}
