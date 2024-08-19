using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Rect detectionArea;

    [SerializeField] private int channel;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Animator animator;

    private bool activated;

    private void FixedUpdate()
    {
        bool currentlyActivated = Physics2D.OverlapBox(detectionArea.center + (Vector2)transform.position, detectionArea.size, 0, layerMask);

        if (activated && !currentlyActivated)
        {
            ObjectEvents.ObjectDisabled(channel);
            animator.SetBool("isActivated", false);
        }
        else if (!activated && currentlyActivated)
        {
            ObjectEvents.ObjectEnabled(channel);
            animator.SetBool("isActivated", true);
        }

        activated = currentlyActivated;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionArea.center + (Vector2)transform.position, detectionArea.size);
    }
}
