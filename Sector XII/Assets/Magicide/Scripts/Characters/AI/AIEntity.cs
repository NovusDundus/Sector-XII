using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : MonoBehaviour {

    //referencing another script allows you to use and call that script.
    private Behaviour_Flee fleeBehaviour; //references the fleebehavior script
    private Behaviour_Wander wanderBehaviour; //references the wanderbehavior script

    int scriptSwitch = 1;

    //for one player
    private Transform player;

	// Use this for initialization
	void Start () {

        fleeBehaviour = GetComponent<Behaviour_Flee>(); //finds the component called Behaviour_Flee
        wanderBehaviour = GetComponent<Behaviour_Wander>(); // finds the component called Behavior_Wonder

        //get access to the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fleeBehaviour.m_Target = player.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //calculate the distance between AI gameobject and the player gameobject. 
        float dist = Vector3.Distance(transform.position, player.transform.position);

        dist = scriptSwitch;

        switch(scriptSwitch)
        {
            case 1:
                if (dist < fleeBehaviour.GetFleeDistance()) //If the distance between the player and the AI gameobject is less the the flee_distance
                {
                    //Unity will activate the fleebahavior script
                    fleeBehaviour.enabled = true;
                }
                break;

            case 2:
                if (dist > fleeBehaviour.GetFleeDistance())
                {
                    fleeBehaviour.enabled = false;
                }
                break;

            case 3:
                if(dist < fleeBehaviour.GetFleeDistance())
                {
                    wanderBehaviour.enabled = true;
                }
             break;

            case 4:
                if(dist > fleeBehaviour.GetFleeDistance())
                {
                    wanderBehaviour.enabled = false;
                }
                break;

        }
    }
}
