using UnityEngine;
using System.Collections;

// A script that contains assets that need global access
public class Globals : MonoBehaviour {
	private static CatImages catImages;
	public static CatImages CatImages { get { return catImages; } }

	[SerializeField]
	private Texture defaultTexture;
	private static Texture defTexture;
	public static Texture DefaultTexture { get { return defTexture; } }

	[SerializeField]
	private GameObject catPrefab;
	private static GameObject prefab;
	public static GameObject CatPrefab { get { return prefab; } }

	private static string dataFolder = "";
	public static string DataFolder { get { return dataFolder; } }

	private static string saveFolder = "";
	public static string SaveFolder { get { return saveFolder; } }

	private static string tempDataFolder = "";
	public static string TempDataFolder { get { return tempDataFolder; } }

	private static string catFolder = "";
	public static string CatFolder { get { return catFolder; } }

	// Use this for initialization
	void Awake () {
		prefab = catPrefab;
		defTexture = defaultTexture;

		dataFolder = Application.persistentDataPath + "/";
		saveFolder = dataFolder + "save/";
		tempDataFolder = dataFolder + "temp/";
		catFolder = dataFolder + "cats/";

		System.IO.Directory.CreateDirectory(saveFolder);
		System.IO.Directory.CreateDirectory(tempDataFolder);
		System.IO.Directory.CreateDirectory(catFolder);

		catImages = GetComponent<CatImages>();
		//catExportImport = GetComponent<CatExportImport>();
		//catFactory = GetComponent<CatFactory>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
