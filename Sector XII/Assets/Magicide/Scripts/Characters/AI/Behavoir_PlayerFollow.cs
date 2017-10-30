using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behavoir_PlayerFollow : MonoBehaviour {

    public Transform _Target;

    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        _agent.destination = _Target.transform.position;

	}
}
