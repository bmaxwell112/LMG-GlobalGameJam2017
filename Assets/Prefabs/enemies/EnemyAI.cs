using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int speed, hp, attack;
    
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
	}
}
