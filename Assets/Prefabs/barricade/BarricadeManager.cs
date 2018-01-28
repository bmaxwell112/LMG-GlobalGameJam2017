using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour {
    
    public int health;
    public GameObject door;
    private BoxCollider2D collisionBox;    
    private GameObject doorImage;

	// Use this for initialization
	void Start () {
        collisionBox = GetComponent<BoxCollider2D>();
        doorImage = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            doorImage.GetComponent<SpriteRenderer>().enabled = true;
        }else
        {
            doorImage.GetComponent<SpriteRenderer>().enabled = false;
        }
	}

    public void BuildBarricade()
    {
        health = 100;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health - damage <= 0)
        {
            health = 0;
        }
    }
}
