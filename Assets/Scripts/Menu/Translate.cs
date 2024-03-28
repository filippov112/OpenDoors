using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Translate : MonoBehaviour
{
    TextMeshProUGUI self => GetComponent<TextMeshProUGUI>();

    [SerializeField] string ru;
    [SerializeField] string en;
    [SerializeField] string tr;

    void Start()
    {
        switch (Env.Instance.getLang())
        {
            case "en":
                self.text = en;
                break;
            case "tr":
                self.text = tr;
                break;
            default:
                self.text = ru;
                break;
        }
    }
}
