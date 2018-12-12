using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Enemy {

    void Start()
    {
        Init();
        Destroy(this.gameObject, 40f);
        transform.LookAt(Vector3.zero);
    }

    void Update()
    {
        Move();
    }
    protected override void Init()
    {
        MaxHP = 93;
        currentHP = MaxHP;
        DmgPower = 10;
        MovementSpeed = 0.3f;
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * MovementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (OnDie != null) OnDie(this.GetType().Name);

            Destroy(this.gameObject);
        }
        
    }
}
