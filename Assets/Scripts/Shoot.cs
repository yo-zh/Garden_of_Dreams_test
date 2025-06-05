using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] float attackDistance = 5;
    [SerializeField] Item ammo;
    [SerializeField] int damage = 1;

    public GameObject FindNearestEnemy(Vector3 playerPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = attackDistance;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void OnShoot(InputValue value)
    {
        GameObject nearestEnemy = FindNearestEnemy(transform.position);

        if (transform.GetComponent<Inventory>().CheckItem(ammo))
        {
            if (nearestEnemy != null)
            {
                Debug.Log("Shots fired!");
                transform.GetComponent<Inventory>().RemoveItem(ammo);
                nearestEnemy.GetComponent<HealthSystem>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("No enemies near me!");
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }

    }
}
