using System;
using Models;
using UniRx;
using UnityEngine;

namespace Presenter
{
    public class ChangedPlayerButtonPresenter : MonoBehaviour
    {
        private ChangedPlayerButtonModel _changedPlayerButtonModel;
        private CurrentPlayerModel _currentPlayerModel;

        private void Awake()
        {
            _changedPlayerButtonModel = new ChangedPlayerButtonModel(this);
            _currentPlayerModel = CurrentPlayerModel.GetInstance();
            // _currentPlayerModel.CurrentPlayer.Where(x => x == null)
            //     .Subscribe((CurrentPlayer) =>
            //     {
            //         Player1Model.GetInstance().InActive();
            //         Player2Model.GetInstance().InActive();
            //     });
            // _currentPlayerModel.CurrentPlayer.Where(x => x != null)
            //     .Subscribe((CurrentPlayer) =>
            //     {
            //         CurrentPlayer.Acrive();
            //     });
        }

        void Start()
        {
            
        }

        void OnMouseDrag()
        {
            
        }

        private void OnMouseUp()
        {
            
        }
        
        private void OnMouseDown()
        {
            _currentPlayerModel.ChangePlayer();
            Destroy(this.gameObject);
        }

    }
}