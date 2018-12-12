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

    public static event Action<GameObject> OnTouch;
    public static event Action<float, float> OnSwipe;
    public static event Action<Vector3> OnHold;
    public static event Action OnRelease;

    private Plane plane;
    private Camera mainCamera;
    private GameObject lastTapped;

    private Touch _touch;

    void Start()
    {
        plane = new Plane(Vector3.up, transform.position);
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            // multiple Tap
            if (touch.tapCount > 1)
            {
                DetectMultipleTap(touch);
            }
           
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TouchBegan(touch);

                    break;

                case TouchPhase.Canceled:
                    TouchCancelled();
                    break;

                case TouchPhase.Moved:
                    TouchMoved(touch);
                    break;

                case TouchPhase.Ended:
                    TouchEnded(touch);
                    break;
            } 
        }
        
        #if UNITY_EDITOR
        _touch.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            TouchBegan(_touch);
        }
        else if (Input.GetMouseButton(0))
        {
            TouchMoved(_touch);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchEnded(_touch);   
        }
        else
        {
            TouchCancelled();
        }
        #endif
    }

    private void TouchEnded(Touch touch)
    {
        DetectRelease(touch);
        isSwipe = false;
    }

    private void TouchMoved(Touch touch)
    {
        DetectSwipe(touch);
        DetectHold(touch);
    }

    private void TouchCancelled()
    {
        isSwipe = false;
    }

    private void TouchBegan(Touch touch)
    {
        DetectTap(touch);
        isSwipe = true;
        fingerStartPos = touch.position;
    }

    private void DetectTap(Touch touch)
    {
        Ray ray = mainCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
               
            if (hit.collider.CompareTag("Interactable"))
            {
                if (OnTouch != null) OnTouch(hit.collider.gameObject);
                Debug.Log("tapnięcie");
            }
        }
    }

    private void DetectMultipleTap(Touch touch)
    {

    }

    private void DetectRelease(Touch touch)
    {
        if (OnRelease != null) OnRelease();
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

    private void DetectHold(Touch touch)
    {
        Ray ray = mainCamera.ScreenPointToRay(touch.position);
        float distance = 0;
        if (plane.Raycast(ray, out distance)){ 
            Vector3 pos = ray.GetPoint(distance); 
            if (OnHold != null) OnHold(pos);
        }
    }
}