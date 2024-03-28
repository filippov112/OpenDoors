using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent; // инициализируем событие свайпа
    public delegate void OnSwipeInput(Vector2 direction); // описываем форму события (тип данных и аттрибуты)
    
    private bool isSwiping;
    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    private float deadZone = 80; // минимальное значение, что считаем за свайп

    private bool isMobile;


//    private void Start()
//    {
//#if UNITY_WEBGL && !UNITY_EDITOR
//        isMobile = API.Instance.IsMobile(); // для WebGL - получаем тип из JS плагина
//#else
//        isMobile = Application.isMobilePlatform; // для Android платформы, WebGL - не поддерживает
//#endif
//    }

    //private void Update()
    //{
    //    if (!isMobile)
    //    {
    //        // Коснулись экрана (зажали мышь), свайп начался, позицию запомнили
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            isSwiping = true;
    //            tapPosition = Input.mousePosition;
    //        } 
    //        // Отпустили экран (отжали мышь), сбрасываем переменные (состояние свайпа, координату касания и пройденную длину).
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

        if (isSwiping) // Если тянем, то пересчитываем каждый кадр расстояние до начальной точки
        {
            Vector2 now_position = isMobile ? Input.GetTouch(0).position : Input.mousePosition;
            swipeDelta = now_position - tapPosition;
        }

        // Как только достигли минимальной для свайпа длины, можем запускать реакцию и сбрасывать переменные
        if (swipeDelta.magnitude > deadZone) // magnitude - длина вектора
        {
            if (SwipeEvent != null) // Событие нельзя триггернуть (равно null) пока на него кто-нибудь не подпишется
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // интересуют только горизонтальные свайпы
                    SwipeEvent(swipeDelta.x < 0 ? Vector2.right : Vector2.left);  // триггерим наше созданное событие в нужный момент
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
