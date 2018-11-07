using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Garden : MonoBehaviour
{
	[SerializeField] private int _curerentHp;
	[SerializeField] private Material barrierCrackedTMaterial;
	[SerializeField] private Texture[] barrierCrackedTextures;
	[SerializeField] private int currentTexture;
	private int _maxHp;

	public static event Action<float> OnHpChange;
	
	void Start ()
	{
		_maxHp = 100;
		_curerentHp = _maxHp;
		currentTexture = -1;
		ReduceHP(0);
	}
	

	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			ReduceHP(other.GetComponent<Enemy>().DmgPower);
		}
	}

	private void ReduceHP(int value)
	{
		_curerentHp -= value;
		if (_curerentHp <= 0) Kill();
		else if (_curerentHp/_maxHp < 10-currentTexture/10)
		{
			ChangeMaterial();
		}
		
		if (OnHpChange != null) OnHpChange((float)_curerentHp/(float)_maxHp);  //do poprawy
		
		
	}

	private void Kill()
	{
		Start();
	}

	private void ChangeMaterial()
	{
		currentTexture++;
		barrierCrackedTMaterial.mainTexture = barrierCrackedTextures[currentTexture];
	}
}
