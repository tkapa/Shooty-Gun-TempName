﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackShopTestScript : MonoBehaviour {
    public Animator MyAnim;

    public string OpenShopTrigger;

    public string CloseShopTrigger;

    public string LeftBuyTrigger;

    public string RightBuyTrigger;

    public string LeftAmmoTrigger;

    public string RightAmmoTrigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        // here are just a bunch of debug keys I was using to make sure things ran smoothly. 
        if (Input.GetKeyDown(KeyCode.C))
        {
            CalledShopClosed();
        }

            if (Input.GetKeyDown(KeyCode.O))
            {
            CalledShopOpen();
            }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CalledBuyNewLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CalledBuyNewRight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CalledBuyAmmoLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CalledBuyAmmoRight();
        }
    }


    public void CalledShopOpen()
    {
        MyAnim.SetTrigger(OpenShopTrigger);
        Debug.Log("Should have opened shop");
    }

    public void CalledShopClosed()
    {
        MyAnim.SetTrigger(CloseShopTrigger);
    }

    public void CalledBuyNewLeft()
    {
        MyAnim.SetTrigger(LeftBuyTrigger);
    }

    public void CalledBuyNewRight()
    {
        MyAnim.SetTrigger(RightBuyTrigger);
    }

    public void CalledBuyAmmoLeft()
    {
        MyAnim.SetTrigger(LeftAmmoTrigger);
    }

    public void CalledBuyAmmoRight()
    {
        MyAnim.SetTrigger(RightAmmoTrigger);
    }
}
