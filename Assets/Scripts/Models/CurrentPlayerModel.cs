using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Models
{
    public class CurrentPlayerModel
    {
        private static CurrentPlayerModel _instance = new CurrentPlayerModel();
        private PlayerModel _player_1 = Player1Model.GetInstance();
        private PlayerModel _player_2 = Player2Model.GetInstance();
        private int _currentPlayerNum;
        [CanBeNull] private ReactiveProperty<PlayerModel> _currentPlayer = new ReactiveProperty<PlayerModel>();

        public ReactiveProperty<PlayerModel> CurrentPlayer
        {
            get { return _currentPlayer; }
            // set { _currentPlayer.Value = value; }
        }

        // プロパティのsetがうまく動かなかった
        public void SetCurrentPlayer(PlayerModel value)
        {
            

            _currentPlayer.Value = value;
            if (value != null)
            {
                
                _currentPlayerNum = value.PlayerNum;
            }
        }

        /**
         * シングルトンパターンにするためprivateで定義
         */
        private CurrentPlayerModel()
        {
            SetCurrentPlayer(_player_1);

            CurrentPlayer.Where(x => x == null)
                .Subscribe((CurrentPlayer) =>
                {
                    Player1Model.GetInstance().InActive();
                    Player2Model.GetInstance().InActive();
                });
            CurrentPlayer.Where(x => x != null)
                .Subscribe((CurrentPlayer) => { CurrentPlayer.Acrive(); });
        }

        /**
         * シングルトンパターンでインスタンスを取得
         */
        public static CurrentPlayerModel GetInstance()
        {
            return _instance;
        }

        /**
         * 現在のプレイヤーを入れ替える
         */
        public void ChangePlayer()
        {
            if (_currentPlayerNum == _player_1.PlayerNum)
            {
                SetCurrentPlayer(_player_2);
            }
            else
            {
                SetCurrentPlayer(_player_1);
            }
        }

        public void ChangeNonPlayer()
        {
            SetCurrentPlayer(null);
        }
    }
}