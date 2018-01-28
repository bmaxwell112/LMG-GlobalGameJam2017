using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRobot : MonoBehaviour {

    public GameObject explode;

    private Animator anim;
    private EnemyAI ai;
    private bool triggered, dead, attacking;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
	}

    void Update()
    {
        anim.SetBool("attack", attacking);
        if(ai.attacked && !triggered)
        {
            attacking = true;
        }
        if (!ai.attacked && triggered)
        {
            attacking = false;
        }
        if (ai.hp <= 0 && !dead)
        {
            attacking = false;
            CreateSmoke();
            dead = true;
        }
        if(ai.knockback && attacking)
        {
            attacking = false;                      
        }
    }

    void CreateSmoke()
    {
        GameObject smoke = Instantiate(explode) as GameObject;
        smoke.transform.parent = transform;
        smoke.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Invoke("DisableSprite", 0.25f);
    }

    void DisableSprite()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
    }
}
