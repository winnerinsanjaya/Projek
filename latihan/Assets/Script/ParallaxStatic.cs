using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxStatic : MonoBehaviour
{
    public Transform target;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float x = target.position.x;
        Vector2 targetPos = new Vector2(x + offset, transform.position.y);
        transform.position = targetPos;
    }
}
