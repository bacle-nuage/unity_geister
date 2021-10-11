using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sometime_flip : MonoBehaviour
{
    public int maxCount = 100;

    private int count = 0;
    private bool flipFlag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count = count + 1;

        if (count >= maxCount)
        {
            this.transform.Rotate(0, 0, 180);

            flipFlag = !flipFlag;
            this.GetComponent<SpriteRenderer>().flipY = flipFlag;

            count = 0;
        }
    }
}
