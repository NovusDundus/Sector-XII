using UnityEngine;
using UnityEngine.AI;

public class Behaviour_Flee : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Callen Mitchel
    /// Created on: 9.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    public GameObject m_Target;                                     // references the game object in the game (target to flee from)   
    public float FleeThreshold = 20f;                               // determains the distance the AI will run(Once it is outside of the determained threshold it will stop running)
                                                                       
    private float FleeDistance;                                     // current distance between the agent and target it is attempting to flee from.
    private float movespeed;                                            
                                                                       
    NavMeshAgent agent;                                             // sets a variable name to be used in the rest of the script

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start()
    {
        // Uses the variable called agent, then uses getcomponent to determain what component is going to be placed into the agent
        agent = GetComponent<NavMeshAgent>();

        movespeed = agent.GetComponent<Char_Wyrm>().GetMovementSpeed();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    // FixedUpdate is called once per frame but with regular update intervals
    void FixedUpdate()
    {
        // Determine distance between ai and target
        FleeDistance = Vector3.Distance(m_Target.transform.position, agent.transform.position);

        // If distance between player and enemy is less than a certain number
        if (FleeDistance <= FleeThreshold)
        {
            // First thing this does it is find the position of the gameobject that this script is associated with(EnemyAI gameobject)
            // the compiler then finds the position of m_target(Which is the player gameobject)
            // then the compiler subtracts whatever the position of the player, by the position of the AI is and move it into the vecbetweenplayerandenemy variable
            Vector3 vecBetweenPlayerAndEnemy = transform.position - m_Target.transform.position;

            // finds the position of the script that is associated with the gameobject(EnemyAI)
            // then adds the new position of the vecbetweenplayerandenemy
            Vector3 targetPosition = transform.position + vecBetweenPlayerAndEnemy.normalized * movespeed;

            // This sets the destination for the AI to move to that position(in this case its the player)
            agent.SetDestination(targetPosition);
        }

        else {

            // Successful flee escape (DO SOME STUFF IDK CHANGE BEHAVIOURS OR SOMETHING)?
        }
    }

}