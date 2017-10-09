using UnityEngine;
using UnityEngine.AI;

public class AiFleeScript : MonoBehaviour
{
    private float FleeDistance; //how close/far the AI is going to get before running away from the player
    public float FleeOffset; //the distance between the player and the enemy
    public float FleeThreshold; //determains the distance the AI will run(Once it is outside of the determained threshold it will stop running)
    public float movespeed;

    public GameObject m_Target; //references the game object in the game]

    NavMeshAgent agent; //sets a variable name to be used in the rest of the script
    // Use this for initialization
    void Start()
    {
        //uses the variable called agent, then uses getcomponent to determain what component is going to be placed into the agent
        agent = GetComponent<NavMeshAgent>();
    }

    // FixedUpdate is called once per frame but with regular update intervals
    void FixedUpdate()
    {
        // Determine distance between ai and target
        FleeDistance = Vector3.Distance(m_Target.transform.position, agent.transform.position);

        //if distance between player and enemy is less than a certain number
        if (FleeDistance <= FleeThreshold)
        {
            //first thing this does it is find the position of the gameobject that this script is associated with(EnemyAI gameobject)
            //the compiler then finds the position of m_target(Which is the player gameobject)
            //then the compiler subtracts whatever the position of the player, by the position of the AI is and move it into the vecbetweenplayerandenemy variable
            Vector3 vecBetweenPlayerAndEnemy = transform.position - m_Target.transform.position;

            //finds the position of the script that is associated with the gameobject(EnemyAI)
            //then adds the new position of the vecbetweenplayerandenemy
            Vector3 targetPosition = transform.position + vecBetweenPlayerAndEnemy.normalized * FleeOffset;

            //this sets the destination for the AI to move to that position(in this case its the player)
            agent.SetDestination(targetPosition);
        }

        else
        {
            Debug.Log("OUT OF RANGE");
        }
        //{

                                                 //  enemy pos - player pos
           

        //}

    }
}
