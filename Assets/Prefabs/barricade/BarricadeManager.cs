using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour {
    
    public int health;
    public GameObject door;
    public AudioClip[] knocks;
    public AudioClip doorOpen;

    private BoxCollider2D collisionBox;    
    private GameObject doorImage;
    private AudioSource audioSource;


    // Use this for initialization
    void Start () {
        collisionBox = GetComponent<BoxCollider2D>();
        doorImage = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0 && doorImage.GetComponent<SpriteRenderer>().enabled == false)
        {
            PlayAudio(doorOpen);
            doorImage.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (health > 0)
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
        int rand = UnityEngine.Random.Range(0, 2);
        PlayAudio(knocks[rand]);
        health -= damage;
        if(health - damage <= 0)
        {
            health = 0;
        }
    }

    void PlayAudio(AudioClip thisClip)
    {
        audioSource.clip = thisClip;
        audioSource.Play();
    }
}
