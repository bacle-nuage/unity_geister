using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMainSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickStartButton()
    {
        String to = "GameScene";
        SceneManager.LoadScene(to);
    }

    public void onClickToDescriptionButton()
    {
        String to = "DescriptionScene";
        SceneManager.LoadScene(to);
    }
}
