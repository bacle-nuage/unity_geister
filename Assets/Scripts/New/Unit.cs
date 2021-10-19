using System;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace.Services;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Unit : MonoBehaviour
    {
        // /// <summary>
        // /// 現在のステート
        // /// </summary>
        // private UnitStateBase _currentState = null;
        
        /// <summary>
        /// アクティブ可否
        /// </summary>
        private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        /// <summary>
        /// タッチされているか
        /// </summary>
        private bool _isTouched = false;

        public bool IsTouched
        {
            get => _isTouched;
            set => _isTouched = value;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _isMoved = false;

        public bool IsMoved
        {
            get => _isMoved;
            set => _isMoved = value;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private bool _isReady = true;

        public bool IsReady
        {
            get => _isReady;
            set => _isReady = value;
        }

        [SerializeField]
        private GameObject _score;

        public GameObject Score
        {
            get => _score;
        }

        [SerializeField]
        private String _name;

        public String Name
        {
            get => _name;
        }

        // private ReactiveProperty<Vector3[,]> _lastMoved;

        public ReactiveProperty<Vector3[,]> LastMoved = new ReactiveProperty<Vector3[,]>();
        // public ReactiveProperty<Vector3[,]> LastMoved
        // {
        //     get => _lastMoved;
        //     set => _lastMoved = value;
        // }

        private GameObject _lastEated = null;

        public GameObject LastEated
        {
            get => _lastEated;
            set => _lastEated = value;
        }

        [SerializeField]
        private GameObject _prevButton;

        public GameObject PrevButton
        {
            get => _prevButton;
        }

        [SerializeField]
        private GameObject _turnEndButton;

        public GameObject TurnEndButton
        {
            get => _turnEndButton;
        }

        private void Awake()
        {
            // _currentState = _stateUnActive;
        }

        private void Start()
        {
            // _currentState.OnEnter(this, null);
            LastMovedListener();
            IsActiveListener();
            _prevButton.SetActive(false);
        }

        private void Update()
        {
            // _currentState.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            // _currentState.OnFixedUpdate(this);
        }

        public void OnClickPrevButton()
        {
            if (_prevButton)
            {
                Vector3 pos1 = LastMoved.Value[0, 0];
                Vector3 pos2 = LastMoved.Value[0, 1];
                
                if (LastEated)
                {
                    RaycastHit2D hit0 = RayWrapper.Raycast(Camera.main.WorldToScreenPoint(pos1));
                    Destroy(hit0.collider);
                    LastEated.SetActive(true);
                    
                    switch (LastEated.gameObject.GetComponent<Ghost>().MyColor)
                    {
                        case Ghost.GhostColor.Red:
                            Score.gameObject.GetComponent<Score>().redRemoveScore();
                            break;
                        case Ghost.GhostColor.Blue:
                            Score.gameObject.GetComponent<Score>().blueRemoveScore();
                            break;
                    }
                }
                
                RaycastHit2D hit1 = RayWrapper.Raycast(Camera.main.WorldToScreenPoint(pos1));
                RaycastHit2D hit2 = RayWrapper.Raycast(Camera.main.WorldToScreenPoint(pos2));
                
                LastMoved.Value = null;
                hit1.transform.position = pos2;
                hit2.transform.position = pos1;

                LastMoved.Value = null;
                IsMoved = false;
                LastEated = null;
                TurnEndButton.SetActive(false);
                
                hit2.collider.gameObject.GetComponent<Ghost>().ChangeState(hit2.collider.gameObject.GetComponent<Ghost>()._statePlay);
            }
        }

        private void LastMovedListener()
        {
            LastMoved.Subscribe((_lastMoved) =>
            {
                if (_lastMoved is null)
                {
                    _prevButton.SetActive(false);
                }
                else
                {
                    _prevButton.SetActive(true);
                }
            });
        }

        private void IsActiveListener()
        {
            this.IsActive.Where(x => x)
                .Subscribe((_is_Active) =>
                {
                    if (_is_Active && this.IsReady)
                    {
                        String MainSystemName = "MainSystem";
                        GameObject MainSystem = GameObject.Find(MainSystemName);
                        if (this.gameObject.name == "Player1")
                        {
                            MainSystem.GetComponent<MainSystem>().InitialPosLead.transform.rotation = Quaternion.Euler(0, 0, 0);;
                            MainSystem.GetComponent<MainSystem>().InitialPosLead.SetActive(true);
                        }
                        else
                        {
                            MainSystem.GetComponent<MainSystem>().InitialPosLead.transform.rotation = Quaternion.Euler(0, 0, 180);;
                            MainSystem.GetComponent<MainSystem>().InitialPosLead.SetActive(true);
                        }
                    }
                });
        }
    }
}