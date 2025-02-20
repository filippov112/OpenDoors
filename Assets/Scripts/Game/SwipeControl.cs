using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent; // �������������� ������� ������
    public delegate void OnSwipeInput(Vector2 direction); // ��������� ����� ������� (��� ������ � ���������)
    
    private bool isSwiping;
    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    private float deadZone = 80; // ����������� ��������, ��� ������� �� �����

    private bool isMobile;


//    private void Start()
//    {
//#if UNITY_WEBGL && !UNITY_EDITOR
//        isMobile = API.Instance.IsMobile(); // ��� WebGL - �������� ��� �� JS �������
//#else
//        isMobile = Application.isMobilePlatform; // ��� Android ���������, WebGL - �� ������������
//#endif
//    }

    //private void Update()
    //{
    //    if (!isMobile)
    //    {
    //        // ��������� ������ (������ ����), ����� �������, ������� ���������
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            isSwiping = true;
    //            tapPosition = Input.mousePosition;
    //        } 
    //        // ��������� ����� (������ ����), ���������� ���������� (��������� ������, ���������� ������� � ���������� �����).
    //        else if (Input.GetMouseButtonUp(0))
    //        {
    //            ResetSwipe();
    //        }
    //    } 
    //    else
    //    {
    //        if (Input.touchCount > 0)
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                isSwiping = true;
    //                tapPosition = Input.GetTouch(0).position;
    //            } 
    //            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
    //            {
    //                ResetSwipe();
    //            }
    //        }
    //    }
        
    //    CheckSwipe();
    //}

    private void CheckSwipe()
    {
        swipeDelta = Vector2.zero;

        if (isSwiping) // ���� �����, �� ������������� ������ ���� ���������� �� ��������� �����
        {
            Vector2 now_position = isMobile ? Input.GetTouch(0).position : Input.mousePosition;
            swipeDelta = now_position - tapPosition;
        }

        // ��� ������ �������� ����������� ��� ������ �����, ����� ��������� ������� � ���������� ����������
        if (swipeDelta.magnitude > deadZone) // magnitude - ����� �������
        {
            if (SwipeEvent != null) // ������� ������ ����������� (����� null) ���� �� ���� ���-������ �� ����������
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // ���������� ������ �������������� ������
                    SwipeEvent(swipeDelta.x < 0 ? Vector2.right : Vector2.left);  // ��������� ���� ��������� ������� � ������ ������
                //else
                //    SwipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
}
