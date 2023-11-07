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

    public float scrollSpeed = 0.1f;

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
        float scrollChange = -scrollInput * scrollSpeed + arrowInput;
        scroll_pos = Mathf.Clamp(scroll_pos + scrollChange, 0f, 1f);

        bool adjustedScrollPos = false; // Untuk menandai apakah scroll_pos telah diubah

        for (int i = 0; i < posLength; i++)
        {
            if (scrollChange > 0 && scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scroll_pos = pos[i] - 0.01f; // Mengurangi scroll_pos untuk melambatkan pergerakan ke atas
                adjustedScrollPos = true;
                break; // Keluar dari loop
            }
            else if (scrollChange < 0 && scroll_pos > pos[i] - (distance / 2) && scroll_pos < pos[i] + (distance / 2))
            {
                scroll_pos = pos[i] + 0.01f; // Menambah scroll_pos untuk melambatkan pergerakan ke bawah
                adjustedScrollPos = true;
                break; // Keluar dari loop
            }
        }

        if (!adjustedScrollPos)
        {
            scrollbar.value = 1 - scroll_pos;
        }

        for (int i = 0; i < posLength; i++)
        {
            scroll_pos = 1 - scrollbar.value;

            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int a = 0; a < posLength; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
}