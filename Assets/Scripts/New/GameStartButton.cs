using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0) {
            // タッチ情報の取得
            // Touch touch = Input.GetTouch(0);
                    
            // if (touch.phase == TouchPhase.Ended)
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
                if (hit2d)
                {
                    if (hit2d.collider.gameObject.name == this.name)
                    {
                        SceneManager.LoadScene("GameScene");
                    }
                }
            }
        // }
    }
}
