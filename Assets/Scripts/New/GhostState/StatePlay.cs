using System;
using DefaultNamespace.Services;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StatePlay : GhostStateBase
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
            
            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            public override void OnUpdate(Ghost owner)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = RayWrapper.Raycast();

                    // 親プレーヤーを取得
                    Unit Unit = owner.gameObject.transform.parent.gameObject.GetComponent<Unit>();
                    // Debug.Log(Unit.gameObject.name + ".IsTouched = " + Unit.IsTouched);

                    if (!Unit.IsMoved && !_isOnUpdatePass && !Unit.IsTouched && hit && hit.collider.gameObject == owner.gameObject)
                    {
                        // Unit.IsTouched = true;
                        // owner.ChangeState(owner._stateReadyTouched);
                        _isOnUpdatePass = true;
                    }
                }
            }

            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Ghost owner)
            {
                if (_isOnUpdatePass)
                {
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = true;
                    owner.ChangeState(owner._statePlayTouched);
                    _isOnUpdatePass = false;
                }
            }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            }
        }   
    }
}