using System;
using UnityEngine;

namespace DefaultNamespace.Services
{
    public class RayWrapper
    {
        public static RaycastHit2D Raycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            return hit;
        }
    }
}