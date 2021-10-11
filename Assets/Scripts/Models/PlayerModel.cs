using System;
using System.Collections.Generic;
using Presenter;
using UniRx;
using UnityEngine;

namespace Models
{
    public abstract class PlayerModel
    {
        private ReactiveProperty<int> _active = new ReactiveProperty<int>();
        private GameObject[] _ghostObjects = new GameObject[8];

        public GameObject[] GhostObjects
        {
            get => _ghostObjects;
        }
        
        private ReactiveProperty<bool> _ready = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Ready
        {
            get => _ready;
        }

        // ゴーストの初期位置
        // {x, y}
        protected float[,] _initialGhostPosition = new float[,]
        {
            {-0.15f, -0.65f}, {-0.05f, -0.65f}, {0.05f, -0.65f}, {0.15f, -0.65f},
            {-0.15f, -0.75f}, {-0.05f, -0.75f}, {0.05f, -0.75f}, {0.15f, -0.75f}
        };

        protected int _initialGhostPositionILen;
        protected int _initialGhostPositionJLen;

        protected PlayerModel()
        {
            // メンバ変数の初期化
            _initialGhostPositionILen = _initialGhostPosition.GetUpperBound(0);
            _initialGhostPositionJLen = _initialGhostPosition.GetUpperBound(1);

            // ゴーストを初期配置
            InitializeGhost();

            // ゴーストをすべて非アクティブ
            InActive();

            // _activeを監視
            // 1なら元の色を表示
            _active.Where(x => x == 1).Subscribe((_active) =>
            {
                for (int i = 0; i < _ghostObjects.Length; i++)
                {
                    _ghostObjects[i].gameObject.GetComponent<GhostPresenter>().GhostModel.ChangeColorToOriginal();
                }
            });
            // 0ならグレーアウト
            _active.Where(x => x == 0).Subscribe((_active) =>
            {
                for (int i = 0; i < _ghostObjects.Length; i++)
                {
                    _ghostObjects[i].gameObject.GetComponent<GhostPresenter>().GhostModel.ChangeColorToGray();
                }
            });

            _ready.Value = false;
        }

        public abstract int PlayerNum { get; }

        public void Acrive()
        {
            _active.Value = 1;
        }

        public void InActive()
        {
            _active.Value = 0;
        }

        private void InitializeGhost()
        {
            GameObject newPrefabGhostBlue = Resources.Load("Prefab/CircleBlue") as GameObject;
            GameObject newPrefabGhostRed = Resources.Load("Prefab/CircleRed") as GameObject;

            for (int i = 0; i < _initialGhostPositionILen + 1; i++)
            {
                GameObject newPrefab = (i < 4) ? newPrefabGhostBlue : newPrefabGhostRed;
                GameObject newGameObject = UnityEngine.Object.Instantiate(newPrefab);

                // 親を変更
                newGameObject.transform.parent = GameObject.Find("Field").gameObject.transform;

                // 初期位置に移動
                Vector3 pos = newGameObject.transform.position;
                pos.x = InitialGhostPosition[i, 0];
                pos.y = InitialGhostPosition[i, 1];
                newGameObject.transform.localPosition = pos;
                newGameObject.transform.localScale = newGameObject.transform.lossyScale;
                _ghostObjects[i] = newGameObject;
            }
        }

        // protected abstract float[,] GetInitialGhostPosition();
        protected abstract float[,] InitialGhostPosition { get; }

        public void ReadyOK()
        {
            _ready.Value = true;
        }

        public bool checkReady()
        {
            Dictionary<string, float> FieldSize = FieldModel.FieldSize;
            bool check = true;
            
            for (int i = 0; i < _ghostObjects.Length; i++)
            {
                
                if (_ghostObjects[i].transform.parent.gameObject.name != "Field")
                {
                    
                    check = false;
                    break;
                }
                
                Vector3 pos = _ghostObjects[i].transform.localPosition;
                
                if (pos.x > FieldSize["xMax"] ||
                    pos.x < FieldSize["xMin"] ||
                    pos.y > FieldSize["yMax"] ||
                    pos.y < FieldSize["xMin"])
                {
                    
                    check = false;
                    break;
                }
            }
            

            return check;
        }
    }
}