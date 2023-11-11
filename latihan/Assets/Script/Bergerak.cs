using UnityEngine;

public class Bergerak : MonoBehaviour
{
    private bool isWalking;
    private int facingDirection = 1;

    Rigidbody2D rb;
    private Animator anim;

    SpriteRenderer spriteRenderer;
    public float jalan;
    public float lompatan;
    public float cekRadius;
    public Transform cekTanah;
    public LayerMask apaItuTanah;
    public bool injakTanah;
    private bool isClimbing = false;
    public float climbSpeed = 3f; // Kecepatan naik turun tangga

    public float dashDistance = 5f; // Jarak yang akan ditempuh selama "dash"
    private bool isDashing = false; // Status apakah karakter sedang "dash"
    private float dashEndTime; // Waktu berakhirnya "dash"
    public float dashCooldown = 3f; // Waktu cooldown antara "dash"
    private float nextDashTime = 0f; // Waktu kapan karakter dapat "dash" lagi

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        injakTanah = Physics2D.OverlapCircle(cekTanah.position, cekRadius, apaItuTanah);
        float gerak = Input.GetAxis("Horizontal");

        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {

            isWalking = false;
        }

        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(gerak * jalan, verticalInput * climbSpeed);
            rb.gravityScale = 0f; // Reset gravitasi saat berada di tangga
        }
        else
        {
            // Mengecek apakah karakter sedang "dash"
            if (isDashing)
            {
                rb.velocity = new Vector2(cekArahBergerak() * dashDistance, rb.velocity.y);

                // Anda juga dapat mengatur waktu "dash" sesuai kebutuhan
                if (Time.time >= dashEndTime)
                {
                    isDashing = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(gerak * jalan, rb.velocity.y);
                rb.gravityScale = 4f; // Set gravitasi kembali ke nilai awal jika tidak berada di tangga
            }


       
        }

        // Membalikkan sprite jika bergerak ke kiri
        if (gerak < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Mengembalikan sprite ke arah semula jika bergerak ke kanan
        else if (gerak > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        UpdateAnimations();
        // Jika pemain melompat, atur isJumping menjadi true
        if (Input.GetButtonDown("Jump") && injakTanah)
        {
            rb.velocity = new Vector2(rb.velocity.x, lompatan);
        }

        // Tangani input untuk "dash" (misalnya, tombol "Shift")
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= nextDashTime)
        {
            Dash();
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking",isWalking);
        anim.SetBool("isGrounded",injakTanah);
        anim.SetFloat("yVelocity",rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    private void Dash()
    {
        isDashing = true;
        // Menentukan waktu berakhirnya "dash" berdasarkan waktu sekarang
        dashEndTime = Time.time + dashDuration;

        // Mengatur waktu cooldown berdasarkan waktu berakhirnya "dash"
        nextDashTime = dashEndTime + dashCooldown;
    }

    private float dashDuration = 0.1f; // Durasi "dash" dalam detik

    private float cekArahBergerak()
    {
        // Mengecek arah bergerak karakter (1 untuk kanan, -1 untuk kiri)
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f)
            return 1;
        else if (horizontalInput < -0.01f)
            return -1;
        return 0;
    }
    public int GetFacingDirection()
{
    return facingDirection;
}
}
