using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {
    public int _Damage = 1;
    public float _attackSpeed = 1;
    public float _attackCooldown = 1;
    public float _attackRange = 1;
    private PlayerCharacter attackPlayer;
    CircleCollider2D _attackAOE;
    bool canattack = true;
    float attackdelay = 0f;
    float attackRate = 1f;

	// Use this for initialization
	void Start ()
    {
        //attackPlayer = GetComponent<HumanoidCharacter>();
        //gameObject.Pla
        _attackAOE.radius = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        attackdelay -= Time.deltaTime;

        _attackAOE.transform.position = transform.position;

        if (_attackAOE.OverlapPoint(attackPlayer.transform.position)) {

            if (attackdelay <= 0f)
            {
                attack();
            }
        }
	}

    void attack()
    {
        attackPlayer.Damage(_Damage);
        attackdelay = attackRate;
    }
}
