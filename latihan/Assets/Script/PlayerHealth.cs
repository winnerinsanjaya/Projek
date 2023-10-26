using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Vector3 respawnPoint; // Posisi respawn pemain

    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private float slowDuration = 2f; // Durasi efek slow dalam detik
    [SerializeField] private float slowFactor = 0.5f; // Faktor slow (0.5 = 50% slower)

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private float originalMoveSpeed;
    private bool isSlowed = false;
    private float slowEndTime;

    private Bergerak bergerak; // Tambahkan referensi ke komponen PlayerMovement

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        bergerak = GetComponent<Bergerak>(); // Ambil komponen PlayerMovement
        originalMoveSpeed = bergerak.jalan; // Simpan kecepatan awal pemain
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isSlowed && Time.time >= slowEndTime)
        {
            isSlowed = false;
            bergerak.jalan = originalMoveSpeed; // Kembalikan kecepatan pemain ke nilai aslinya
        }

        // Check jika darah mencapai 0
        if (currentHealth <= 0)
        {
            // Panggil fungsi RespawnPlayer
            RespawnPlayer();
        }
    }

    // Fungsi untuk respawn pemain
    private void RespawnPlayer()
    {
        // Reset posisi pemain ke checkpoint
        transform.position = respawnPoint;

        // Reset ulang darah pemain
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void SetRespawnPoint(Vector3 checkpointPosition)
    {
        respawnPoint = checkpointPosition;
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        StartCoroutine(Invunerability());
        
        // Selalu aktifkan efek slow ketika terkena trap
        isSlowed = true;
        bergerak.jalan *= slowFactor; // Mengurangi kecepatan pemain
        slowEndTime = Time.time + slowDuration; // Waktu berakhirnya efek slow
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleTrap"))
        {
            TakeDamage(10f);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("HealingItem"))
        {
            Heal(60f);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
