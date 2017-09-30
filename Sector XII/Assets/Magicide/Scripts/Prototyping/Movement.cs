using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


	public float bulletSpeed;
	public float fireRate;

	public GameObject shot;
	public Transform shotSpawn;


	public float nextFire;

	public KeyCode shoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		PControls ();
	}

	void PControls() {
		
		if (Input.GetKey (shoot) && Time.time > nextFire) {
			nextFire = fireRate + Time.time;
			GameObject GO = Instantiate (shot, shotSpawn.position, Quaternion.identity) as GameObject;
			GO.GetComponent<Rigidbody> ().AddForce (shotSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
		}
	}
}


