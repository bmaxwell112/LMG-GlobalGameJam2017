using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour {
    
    public int health;
    private BoxCollider2D collisionBox;

    public Sprite[] imageArray;

	// Use this for initialization
	void Start () {
        collisionBox = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(health == 0)
        {
            collisionBox.enabled = false;
        }else
        {
            collisionBox.enabled = true;
        }
	}

    public void BuildBarricade()
    {
        health++;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }
}
