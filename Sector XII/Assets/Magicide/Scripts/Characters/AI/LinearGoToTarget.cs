using UnityEngine;
using UnityEngine.AI;

public class LinearGoToTarget : MonoBehaviour
{
    /// Public
    public GameObject _target;
    
    /// Private
    private float _AgentSpeed;
    private Collider _AgentCollider;
    
    void Start() {

        // Get references
        _AgentSpeed = GetComponent<Char_Crystal>().GetMovementSpeed();
        _AgentCollider = GetComponent<Collider>();
    }
    
	void Update () {

        // If the agent is outside of the arena still
        if (_target != null && _AgentCollider != null) {

            // Look at target's position
            transform.LookAt(_target.transform);

            // Move towards last known facing direction
            transform.Translate(Vector3.forward * Time.deltaTime * _AgentSpeed);

            foreach (var trigger in AiManager._pInstance._AgentTriggers) {

                // Once a collision happens between the Agent and a trigger
                if (_AgentCollider.bounds.Intersects(trigger.bounds)) {

                    // Enable agency
                    GetComponent<NavMeshAgent>().enabled = true;
                    GetComponent<Char_Crystal>().Start();

                    // Disable manual seek
                    this.enabled = false;
                }
            }
        }
    }

    public void SetTarget(GameObject target) {

        // Set the gameObject target to move towards
        _target = target;
    }

}