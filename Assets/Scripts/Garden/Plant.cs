using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private int plantState;
    private bool moveUp;

    // Use this for initialization
    void Start()
    {
        TouchController.OnHold += Move;
        TouchController.OnTouch += ShowStats;
        TouchController.OnRelease += Planting;
        plantState = 0;
        moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (plantState == 0)
        {
            Float();
        }
    }
    private void Move(Vector3 touch)
    {
        if ((touch - transform.position).magnitude < 5.0f)
        {
            transform.position = new Vector3((int)touch.x, 1.0f, (int)touch.z);
            plantState = 1;
        }
    }

    private void Float()
    {
        if (transform.position.y > 2.0f)
            moveUp = false;
        if (transform.position.y < 1.0f)
            moveUp = true;

        if (moveUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
    }

    void ShowStats(GameObject plant)
    {
         Debug.Log("To moje staty");
    }

    void Planting()
    {
        TouchController.OnHold -= Move;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void OnDestroy()
    {
        //TouchController.OnHold -= Move;
        TouchController.OnTouch -= ShowStats;
    }

}
