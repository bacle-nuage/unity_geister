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
                Vector3 pos1 = owner.transform.position;
                Vector3 pos2 = hit.transform.position;

                owner.transform.position = pos2;
                hit.transform.position = pos1;

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