using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionMainSystem : MonoBehaviour
{
    public void onClickToTitleButton()
    {
        String to = "TitleScene";
        SceneManager.LoadScene(to);
    }
}
