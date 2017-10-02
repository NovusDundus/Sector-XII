using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES
    
    [HideInInspector]
    public static MatchManager _pInstance;                // This is a singleton script, Initialized in Startup().
    [HideInInspector]
    public enum Team {                                    

        Humanoid,
        Ethereal
    }                                  // Enumerator representing which faction in the match an object is tied to.

    //--------------------------------------
    // FUNCTIONS

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
    }

    void Update () {
		
	}

    private void FixedUpdate() {

    }

    public void Introduction() {

    }

    public void MatchStart() {

    }

    public void MatchCompleted() {

    }
}
