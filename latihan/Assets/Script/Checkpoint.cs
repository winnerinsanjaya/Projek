using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Mendapatkan komponen PlayerHealth dari pemain
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Mengatur respawn point pemain ke posisi checkpoint ini
                playerHealth.SetRespawnPoint(transform.position);
            }
        }
    }
}
