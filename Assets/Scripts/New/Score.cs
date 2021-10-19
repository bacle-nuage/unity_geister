using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Score : MonoBehaviour
    {
        private ReactiveProperty<int> _redScore = new ReactiveProperty<int>();
        private ReactiveProperty<int> _blueScore = new ReactiveProperty<int>();

        [SerializeField]
        private Text _redScoreText;
        [SerializeField]
        private Text _blueScoreText;

        private void Start()
        {
            _redScore.Value = 0;
            _blueScore.Value = 0;
            ScoreListener();
        }

        public void redAddScore()
        {
            _redScore.Value++;
        }

        public void redRemoveScore()
        {
            _redScore.Value--;
        }
        
        public void blueAddScore()
        {
            _blueScore.Value++;
        }
        
        public void blueRemoveScore()
        {
            _blueScore.Value--;
        }
 
        void Update()
        {
            _redScoreText.text = "× " + _redScore.ToString();
            _blueScoreText.text = "× " + _blueScore.ToString();
        }

        void ScoreListener()
        {
            _redScore.Where(x => x > 3).Subscribe((_score) =>
            {
                String MainSystemName = "MainSystem";
                GameObject MainSystem = GameObject.Find(MainSystemName);
                MainSystem.GetComponent<MainSystem>().ChangePlayer();
                MainSystem.GetComponent<MainSystem>().GameOver();
            });
            
            _blueScore.Where(x => x > 3).Subscribe((_score) =>
            {
                String MainSystemName = "MainSystem";
                GameObject MainSystem = GameObject.Find(MainSystemName);
                MainSystem.GetComponent<MainSystem>().GameOver();
            });
        }
    }
}