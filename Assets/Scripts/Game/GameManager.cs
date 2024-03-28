using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("����������")]
    [SerializeField] GameObject RightLeftButtons;

    [Header("������� �����")]
    [SerializeField] DoorController d1;
    [SerializeField] DoorController d2;

    [Header("������")]
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject GameCanvas;
    [SerializeField] GameObject EndCanvas;
    [SerializeField] GameObject ADCanvas;

    [Header("��������� Game ������")]
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI PowerReserve;
    [SerializeField] TextMeshProUGUI CurrentSpeed;
    [SerializeField] TextMeshProUGUI Strike;
    [SerializeField] TextMeshProUGUI Fall;
    [SerializeField] TextMeshProUGUI ComboScore;
    [Space(5)]
    [SerializeField] TextMeshProUGUI userText;
    [SerializeField] Image userForm;


    [Header("��������� End ������")]
    [SerializeField] TextMeshProUGUI EndScore;
    [SerializeField] TextMeshProUGUI EndSpeed;
    [SerializeField] TextMeshProUGUI EndStrike;
    [SerializeField] TextMeshProUGUI EndFall;
    [SerializeField] GameObject isRecord;

    [Header("��������� ��������")]
    [SerializeField] float _startSpeed;

    AudioController AC => FindAnyObjectByType<AudioController>();

    bool _game_state = false;

    UserText buffer = null;
    int _score = 0;
    float _currentSpeed = 0;
    int _power_reserve = 0;
    int _combo = 1;
    int _strike = 0;
    int _fall = 0;

    int _maxScore = 0;


    private void Awake()
    {
        instance = this;
        _maxScore = Env.Instance.getRecord();
        RightLeftButtons.SetActive(!Env.Instance.is_desktop);

        // �������������� ��������� ������� ������
        d1.SetStartState();
        d2.SetStartState();

        // ������ ������� ����� ������� ����� ������������, ������� - ������ � �����
        UserText ut = d1.SetParameters(false);
        this.buffer = d2.SetParameters(false);
        userText.color = ut.Color;
        userText.text = ut.Text;
        userForm.sprite = ut.Form;
        userForm.color = ut.Color;

        // ��������� ��������� �������� � ������� � ������
        _currentSpeed = _startSpeed;
        CurrentSpeed.text = _startSpeed.ToString();
        d1.SetSpeed(_startSpeed);
        d2.SetSpeed(_startSpeed);

        // ��������� ����
        PausePlayGame();
    }
    // ���������� ������ ����� ���� � �����, � �� ��� ���� � ������ �� ����� - ���� ������������
    public void SetUserText(UserText ut)
    {
        userText.color = buffer.Color;
        userText.text = buffer.Text;
        userForm.sprite = buffer.Form;
        userForm.color = buffer.Color;
        this.buffer = ut;
    }

    
    public void AddScore()
    {
        // ��������� ���������
        _strike++; 
        Strike.text = _strike.ToString();
        
        // �������� ����
        _score += _combo; 
        Score.text = _score.ToString();
        if (_score > _maxScore)
        {
            _maxScore = _score;
            Env.Instance.setRecord(_score);
            isRecord.SetActive(true);
        }  
        
        // �������� ����� ���������
        _combo++; 
        ComboScore.text = _combo.ToString();
        
        // �������� ����� �����
        _power_reserve = Mathf.Clamp(_power_reserve + 1, 0, 3); 
        PowerReserve.text = _power_reserve.ToString();
        
        // �������� ��������
        _currentSpeed = d1.AddSpeed();
        CurrentSpeed.text = _currentSpeed.ToString(); 
        d2.AddSpeed();
    }
    public void PopScore()
    {
        // ��������� ������
        _fall++; 
        Fall.text = _fall.ToString();
        
        // ������� ����� ���������
        _combo = 1; 
        ComboScore.text = _combo.ToString();
        
        // �������� ����� �����
        if (_power_reserve == 0)
        {
            EndGame();
        } else
        {
            // ��������� ����� �����
            _power_reserve -= 1; 
            PowerReserve.text = _power_reserve.ToString();
        }
    }

    
    public void EndGame()
    {
        _game_state = false;
        EndScore.text = _score.ToString();
        EndSpeed.text = _currentSpeed.ToString();
        EndStrike.text = _strike.ToString();
        EndFall.text = _fall.ToString();

        AC.StopAudio();
        UpdateGameState();

        GameCanvas.SetActive(false);
        if (Env.Instance.TimeAD())
            ADCanvas.SetActive(true);//EndCanvas.SetActive(true);
        else
            EndCanvas.SetActive(true);
    }

    public void PausePlayGame()
    {
        GameCanvas.SetActive(!GameCanvas.activeSelf);
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
        _game_state = !_game_state;
        UpdateGameState();
        if (_game_state)
        {
            AC.PlayBack();
        }
        else
        {
            AC.StopAudio();
        }
    }

    private void UpdateGameState()
    {
        d1.SetStateRunning(_game_state);
        d2.SetStateRunning(_game_state);
        PlayerMove.SwitchMove(_game_state);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
