﻿using System;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private float shotPower, maxForce, minSpeed;

    private Rigidbody myRB;
    private float shotForce;
    private Vector3 startPos, endPos, direction;
    private bool canShoot, shotStarted;

    private void Start ()
    {
        myRB = GetComponent<Rigidbody>();
        canShoot = true;
        myRB.sleepThreshold 0 minSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            startPos = MousePositionInWorld();
            shotStarted = true;
        }

        if (Input.GetMouseButton(0) && shotStarted)
        {
            endPos = MousePositionInWorld();
            shotForce = Mathf.Clamp(Vector3.Distance(endPos, startPos), 0, maxForce);
        }
        if (Input.GetMouseButtonUp(0) && shotStarted)
        {
            canShoot = false;
            shotStarted = false;
        }
    }

    private void FixedUpdate()
    {
       if (!canShoot)
        {
            direction = startPos - endPos;
            myRB.AddForce(Vector3.Normalize(direction) * shotForce * shotPower, ForceMode.Impulse);
            startPos = endPos = Vector3.zero; 
        }

       if (myRB.IsSleeping())
        {
            canShot = true;
        }
    }

    private Vector3 MousePositionInWorld()
    {
        Vector3 position = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            position = hit.point;
        }

        return position;

    }
}
