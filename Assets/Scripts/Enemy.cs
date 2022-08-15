using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy start")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject shootVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip VFXSound;
    [SerializeField][Range(0, 1)] float deathSoubdVoluem = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoubdVoluem = 0.25f;
    Coroutine firingCoroutine;

    
    
    [SerializeField][Range(0, 1)] float VFXSoubdVoluem = 0.75f;

    // Start is called before the first frame update
    void Start()
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
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoubdVoluem); 
    }

    public void TurnOffFire()
    {
        gameObject.SetActive(false);
    }
    public void TurnOnFire()
    {
        gameObject.SetActive(true);
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                    projectile, transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoubdVoluem);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcesHit(damageDealer);
    }

    private void ProcesHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        GameObject exposion = Instantiate(shootVFX, transform.position, transform.rotation);
        Destroy(exposion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(VFXSound, Camera.main.transform.position, VFXSoubdVoluem);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject exposion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(exposion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoubdVoluem);
    }
}
