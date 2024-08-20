using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cam;
    [SerializeField] private float offset = 0f;
    [SerializeField] private float scale = 0.5f;
    [SerializeField] private float endpoint = 0f;
    [SerializeField] private float xspeed = 0f;
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
        if (xspeed != 0)
        {
            pos.x += xspeed * Time.deltaTime;
            pos.x = pos.x % endpoint;
        }
        transform.position = pos;
    }
}
