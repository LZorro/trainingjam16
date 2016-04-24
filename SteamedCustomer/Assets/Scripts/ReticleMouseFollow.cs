using UnityEngine;
using System.Collections;

public class ReticleMouseFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = Input.mousePosition;
		newPos.z = 5.0f;  

		this.transform.position = Camera.main.ScreenToWorldPoint(newPos);
	
	}
}
