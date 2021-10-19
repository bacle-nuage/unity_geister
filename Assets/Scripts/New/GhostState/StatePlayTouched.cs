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
                
                // Debug.Log(UpStartPos);
                // Debug.Log(DownStartPos);
                // Debug.Log(LeftStartPos);
                // Debug.Log(RightStartPos);
                
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
                        Debug.Log(Hits[i].collider.transform.name);
                        Debug.Log(Hits[i].collider.GetComponent<Dummy>());
                        Hits[i].collider.GetComponent<Dummy>().Watched();
                    }
                
                    if (Hits[i].collider.transform.parent.name!=owner.transform.parent.name && owner.GetComponent<Ghost>().MyColor == Ghost.GhostColor.Blue && Hits[i].collider.transform.tag == "Goal")
                    {
                        Debug.Log(Hits[i].collider.transform.name);
                        Hits[i].collider.GetComponent<Goal>().Watched();
                    }
                    
                    if (Hits[i].collider.transform.parent.name!=owner.transform.parent.name && Hits[i].collider.GetComponent<Ghost>())
                    {
                        Hits[i].collider.GetComponent<Ghost>().Watched();
                    }
                }

                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
                // Debug.Log(owner.gameObject);
                // Debug.Log(owner.gameObject.transform.parent.gameObject);
                // Debug.Log(owner.gameObject.transform.parent.gameObject.GetComponent<Unit>());
                // owner.gameObject.transform.parent.gameObject.GetComponent<Unit>().PrevButton.SetActive(true);
                // Debug.Log(owner.transform.position);

            }
            
            // if分の中のにはいったかどうか
            private HitItem _hitFlg = HitItem.None;
            private RaycastHit2D hit;

            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            public override void OnUpdate(Ghost owner)
            {
                // Vector3 Direction = owner.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
                // RaycastHit2D ForwardHit = Physics2D.Raycast(owner.transform.position, Vector3.up, maxDistance);
                // RaycastHit2D RearHit = Physics2D.Raycast(owner.transform.position, Vector3.down, maxDistance);
                // RaycastHit2D LeftHit = Physics2D.Raycast(owner.transform.position, Vector3.left, maxDistance);
                // RaycastHit2D RightHit = Physics2D.Raycast(owner.transform.position, Vector3.right, maxDistance);
                
                // Debug.DrawRay(origin, hit.point - origin, Color.blue, RAY_DISPLAY_TIME, false);
                // Debug.DrawRay(owner.transform.position, (Vector3.up - owner.transform.position), Color.cyan, 3, false );
                // Debug.DrawRay(owner.transform.position, (Vector3.down - owner.transform.position), Color.cyan, 3, false );
                // Debug.DrawRay(owner.transform.position, (Vector3.left - owner.transform.position), Color.cyan, 3, false );
                // Debug.DrawRay(owner.transform.position, (Vector3.right - owner.transform.position), Color.cyan, 3, false );

                // if (ForwardHit.collider)
                // {
                //     if (ForwardHit.transform.tag == "Dummy")
                //     {
                //         Debug.Log("ForwardHit");
                //     }
                // }
                // if (RearHit.collider)
                // {
                //     if (RearHit.transform.tag == "Dummy")
                //     {
                //         Debug.Log("RearHit");
                //     }
                // }
                // if (LeftHit.collider)
                // {
                //     if (LeftHit.transform.tag == "Dummy")
                //     {
                //         Debug.Log("LeftHit");
                //     }
                // }
                // if (RightHit.collider)
                // {
                //     if (RightHit.transform.tag == "Dummy")
                //     {
                //         Debug.Log("RightHit");
                //     }
                // }
                
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("7");
                    hit = RayWrapper.MousePositionRaycast();
                    Debug.Log(hit);
                    Debug.Log(hit.collider);

                    if (!hit)
                    {
                        Debug.Log("6");
                        return;
                    }

                    if (hit && hit.collider.gameObject == owner.gameObject)
                    {
                        Debug.Log("5");
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
                    if (hit.collider.gameObject.GetComponent<Goal>()
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Red)
                    {
                        Debug.Log("1");
                        return;
                    }
                    
                    // Debug.Log(_hitFlg);
                    // Debug.Log(Unit.IsTouched );
                    // Debug.Log(hit );
                    // Debug.Log(hit.collider.gameObject.transform.parent.gameObject != owner.gameObject.transform.parent.gameObject);
                    // Debug.Log(hit.collider.gameObject.GetComponent<Goal>());
                    // Debug.Log(this.isOkArea(owner, hit));
                    // Debug.Log(owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Blue);
                    
                    if (_hitFlg == HitItem.None 
                        && Unit.IsTouched 
                        && hit 
                        && hit.collider.gameObject.transform.parent.gameObject != owner.gameObject.transform.parent.gameObject
                        && hit.collider.gameObject.GetComponent<Goal>()
                        && this.isOkArea(owner, hit)
                        && owner.gameObject.GetComponent<Ghost>().MyColor == GhostColor.Blue
                    )
                    {
                        Debug.Log("2");
                        // Debug.Log("HitItem.Goal");
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
                        Debug.Log("3");
                        // Debug.Log("HitItem.Enemy");
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
                        Debug.Log("4");
                        // Debug.Log("HitItem.Dummy");
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
                Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "() dis = " + dis);

                if (dis > limit)
                {
                    result = false;
                }

                return result;
            }
        }
    }
}