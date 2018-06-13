using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class Laser : MonoBehaviour
{
    public LayerMask LaserMask;
    public bool isTargetHit;

    public float temp;

    //private LineRenderer lineRenderer;
    private Vector3[] linePositions;
    private AnimatedLineRenderer alr;

    public Material mat;

    // Use this for initialization
    void Start()
    {
        alr = GetComponent<AnimatedLineRenderer>();
        isTargetHit = false;

        resetAlr();
    }

    private void resetAlr()
    {
        alr.Reset();
        alr.SecondsPerLine = 1f;
        alr.StartWidth = 0.7f;
        alr.EndWidth = 0.7f;
        alr.LineRenderer.material = mat;
    }

    public void ShootLaser()
    {
        resetLaser();
        if (!isTargetHit)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 10000f, LaserMask))
            {
                Vector3 point = hit.transform.GetComponent<Renderer>().bounds.center;
                
                //Laser Tutorial
                laserEndPoint endPoint = hit.transform.GetComponent<laserEndPoint>();
                if (endPoint != null && !endPoint.WasHit)
                {
                    endPoint.OnHitTarget();
                }
                
                //Laser Level
                LaserLevelTargets target = hit.transform.GetComponent<LaserLevelTargets>();
                if (target != null && !target.WasHit)
                {
                    target.OnHitTarget();
                }

                addPoint(point, true);
            }
            else
            {
                resetLaser();
            }
        }
    }

    public void levelHitTarget()
    {
        isTargetHit = true;

    }

    public void resetLaser()
    {
        resetAlr();
    }

    private void updateOriginPosition()
    {
        linePositions[0] = transform.position;
    }

    public void addPoint(Vector3 newPoint, bool local)
    {
        alr.Enqueue(transform.position);
        alr.Enqueue(newPoint);
    }
}
