using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class StaticParallaxObject : MonoBehaviour
{
    public Transform target;

    float targetStartX;

    float xOrigin;

    public float xFactor;
    // Start is called before the first frame update
    void Start()
    {
        targetStartX = target.position.x;
        xOrigin = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = target.position.x - targetStartX;
        Debug.Log(offset);
        
        Vector2 targetPos = new Vector2(xOrigin - (offset * xFactor), transform.localPosition.y);
        transform.localPosition = targetPos;
    }
}
