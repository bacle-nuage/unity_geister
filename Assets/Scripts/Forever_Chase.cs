using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forever_Chase : MonoBehaviour
{
    public String targetObjectName;
    public float speed = 5;

    private GameObject targetObject;
    private Rigidbody2D rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find(targetObjectName);
        
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;
        float vx = dir.x * speed;
        float vy = dir.y * speed;

        rbody.velocity = new Vector2(vx, vy);
        this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
    }
}
