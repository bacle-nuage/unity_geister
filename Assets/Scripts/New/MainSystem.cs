using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unit = DefaultNamespace.Unit;

public class MainSystem : MonoBehaviour
{
    [SerializeField]
    private Unit _player1;
    
    [SerializeField]
    private Unit _player2;
    
    [SerializeField]
    private GameObject _resultPanel;
    
    [SerializeField]
    private GameObject _changedPlayerPanel;
    
    private ReactiveProperty<bool> _isGameOver = new ReactiveProperty<bool>();
    
    private ReactiveProperty<bool> _isPlayer1 = new ReactiveProperty<bool>();
    
    private Unit _winner;
    
    [SerializeField] 
    private Text _winnerText;

    [SerializeField]
    private Text _changedPlayerText;
    
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
        OnChangedIsPlayerFlg();
        OnChangedIsGameOverFlg();
        _resultPanel.SetActive(false);
        _changedPlayerPanel.SetActive(false);
        _isGameOver.Value = false;
        _isPlayer1.Value = true;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void PushTurnEndButton()
    {
        // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
        _changedPlayerPanel.SetActive(true);
        
        _player2.IsActive.Value = false;
        _player1.IsActive.Value = false;
        
        _player2.LastMoved.Value = null;
        _player1.LastMoved.Value = null;
        
        _player2.LastEated = null;
        _player1.LastEated = null;
        
        _player2.TurnEndButton.SetActive(false);
        _player1.TurnEndButton.SetActive(false);
        
        if (_isPlayer1.Value)
        {
            _player1.IsReady = false;
        }
        else
        {
            _player2.IsReady = false;
        }
    }

    public void PushPlayerChangedButton()
    {
        // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
        _player1.gameObject.GetComponent<Unit>().IsMoved = false;
        _player2.gameObject.GetComponent<Unit>().IsMoved = false;
        ChangePlayer();
        _changedPlayerPanel.SetActive(false);
    }

    public void ChangePlayer()
    {
        // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
        _isPlayer1.Value = !_isPlayer1.Value;
    }

    void OnChangedIsPlayerFlg()
    {
        _isPlayer1.Subscribe((_isPlayer1) =>
        {
            // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
            if (_isPlayer1)
            {
                _player2.IsActive.Value = false;
                _player1.IsActive.Value = true;
                _changedPlayerText.text = "プレイヤー１のターンです";
            }
            else
            {
                _player1.IsActive.Value = false;
                _player2.IsActive.Value = true;
                _changedPlayerText.transform.Rotate(0,0,180);
                _changedPlayerText.text = "プレイヤー２のターンです";
            }
        });
    }

    private void OnChangedIsGameOverFlg()
    {
        _isGameOver.Where(x => x)
            .Subscribe((_isGameOver) =>
            {
                if (_isPlayer1.Value)
                {
                    _winner = _player1;
                }
                else
                {
                    _winner = _player2;
                    _winnerText.transform.Rotate(0.0f, 0.0f, 180);
                }

                _winnerText.text = _winner.Name + "の勝利";
                
                // Debug.Log(this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
                _resultPanel.SetActive(true);
            });
    }

    public void GameOver()
    {
        _isGameOver.Value = true;
    }

    public void onClickToTitleButton()
    {
        String to = "TitleScene";
        SceneManager.LoadScene(to);
    }

    public void onClickRetryButton()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}