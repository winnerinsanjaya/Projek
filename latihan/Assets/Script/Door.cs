using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorTransform;   // Transform objek pintu
    public float openDistance = 2.0f; // Jarak maksimum untuk membuka pintu
    public float openSpeed = 2.0f;    // Kecepatan membuka pintu
    public Transform player;          // Transform pemain
    private Vector3 initialPosition;  // Posisi awal pintu
    private bool isOpen = false;      // Status pintu terbuka/tidak

    private void Start()
    {
        initialPosition = doorTransform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, doorTransform.position);

        if (Input.GetKeyDown(KeyCode.C) && distanceToPlayer <= openDistance)
        {
            if (!isOpen)
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        Vector3 targetPosition = initialPosition + Vector3.right * openDistance;

        while (doorTransform.position != targetPosition)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = true;
    }

    private IEnumerator CloseDoor()
    {
        while (doorTransform.position != initialPosition)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, initialPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = false;
    }
}