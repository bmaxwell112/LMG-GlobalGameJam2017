using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRobot : MonoBehaviour {

    public GameObject explode;

    private Animator anim;
    private EnemyAI ai;
    private bool triggered, dead;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
	}

    void Update()
    {
        if(ai.attacked && !triggered)
        {
            anim.SetTrigger("attack");
            triggered = true;
        }
        if (!ai.attacked && triggered)
        {
            triggered = false;
        }
        if (ai.hp <= 0 && !dead)
        {
            CreateSmoke();
            dead = true;
        }
        if(ai.knockback)
        {
            anim.SetTrigger("stopAttack");
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
