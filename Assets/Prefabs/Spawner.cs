using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Spawner : MonoBehaviour {      public GameObject[] enemyPrefab;     public int[] spawnRate, spawnTime;      // Use this for initialization     void Start() {         for (int i = 0; i < spawnRate.Length; i++)
        {
            StartCoroutine(SpawnFunction(spawnTime[i], i));
        }     } 	 	// Update is called once per frame 	void Update () { 		 	}      void SpawnEnemies(int value)     {         int rand = Random.Range(0, 100);         if (rand <= spawnRate[value])
        {
            GameObject attacker = Instantiate(enemyPrefab[value], Vector3.zero, Quaternion.identity, transform) as GameObject;             attacker.transform.parent = transform;             attacker.transform.position = transform.position;
        }     }      IEnumerator SpawnFunction(float delayTime, int value)
    {
        yield return new WaitForSeconds(delayTime);        
        SpawnEnemies(value);
        print("spawned" + value);
        StartCoroutine(SpawnFunction(spawnTime[value], value));
    } } 