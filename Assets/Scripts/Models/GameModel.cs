using System;
using Presenter;
using UniRx;
using UnityEngine;

namespace Models
{
    public class GameModel
    {
        private static GameModel _instance = new GameModel();
        private CurrentPlayerModel _currentPlayerModel;
        private PlayerModel _player1Model;
        private PlayerModel _player2Model;

        private GameModel()
        {
            _currentPlayerModel = CurrentPlayerModel.GetInstance();
            _player1Model = Player1Model.GetInstance();
            _player2Model = Player2Model.GetInstance();

            _currentPlayerModel.CurrentPlayer.Where(x => x != null)
                .Subscribe((CurrentPlayer) => { SetGhostEvent(); });

            _currentPlayerModel.CurrentPlayer.Where(x => x == null)
                .Subscribe((CurrentPlayer) => { UnSetGhostEvent(); });
        }

        public static GameModel GetInstance()
        {
            return _instance;
        }

        /**
         * Ghost
         */
        public void SetGhostEvent()
        {
            UnSetGhostEvent();

            for (int i = 0; i < _currentPlayerModel.CurrentPlayer.Value.GhostObjects.Length; i++)
            {
                if (!_currentPlayerModel.CurrentPlayer.Value.GhostObjects[i]) continue;
                GhostModel _ghostModel = _currentPlayerModel.CurrentPlayer.Value.GhostObjects[i].gameObject
                    .GetComponent<GhostPresenter>().GhostModel;
                _currentPlayerModel.CurrentPlayer.Value.GhostObjects[i].gameObject.GetComponent<GhostPresenter>()
                    .OnMouseDragListener = GhostOnMouseDragEvent(_ghostModel);
                _currentPlayerModel.CurrentPlayer.Value.GhostObjects[i].gameObject.GetComponent<GhostPresenter>()
                    .OnMouseDownListener = GhostOnMouseDownEvent(_ghostModel);
                _currentPlayerModel.CurrentPlayer.Value.GhostObjects[i].gameObject.GetComponent<GhostPresenter>()
                    .OnMouseUpListener = GhostOnMouseUpEvent(_ghostModel);
            }
        }

        public void UnSetGhostEvent()
        {
            PlayerModel[] _playerModels =
            {
                _player1Model,
                _player2Model
            };

            for (int j = 0; j < _playerModels.Length; j++)
            {
                for (int i = 0; i < _playerModels[j].GhostObjects.Length; i++)
                {
                    if (!_playerModels[j].GhostObjects[i]) continue;
                    _playerModels[j].GhostObjects[i].gameObject.GetComponent<GhostPresenter>().OnMouseDragListener =
                        null;
                    _playerModels[j].GhostObjects[i].gameObject.GetComponent<GhostPresenter>().OnMouseDownListener =
                        null;
                    _playerModels[j].GhostObjects[i].gameObject.GetComponent<GhostPresenter>().OnMouseUpListener = null;
                }
            }
        }

        private Action GhostOnMouseDragEvent(GhostModel _ghostModel)
        {
            return () => { _ghostModel.MoveGhost(); };
        }

        private Action GhostOnMouseDownEvent(GhostModel _ghostModel)
        {
            return () => { _ghostModel.OnMouseDownLocalPosition(); };
        }

        private Action GhostOnMouseUpEvent(GhostModel _ghostModel)
        {
            return () =>
            {
                _ghostModel.JustFitGhost();
                _ghostModel.DropNotArea(_currentPlayerModel.CurrentPlayer.Value);
            };
        }
    }
}