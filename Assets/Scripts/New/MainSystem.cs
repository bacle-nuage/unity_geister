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

    public ReactiveProperty<bool> IsGameOver
    {
        get => _isGameOver;
    }
    
    private ReactiveProperty<bool> _isPlayer1 = new ReactiveProperty<bool>();
    
    private Unit _winner;
    
    [SerializeField] 
    private Text _winnerText;

    [SerializeField]
    private Text _changedPlayerText;

    [SerializeField]
    private GameObject _initialPosLead;

    public GameObject InitialPosLead
    {
        get => _initialPosLead;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        OnChangedIsPlayerFlg();
        ChangedGameOverListener();
        _resultPanel.SetActive(false);
        _changedPlayerPanel.SetActive(false);
        _initialPosLead.SetActive(false);
        _isGameOver.Value = false;
        _isPlayer1.Value = true;
        _player1.TurnEndButton.SetActive(false);
        _player2.TurnEndButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void PushTurnEndButton()
    {
        if (IsGameOver.Value)
        {
            return;
        }
        _changedPlayerPanel.SetActive(true);
        _initialPosLead.SetActive(false);
        
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
        _player1.gameObject.GetComponent<Unit>().IsMoved = false;
        _player2.gameObject.GetComponent<Unit>().IsMoved = false;
        ChangePlayer();
        _changedPlayerPanel.SetActive(false);
    }

    public void ChangePlayer()
    {
        _isPlayer1.Value = !_isPlayer1.Value;
    }

    void OnChangedIsPlayerFlg()
    {
        _isPlayer1.Subscribe((_isPlayer1) =>
        {
            if (_isPlayer1)
            {
                _player2.IsActive.Value = false;
                _player1.IsActive.Value = true;
                _changedPlayerText.text = "プレイヤー2のターンです。\nプレイヤーが変わったらここをタップしてください。";
                _changedPlayerPanel.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                _player1.IsActive.Value = false;
                _player2.IsActive.Value = true;
                _changedPlayerText.text = "プレイヤー1のターンです\nプレイヤーが変わったらここをタップしてください。";
                _changedPlayerPanel.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        });
    }

    private void ChangedGameOverListener()
    {
        _isGameOver.Where(x => x)
            .Subscribe((_isGameOver) =>
            {
                _player1.PrevButton.SetActive(false);
                _player2.PrevButton.SetActive(false);
                
                if (_isPlayer1.Value)
                {
                    _winner = _player1;
                }
                else
                {
                    _winner = _player2;
                    _resultPanel.transform.Rotate(0.0f, 0.0f, 180);
                }

                _winnerText.text = _winner.Name + "の勝利";
                
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