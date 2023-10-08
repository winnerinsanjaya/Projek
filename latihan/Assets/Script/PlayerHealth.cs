using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private float slowDuration = 2f; // Durasi efek slow dalam detik
    [SerializeField] private float slowFactor = 0.5f; // Faktor slow (0.5 = 50% slower)

    private bool isSlowed = false;
    private float originalMoveSpeed;

    private Bergerak bergerak; // Tambahkan referensi ke komponen PlayerMovement

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        bergerak = GetComponent<Bergerak>(); // Ambil komponen PlayerMovement
        originalMoveSpeed = bergerak.moveSpeed; // Simpan kecepatan awal pemain
    }

    private void Update()
    {
        if (isSlowed)
        {
            // Mengurangi efek slow setelah durasi berakhir
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                isSlowed = false;
                bergerak.moveSpeed = originalMoveSpeed; // Kembalikan kecepatan pemain ke nilai aslinya
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        // Aktifkan efek slow
        isSlowed = true;
        bergerak.moveSpeed *= slowFactor; // Mengurangi kecepatan pemain
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
}