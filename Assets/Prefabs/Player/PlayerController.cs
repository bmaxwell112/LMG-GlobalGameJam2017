using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float minPos, maxPos, speed;
    public int escapeNumber;

    private bool left;
    private int buttonPressed = 0;

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
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= maxPos)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            left = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !left)
        {      
            if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 20f))
            {
                print("hit");
            }
            else
            {
                if(transform.position.x >= maxPos - 0.2f)
                {
                    print("build barricade");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && left && transform.position.x <= minPos + 0.5)
        {
            print("escape button");
            buttonPressed++;
            if(buttonPressed >= escapeNumber)
            {
                LevelManager lvl = FindObjectOfType<LevelManager>();
                lvl.LoadLevel("03a Win");
            }
        }
    }
}
