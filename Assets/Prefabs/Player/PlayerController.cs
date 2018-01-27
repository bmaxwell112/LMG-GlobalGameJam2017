using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float minPos, maxPos, speed;

    // Update is called once per frame
    void Update () {
        MovementCheck();
    }

    void MovementCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= minPos)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= maxPos)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("attack");
            
            if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 20f))
            {
                print("hit");
            }
        }
    }
}
