using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyPress_MoveSprite : MonoBehaviour
{
    public int speed = 2;

    private float vx = 0;
    private float vy = 0;
    private bool leftFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vx = 0;
        vy = 0;

        if (Input.GetKey("right"))
        {
            vx = speed;
            leftFlag = false;
        }

        if (Input.GetKey("left"))
        {
            vx = -speed;
            leftFlag = true;
        }
        
        if (Input.GetKey("up"))
        {
            vy = speed;
        }

        if (Input.GetKey("down"))
        {
            vy = -speed;
        }
    }

    private void FixedUpdate()
    {
        
        
        
        
        
        this.transform.Translate(vx / 50, vy / 50, 0);
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
    }
}