using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    public float attackCooldown = 2.0f;
    public float preAttackDuration = 1.0f;
    public float damageAmount = 10f; // Tambahkan variabel untuk jumlah damage yang diberikan musuh
    private Transform player;
    private bool isAttacking = false;
    private float timeSinceLastAttack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

    // ...

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
        player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }

    // ...

}