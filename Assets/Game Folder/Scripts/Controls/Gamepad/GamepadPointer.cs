﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GamepadPointer : MonoBehaviour {

    // Refers to the attached line renderer.
    private LineRenderer targetBeam;
    public bool displayLines = true;

    private Vector3 hitLocation;
    private GameObject hitObject;

    public LayerMask mask;

    public Vector3 rotOffset;

    // Use this for initialization
    protected virtual void Start () {
        targetBeam = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update () {
            initController(); // TEMP

        CastRays();
        SetBeamPoints();
	}

    private void CastRays()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.Normalize(this.transform.forward + rotOffset), out hit ))
        {
            if (hit.transform.gameObject.layer == mask)
                return;

            hitLocation = hit.point;
            hitObject = hit.collider.gameObject;
        }else
        {
            hitLocation = this.transform.position + (Vector3.Normalize(this.transform.forward + rotOffset) * 1000);
            hitObject = null;
        }
    }

    private void SetBeamPoints()
    {
        if (!displayLines)
            targetBeam.enabled = false;
        else
            targetBeam.enabled = true;

        Vector3[] points = new Vector3[2];

        points[0] = this.transform.position;
        points[1] = hitLocation;

        targetBeam.SetPositions(points);
    }

    public Vector3 GetHitLocation()
    {
        return hitLocation;
    }

    public bool testHitObjectAgainst(GameObject go)
    {
        return (go == hitObject);
    }

    public bool testHitObjectTag(string testTag)
    {
        if (hitObject == null)
            return false;
        return (testTag == hitObject.tag);
    }

    protected virtual void initController()
    {
        EventManager.instance.OnWeaponInit.Invoke(this, 0);
        EventManager.instance.OnWeaponInit.Invoke(this, 1);
    }
}
