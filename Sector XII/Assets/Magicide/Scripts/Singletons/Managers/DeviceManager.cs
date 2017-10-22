using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 23.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    [Header(" - Teleporters")]
    [Tooltip("Time in seconds that allows reusing of the teleporters once they has been activated.")]
    public int _TeleportCooldownTime = 10;
    [Tooltip("Can the teleporters be used in phase 1?")]
    public bool _CanBeUsedInPhase1 = true;

    /// Public (internal)
    [HideInInspector]
    public static DeviceManager _pInstance;                         // This is a singleton script, Initialized in Awake().

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

}