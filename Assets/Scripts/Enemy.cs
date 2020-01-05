using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int killPoints = 50;


    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionDuration = 1f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();        
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        Player player = FindObjectOfType<Player>();
        if (!player) { return; }
        GameObject laser = Instantiate(
                    projectile,
                    transform.position,
                    Quaternion.identity) as GameObject;
        // Aim the projectile where the player is
        Vector2 velocity = new Vector2(player.transform.position.x - transform.position.x,
                                       player.transform.position.y - transform.position.y);
        velocity.Normalize();
        velocity *= projectileSpeed;

        laser.GetComponent<Rigidbody2D>().velocity = velocity;
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {   
           Die(); 
        }
    }

    private void Die() {
        GameObject explosion = Instantiate(
                                    explosionParticles,
                                    transform.position,
                                    Quaternion.identity
                                ) as GameObject;
        Destroy(gameObject);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<GameSession>().IncreaseScore(killPoints);
    }
}
