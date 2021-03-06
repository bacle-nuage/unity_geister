using System;
using System.Reflection;
using DefaultNamespace.Services;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StateReady : GhostStateBase
        {
            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Ghost owner, GhostStateBase prevState)
            {
                if (owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsActive.Value)
                {
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().TurnEndButton.SetActive(true);
                    
                    // String MainSystemName = "MainSystem";
                    // GameObject MainSystem = GameObject.Find(MainSystemName);
                    // if (owner.transform.tag == "Player2")
                    // {
                    //     MainSystem.GetComponent<MainSystem>().InitialPosLead.transform.Rotate(0f, 0f, 180f);
                    // }
                    // MainSystem.GetComponent<MainSystem>().InitialPosLead.SetActive(true);
                }
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
                    RaycastHit2D hit = RayWrapper.MousePositionRaycast();

                    // 親プレーヤーを取得
                    Unit Unit = owner.gameObject.transform.parent.gameObject.GetComponent<Unit>();

                    if (!_isOnUpdatePass && !Unit.IsTouched && hit && hit.collider.gameObject == owner.gameObject)
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
                    owner.ChangeState(owner._stateReadyTouched);
                    _isOnUpdatePass = false;
                }
            }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
            }
        }
    }
}