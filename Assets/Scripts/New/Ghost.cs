using System;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    // ゴースト
    public partial class Ghost : MonoBehaviour, BeingWatched
    {
        public readonly GhostStateBase _stateReady = new StateReady();
        public readonly GhostStateBase _stateReadyTouched = new StateReadyTouched();
        public readonly GhostStateBase _stateStandBy = new StatusStandBy();
        public readonly GhostStateBase _statePlay = new StatePlay();
        public readonly GhostStateBase _statePlayTouched = new StatePlayTouched();
        public readonly GhostStateBase _statePlayTouchedEnemy = new StatePlayTouchedEnemy();
        public readonly GhostStateBase _statePlayTouchedDummyGhost = new StatePlayTouchedDummyGhost();
        public readonly GhostStateBase _stateDead = new StateDead();
        public readonly GhostStateBase _stateGoal = new StateGoal();

        /// <summary>
        /// 現在のステート
        /// </summary>
        public GhostStateBase _currentState = null;

        /// <summary>
        /// 現在のゴーストの色
        /// </summary>
        private Color _currentColor;

        /// <summary>
        /// 現在のゴーストの色
        /// </summary>
        [SerializeField]
        private GhostColor _myColor;

        public GhostColor MyColor
        {
            get
            {
                return _myColor;
            }
        }
        
        /// <summary>
        /// プレイヤー
        /// </summary>
        private GameObject _player;

        [SerializeField]
        private GameObject _dummyGhost;

        [SerializeField]
        private Sprite _hiddenSprite;

        public Sprite HiddenSprite
        {
            get => _hiddenSprite;
        }
        
        private Image _childrenSpriteRenderer;

        private void Awake()
        {
            
        }

        private void Start()
        {
            // _myColor = GetMyColor();
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            _player = this.gameObject.transform.parent.gameObject;
            _currentState = _stateReady;
            _currentColor = GetColor(GhostColor.Gray);
            _currentState.OnEnter(this, null);
            UnitIsActiveListener();
            // _player.gameObject.GetComponent<Unit>().LastMoved.Value = new Vector3[,] {{this.gameObject.transform.position, this.gameObject.transform.position}};
            _childrenSpriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<Image>();
        }

        private void Update()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // Debug.Log("_currentState" + _currentState.GetType().Name);
            _currentState.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            _currentState.OnFixedUpdate(this);
        }

        // ステート変更
        public void ChangeState(GhostStateBase nextState)
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            // Debug.Log(this.gameObject.transform.parent.gameObject.name + " " + this.gameObject.name + " _currentState = " + _currentState);
            _currentState.OnExit(this, nextState);
            nextState.OnEnter(this, _currentState);
            _currentState = nextState;
            // Debug.Log("_currentState = " + _currentState);
        }
        
        public enum GhostColor
        {
            Blue,
            Red,
            Gray
        }

        private static Color GetColor(GhostColor color)
        {
            Color returnColor;
            switch (color)
            {
                case GhostColor.Blue:
                    returnColor = Color.blue;
                    break;
                case GhostColor.Red :
                    returnColor = Color.red;
                    break;
                default:
                    returnColor = Color.gray;
                    break;
            }

            return returnColor;
        }

        private void UnitIsActiveListener()
        {
            Unit Unit = this.gameObject.transform.parent.gameObject.GetComponent<Unit>();
            Unit.IsActive
                .Subscribe((_is_Active) =>
                {
                    // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
                    // Debug.Log(this.gameObject.transform.parent.gameObject.name + ".isActive = " + _is_Active);
                    // Unit Unit = this.gameObject.transform.parent.gameObject.GetComponent<Unit>();
                    Unit Unit = _player.GetComponent<Unit>();
                    // Debug.Log(this.gameObject.transform.parent.gameObject.name + ".IsReady = " + Unit.IsReady);

                    if (_is_Active)
                    {
                        if (Unit.IsReady)
                        {
                            ChangeState(_stateReady);
                        }
                        else
                        {
                            ChangeState(_statePlay);
                        }
                    }
                    else
                    {
                        ChangeState(_stateStandBy);
                    }
                });
        }

        // private GhostColor GetMyColor()
        // {
        //     Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
        //
        //     GhostColor MyColor;
        //     if (color == Color.red)
        //     {
        //         MyColor = GhostColor.Red;
        //     }
        //     else if (color == Color.blue)
        //     {
        //         MyColor = GhostColor.Blue;
        //     }
        //     else
        //     {
        //         MyColor = GhostColor.Gray;
        //     }
        //
        //     return MyColor;
        // }

        public void Watched()
        {
            Color Color = _childrenSpriteRenderer.color;
            _childrenSpriteRenderer.color = new Color(Color.r, Color.g, Color.b, 0.6f);
        }

        public void Out()
        {
            Color Color = _childrenSpriteRenderer.color;
            _childrenSpriteRenderer.color = new Color(Color.r, Color.g, Color.b, 0f);
        }
    }
}