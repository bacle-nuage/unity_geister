using System;
using System.Collections.Generic;
using Presenter;
using UnityEngine;

namespace Models
{
    public class GhostModel
    {
        private Color _color;
        private Vector3 _position;
        private Vector3 _localPosition;
        private Rigidbody2D _rbody;
        private Collider2D _collider2D;
        private GhostPresenter _ghostPresenter;
        private Vector3 _onMouseDownLocalPosition;
        private Collider2D _collider;

        public GhostModel(GhostPresenter GhostPresenter)
        {
            _ghostPresenter = GhostPresenter;
            _position = _ghostPresenter.transform.position;
            _localPosition = _ghostPresenter.transform.localPosition;

            _ghostPresenter.gameObject.AddComponent<Rigidbody2D>();
            _ghostPresenter.gameObject.AddComponent<CircleCollider2D>();

            _rbody = _ghostPresenter.GetComponent<Rigidbody2D>();
            _rbody.gravityScale = 0;
            _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            _collider2D = _ghostPresenter.GetComponent<CircleCollider2D>();
            _collider2D.isTrigger = true;

            _color = _ghostPresenter.GetComponent<Renderer>().material.color;
        }

        public void MoveGhost()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = _position.z;
            _ghostPresenter.transform.position = mousePos;
        }

        public void JustFitGhost()
        {
            GameObject _parent = _ghostPresenter.transform.parent.gameObject;
            Dictionary<string, float> FieldSize = FieldModel.FieldSize;

            // 範囲外に置こうとしときは元の位置に戻す
            if (_ghostPresenter.transform.localPosition.x > FieldSize["_xMax"] ||
                _ghostPresenter.transform.localPosition.x < FieldSize["_xMin"] ||
                _ghostPresenter.transform.localPosition.y > FieldSize["_yMax"] ||
                _ghostPresenter.transform.localPosition.y < FieldSize["_yMin"])
            {
                _ghostPresenter.transform.SetParent(_parent.transform, false);
                _ghostPresenter.transform.localPosition = _localPosition;

                return;
            }

            _localPosition.x = Mathf.Clamp(Rounding(_ghostPresenter.transform.localPosition.x), FieldSize["_xMin"], FieldSize["_xMax"]);
            _localPosition.y = Mathf.Clamp(Rounding(_ghostPresenter.transform.localPosition.y), FieldSize["_yMin"], FieldSize["_yMax"]);

            _ghostPresenter.transform.localPosition = _localPosition;
        }

        private float Rounding(float axis)
        {
            const float _pitch = 0.1f; //フィールドのマス目のピッチ
            // マス数を計算
            float dec = axis / _pitch;
            // マス数の絶対値を取得
            float abs = Math.Abs(dec);
            // 小数点以下を切り上げ
            float conf = (float) (Math.Ceiling(abs) * _pitch);
            // マスの中心になるようにpitchの半分を引く
            conf = conf - (_pitch / 2);
            // 絶対値を取得しているためマイナスの場合はマイナスにする
            if (axis < 0) conf = conf * (-1);

            return conf;
        }

        public void OnMouseDownLocalPosition()
        {
            _onMouseDownLocalPosition = _ghostPresenter.transform.localPosition;
        }

        public void DropNotArea(PlayerModel Player)
        {
            // 準備完了していたら何もしない
            if (Player.Ready.Value)
            {
                return;
            }
            
            float Xmax = 0.2f; //フィールドのX座標上限
            float Xmin = -0.2f; //フィールドのX座標下限
            float Ymax = -0.1f; //フィールドのY座標上限
            float Ymin = -0.3f; //フィールドのY座標下限

            if (Player.PlayerNum == 2)
            {
                Xmax = 0.2f; //フィールドのX座標上限
                Xmin = -0.2f; //フィールドのX座標下限
                Ymax = 0.3f; //フィールドのY座標上限
                Ymin = 0.1f; //フィールドのY座標下限
            }
            
            // 範囲外に置こうとしときは元の位置に戻す
            if (_ghostPresenter.transform.localPosition.x > Xmax ||
                _ghostPresenter.transform.localPosition.x < Xmin ||
                _ghostPresenter.transform.localPosition.y > Ymax ||
                _ghostPresenter.transform.localPosition.y < Ymin)
            {
                _ghostPresenter.transform.localPosition = _onMouseDownLocalPosition;
                return;
            }
        }

        public void ChangeColorToGray()
        {
            _ghostPresenter.GetComponent<Renderer>().material.color = Color.gray;
        }

        public void ChangeColorToOriginal()
        {
            _ghostPresenter.GetComponent<Renderer>().material.color = _color;
        }
    }
}