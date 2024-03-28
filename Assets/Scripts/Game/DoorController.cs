using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [SerializeField] Transform D1;
    [SerializeField] Transform D2;
    [SerializeField] Transform D3;

    [SerializeField] Renderer d1Renderer;
    [SerializeField] Renderer d2Renderer;
    [SerializeField] Renderer d3Renderer;

    [SerializeField] TextMeshProUGUI d1Text;
    [SerializeField] TextMeshProUGUI d2Text;
    [SerializeField] TextMeshProUGUI d3Text;

    [SerializeField] Image d1Image;
    [SerializeField] Image d2Image;
    [SerializeField] Image d3Image;

    [SerializeField] float _speedUp;
    [SerializeField] float _speedRun;
    [SerializeField] float _zDelete;
    [SerializeField] float _zReset;

    AudioController AC => FindAnyObjectByType<AudioController>();

    // Start is called before the first frame update

    Transform UpedDoor = null;
    DoorState state;
    int openDoor = -1;
    bool state_runing = false;

    public float AddSpeed()
    {
        if (_speedRun < 12)
        {
            _speedRun += 0.2f;
        }
        else
        {
            _speedRun += 0.01f;
        }
        return (float)System.Math.Round(_speedRun, 2);
    }
    public void SetSpeed(float val)
    {
        _speedRun = val;
    }


    public UserText SetParameters(bool update)
    {
        if (!update)
            this.state.SetState();
        else
            this.state.UpdateState();
        SetColors(this.state.GetColors());
        SetNumbers(this.state.GetNumbers());
        SetForms(this.state.GetForms());

        return this.state.GetUserText();
    }

    private void SetNewParameters()
    {
        UserText ut = SetParameters(true);
        GameManager.instance.SetUserText(ut);
    }

    public void SetStateRunning(bool running)
    {
        this.state_runing = running;
    }

    private void CheckOpenDoor()
    {
        if (this.state.CheckTrue(this.openDoor))
        {
            GameManager.instance.AddScore();
            AC.PlayGood();
        } 
        else
        {
            GameManager.instance.PopScore();
            AC.PlayBad();
        }
        SetNewParameters();
    }


    private void SetColors(List<Color> colors)
    {
        if (colors.Count == 3)
        {
            d1Renderer.material.color = colors[0];
            d2Renderer.material.color = colors[1];
            d3Renderer.material.color = colors[2];
        }
    }

    private void SetNumbers(List<int> numbers) 
    {
        if (numbers.Count == 3)
        {
            d1Text.text = numbers[0].ToString();
            d2Text.text = numbers[1].ToString();
            d3Text.text = numbers[2].ToString();
        }
    }

    private void SetForms(List<Sprite> forms)
    {
        if (forms.Count == 3)
        {
            d1Image.sprite = forms[0];
            d2Image.sprite = forms[1];
            d3Image.sprite = forms[2];
        }
    }


    public void SetStartState()
    {
        this.state = gameObject.GetComponent<DoorState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state_runing)
        {
            if (transform.position.z < -_zDelete)
            {
                transform.position = new Vector3(0, 0, _zReset);
                D1.position = new Vector3(-3.5f, 0, _zReset);
                D2.position = new Vector3(0, 0, _zReset);
                D3.position = new Vector3(3.5f, 0, _zReset);
                PlayerMove.SwitchMove(true);
                CheckOpenDoor();
            }
            else
            {
                if (this.UpedDoor != null)
                {
                    this.UpedDoor.position += new Vector3(0, Time.deltaTime * _speedUp, 0);
                    if (this.UpedDoor.position.y > 4)
                    {
                        this.UpedDoor = null;
                    }
                }
            }
            transform.position += new Vector3(0, 0, -_speedRun * Time.deltaTime);
        }    
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            PlayerMove.SwitchMove(false);
            Transform t = other.transform;
            switch (t.position.x)
            {
                case -3.5f:
                    this.UpedDoor = D1;
                    this.openDoor = 0;
                    break;
                case 0f:
                    this.UpedDoor = D2;
                    this.openDoor = 1;
                    break;
                case 3.5f:
                    this.UpedDoor = D3;
                    this.openDoor = 2;
                    break;
            }
        }
    }
}
