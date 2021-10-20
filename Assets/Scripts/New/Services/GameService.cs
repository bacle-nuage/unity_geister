using System;
using UnityEngine;

namespace DefaultNamespace.Services
{
    public class GameService
    {
        public static void createDummyGhost(Vector3 pos)
        {
            Color color = new Color(255f,240f,0,0);
            String parentName = "Dummy";
            GameObject parentGameObject = GameObject.Find(parentName);
            // Texture2D tex = new Texture2D (
            //     width  : 1,
            //     height : 1
            // );
            // Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            // String DummyGameObject
            Sprite sprite = parentGameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;;

            String sortingLayerName = "UI";
            // Vector2 size = new Vector2(1.3f, 1.3f);
            Vector3 size = new Vector3(1f, 1f);
            
            GameObject newGameObject = new GameObject();
            newGameObject.AddComponent<RectTransform>();
            newGameObject.AddComponent<SpriteRenderer>();
            newGameObject.GetComponent<SpriteRenderer>().color = color;
            newGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            newGameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            newGameObject.AddComponent<Dummy>();
            newGameObject.AddComponent<BoxCollider2D>();
            newGameObject.GetComponent<BoxCollider2D>().size = size;
            newGameObject.transform.SetParent(parentGameObject.transform);
            newGameObject.transform.position = pos;
            newGameObject.transform.localScale = new Vector3(90f, 90f, 0);

            String tag = "Dummy";;
            newGameObject.transform.tag = tag;
        }
    }
}