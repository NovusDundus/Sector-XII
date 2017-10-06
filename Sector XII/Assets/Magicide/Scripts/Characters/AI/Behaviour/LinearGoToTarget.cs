using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearGoToTarget : MonoBehaviour
{
    public float Speed = 0f;
    public GameObject _target;
    
    void Start() {

    }
    
	void Update () {

        if (_target != null) {

            // Look at target's position
            transform.LookAt(_target.transform);

            // Move towards last known facing direction
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
    }

    public void SetTarget(GameObject target) {

        // Set the gameObject target to move towards
        _target = target;
    }
}
