using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject MenuDialog;
    // Start is called before the first frame update
    
    public void OpenMenuDialog()
    {
        MenuDialog.SetActive(true);
    }

    public void CloseMenuDialog()
    {
        MenuDialog.SetActive(false);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(2);
    }
}
