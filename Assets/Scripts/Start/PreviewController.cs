using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreviewController : MonoBehaviour
{
    [SerializeField] float _speedScreen;
    [SerializeField] Image _logo;

    float time = 0;
    float begin_alpha = 1;
    Color now_c;
    // Start is called before the first frame update
    void Start()
    {
        now_c = _logo.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Env.Instance.is_loading)
        {
            time += Time.deltaTime;
        }
        if (time > 0 && time < _speedScreen)
        {
            begin_alpha -= Time.deltaTime / _speedScreen;
            begin_alpha = Mathf.Clamp01(begin_alpha);
            _logo.material.color = new Color(now_c.r, now_c.g, now_c.b, begin_alpha);
        }
        else if (time >= 1.5f * _speedScreen && time < 2.5f * _speedScreen)
        {
            begin_alpha += Time.deltaTime / _speedScreen;
            begin_alpha = Mathf.Clamp01(begin_alpha);
            _logo.material.color = new Color(now_c.r, now_c.g, now_c.b, begin_alpha);
        }
        else if (time >= 2.5f * _speedScreen)
        {
            SceneManager.LoadScene(2);
        }
    }
}
