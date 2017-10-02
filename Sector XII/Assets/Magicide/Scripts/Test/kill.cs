using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour {

	public float maxTimeAlive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Destroy (this.gameObject, maxTimeAlive);
	}
}
