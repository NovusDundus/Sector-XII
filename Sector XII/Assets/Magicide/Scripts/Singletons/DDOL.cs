using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {
    
	public void Awake() {

        // The gameObject this script is attached is to be persistent through scene loading
        DontDestroyOnLoad(gameObject);
    }
}
