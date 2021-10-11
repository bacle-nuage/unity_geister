using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forever_Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float angle = 90;
    private void FixedUpdate()
    {
        this.transform.Rotate(0,0,angle);
    }
}
