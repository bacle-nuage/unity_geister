using UniRx;
using UnityEngine;

namespace Models
{
    public class Player2Model : PlayerModel
    {
        private static PlayerModel _instance = new Player2Model();
        private int _playerNum = 2;

        public static PlayerModel GetInstance()
        {
            return _instance;
        }

        protected override float[,] InitialGhostPosition
        {
            get
            {
                // player2の場合はyの座標を+-反転させる
                // iLen jLen はインデックスなので個数は＋1する
                float[,] newInitialGhostPosition = new float[_initialGhostPositionILen + 1, _initialGhostPositionJLen + 1];
                for (int i = 0; i < _initialGhostPositionILen + 1; i++)
                {
                    newInitialGhostPosition[i, 0] = _initialGhostPosition[i, 0];
                    newInitialGhostPosition[i, 1] = -(_initialGhostPosition[i, 1]);
                }
            
                return newInitialGhostPosition;   
            }
        }

        public override int PlayerNum
        {
            get => _playerNum;
        }
    }
}