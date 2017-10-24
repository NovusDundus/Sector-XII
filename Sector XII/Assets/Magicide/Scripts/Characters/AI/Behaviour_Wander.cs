using UnityEngine;
using UnityEngine.AI;

public class Behavior_Wonder : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    //NOTE - might be worth adding a timer to change how often the AI chooses a new position.

    //create a Vec3 (3 floats - x,y,z) to store the target
    private Vector3 m_targetDestination;

    //create a boolean to store whether the AI has a target yet
    private bool hasTarget = false;

    private float DistanceThreshold = 10f; //the distance between the Ai and the determaind position

    private float walkDistance = 20f; //the distance of the field that the determained positions are going to be built

    //store nav mesh agent
    private NavMeshAgent m_agent;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    /// CALCULATES THE RANDOM POINT ON THE NAVMECHAGENT
    /// ------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        //if we have no target
        if (hasTarget == false)
        {
            //calculates a randomDirection on the vector3(x, y, z)
            //then randomises the points inside of the unitsphear
            //the walkDistance is the sphere that the random points will use 
            Vector3 randomDirection = Random.insideUnitSphere * walkDistance;

            //get target pos by adding the AI's position
            Vector3 targetPosAnyHeight = transform.position + randomDirection;
            Vector3 targetPos = new Vector3(targetPosAnyHeight.x, 1, targetPosAnyHeight.z);
            
            // clamp the target pos within the map boundaries if it exceeds in any axis
            ClampTargetPos(targetPos);
            
            //check to see if targetPos is on the NavMesh
            NavMeshHit hit = new NavMeshHit();
            if (NavMesh.SamplePosition(targetPos, out hit, DistanceThreshold, 1))
            {
                //set that point as our target
                m_targetDestination = targetPos;

                //update hasTarget to true
                hasTarget = true;
            }
        }
        ///-------------------------------------------------------------------------------------------
        ///WILL MOVE THE AI TO THE POSITION THAT WAS CALCULATED ABOVE
        ///-----------------------------------------------------------
        else //if we have a target
        {
            //move towards targetDestination (you've got code for moving towards a point)
            m_agent.SetDestination(m_targetDestination);

            //if we reach the target (you've got for seeing if you're near a point)
            if (Vector3.Distance(m_agent.transform.position, m_targetDestination) <= DistanceThreshold)
            {
                hasTarget = false;
            }

            //Debug.Log("Has Target: " + targetDestination);
            //hasTarget = false;
        }

    }

    public void ClampTargetPos(Vector3 targetpos)
    {
        // Clamp minimum X
        if (targetpos.x < minX)
        {
            //adds a new vector to the targetpos(x, y, z)
            targetpos = new Vector3(minX, targetpos.y, targetpos.z);
        }

        // Clamp maximum X
        else if (targetpos.x > maxX)
        {
            //adds a new vector3 to the targetpos(x, y, z)
            targetpos = new Vector3(maxX, targetpos.y, targetpos.z);
        }

        // Clamp minimum Z
        if (targetpos.z < minZ)
        {
            targetpos = new Vector3(targetpos.x, targetpos.y, minZ);
        }

        // Clamp maximum Z
        else if (targetpos.z > maxZ)
        {
            targetpos = new Vector3(targetpos.x, targetpos.y, maxZ);
        }
    }

}