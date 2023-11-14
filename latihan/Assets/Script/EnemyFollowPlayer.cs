using System.Collections;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    public float attackCooldown = 2.0f;
    public float preAttackDuration = 1.0f;

    private Transform player;
    private bool isAttacking = false;
    private float timeSinceLastAttack = 0.0f;

    // Variabel untuk ukuran dan damage enemy
    private float originalSize;
    private float currentDamage = 10f; // Damage awal
    private float sizeIncreaseAmount = 0.1f; // Jumlah peningkatan ukuran setiap kali menyerang
    private float damageIncreaseAmount = 5f; // Jumlah peningkatan damage setiap kali menyerang
    private int attackCount = 0; // Jumlah serangan yang sudah dilakukan

    // Maksimal serangan yang diizinkan
    private int maxAttacks = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight)
        {
            if (distanceFromPlayer <= attackRange)
            {
                if (!isAttacking && timeSinceLastAttack >= attackCooldown)
                {
                    StartCoroutine(PreAttack());
                }
            }
            else
            {
                timeSinceLastAttack = 0.0f;
                StopCoroutine(PreAttack());
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator PreAttack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(preAttackDuration);

        AttackPlayer();

        yield return new WaitForSeconds(attackCooldown - preAttackDuration);
        isAttacking = false;
    }

    void AttackPlayer()
    {
        // Menambahkan damage dan ukuran setiap kali menyerang
        if (attackCount < maxAttacks)
        {
            currentDamage += damageIncreaseAmount;
            float newSize = transform.localScale.x + sizeIncreaseAmount;
            transform.localScale = new Vector3(newSize, newSize, 1f);

            attackCount++;
        }

        // Menyerang pemain dengan damage yang baru
        player.GetComponent<PlayerHealth>().TakeDamage(currentDamage, false); // Ubah parameter kedua menjadi false
    }
}
