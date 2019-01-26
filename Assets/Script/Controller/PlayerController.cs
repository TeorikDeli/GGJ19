﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rg;

    [SerializeField]
    float speed;

    Transform currentStuff = null;

    bool allowHold;
    bool isHold;
    bool isEnter;


    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector2.zero;
        Vector3 dir = Vector2.zero;

        if (vertical != 0 || horizontal != 0)
        {
            dir = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        }

        velocity = dir * speed;


        rg.velocity = velocity;

        /* HOLD */

        if (isEnter)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("hold");
                Hold(currentStuff);
            }
        }

        if (isHold)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                print("break");
                Break(currentStuff);
            }
        }



        /* TEST */

        if (Input.GetKeyDown(KeyCode.Y))
        {
            print("Test Work : Press Y");
            print("Name current : " + currentStuff.name);
            Hold(currentStuff);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            print("Test Work : Press Y");
            Break(currentStuff);
        }

    }

    void Hold(Transform current)
    {
        //Kenime bir joint componenti ekle.
        CharacterJoint joint = gameObject.AddComponent<CharacterJoint>();

        //connected body'sine current objeyi ver.
        joint.connectedBody = current.GetComponent<Rigidbody>();

        isHold = true;
    }

    void Break(Transform currentStuff)
    {
        //Connected destroy
        currentStuff.GetComponent<StuffController>().Break();

        Destroy(gameObject.GetComponent<CharacterJoint>());

        rg.angularVelocity = Vector3.zero;

        isHold = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        print("Enter");
        if (other.tag == "Stuff" && isEnter == false)
        {
            print("is Enter t");
            isEnter = true;
            currentStuff = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Exit");
        if (other.tag == "Stuff" && other.transform == currentStuff)
        {
            currentStuff = null;
            isEnter = false;
            print("isEnter : False");
        }
    }
    
}