using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [Header("Пользователь")]
    [SerializeField] GameObject userBlock;
    [SerializeField] TextMeshProUGUI userName;
    [SerializeField] RawImage userIcon;
    [SerializeField] TextMeshProUGUI maxScore;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RefreshScore();
    }

    void RefreshFull()
    {
        (string id, string name, int score, Texture icon) = Env.Instance.GetMenuInfo();
        SetUserData(id, name, score, icon);
    }

    void RefreshScore()
    {
        int score = Env.Instance.getRecord();
        switch (Env.Instance.getLang())
        {
            case "en":
                maxScore.text = $"Best result: {score}";
                break;
            case "tr":
                maxScore.text = $"En iyi sonuç: {score}";
                break;
            default:
                maxScore.text = $"Лучший результат: {score}";
                break;
        }
    }

    public void StartGame()
    {
        if (Env.Instance.isFirst())
        {
            Env.Instance.setNoFirst();
            Env.Instance.tutorial_from_play = true;
            StartTutorial();
        }
        else
        {
            SceneManager.LoadScene(1); 
        }
    }

    private void Update()
    {
        if (Env.Instance.geted_user && !userBlock.activeSelf)
        {
            RefreshFull();
        }
    }

    public void SetUserData(string id, string name, int score, Texture icon)
    {   
        userName.text = name;
        userIcon.texture = icon;
        switch (Env.Instance.getLang())
        {
            case "en":
                maxScore.text = $"Best result: {score}";
                break;
            case "tr":
                maxScore.text = $"En iyi sonuç: {score}";
                break;
            default:
                maxScore.text = $"Лучший результат: {score}";
                break;
        }
        if (Env.Instance.geted_user)
            userBlock.SetActive(true);
    }

    public void SwitchLanguage()
    {
        string current = Env.Instance.getLang();
        switch (current)
        {
            case "en":
                Env.Instance.setLang("tr");
                break;
            case "tr":
                Env.Instance.setLang("ru");
                break;
            default:
                Env.Instance.setLang("en");
                break;
        }
        RefreshFull();
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(3);
    }
}
