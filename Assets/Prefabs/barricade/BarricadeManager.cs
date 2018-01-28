using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour {
    
    public int health;
    public GameObject door;
    public AudioClip[] knocks;
    public AudioClip doorOpen;
    public Sprite[] doorImages;

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
        print(health);
        if (health > 50)
        {
            doorImage.GetComponent<SpriteRenderer>().enabled = false;
        }else if( health <= 50 && health > 0)
        {
            doorImage.GetComponent<SpriteRenderer>().enabled = true;
            doorImage.GetComponent<SpriteRenderer>().sprite = doorImages[0];
            doorImage.transform.localPosition = new Vector3(-1f, 2.95f, 0f);
            doorImage.transform.localScale = new Vector3(4.5f, 5f, 1f);
        }
        else if (health <= 0 && doorImage.GetComponent<SpriteRenderer>().enabled == true)
        {
            doorImage.GetComponent<SpriteRenderer>().sprite = doorImages[1];
            doorImage.transform.localPosition = new Vector3(-1.75f, 3.5f, 0f);
            doorImage.transform.localScale = new Vector3(4.5f, 4.5f, 1f);
            PlayAudio(doorOpen);
        }
	}

    public void BuildBarricade()
    {
        health = 150;
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
