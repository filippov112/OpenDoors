using System;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class DataAPI
{
    public int Score;
    public int First;
}

[Serializable]
public class UserAPI
{
    public string userId;
    public string userName;
    public string userIconStr;
}

[Serializable]
public class LangAPI
{
    public string Lang;
}

public class API : MonoBehaviour
{
    public static API Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    // »Õ»÷»¿À»«¿÷»ﬂ ‘”Õ ÷»… œÀ¿√»Õ¿
    [DllImport("__Internal")]
    private static extern void ShowAD();

    [DllImport("__Internal")]
    private static extern bool thisIsDesktop();

    [DllImport("__Internal")]
    private static extern void RequestData();

    [DllImport("__Internal")]
    private static extern void SaveData(string str);


    [DllImport("__Internal")]
    private static extern void RequestUser();

    [DllImport("__Internal")]
    private static extern string RequestLang();


    // ¬Õ”“–≈ÕÕ»≈ ‘”Õ ÷»» «¿œ–Œ—Œ¬   —≈–¬≈–”

    public bool API_IsDesktop()
    {
        //return true;
        return thisIsDesktop();
    }

    public void API_ShowAD()
    {
        ShowAD();
    }

    public void API_GetData()
    {
       RequestData();
    }
    public void API_SaveData(DataAPI env)
    {
        string s = JsonUtility.ToJson(env);
        SaveData(s);
    }

    public void API_GetUser()
    {
        RequestUser();
    }

    public string API_GetLang()
    {
        return RequestLang();
    }

    


    // “Œ◊ » ¿œ»

    public void ContinueLoading()
    {
        Env.Instance.ContinueLoading();
    }

    public void GetData(string str_json)
    {
        DataAPI env = new DataAPI();
        env.First = 1;
        env.Score = 0;
        JsonUtility.FromJsonOverwrite(str_json, env);

        Env.Instance.LoadGame(env);
    }

    public void GetUser(string str_json)
    {
        UserAPI user = new UserAPI();
        user.userId = "";
        user.userName = "";
        user.userIconStr = "";
        JsonUtility.FromJsonOverwrite(str_json, user);

        Env.Instance.LoadUser(user);
    }
}
