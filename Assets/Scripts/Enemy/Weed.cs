using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weed : Enemy
{

    [SerializeField] private Image hpBarImage; 
    [SerializeField] private Text counterText;
    public int enemyCounter = 0;
    
    public event Action<GameObject> OnDestroyWeed;
    
    void Start()
    {
        Init();
        TouchController.OnTouch += IsTouch;
        StartCoroutine("IncreaseCounter");
    }
    
    protected override void Init()
    {
        MaxHP = 20;
        currentHP = MaxHP;
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    void IsTouch(GameObject obj)
    {
        Debug.Log("Ojć " + obj.name);
        if (obj == this.gameObject)
        {
            IsAttacked();
        }
    }

    void IsAttacked()
    {
        currentHP -= 1;

        if (currentHP <= 0) Kill();
        hpBarImage.fillAmount = (float) currentHP / MaxHP;
        
        Debug.Log("Ojć " + this.name);
    }

    void Kill()
    {
        TouchController.OnTouch -= IsTouch;
        if (OnDestroyWeed != null) OnDestroyWeed(gameObject);
    }

    IEnumerator IncreaseCounter()
    {
        while (true)
        {
            if (enemyCounter == 50) Kill();
            enemyCounter++;
            counterText.text = enemyCounter.ToString();
            yield return new WaitForSeconds(0.4f);
        } 
    }
}
