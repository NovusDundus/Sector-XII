using UnityEngine;
using UnityEngine.AI;

public class LinearGoToTarget : MonoBehaviour
{
    /// Public
    public GameObject _target;
    
    /// Private
    private float _AgentSpeed;
    
    void Start() {

        // Get references
        _AgentSpeed = GetComponent<Char_Crystal>().GetMovementSpeed();
    }
    
	void Update () {

        // If the agent is outside of the arena still
        if (_target != null) {

            // Look at target's position
            transform.LookAt(_target.transform);

            // Move towards last known facing direction
            transform.Translate(Vector3.forward * Time.deltaTime * _AgentSpeed);
            
        }
    }

    public void SetTarget(GameObject target) {

        // Set the gameObject target to move towards
        _target = target;
    }

}