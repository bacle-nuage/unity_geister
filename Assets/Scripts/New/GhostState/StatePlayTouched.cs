using System;
using System.Reflection;
using DefaultNamespace.Services;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StatePlayTouched : GhostStateBase
        {
            private enum HitItem
            {
                None,
                Dummy,
                Enemy,
                Goal,
            }
            
            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Ghost owner, GhostStateBase prevState)
            {
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
                // Debug.Log(owner.gameObject);
                // Debug.Log(owner.gameObject.transform.parent.gameObject);
                // Debug.Log(owner.gameObject.transform.parent.gameObject.GetComponent<Unit>());
                // owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().PrevButton.SetActive(true);
                Debug.Log(owner.transform.position);
            }
            
            // if分の中のにはいったかどうか
            private HitItem _hitFlg = HitItem.None;
            private RaycastHit2D hit;

            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            public override void OnUpdate(Ghost owner)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit = RayWrapper.MousePositionRaycast();

                    if (!hit)
                    {
                        return;
                    }

                    if (hit && hit.collider.gameObject == owner.gameObject)
                    {
                        owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                        owner.ChangeState(owner._statePlay);
                        return;
                    }

                    // 親プレーヤーを取得
                    Unit Unit = owner.gameObject.transform.parent.gameObject.GetComponent<Unit>();
                    
                    // 別の自分のゴーストにタップされたらそのゴーストと場所を入れ替える
                    // String target_1 = "Unit";

                    
                    // Debug.Log(hit.collider.gameObject.transform.parent.gameObject.GetComponent<Unit>());
                    // Debug.Log(hit.collider.gameObject.transform.parent.gameObject.GetComponent<Unit>()?.name == target_1 );

                    // RedがGoalに来たときは何もせず終了
                    if (hit.collider.gameObject.GetComponent<Gool>()
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Red)
                    {
                        return;
                    }
                    
                    if (_hitFlg == HitItem.None 
                        && Unit.IsTouched 
                        && hit 
                        && hit.collider.gameObject.transform.parent.gameObject != owner.gameObject.transform.parent.gameObject
                        && hit.collider.gameObject.GetComponent<Gool>()
                        && this.isOkArea(owner, hit)
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Blue
                    )
                    {
                        Debug.Log("HitItem.Goal");
                        _hitFlg = HitItem.Goal;
                    }

                    if (_hitFlg == HitItem.None 
                        && Unit.IsTouched 
                        && hit 
                        && hit.collider.gameObject.transform.parent.gameObject != owner.gameObject.transform.parent.gameObject  // 自分と違う親であれば敵とみなす
                        && hit.collider.gameObject.transform.parent.gameObject.GetComponent<Unit>()
                        && this.isOkArea(owner, hit)
                    )
                    {
                        Debug.Log("HitItem.Enemy");
                        _hitFlg = HitItem.Enemy;
                    }
                    
                    String target_2 = "Dummy";
                    if (_hitFlg == HitItem.None 
                        && Unit.IsTouched 
                        && hit 
                        && hit.collider.gameObject.transform.parent.gameObject.name == target_2 
                        && this.isOkArea(owner, hit)
                        )
                    {
                        Debug.Log("HitItem.Dummy");
                        _hitFlg = HitItem.Dummy;
                    }
                }
            }

            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Ghost owner)
            {
                if (_hitFlg == HitItem.Goal)
                {
                    Vector3 pos1 = owner.transform.position;
                    Vector3 pos2 = hit.transform.position;

                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().LastMoved.Value = new Vector3[,] {{pos1, pos2}};
                    owner.transform.position = pos2;
                    hit.transform.position = pos1;
                    
                    hit.collider.gameObject.SetActive(false);
                    GameService.createDummyGhost(pos1);

                    String MainSystemName = "MainSystem";
                    GameObject MainSystem = GameObject.Find(MainSystemName);
                    MainSystem.GetComponent<MainSystem>().GameOver();
                    
                    owner.ChangeState(owner._statePlay);
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                }
                
                if (_hitFlg == HitItem.Enemy)
                {
                    Vector3 pos1 = owner.transform.position;
                    Vector3 pos2 = hit.transform.position;

                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().LastMoved.Value = new Vector3[,] {{pos1, pos2}};
                    owner.transform.position = pos2;
                    hit.transform.position = pos1;
                    
                    
                    hit.collider.gameObject.SetActive(false);
                    GameService.createDummyGhost(pos1);
                 
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().LastEated = hit.collider.gameObject;
                    Unit Unit = owner.gameObject.transform.parent.GetComponent<Unit>();
                    Score Score = Unit.Score.gameObject.GetComponent<Score>();
                    switch (hit.collider.gameObject.GetComponent<Ghost>().MyColor)
                    {
                        case GhostColor.Red:
                            Score.redAddScore();
                            break;
                        case GhostColor.Blue:
                            Score.blueAddScore();
                            break;
                    }

                    owner.ChangeState(owner._statePlay);
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                }
                
                if (_hitFlg == HitItem.Dummy)
                {
                    Vector3 pos1 = owner.transform.position;
                    Vector3 pos2 = hit.transform.position;
                    
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().LastMoved.Value = new Vector3[,] {{pos1, pos2}};
                    owner.transform.position = pos2;
                    hit.transform.position = pos1;

                    // Debug.Log("pos2.y = " + pos2.y);
                    // if (pos2.y > 150 || pos2.y < -150)
                    // {
                    //     String MainSystemName = "MainSystem";
                    //     GameObject MainSystem = GameObject.Find(MainSystemName);
                    //     MainSystem.GetComponent<MainSystem>().GameOver();
                    // }

                    owner.ChangeState(owner._statePlay);
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                }
            }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
                // owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().PrevButton.SetActive(false);
                // Debug.Log(owner.transform.position);
                owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().TurnEndButton.SetActive(true);
            }

            private bool isOkArea(Ghost owner, RaycastHit2D hit)
            {
                Vector3 ownerPos = owner.gameObject.transform.position;
                Vector3 hitPos = hit.collider.gameObject.transform.position;

                bool result = true;

                float limit = 2f;
                float dis = Vector3.Distance(ownerPos, hitPos);
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "() dis = " + dis);

                if (dis > limit)
                {
                    result = false;
                }

                return result;
            }
        }
    }
}