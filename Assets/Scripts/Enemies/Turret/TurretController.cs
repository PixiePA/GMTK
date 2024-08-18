using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Vector2 rayDirection;

    [SerializeField] private Vector2 rayOrigin;

    [SerializeField] private float maxLaserDistance;

    [SerializeField] private LineRenderer laserRenderer;

    private void Start()
    {
        rayDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Vector2 rayStart = (Vector2)transform.position + rayOrigin;

        RaycastHit2D castResult = Physics2D.Raycast((Vector2)transform.position + rayOrigin, rayDirection, maxLaserDistance);

        Vector2 hitLocation = rayStart + rayDirection*maxLaserDistance;

        if (castResult)
        {
            hitLocation = castResult.point;
        }

        

        Vector3[] points = new Vector3[2];

        points[0] = rayStart;
        points[1] = hitLocation;

        laserRenderer.SetPositions(points);


    }
}
