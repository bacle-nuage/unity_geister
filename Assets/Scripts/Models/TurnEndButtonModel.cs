using Presenter;
using UnityEngine;

namespace Models
{
    public class TurnEndButtonModel
    {
        private MonoBehaviour _turnEndButtonPresenter;
        private Rigidbody2D _rbody;
        
        public TurnEndButtonModel(MonoBehaviour TurnEndButtonPresenter)
        {
            _turnEndButtonPresenter = TurnEndButtonPresenter;

            _turnEndButtonPresenter.gameObject.AddComponent<Rigidbody2D>();
            _turnEndButtonPresenter.gameObject.AddComponent<BoxCollider2D>();
            
            _rbody = _turnEndButtonPresenter.GetComponent<Rigidbody2D>();
            _rbody.gravityScale = 0;
            _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        void ShowChangedPlayerButton()
        {
            // _turnEndButtonPresenter.Name.Instantiate(GameObject.Find("Prefab/BraindCanvas"));
        }
    }
}