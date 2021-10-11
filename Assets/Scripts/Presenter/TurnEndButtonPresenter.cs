using System;
using Models;
using UnityEngine;

namespace Presenter
{
    public class TurnEndButtonPresenter : MonoBehaviour
    {
        private TurnEndButtonModel _turnEndButtonModel;
        private CurrentPlayerModel _currentPlayerModel;
        // private GhostModel _ghostModel;
        private PlayerModel _player1;
        private PlayerModel _player2;

        private void Awake()
        {
            _turnEndButtonModel = new TurnEndButtonModel(this);
            _currentPlayerModel = CurrentPlayerModel.GetInstance();
            _player1 = Player1Model.GetInstance();
            _player2 = Player2Model.GetInstance();
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
            
            if (_currentPlayerModel.CurrentPlayer.Value.checkReady())
            {
                
                _currentPlayerModel.CurrentPlayer.Value.ReadyOK();
                _currentPlayerModel.ChangeNonPlayer();
            
                // Instantiate()がモデルで使用できなかった
                GameObject newPrefab = Resources.Load("Prefab/Braind") as GameObject;
                Instantiate(newPrefab);
            }
            
        }

    }
}