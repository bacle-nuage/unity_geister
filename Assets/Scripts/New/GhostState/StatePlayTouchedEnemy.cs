using System;
using System.Reflection;
using DefaultNamespace.Services;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StatePlayTouchedEnemy : GhostStateBase
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
                if (Input.GetMouseButtonDown(0))
                {
                    // hit = RayWrapper.MousePositionRaycast();
                    //
                    // if (hit && hit.collider.gameObject == owner.gameObject)
                    // {
                    //     Debug.Log("HIT");
                    //     owner.ChangeState(owner._statePlay);
                    //     return;
                    // }
                    //
                    // // 親プレーヤーを取得
                    // Unit Unit = owner.gameObject.transform.parent.gameObject.GetComponent<Unit>();
                    //
                    // // 別の自分のゴーストにタップされたらそのゴーストと場所を入れ替える
                    // String target_1 = "Dummy";
                    // if (!_isOnUpdatePass 
                    //     && Unit.IsTouched 
                    //     && hit 
                    //     && hit.collider.gameObject.transform.parent.gameObject.name == target_1 
                    //     && this.isOkArea(owner, hit)
                    // )
                    // {
                    //     owner.ChangeState(owner._statePlayTouchedEnemy);
                    // }
                    // String target_2 = "Dummy";
                    // if (!_isOnUpdatePass 
                    //     && Unit.IsTouched 
                    //     && hit 
                    //     && hit.collider.gameObject.transform.parent.gameObject.name == target_2 
                    //     && this.isOkArea(owner, hit)
                    //     )
                    // {
                    //     owner.ChangeState(owner._statePlayTouchedDummyGhost);
                    // }
                }
            }

            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Ghost owner)
            {
                
            }
            
            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState) { }
        }
    }
}