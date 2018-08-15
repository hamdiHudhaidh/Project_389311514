using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffect;

    float bulletDamage = 10;
    float bulletSpeed = 10f;
    float radius = 5f;

	void FixedUpdate ()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject explosionEffectInstance = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            EnemyHealth enemyHealth = nearbyObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float damagePoints = (10f - distance) * bulletDamage;
                enemyHealth.currentEnemyHealth -= damagePoints;
                enemyHealth.healthBar.fillAmount = enemyHealth.currentEnemyHealth / enemyHealth.initialEnemyHealth;
            }
        }
        Destroy(this.gameObject);
    }
}
