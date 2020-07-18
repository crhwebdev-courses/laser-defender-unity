using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _padding = 1f;
    [SerializeField] private int _health = 200;    
    [Header("Projectile")]
    [SerializeField] private GameObject _projectile;    
    [SerializeField] private AudioClip _fireLaserSFX;
    [SerializeField] [Range(0, 1)] private float _fireLaserSFXVolume = 0.25f;    
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileFiringPeriod = 0.1f;
    [Header("Explosion")]
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _durationOfExplosion = 1f;
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] [Range(0, 1)] private float _deathSFXVolume = 0.7f;

    // state
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;
    Coroutine _firingCoroutine;
    
    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

        transform.position = new Vector2(newXPos, newYPos);        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            // StopAllCoroutines();
            StopCoroutine(_firingCoroutine);
        }
    }
    
    private IEnumerator FireContinuously()
    {
        while(true)
        {
            // Play fire SFX Here
            GameObject laser = Instantiate(
                _projectile,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);

            // Play fire SFX Here
            AudioSource.PlayClipAtPoint(_fireLaserSFX, Camera.main.transform.position, _fireLaserSFXVolume);

            yield return new WaitForSeconds(_projectileFiringPeriod);
        }                
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;

        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        damageDealer.Hit();

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {        
        Destroy(gameObject);
        GameObject explosion = Instantiate(_explosion, transform.position, transform.rotation);
        Destroy(explosion, _durationOfExplosion);
        //Play death SFX here
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, _deathSFXVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }
}
