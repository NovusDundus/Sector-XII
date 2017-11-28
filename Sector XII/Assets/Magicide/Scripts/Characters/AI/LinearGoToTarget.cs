using UnityEngine;
using UnityEngine.AI;

public class LinearGoToTarget : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 6.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public GameObject _target;
    public bool _AiControlled = true;

    /// Private
    private float _AgentSpeed;
    private Collider _AgentCollider;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get references
        _AgentSpeed = GetComponent<Character>().GetMovementSpeed();
        _AgentCollider = GetComponent<Collider>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {

        // If the agent is outside of the arena still
        if (_target != null && _AgentCollider != null) {

            // Look at target's position
            transform.LookAt(_target.transform);

            // Move towards last known facing direction
            float speed = _AgentSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * speed);

            if (_AiControlled == true) {

                foreach (var trigger in AiManager._pInstance._AgentTriggers) {

                    // Once a collision happens between the Agent and a trigger
                    if (_AgentCollider.bounds.Intersects(trigger.bounds)) {

                        // Enable agency
                        this.GetComponent<NavMeshAgent>().enabled = true;
                        this.GetComponent<Char_Crystal>().Start();

                        // Disable manual seek
                        this.enabled = false;
                        break;
                    }
                }
            }

            else { /// _AiControlled == false

                foreach (var trigger in PlayerManager._pInstance._RespawnTriggers) {

                    // Once a collision happens between the Agent and a trigger
                    if (_AgentCollider.bounds.Intersects(trigger.bounds)) {

                        // Enable player controller input
                        GetComponent<Char_Geomancer>().SetActive(true);
                        
                        // Disable this script component
                        this.enabled = false;
                        break;
                    }

                    // Activate geomancer invincibility whilst this script component is enabled
                    GetComponent<Char_Geomancer>().ActivateInvincibility();
                }
            }
        }
    }

    public void SetTarget(GameObject target) { _target = target; }

}