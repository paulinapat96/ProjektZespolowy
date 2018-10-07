using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameObject potion;
    private Vector2 fingerStartPos = Vector2.zero;
    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            // double Click
            if (touch.tapCount == 2)
            {
                Debug.Log("Double Click");
            }
           
            bool wasSwiping = false;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Tap
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit)){
               
                        Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.tag == "Interactable")
                        {
                            potion = hit.collider.gameObject;
                        }
                    }
                    
                    //Swipe
                    isSwipe = true;
                    fingerStartPos = touch.position;
                    break;

                case TouchPhase.Canceled:
                    isSwipe = false;

                    break;

                case TouchPhase.Moved:
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

                        potion.GetComponent<Rigidbody>().AddForce(new Vector2(deltaShiftX,deltaShiftY));
                        
                       
//                        potion.transform.position = new Vector3(
//                            potion.transform.position.x + deltaShiftX * 0.0001f,
//                            potion.transform.position.y + deltaShiftY * 0.0001f,
//                            -10);

  
                    }

                    break;

                case TouchPhase.Ended:
                    wasSwiping = false;
                    break;
            }

      
        }
    }
}