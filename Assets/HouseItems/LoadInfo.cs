using UnityEngine;
using System.Collections;

public class LoadInfo : MonoBehaviour {
	[SerializeField]
	private string resource;
	public string Resource {
		get { return resource; }
	}
}
