using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterUndergroundTrigger : MonoBehaviour
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
            // Cek jika ada posisi pemain yang tersimpan di PlayerPrefs
            if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY"))
            {
                // Ambil posisi pemain dari PlayerPrefs
                float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
                float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");

                // Set posisi pemain
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(playerPosX, playerPosY, player.transform.position.z);
            }

            // Load target scene tanpa unload scene sebelumnya
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
