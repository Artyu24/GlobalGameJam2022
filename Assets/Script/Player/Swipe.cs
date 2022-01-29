using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool _tap, _swipeLeft, _swipeRight;
    private bool isDragging;
    private Vector2 _startTouch, _swipeDelta;

    private void Update()
    {
        _tap = _swipeLeft = _swipeRight = false; // turn them false right after (action is made in 1frame)


        #region SwipySwipe Ordi
        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            isDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        //touches.lenth --> liste of object that represent all touch in 1 frame 
        #region SwipySwipe Mobile

        if (Input.touches.Length != 0)//check id there is a touch on the screen
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        

        #endregion


        //Calculate the distance
        _swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
        }

        //Add litle DeadZone
        if (swipeDelta.magnitude > 100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            //check the direction (1/2 usless but usefull for ctrl(c + v) 
            if (Mathf.Abs(x) > Mathf.Abs(y))//gauche / Droite
            {
                if (x < 0)//left
                    _swipeLeft = true;
                else//right
                    _swipeRight = true;
            }
            /*else// haut / Bas
            {
                if (y < 0)//up
                    Debug.Log("UP");
                else//down
                    Debug.Log("Down");
            }*/
        }

    }

    private void Reset()//reset the Vector 2
    {
        _startTouch = _swipeDelta = Vector2.zero;
    }

    public Vector2 swipeDelta { get { return _swipeDelta; } }
    public bool swipeLeft { get { return _swipeLeft; } }
    public bool tap { get { return _tap; } }
    public bool swipeRight { get { return _swipeRight; } }

}
