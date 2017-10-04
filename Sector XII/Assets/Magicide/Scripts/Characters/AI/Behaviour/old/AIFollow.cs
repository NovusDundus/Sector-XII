using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    
    void Start()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

	// Update is called once per frame
	void Update ()
    {

      
	}
}
