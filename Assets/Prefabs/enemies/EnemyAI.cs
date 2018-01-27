using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int hp, attack;
    public float speed, attackRateInSeconds;

    private float speedVariance;
    private PlayerController player;
    private bool attacked;

    void Start()
    {
        float rand = Random.Range(-0.2f, 0.2f);
        speedVariance = speed + rand;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.x > player.transform.position.x + 0.1)
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
        attacked = true;
        Invoke("AttackDelay", attackRateInSeconds);
    }

    public void DamageEnemy(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
