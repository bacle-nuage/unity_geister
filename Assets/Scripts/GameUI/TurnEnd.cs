using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    public GameObject newPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // newPrefab = GameObject.Find("Prefab/BraindCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        
        GameObject newGemeObject = Instantiate(newPrefab) as GameObject;
        
    }
}
