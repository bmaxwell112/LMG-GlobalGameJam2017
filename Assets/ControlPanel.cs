using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour {

    public bool active;

    private BarricadeManager barricade;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        barricade = FindObjectOfType<BarricadeManager>();
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(barricade.health <= 0)
        {
            active = false;
            sprite.color = Color.gray;
        }
        else
        {
            active = true;
            sprite.color = Color.white;
        }
	}
}
