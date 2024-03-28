using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject[] slides;

    [SerializeField] Button Prev;
    [SerializeField] Button Next;

    int active_slide;

    // Start is called before the first frame update
    void Start()
    {
        active_slide = 0;
    }

    public void PrevSlide()
    {
        slides[active_slide].SetActive(false);
        active_slide--;
        slides[active_slide].SetActive(true);
        if (active_slide == 0)
        {
            Prev.interactable = false;
        }
        Next.interactable = true;
    }

    public void NextSlide()
    {
        slides[active_slide].SetActive(false);
        active_slide++;
        slides[active_slide].SetActive(true);
        if (active_slide+1 == slides.Length)
        {
            Next.interactable = false;
        }
        Prev.interactable = true;
    }

    public void SkipTutorial()
    {
        bool from_start = Env.Instance.tutorial_from_play;
        Env.Instance.tutorial_from_play = false;
        if (from_start)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
