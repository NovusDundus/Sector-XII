using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : MonoBehaviour {

    //referencing another script allows you to use and call that script.
    private Behaviour_Flee m_fleeBehaviour; //references the fleebehavior script
    private Behavior_Wonder m_wanderBehaviour; //references the wanderbehavior script

    int m_scriptSwitch;

    //for one player
    private Transform m_player;

	// Use this for initialization
	void Start () {

        m_fleeBehaviour = GetComponent<Behaviour_Flee>(); //finds the component called Behaviour_Flee
        m_wanderBehaviour = GetComponent<Behavior_Wonder>(); // finds the component called Behavior_Wonder

        //get access to the player
        m_player = GameObject.FindGameObjectWithTag("Player").transform;

        m_fleeBehaviour.m_Target = m_player.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //calculate the distance between AI gameobject and the player gameobject. 
        float m_dist = Vector3.Distance(transform.position, m_player.transform.position);

       //m_dist = m_scriptSwitch;

       // if(m_fleeBehaviour.GetFleeDistance() > m_dist)
       // {
          //  m_wanderBehaviour.get
      //  }

        //if we're close enough to flee
        if(m_dist < m_fleeBehaviour.GetFleeThreshold())
        {
            //activate flee if it isn't activated already
            if (m_fleeBehaviour.enabled == false)
            {
                m_fleeBehaviour.enabled = true;
                m_wanderBehaviour.enabled = false;
            }
        }
        else
        {
            //activate wander if it isn't activated already
            if (m_wanderBehaviour.enabled == false)
            {
                m_wanderBehaviour.enabled = true;
                m_fleeBehaviour.enabled = false;
                m_wanderBehaviour.RecalculateTarget(); //reset wander target to make sure it doesn't keep trying to get the player
            }
        }
    }
}
