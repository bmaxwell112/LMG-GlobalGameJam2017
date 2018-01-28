using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float minPos, maxPos, speed, stikeDistance, coolDownRateInSeconds, walkDistance;
    public int escapeNumber, damage, hp;
    public GameObject number, controlPanel, teleport;

    private bool left, coolDown, youShallNotPass, win;
    private int buttonPressed = 0;
    private LevelManager lvl;
    private BarricadeManager BarricadeReference;
    private SpriteRenderer sprite;
    private Animator anim, button;

    private void Start()
    {
        lvl = FindObjectOfType<LevelManager>();
        BarricadeReference = GameObject.FindWithTag("Barricade").GetComponent<BarricadeManager>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        button = controlPanel.GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemy")
        {
            youShallNotPass = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "enemy")
        {
            youShallNotPass = false;
        }
    }

    // Update is called once per frame
    void Update () {
        InputCheck();        
    }

    void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= minPos)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            left = true;
            sprite.flipX = true;
            anim.SetInteger("animState", 1);
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= maxPos)
        {
            if (!youShallNotPass)
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
            left = false;
            sprite.flipX = false;
            anim.SetInteger("animState", 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !left && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            AttackOrBarricade();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && left && transform.position.x <= minPos + 0.5)
        {
            ControlPanelPressed();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("animState", 0);
        }
        if (win)
        {
            WinFunction();
        }
    }

    private void ControlPanelPressed()
    {
        if (!coolDown && !win)
        {
            buttonPressed++;
            SpawnNumber(buttonPressed, transform.position);
            if (buttonPressed >= escapeNumber)
            {
                Instantiate(teleport);
                win = true;
            }
            button.SetTrigger("press");
            coolDown = true;
            anim.SetInteger("animState", 2);
            Invoke("AttackCoolDown", coolDownRateInSeconds);
        }
    }

    void WinFunction()
    {
        sprite.color -= new Color(0, 0, 0, 0.75f) * Time.deltaTime;
        if (sprite.color.a <= 0)
        {
            lvl.LoadLevel("03a Win");
        }
    }

    void AttackOrBarricade()
    {
        if(!coolDown)
        {
            LayerMask mask = 2;
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
            anim.SetInteger("animState", 2);
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
        anim.SetInteger("animState", 0);
    }
    void ColorReset()
    {
        sprite.color = Color.white;
    }
}
