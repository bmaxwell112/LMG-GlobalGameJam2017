using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int hp, attack;
    public float speed, attackRateInSeconds;
    public GameObject number;

    private float speedVariance, deathTime, hitTime;
    private PlayerController player;
    private bool attacked, dead, knockback;

    void Start()
    {
        float rand = Random.Range(-0.2f, 0.2f);
        speedVariance = speed + rand;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!dead && !knockback) { MovementAndAttack(); }   
        if (dead)
        {
            if (Time.time >= deathTime + 15)
            {
                transform.localScale += Vector3.down * Time.deltaTime;
                if(transform.localScale.y <= 0)
                {
                    Destroy(gameObject);
                }
                
            }
        }
        if(knockback)
        {
            transform.position += (Vector3.right*2) * Time.deltaTime;
            if(Time.time >= hitTime + 0.4f)
            {
                knockback = false;
            }
        }
    }

    private void MovementAndAttack()
    {
        if (transform.position.x > player.transform.position.x + 0.3)
        {
            transform.position -= new Vector3(speedVariance, 0, 0) * Time.deltaTime;
        }
        else if (transform.position.x < player.transform.position.x - 0.1)
        {
            transform.position += new Vector3(speedVariance, 0, 0) * Time.deltaTime;
        }
        else
        {
            if (!attacked)
                EnemyAttack();
        }
    }

    void AttackDelay()
    {
        attacked = false;
    }
    void EnemyAttack()
    {
        player.DamagePlayer(attack);
        SpawnNumber(attack, player.transform.position);
        attacked = true;
        Invoke("AttackDelay", attackRateInSeconds);
    }

    public void DamageEnemy(int damage)
    {
        hp -= damage;        
        if (hp <= 0)
        {           
            Dead();
        }
        else
        {
            hitTime = Time.time;
            knockback = true;
        }
    }

    void SpawnNumber(int value, Vector3 spawnPos)
    {
        GameObject numClone = Instantiate(number, transform.position + (Vector3.up * 3), Quaternion.identity) as GameObject;
        numClone.GetComponent<NumberDisplay>().SetNumber(value);
        numClone.transform.position = spawnPos + (Vector3.up * 2);
    }

    void Dead()
    {
        dead = true;
        Collider2D coll = GetComponent<Collider2D>();
        Animator anim = GetComponent<Animator>();
        coll.enabled = false;
        deathTime = Time.time;
        anim.SetTrigger("dead");
    }
}
