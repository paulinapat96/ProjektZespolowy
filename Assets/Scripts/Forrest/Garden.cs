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
	[SerializeField] private ParticleSystem bubbleBoomParticle;
	private int _maxHp;

	public static event Action<float> OnHpChange;
	

	void Start ()
	{
		_maxHp = 100;
		_curerentHp = _maxHp;
		currentTexture = 0;
		ReduceHP(0);
		ChangeMaterial();
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
		else if ((float)_curerentHp / _maxHp < (float)(barrierCrackedTextures.Length - currentTexture -1) / 10)
		{
			ChangeMaterial();
			currentTexture = (int) (10 - (((float) _curerentHp / _maxHp) * 10 % 10));
		}
		
		if (OnHpChange != null) OnHpChange((float)_curerentHp/(float)_maxHp);  
		
		
	}

	private void Kill()
	{
		bubbleBoomParticle.Play();
		Start();
	}

	private void ChangeMaterial()
	{
		barrierCrackedTMaterial.mainTexture = barrierCrackedTextures[currentTexture];
	}
}
