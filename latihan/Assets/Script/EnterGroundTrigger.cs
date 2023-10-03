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
            // Simpan posisi pemain ke PlayerPrefs
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);

            // Load target scene tanpa unload scene sebelumnya
            SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single); // "Single" untuk mengganti scene sebelumnya
        }
    }
}
