using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterUnderGroundTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "SceneBawahTanah"; // Nama scene yang ingin diganti

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
            // Simpan posisi respawn pemain ke PlayerPrefs
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 respawnPosition = transform.Find("RespawnPointSample").position; // Mengambil posisi RespawnPointSample
            PlayerPrefs.SetFloat("RespawnPosX", respawnPosition.x);
            PlayerPrefs.SetFloat("RespawnPosY", respawnPosition.y);

            // Load target scene tanpa unload scene sebelumnya
            SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single); // "Single" untuk mengganti scene sebelumnya
        }
    }
}
