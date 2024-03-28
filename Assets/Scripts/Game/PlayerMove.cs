using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;
    public static bool can_move = true;
    private bool is_pc;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(Instance);
        Instance = this;
        is_pc = Env.Instance.is_desktop;
        //SwipeControl.SwipeEvent += OnSwipe;
    }

    private void OnDestroy()
    {
        //SwipeControl.SwipeEvent -= OnSwipe;
    }

    //private void OnSwipe(Vector2 direction)
    //{
    //    if (direction.x < 0)
    //    {
    //        MovePlayer(-1);
    //    } else
    //    {
    //        MovePlayer(1);
    //    }
    //}

    public static void SwitchMove(bool v)
    {
        can_move = v;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_pc)
        {
            bool l = Input.GetKeyDown(KeyCode.LeftArrow);
            bool r = Input.GetKeyDown(KeyCode.RightArrow);
            if (l || r)
            {
                if (l)
                {
                    MovePlayer(-1);
                }
                else
                {
                    MovePlayer(1);
                }
            }
        }
    }

    public void MovePlayer(int _direct)
    {
        if (can_move)
        {
            float x = Instance.transform.position.x;
            x = Mathf.Clamp(x + _direct * 3.5f, -3.5f , 3.5f);
            Instance.transform.position = new Vector3(x, 0, 0);
        }
    }

    public void toRight()
    {
        MovePlayer(1);
    }
    public void toLeft()
    {
        MovePlayer(-1);
    }
}


