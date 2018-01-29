using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Spawner : MonoBehaviour {      public GameObject[] enemyPrefab;     public int[] spawnRate;
    public float[] spawnTime;     public bool SetSpawnsAI, wait;          private int phase = 0;       // Use this for initialization     void Start() {         for (int i = 0; i < spawnRate.Length; i++)
        {
            StartCoroutine(SpawnFunction(spawnTime[i], i));
        }         if(SetSpawnsAI)
        {
            SpawningOverTime();
        }     } 	 	// Update is called once per frame 	void Update () {        
        if (!gameObject.GetComponentInChildren<EnemyAI>() && wait)
        {
            if (phase == 1)
            {
                PhaseTwo();
            }
            else if (phase == 2)
            {
                PhaseThree();
            }
            else if (phase == 3)
            {
                PhaseFour();
            }
            wait = false;
        }
    }      void SpawnEnemies(int value)     {         int rand = Random.Range(1, 101);         if (rand <= spawnRate[value])
        {
            GameObject attacker = Instantiate(enemyPrefab[value], Vector3.zero, Quaternion.identity, transform) as GameObject;             attacker.transform.parent = transform;             attacker.transform.position = transform.position;
        }     }      IEnumerator SpawnFunction(float delayTime, int value)
    {
        yield return new WaitForSeconds(delayTime);        
        SpawnEnemies(value);
        StartCoroutine(SpawnFunction(spawnTime[value], value));
    }      void SpawningOverTime()
    {
        spawnRate[0] = 70;
        spawnRate[1] = 0;
        spawnRate[2] = 0;
        phase = 1;
        Invoke("PhasePause", 5);
    }     void PhaseTwo()
    {
        spawnRate[0] = 0;
        spawnRate[1] = 70;
        spawnRate[2] = 0;
        phase = 2;
        Invoke("PhasePause", 5);
    }     void PhaseThree()
    {
        spawnRate[0] = 0;
        spawnRate[1] = 0;
        spawnRate[2] = 70;
        phase = 3;
        Invoke("PhasePause", 5);
    }     void PhaseFour()
    {
        spawnRate[0] = 30;
        spawnRate[1] = 90;
        spawnRate[2] = 50;
        Invoke("PhasePause", 5);
    }     void PhasePause()
    {
        wait = true;
        spawnRate[0] = 0;
        spawnRate[1] = 0;
        spawnRate[2] = 0;        
    } } 