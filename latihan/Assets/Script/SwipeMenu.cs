using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{

    public Scrollbar scrollbar;
    private float[] pos;
    private float distance;
    private int posLength;

    private float scrollSpeed = 0.25f;
    private float scrollStep = 0.25f; // Kenaikan nilai saat scrolling

    private void Start()
    {
        posLength = transform.childCount;
        pos = new float[posLength];
        distance = 1f / (posLength - 1f);
    }

    private void Update()
    {
        for (int i = 0; i < posLength; i++)
        {
            pos[i] = 1 - (distance * i);
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float arrowInput = Input.GetAxis("Vertical");
        float scroll_pos = 1 - scrollbar.value;

        // Menghitung perubahan nilai scroll_pos
        float scrollChange = (-scrollInput * scrollSpeed + arrowInput) * scrollSpeed;

        // Menaikkan atau menurunkan value sesuai dengan scrollChange
        if (scrollChange > 0)
        {
            scrollbar.value = Mathf.Clamp(scrollbar.value + scrollStep, 0f, 1f);
        }
        else if (scrollChange < 0)
        {
            scrollbar.value = Mathf.Clamp(scrollbar.value - scrollStep, 0f, 1f);
        }

        int closestElementIndex = FindClosestElementIndex(1 - scrollbar.value);

        for (int i = 0; i < posLength; i++)
        {
            scroll_pos = 1 - scrollbar.value;

            if (i == closestElementIndex)
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
            }
            else
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
            }
        }
    }

    private int FindClosestElementIndex(float scrollPos)
    {
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < posLength; i++)
        {
            float elementDistance = Mathf.Abs(pos[i] - scrollPos);
            if (elementDistance < closestDistance)
            {
                closestDistance = elementDistance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}
