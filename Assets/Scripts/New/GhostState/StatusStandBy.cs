using System.Reflection;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class Ghost
    {
        public class StatusStandBy : GhostStateBase
        {
            private SpriteRenderer _mainSpriteRenderer;
            private Sprite _mySprite;

            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Ghost owner, GhostStateBase prevState) {
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");

                // このobjectのSpriteRendererを取得
                _mainSpriteRenderer = owner.gameObject.GetComponent<SpriteRenderer>();
                _mySprite = _mainSpriteRenderer.sprite;
                // _mainColor = _mainSpriteRenderer.color;
            
                // 色を判断できないようにする
                // Debug.Log(_mainSpriteRenderer.color);
                // _mainSpriteRenderer.color = GetColor(GhostColor.Gray);
                // Debug.Log(_mainSpriteRenderer.color);
                _mainSpriteRenderer.sprite = owner.HiddenSprite;
            }
            
            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            
            public override void OnUpdate(Ghost owner) { }
            
            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Ghost owner) { }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Ghost owner, GhostStateBase nextState)
            {
                // 色を判断できるようにする
                // _mainSpriteRenderer.color = _mainColor;
                _mainSpriteRenderer.sprite = _mySprite;
            }
        }   
    }
}