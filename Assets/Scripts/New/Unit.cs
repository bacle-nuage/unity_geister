using System;
using System.Reflection;
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

        private void Awake()
        {
            // _currentState = _stateUnActive;
        }

        private void Start()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // _currentState.OnEnter(this, null);
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
    }
}