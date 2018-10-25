using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
	public static event Action<GameObject> OnDestroy;

	[SerializeField] int power = 3;
	private float yTransform;
	private int isMovingUp = 1;
	private bool isTouched = false;
	private float levitationSpeed = 0.02f;
	
	void Start ()
	{
		TouchController.OnSwipe += IsSwiped;
		TouchController.OnTouch += IsTouched;
		yTransform = transform.localPosition.y;
	}

	private void IsSwiped(float x, float y)
	{
		if (isTouched)
		{
			this.GetComponent<Rigidbody>().AddForce(new Vector3(x, 0 , y) * power);
			OnDestroy(this.gameObject);
			isTouched = false;
			isMovingUp = -1;
			
			DestroyThis();

		}
	}

	private void IsTouched(GameObject obj)
	{
		if (obj == this.gameObject)
		{
			isTouched = true;
		}
		
	}

	private void DestroyThis()
	{
		TouchController.OnSwipe -= IsSwiped;
		TouchController.OnTouch -= IsTouched;
		Destroy(this.gameObject, 15);
		
	}
	
	
	void Update () {
		if (!isTouched)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, yTransform, transform.localPosition.z);

			if(isMovingUp == 1) yTransform += levitationSpeed;
			if(isMovingUp == 0) yTransform -= levitationSpeed;
			

			if (yTransform < 0.0f) isMovingUp = 1;
			if (yTransform > 2.5f) isMovingUp = 0;
		}
		
	}
}
