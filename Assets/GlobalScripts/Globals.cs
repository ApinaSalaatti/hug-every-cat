using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	private static CatImages catImages;
	public static CatImages CatImages { get { return catImages; } }

	[SerializeField]
	private GameObject catPrefab;
	private static GameObject prefab;
	public static GameObject CatPrefab { get { return prefab; } }

	private static string dataFolder = "";
	public static string DataFolder { get { return dataFolder; } }

	private static string catFolder = "";
	public static string CatFolder { get { return catFolder; } }

	private static CatExportImport catexportImport;
	public static CatExportImport CatExportImport { get { return catexportImport; } }

	// Use this for initialization
	void Awake () {
		prefab = catPrefab;

		dataFolder = Application.persistentDataPath + "/";
		catFolder = dataFolder + "cats/";
		catImages = GetComponent<CatImages>();
		catexportImport = GetComponent<CatExportImport>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
