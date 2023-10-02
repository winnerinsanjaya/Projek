using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bergerak : MonoBehaviour
{
    // Start is called before the first frame update\
    Rigidbody2D rb;
    public float jalan;
    public float lompatan;
    public float cekRadius;
    public Transform cekTanah;
    public LayerMask apaItuTanah;
    public bool injakTanah;
    void Start()
    
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        injakTanah = Physics2D.OverlapCircle(cekTanah.position, cekRadius, apaItuTanah);
        float gerak = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (gerak * jalan, rb.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && injakTanah)
        {
            rb.velocity = Vector2.up * lompatan;
        }
    }
}
