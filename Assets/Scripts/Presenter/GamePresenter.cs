using Models;
using UnityEngine;

namespace Presenter
{
    public class GamePresenter : MonoBehaviour
    {
        private GameModel _gameModel;
        
        void Awake()
        {
            _gameModel = GameModel.GetInstance();
            _gameModel.SetGhostEvent();
        }
    }
}