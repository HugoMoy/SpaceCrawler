using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

	public Transform ply;
	// Use this for initialization
	void Start () {
		//ply = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {
		Vector3 position = this.transform.position;
		position.x = ply.position.x;
		position.y = ply.position.y;
		this.transform.position = position;
		//Debug.Log(ply.position);
	}
}
