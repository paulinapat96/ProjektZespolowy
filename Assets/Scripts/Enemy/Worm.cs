﻿using System.Collections;
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
        MaxHP = 100;
        HP = MaxHP;
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
        if (other.tag == "Interactable")
        {
            Destroy(this.gameObject);
        }
    }
}