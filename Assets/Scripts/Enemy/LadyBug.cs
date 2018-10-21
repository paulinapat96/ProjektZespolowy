using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBug : Enemy {

    void Start()
    {
        Init();
        Destroy(this.gameObject, 40f);
    }

    void Update()
    {
        Move();
    }
    protected override void Init()
    {
        MaxHP = 100;
        HP = MaxHP;
        DmgPower = 10;
        MovementSpeed = 0.1f;
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - MovementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            Destroy(this.gameObject);
        }
    }
}
