using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Movement : MonoBehaviour {

	private Rigidbody rigidBody;
	public XboxController controller;

	public float movementSpeed = 60;
	public float maxSpeed = 5;

	public Vector3 previousRotationDirection = Vector3.forward;

	public GameObject bulletSpawnPosition;
	public GameObject bulletPrefab;

	private float shootingTimer;
	public float timeBetweenShots = 0.02f;

	public float maxBulletTimeAlive;

	// Use this for initialization
	void Start () {
		
		rigidBody = GetComponent<Rigidbody> ();
		shootingTimer = Time.time;

	}

	void Update(){
		
		RotatePlayer ();
		FireGun ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		MovePlayer ();

	}

	private void MovePlayer(){

		float axisX = XCI.GetAxis (XboxAxis.LeftStickX, controller);
		float axisZ = XCI.GetAxis (XboxAxis.LeftStickY, controller);
		Vector3 movement = new Vector3 (axisX, 0, axisZ);
		rigidBody.AddForce (movement * movementSpeed);

		//Ensure the player can't go faster than the max speed

		if (rigidBody.velocity.magnitude > maxSpeed) {
			
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;

		}

	}

	private void RotatePlayer(){
		
		float rotateAxisX = XCI.GetAxis (XboxAxis.RightStickX, controller);

		float rotateAxisZ = XCI.GetAxis (XboxAxis.RightStickY, controller);

		Vector3 directionVector = new Vector3 (rotateAxisX, 0, rotateAxisZ);

		//Checks to see if the right thumbstick is not being used, if not, keep shooting in the same direction that it was previously
		if (directionVector.magnitude < 0.1f){
			directionVector = previousRotationDirection;
		}
		directionVector = directionVector.normalized;
		previousRotationDirection = directionVector;
		transform.rotation = Quaternion.LookRotation (directionVector);
	}

	private void FireGun(){

		if (XCI.GetAxis (XboxAxis.RightTrigger) > 0.1f) {
			if (Time.time - shootingTimer > timeBetweenShots) {
				GameObject GO = Instantiate (bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity) as GameObject;
				GO.GetComponent<Rigidbody> ().AddForce (transform.forward * 10, ForceMode.Impulse);
				Destroy (GO, maxBulletTimeAlive);

				shootingTimer = Time.time;
			}
		}

	}

}
