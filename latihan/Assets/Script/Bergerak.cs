using UnityEngine;

public class Bergerak : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float jalan;
    public float lompatan;
    public float cekRadius;
    public Transform cekTanah;
    public LayerMask apaItuTanah;
    public bool injakTanah;
    private bool isClimbing = false;
    public float climbSpeed = 3f; // Kecepatan naik turun tangga

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        injakTanah = Physics2D.OverlapCircle(cekTanah.position, cekRadius, apaItuTanah);
        float gerak = Input.GetAxis("Horizontal");

        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(gerak * jalan, verticalInput * climbSpeed);
            rb.gravityScale = 0f; // Reset gravitasi saat berada di tangga
        }
        else
        {
            rb.velocity = new Vector2(gerak * jalan, rb.velocity.y);
            rb.gravityScale = 4f; // Set gravitasi kembali ke nilai awal jika tidak berada di tangga
        }

        // Jika pemain melompat, atur isJumping menjadi true
        if (Input.GetButtonDown("Jump") && injakTanah)
        {
            rb.velocity = new Vector2(rb.velocity.x, lompatan);
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
        // Tangani naik dan turun tangga hanya jika pemain menekan tombol 'W' atau 'S'
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0 && isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
        }
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
}
