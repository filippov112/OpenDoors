using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Env : MonoBehaviour
{
    public static Env Instance;

    private DataAPI _env;
    private UserAPI _user;
    private string _lang;

    private bool _mute;
    public float last_ad_show;
    float _adTime;
    public bool tutorial_from_play;
    Texture icon;

    public bool is_loading;
    public bool geted_user;
    public bool is_desktop;


    //void Test() // убрать
    //{
    //    is_desktop = false;
    //    _env = new DataAPI();
    //    _env.Score = 0;
    //    _env.First = 0;
    //    _user = new UserAPI();
    //    _user.userName = "";
    //    _user.userIconStr = "";
    //    _user.userId = "";
    //    _lang = "ru";

    //    _mute = true;
    //    last_ad_show = 0;
    //    _adTime = 6000;
    //    tutorial_from_play = false;
    //    icon = default;
    //    is_loading = false;
    //}
    //private void Awake() // убрать
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this);
    //        Test();
    //    }
    //    else
    //        Destroy(gameObject);
    //}


    //Не дожидаясь готовности браузера устанавливаем начальные параметры, и ждем команды
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            _lang = "ru";
            _mute = true;
            last_ad_show = 0;
            _adTime = 70;
            tutorial_from_play = false;
            icon = default;
            is_loading = true;
            geted_user = false;
        }
        else
            Destroy(gameObject);
    }

    // Сюда поступает команда о готовности получать запросы, запрашиваем всю информацию, пока не придет ответ, ждем...
    public void ContinueLoading()
    {
        is_desktop = API.Instance.API_IsDesktop();
        API.Instance.API_GetData();
        API.Instance.API_GetUser();
        _lang = API.Instance.API_GetLang();
    }


    // ТОЧКИ АПИ
    
    // Как только приходят данные обновляем их и запускаем заставку
    public void LoadGame(DataAPI __env)
    {
        _env = __env;
        is_loading = false;
    }


    public void LoadUser(UserAPI __user)
    {
        _user = __user;
        if (_user.userIconStr != "")
            GetAndConvertTexture(_user.userIconStr);
        if (_user.userName != "")
            geted_user = true;
    }
    void GetAndConvertTexture(string url)
    {
        StartCoroutine(DownloadImage(url));
    }
    IEnumerator DownloadImage(string mediaurl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaurl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            icon =  ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }





    // ВНУТРЕННИЕ ФУНКЦИИ

    public bool TimeAD()
    {
        if (last_ad_show + _adTime <= Time.time)
        {
            last_ad_show = Time.time;
            return true;
        }
        return false;
    }



    // MENU
    public (string, string, int, Texture) GetMenuInfo()
    {
        return (_user.userId, _user.userName, _env.Score, icon);
    }



    // RECORD
    public void setRecord(int newRecord)
    {
        _env.Score = newRecord;
        API.Instance.API_SaveData(_env);
    }
    public int getRecord()
    {
        return _env.Score;
    }



    // MUTE
    public bool getMute()
    {
        return _mute;
    }
    public void setMute(bool move)
    {
        _mute = move;
    }



    // LANGUAGE
    public string getLang()
    { 
        return _lang; 
    }
    public void setLang(string l)
    {
        _lang = l;
    }


    // FIRST TIME
    public bool isFirst()
    { 
        return _env.First > 0; 
    }

    public void setNoFirst()
    {
        _env.First = 0;
        API.Instance.API_SaveData(_env);
    }
}
