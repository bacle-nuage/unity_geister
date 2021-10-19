using System;
using System.Collections.Generic;
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
            
            List<RaycastHit2D> Hits = new List<RaycastHit2D>();

            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Ghost owner, GhostStateBase prevState)
            {
                BoxCollider2D col = owner.transform.GetComponent<BoxCollider2D>();
                
                Vector3 UpStartPos = owner.transform.position;
                Vector3 DownStartPos = owner.transform.position;
                Vector3 LeftStartPos = owner.transform.position;
                Vector3 RightStartPos = owner.transform.position;
                
                UpStartPos.y = (UpStartPos.y < 0) ? -(Math.Abs(UpStartPos.y) + col.bounds.size.y) : Math.Abs(UpStartPos.y) + col.bounds.size.y;
                DownStartPos.y = (DownStartPos.y < 0) ? -(Math.Abs(DownStartPos.y) - col.bounds.size.y) : Math.Abs(DownStartPos.y) - col.bounds.size.y;
                LeftStartPos.x = (LeftStartPos.x < 0) ? -(Math.Abs(LeftStartPos.x) - col.bounds.size.x) : Math.Abs(LeftStartPos.x) - col.bounds.size.x;
                RightStartPos.x = (RightStartPos.x < 0) ? -(Math.Abs(RightStartPos.x) + col.bounds.size.x) : Math.Abs(RightStartPos.x) + col.bounds.size.x;
                
                
                Hits = new List<RaycastHit2D>();
                Hits.Add(Physics2D.Raycast(UpStartPos, -Vector2.up));
                Hits.Add(Physics2D.Raycast(DownStartPos, -Vector2.down));
                Hits.Add(Physics2D.Raycast(LeftStartPos, -Vector2.left));
                Hits.Add(Physics2D.Raycast(RightStartPos, -Vector2.right));
                
                for (int i = 0; i < Hits.Count; i++)
                {
                    if (!Hits[i].collider)
                    {
                        continue;
                    }
                    
                    if (Hits[i].collider.transform.tag == "Dummy")
                    {
                        Hits[i].collider.GetComponent<Dummy>().Watched();
                    }
                
                    if (Hits[i].collider.transform.parent.name!=owner.transform.parent.name && owner.GetComponent<Ghost>().MyColor == Ghost.GhostColor.Blue && Hits[i].collider.transform.tag == "Goal")
                    {
                        Hits[i].collider.GetComponent<Goal>().Watched();
                    }
                    
                    if (Hits[i].collider.transform.parent.name!=owner.transform.parent.name && Hits[i].collider.GetComponent<Ghost>())
                    {
                        Hits[i].collider.GetComponent<Ghost>().Watched();
                    }
                }

                // owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().PrevButton.SetActive(true);

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

                    

                    // RedがGoalに来たときは何もせず終了
                    if (hit.collider.gameObject.GetComponent<Goal>()
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Red)
                    {
                        return;
                    }
                    
                    
                    if (_hitFlg == HitItem.None 
                        && Unit.IsTouched 
                        && hit 
                        && hit.collider.gameObject.transform.parent.gameObject != owner.gameObject.transform.parent.gameObject
                        && hit.collider.gameObject.GetComponent<Goal>()
                        && this.isOkArea(owner, hit)
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Blue
                    )
                    {
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
                    
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                    owner.ChangeState(owner._statePlay);
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

                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                    owner.ChangeState(owner._statePlay);
                }
                
                if (_hitFlg == HitItem.Dummy)
                {
                    Vector3 pos1 = owner.transform.position;
                    Vector3 pos2 = hit.transform.position;
                    
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().LastMoved.Value = new Vector3[,] {{pos1, pos2}};
                    owner.transform.position = pos2;
                    hit.transform.position = pos1;

                    // if (pos2.y > 150 || pos2.y < -150)
                    // {
                    //     String MainSystemName = "MainSystem";
                    //     GameObject MainSystem = GameObject.Find(MainSystemName);
                    //     MainSystem.GetComponent<MainSystem>().GameOver();
                    // }

                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsTouched = false;
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved = true;
                    _hitFlg = HitItem.None;
                    owner.ChangeState(owner._statePlay);
                }
            }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
                // owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().PrevButton.SetActive(false);
                if (owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().IsMoved)
                {
                    owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().TurnEndButton.SetActive(true);
                }
                
                for (int i = 0; i < Hits.Count; i++)
                {
                    if (!Hits[i].collider)
                    {
                        continue;
                    }
                    
                    if (Hits[i].collider.transform.tag == "Dummy")
                    {
                        Hits[i].collider.GetComponent<Dummy>().Out();
                    }
                
                    if (Hits[i].collider.transform.tag == "Goal")
                    {
                        Hits[i].collider.GetComponent<Goal>().Out();
                    }
                    
                    if (Hits[i].collider.transform.parent.name!=owner.transform.parent.name && Hits[i].collider.GetComponent<Ghost>())
                    {
                        Hits[i].collider.GetComponent<Ghost>().Out();
                    }
                }
            }

            private bool isOkArea(Ghost owner, RaycastHit2D hit)
            {
                Vector3 ownerPos = owner.gameObject.transform.position;
                Vector3 hitPos = hit.collider.gameObject.transform.position;

                bool result = true;

                float limit = 2.1f;
                float dis = Vector3.Distance(ownerPos, hitPos);

                if (dis > limit)
                {
                    result = false;
                }

                return result;
            }
        }
    }
}