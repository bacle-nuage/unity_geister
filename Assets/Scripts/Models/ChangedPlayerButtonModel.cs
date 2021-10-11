using Presenter;
using UnityEngine;

namespace Models
{
    public class ChangedPlayerButtonModel
    {
        private ChangedPlayerButtonPresenter _changedPlayerButtonPresenter;
        private Rigidbody2D _rbody;

        public ChangedPlayerButtonModel(ChangedPlayerButtonPresenter ChangedPlayerButtonPresenter)
        {
            _changedPlayerButtonPresenter = ChangedPlayerButtonPresenter;
            
            _changedPlayerButtonPresenter.gameObject.AddComponent<Rigidbody2D>();
            _changedPlayerButtonPresenter.gameObject.AddComponent<CircleCollider2D>();

            _rbody = _changedPlayerButtonPresenter.GetComponent<Rigidbody2D>();
            _rbody.gravityScale = 0;
            _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}