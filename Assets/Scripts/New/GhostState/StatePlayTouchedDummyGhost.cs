using System;
using System.Reflection;
using DefaultNamespace.Services;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StatePlayTouchedDummyGhost : GhostStateBase
        {
            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Ghost owner, GhostStateBase prevState)
            {
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            }
            
            // if分の中のにはいったかどうか
            private bool _isOnUpdatePass = false;
            private RaycastHit2D hit;

            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            public override void OnUpdate(Ghost owner)
            {
            }

            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Ghost owner)
            {
                Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
                Vector3 pos1 = owner.transform.position;
                Vector3 pos2 = hit.transform.position;
                Debug.Log("pos1=" + pos1);
                Debug.Log("pos2=" + pos2);

                owner.transform.position = pos2;
                hit.transform.position = pos1;
                Debug.Log("owner.transform.position= " + owner.transform.position);
                Debug.Log("hit.transform.position= " + hit.transform.position);

                owner.ChangeState(owner._statePlay);
                owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                _isOnUpdatePass = false;
            }
            
            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState) { }
        }
    }
}