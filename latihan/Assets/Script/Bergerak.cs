using UnityEngine;

public class Bergerak : MonoBehaviour
{
    Rigidbody2D rb;
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
    }

    private void FixedUpdate()
    {
        injakTanah = Physics2D.OverlapCircle(cekTanah.position, cekRadius, apaItuTanah);
        float gerak = Input.GetAxis("Horizontal");

        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(gerak * jalan, verticalInput * climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2(gerak * jalan, rb.velocity.y);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && injakTanah)
        {
            rb.velocity = new Vector2(rb.velocity.x, lompatan);
        }

        // Deteksi tombol W (naik) dan S (turun) saat berada di dekat tangga
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
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
