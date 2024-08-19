using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReciever : MonoBehaviour
{
    [SerializeField] private int channel;

    [SerializeField] private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        ObjectEvents.onObjectEnabled += EnableObject;
        ObjectEvents.onObjectDisabled += DisableObject;
    }

    private void OnDisable()
    {
        ObjectEvents.onObjectEnabled -= EnableObject;
        ObjectEvents.onObjectDisabled -= DisableObject;
    }

    private void EnableObject(int channel)
    {
        if (channel == this.channel)
        {
            animator.SetBool("enabled", true);
        }
    }

    private void DisableObject(int channel)
    {
        if (channel == this.channel)
        {
            animator.SetBool("enabled", false);
        }
    }
}
