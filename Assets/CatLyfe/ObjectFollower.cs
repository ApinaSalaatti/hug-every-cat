using UnityEngine;
using System.Collections;

public class ObjectFollower : MonoBehaviour {
		[SerializeField]
		private GameObject objectToFollow;

		private Vector3 offset;

		// Use this for initialization
		void Start () {
			offset = transform.position - objectToFollow.transform.position;
		}
		
		// Update is called once per frame
		void Update () {
			transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position + offset, Time.deltaTime * 5f);
		}
	}

