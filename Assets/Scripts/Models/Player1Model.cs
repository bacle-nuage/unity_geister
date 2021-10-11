using UniRx;
using UnityEngine;

namespace Models
{
    public class Player1Model : PlayerModel
    {
        private static PlayerModel _instance = new Player1Model();
        private int _playerNum = 1;

        public static PlayerModel GetInstance()
        {
            return _instance;
        }

        protected override float[,] InitialGhostPosition
        {
            get => _initialGhostPosition;
        }

        public override int PlayerNum
        {
            get => _playerNum;
        }
    }
}