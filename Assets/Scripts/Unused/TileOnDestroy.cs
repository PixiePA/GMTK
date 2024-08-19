using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnDestroy : MonoBehaviour
{
    public GameObject ps;
    // Start is called before the first frame update
    void OnDestroy()
    {
        GameEvents.TileRemoved(transform.position);
        Instantiate(ps, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
