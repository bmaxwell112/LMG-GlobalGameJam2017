using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int hp, attack;
    public float speed, attackRateInSeconds;
    public GameObject number;

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
            Destroy(gameObject);
        }
    }

    void SpawnNumber(int value, Vector3 spawnPos)
    {
        GameObject numClone = Instantiate(number, transform.position + (Vector3.up * 3), Quaternion.identity) as GameObject;
        numClone.GetComponent<NumberDisplay>().SetNumber(value);
        numClone.transform.position = spawnPos + (Vector3.up * 2);
    }
}
