using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private HealthSystem healthSystem;
    private int healthPoints;

    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject loot;
    [SerializeField] private float visionDistance = 4;
    [SerializeField] private float attackDistance = 2;
    private Rigidbody2D rb;
    private bool isAttacking;

    private GameObject player;
    private Vector2 playerPosition;
    private Vector2 direction;
    private float distance;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void DropLoot()
    {
        Instantiate(loot, transform.position, transform.rotation);
    }
    IEnumerator Attacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            player.GetComponent<HealthSystem>().TakeDamage(damage);
            Debug.Log("Ouch!");
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
    }

    private void MoveToPlayer()
    {
        playerPosition = player.transform.position;
        distance = (playerPosition - rb.position).magnitude;
        direction = (playerPosition - rb.position).normalized;

        if (distance < visionDistance)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            if (distance <= attackDistance)
            {
                StartCoroutine(Attacking());
            }
        }
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        DropLoot();
    }

    void Update()
    {
        MoveToPlayer();
    }
}
