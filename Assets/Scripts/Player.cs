using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Sound Effects")]
    [SerializeField] GameObject shootVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip VFXSound;
    [SerializeField][Range(0, 1)] float VFXSoubdVoluem = 0.75f;

    //am thanh
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoubdVoluem = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoubdVoluem = 0.25f;

    //dan
    [Header("projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries(); // ham tao ranh gioi di chuyen
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoubdVoluem);
        //SceneManager.LoadScene(2);

    }

    public int getHealh()
    {
        return health;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
        
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
        GameObject laser = Instantiate(
                laserPrefab, transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoubdVoluem);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

   

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPOs = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPOs = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPOs, newYPOs);
    }

    private void SetUpMoveBoundaries() // ham tao ranh gioi di chuyen
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
