﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float minPos, maxPos, speed, stikeDistance, coolDownRateInSeconds, walkDistance, enemyCheckDistance;
    public int escapeNumber, damage, hp;
    public GameObject number;

    private bool left, coolDown, youShallNotPass;
    private int buttonPressed = 0;
    private LevelManager lvl;
    private BarricadeManager BarricadeReference;
    private SpriteRenderer sprite;

    private void Start()
    {
        lvl = FindObjectOfType<LevelManager>();
        BarricadeReference = GameObject.FindWithTag("Barricade").GetComponent<BarricadeManager>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        youShallNotPass = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), (Vector2.right / 10), enemyCheckDistance);
        InputCheck();
    }

    void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= minPos)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            left = true;
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= maxPos && !youShallNotPass)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            left = false;
            sprite.flipX = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !left)
        {
            AttackOrBarricade();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && left && transform.position.x <= minPos + 0.5)
        {
            ControlPanelPressed();
        }
    }

    private void ControlPanelPressed()
    {
        if (!coolDown)
        {
            buttonPressed++;
            SpawnNumber(buttonPressed, transform.position);
            if (buttonPressed >= escapeNumber)
            {
                lvl.LoadLevel("03a Win");
            }
            coolDown = true;
            Invoke("AttackCoolDown", coolDownRateInSeconds);
        }
    }

    void AttackOrBarricade()
    {
        if(!coolDown)
        {             
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), (Vector2.right/10), stikeDistance);
            if (hit)
            {      
                if(hit.collider.gameObject.CompareTag("enemy"))
                {
                    print("hit enemy");
                    hit.collider.gameObject.GetComponent<EnemyAI>().DamageEnemy(damage);
                    SpawnNumber(damage, hit.collider.gameObject.transform.position);                    
                }                        
            }
            else
            {
                if (transform.position.x >= maxPos - 0.2f)
                {
                    print("build barricade");
                    BarricadeReference.BuildBarricade();
                }
                youShallNotPass = false;
            }
            coolDown = true;
            Invoke("AttackCoolDown", coolDownRateInSeconds);
        }
    }

    public void DamagePlayer(int damage)
    {
        hp -= damage;        
        if (hp <= 0)
        {
            lvl.LoadLevel("03b Lose");
        }
        else
        {
            sprite.color = Color.red;
            Invoke("ColorReset", 0.25f);
        }
    }

    void SpawnNumber(int value, Vector3 spawnPos)
    {
        GameObject numClone = Instantiate(number, transform.position + (Vector3.up*3), Quaternion.identity) as GameObject;
        numClone.GetComponent<NumberDisplay>().SetNumber(value);
        numClone.transform.position = spawnPos + (Vector3.up * 2);
    }

    void AttackCoolDown()
    {
        coolDown = false;
    }
    void ColorReset()
    {
        sprite.color = Color.white;
    }
}
