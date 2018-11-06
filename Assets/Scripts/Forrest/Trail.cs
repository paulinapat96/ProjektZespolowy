using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		TouchController.OnHold += Move;
	}

	private void Move(Vector3 touch)
	{
		transform.position = new Vector3(touch.x, 0, touch.z);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
