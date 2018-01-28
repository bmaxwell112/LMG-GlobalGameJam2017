using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int hp, attack;
    public float speed, attackRateInSeconds, destoryTimeFromDeath, attackDistance;
    public GameObject number;

    private float speedVariance, deathTime, hitTime;
    private PlayerController player;
    private BarricadeManager barricade;
    private int colliding = 0;
    [HideInInspector]
    public bool attacked, dead, knockback;

    void Start()
    {
        float rand = Random.Range(-0.2f, 0.2f);
        speedVariance = speed + rand;
        player = FindObjectOfType<PlayerController>();
        barricade = FindObjectOfType<BarricadeManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (dead)
        {
            if (Time.time >= deathTime + destoryTimeFromDeath)
            {
                transform.localScale += Vector3.down * Time.deltaTime;
                if(transform.localScale.y <= 0)
                {
                    Destroy(gameObject);
                }
                
            }
        }

        if(colliding == 0 && !dead && !knockback)
        {
            Move();
        }

        if (knockback)
        {
            transform.position += (Vector3.right * 2) * Time.deltaTime;
            if (Time.time >= hitTime + 0.4f)
            {
                knockback = false;
            }
        }
    }

   void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player" || coll.gameObject.tag == "barricade")
        {
            colliding++;
            if (!attacked)
            {
                EnemyAttack(coll.gameObject.tag);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "barricade")
        {
            if (!attacked)
            {
                EnemyAttack(other.gameObject.tag);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player" || coll.gameObject.tag == "barricade")
        {
            colliding--;
        }

    }


    private void Move()
    {
        if (transform.position.x > player.transform.position.x + attackDistance)
        {
            transform.position -= new Vector3(speedVariance, 0, 0) * Time.deltaTime;
        }
    }

    void AttackDelay()
    {
        attacked = false;
    }
    void EnemyAttack(string tag)
    {
        if(tag == "Player")
        {
            player.DamagePlayer(attack);
            SpawnNumber(attack, player.transform.position);
        }
        if(tag == "Barricade")
        {
            barricade.TakeDamage(attack);
            SpawnNumber(attack, barricade.transform.position);
        }
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
