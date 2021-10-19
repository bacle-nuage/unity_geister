using System.Reflection;
using DefaultNamespace.Services;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StateReadyTouched : GhostStateBase
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
                if (Input.GetMouseButtonDown(0))
                {
                    hit = RayWrapper.MousePositionRaycast();

                    if (hit && hit.collider.gameObject == owner.gameObject)
                    {
                        owner.ChangeState(owner._stateReady);
                        return;
                    }

                    // 親プレーヤーを取得
                    Unit Unit = owner.gameObject.transform.parent.gameObject.GetComponent<Unit>();

                    // 別の自分のゴーストにタップされたらそのゴーストと場所を入れ替える
                    if (!_isOnUpdatePass && Unit.IsTouched && hit && hit.collider.gameObject.transform.parent.gameObject == owner.transform.parent.gameObject)
                    {
                        // Vector3 pos1 = owner.transform.position;
                        // Vector3 pos2 = hit.transform.position;
                        //
                        // owner.transform.position = pos2;
                        // hit.transform.position = pos1;
                        //
                        // owner.ChangeState(owner._stateReady);
                        // Unit.IsTouched = false;
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
                    Vector3 pos1 = owner.transform.position;
                    Vector3 pos2 = hit.transform.position;

                    owner.transform.position = pos2;
                    hit.transform.position = pos1;

                    owner.ChangeState(owner._stateReady);
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    _isOnUpdatePass = false;
                }
            }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
                owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                _isOnUpdatePass = false;
            }
        }   
    }
}