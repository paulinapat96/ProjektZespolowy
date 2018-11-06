using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBarrier : MonoBehaviour
{
	private  Renderer rend;
	void Start ()
	{
		rend = GetComponent<Renderer> ();
		

	}
	
	// Update is called once per frame
	void Update ()
	{

		rend.material.SetTextureOffset("_MainTex", new Vector2(0,rend.material.mainTextureOffset.y + 0.005f));
	}
}
