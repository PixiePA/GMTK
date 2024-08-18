using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenChargerController : ChargerController
{
    [SerializeField] private Rect floorChecker;
    [SerializeField] bool moveLeft;

    protected override bool isAgitated()
    {
        return true;
    }

    protected override void OnMoveInterrupt()
    {
        moveLeft = !moveLeft;
    }

    protected override bool CanMove()
    {
        return base.CanMove() && Physics2D.OverlapBox(floorChecker.center + (Vector2)transform.position, floorChecker.size, 0, layerMask);
    }

    protected override void Charge()
    {
        int sign = 1;
        if (moveLeft)
        {
            sign = -1;
        }

        floorChecker.x = Mathf.Abs(floorChecker.x) * sign;
        wallDetector.x = Mathf.Abs(wallDetector.x) * sign;
        chargeForce = new Vector2(moveSpeed * sign, 0);

    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(floorChecker.center + (Vector2)transform.position, floorChecker.size);
        base.OnDrawGizmos();
    }
}
