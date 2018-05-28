using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask LaserMask;
    public bool isTargetHit;

    public float temp;

    private LineRenderer lineRenderer;
    private Vector3[] linePositions;

    public Material mat;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        linePositions = new Vector3[1];
        linePositions[0] = transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPositions(linePositions);
        isTargetHit = false;

        lineRenderer.material = mat;
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
        updateOriginPosition();
        Vector3[] newLinePositions = new Vector3[1];

        newLinePositions[0] = linePositions[0];

        linePositions = newLinePositions;
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }

    private void updateOriginPosition()
    {
        linePositions[0] = transform.position;
    }

    public void addPoint(Vector3 newPoint, bool local)
    {
        Vector3[] newLinePositions = new Vector3[linePositions.Length + 1];

        for (int i = 0; i < linePositions.Length; i++)
        {
            newLinePositions[i] = linePositions[i];
        }

        newLinePositions[linePositions.Length] = newPoint;
        linePositions = newLinePositions;
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }
}
