
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   #region Properties
    public int HP
    {
        get { return _currentHp;}
        set { _currentHp = value; }
    }
    
    public int MaxHP
    {
        get { return _maxHp;}
        set { _maxHp = value; }
    }
    
    public int DmgPower
    {
        get { return _damagePower;}
        set { _damagePower = value; }
    }
    
    public float MovementSpeed
    {
        get { return _movementSpeed;}
        set { _movementSpeed = value; }
    }

    public bool IsDead()
    {
        return (_currentHp <= 0) ?true : false;
    }
    #endregion
    
    private int _currentHp;
    private int _maxHp;
    private int _damagePower;
    private float _movementSpeed;

    protected abstract void Init();
    protected abstract void Attack();
    protected abstract void Move();
    
    
}
