using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGroundTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nama scene yang ingin diganti

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
            // Ganti ke scene yang ditentukan jika pemain masuk ke dalam tanah dan menekan tombol yang sesuai
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
