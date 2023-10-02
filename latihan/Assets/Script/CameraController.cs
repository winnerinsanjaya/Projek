using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float roomCameraSpeed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        // Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, roomCameraSpeed * Time.deltaTime);

        // Follow player
        float targetX = player.position.x + lookAhead;
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        // Update lookAhead
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * Mathf.Sign(player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPosX = newRoom.position.x;
    }
}
