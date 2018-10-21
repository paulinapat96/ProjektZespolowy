using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    private Vector2 fingerStartPos = Vector2.zero;
    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    public static event Action<GameObject> OnTouch;
    public static event Action<float, float> OnSwipe;
    
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            // double Click
            if (touch.tapCount == 2)
            {
                DetectDoubleTap(touch);
            }
           
            bool wasSwiping = false;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DetectTap(touch);
                    
                    isSwipe = true;
                    fingerStartPos = touch.position;
                    break;

                case TouchPhase.Canceled:
                    isSwipe = false;
                    break;

                case TouchPhase.Moved:
                    DetectSwipe(touch);
                    break;

                case TouchPhase.Ended:
                    wasSwiping = false;
                    break;
            } 
        }
    }

    private void DetectTap(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
               
            if (hit.collider.CompareTag("Interactable"))
            {
                if (OnTouch != null) OnTouch(hit.collider.gameObject);
            }
        }
    }

    private void DetectDoubleTap(Touch touch)
    {
        Debug.Log("Double Click");
    }
   
    private void DetectSwipe(Touch touch)
    {
        float gestureDist = (touch.position - fingerStartPos).magnitude;

        if (isSwipe && gestureDist > minSwipeDist)
        {
            Vector2 swipeType = Vector2.zero;
            // Horizontal
            float deltaShiftX = 0;
            deltaShiftX = touch.position.x - fingerStartPos.x;

            // Vertical
            float deltaShiftY = 0;
            deltaShiftY = touch.position.y - fingerStartPos.y;

            if (OnSwipe != null) OnSwipe(deltaShiftX, deltaShiftY);
        }
    }
}