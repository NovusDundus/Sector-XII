using UnityEngine;
using UnityEngine.AI;

public class Behaviour_Flee : MonoBehaviour
{

    ///--------------------------------------///
    /// Created by: Callen Mitchell
    /// Created on: 9.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    [HideInInspector]
    public GameObject m_Target;                                     // references the game object in the game (target to flee from)   
    public float m_FleeThreshold;                                   // determains the distance the AI will run(Once it is outside of the determained threshold it will stop running)
    private float m_FleeDistance;                                   // current distance between the agent and target it is attempting to flee from.
    private NavMeshAgent m_agent;                                   // sets a variable name to be used in the rest of the script
    private SphereCollider _RetargetCollision;
    private LinearGoToTarget _GoToTarget;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start()
    {
        // Uses the variable called agent, then uses getcomponent to determain what component is going to be placed into the agent
        m_agent = GetComponent<NavMeshAgent>();
        _RetargetCollision = GetComponent<SphereCollider>();
        _GoToTarget = GetComponent<LinearGoToTarget>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    // FixedUpdate is called once per frame but with regular update intervals
    void FixedUpdate()
    {
        if (m_Target != null && m_agent != null) {

            // Determine distance between ai and target
            //Will get the transform.position of both the target(player) and the gameobject that the navmeshagent is attached too
            m_FleeDistance = Vector3.Distance(m_Target.transform.position, m_agent.transform.position);

            // Checks to see if the distance from the AI gameobject is less then or equal to the flee_threshold
            if (m_FleeDistance <= m_FleeThreshold) {
                // First thing this does it is find the position of the gameobject that this script is associated with(EnemyAI gameobject)
                // the compiler then finds the position of m_target(Which is the player gameobject)
                // then the compiler subtracts whatever the position of the player, by the position of the AI is and move it into the vecbetweenplayerandenemy variable
                Vector3 vecBetweenPlayerAndEnemy = transform.position - m_Target.transform.position;

                // finds the position of the script that is associated with the gameobject(EnemyAI)
                // then adds the new position of the vecbetweenplayerandenemy
                Vector3 targetPosition = transform.position + vecBetweenPlayerAndEnemy.normalized * m_FleeThreshold;

                // This sets the destination for the AI to move to that position(in this case its the player)
                m_agent.SetDestination(targetPosition);
            }
            _GoToTarget.enabled = false;
        }

        else {

            ///_GoToTarget.enabled = true;
        }
    }

    public float GetFleeDistance()
    {
        return m_FleeDistance;
    }
    
    private void OnTriggerEnter(Collider other) {

        // Check if other object is a player
        if (other.tag == "P1_Character" || other.tag == "P2_Character" || other.tag == "P3_Character" || other.tag == "P4_Character") {

            // Set gameobject to be the new seek target
            m_Target = other.gameObject;
        }
    }

}