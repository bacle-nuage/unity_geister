using System;
using UnityEngine;

namespace DefaultNamespace.Services
{
    public class RayWrapper
    {
        public static RaycastHit2D MousePositionRaycast()
        {
            RaycastHit2D hit = Raycast(Input.mousePosition);

            return hit;
        }
        
        public static RaycastHit2D Raycast(Vector3 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            return hit;
        }
    }
}