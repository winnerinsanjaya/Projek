using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGroundTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "SampleScene"; // Nama scene yang ingin diganti

    private bool canLoadScene = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canLoadScene = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canLoadScene = false;
        }
    }

    private void Update()
    {
        if (canLoadScene && Input.GetKeyDown(KeyCode.C)) // Ganti dengan input sesuai kebutuhan Anda
        {
            // Simpan posisi pemain saat ini di PlayerPrefs
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);

            // Load target scene tanpa unload scene sebelumnya
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
