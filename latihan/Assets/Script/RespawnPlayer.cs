using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private void Start()
    {
        // Cek jika ada posisi respawn yang tersimpan di PlayerPrefs
        if (PlayerPrefs.HasKey("RespawnPosX") && PlayerPrefs.HasKey("RespawnPosY"))
        {
            // Ambil posisi respawn dari PlayerPrefs
            float respawnPosX = PlayerPrefs.GetFloat("RespawnPosX");
            float respawnPosY = PlayerPrefs.GetFloat("RespawnPosY");

            // Set posisi respawn pemain
            transform.position = new Vector3(respawnPosX, respawnPosY, transform.position.z);

            // Hapus posisi respawn dari PlayerPrefs setelah digunakan
            PlayerPrefs.DeleteKey("RespawnPosX");
            PlayerPrefs.DeleteKey("RespawnPosY");
        }
    }
}
