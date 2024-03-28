using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AD : MonoBehaviour
{

    bool begin = false;
    float time = 3;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject EndCanvas;
    [SerializeField] GameObject ADCanvas;

    // Update is called once per frame
    void Update()
    {
        if (ADCanvas.activeSelf && !begin)
        {
            begin = true;
        }
        if (begin)
        {
            timer.text = Mathf.Round(time).ToString();
            time -= Time.deltaTime;
            if (time < 0)
            {
                begin = false;
                time = 3;
                StartAD(); // Заглушка для рекламного АПИ
                ADCanvas.SetActive(false);
                EndCanvas.SetActive(true);
            }
        }
    }

    void StartAD()
    {
        API.Instance.API_ShowAD();
    }
}
