using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cam;
    [SerializeField] private float offset = 0f;
    [SerializeField] private float scale = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = cam.position.y * scale + offset;
        transform.position = pos;
    }
}
