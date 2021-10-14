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
        /// タッチされているか
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

        [SerializeField]
        private GameObject _prevButton;

        public GameObject PrevButton
        {
            get => _prevButton;
        }

        private void Awake()
        {
            // _currentState = _stateUnActive;
        }

        private void Start()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // _currentState.OnEnter(this, null);
            LastMovedListener();
            _prevButton.SetActive(false);
        }

        private void Update()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // _currentState.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // _currentState.OnFixedUpdate(this);
        }

        public void OnClickPrevButton()
        {
            if (_prevButton)
            {
                Debug.Log("LastMoved.Value = " + LastMoved.Value);
                Vector3 pos1 = LastMoved.Value[0, 0];
                Vector3 pos2 = LastMoved.Value[0, 1];
                
                RaycastHit2D hit1 = RayWrapper.Raycast(Camera.main.WorldToScreenPoint(pos1));
                RaycastHit2D hit2 = RayWrapper.Raycast(Camera.main.WorldToScreenPoint(pos2));
                
                LastMoved.Value = null;
                hit1.transform.position = pos2;
                hit2.transform.position = pos1;

                LastMoved.Value = null;
                IsMoved = false;

                Debug.Log(hit2);
                Debug.Log(hit2.collider);
                Debug.Log(hit2.collider.gameObject);
                Debug.Log(hit2.collider.gameObject.GetComponent<Ghost>());
                
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
    }
}