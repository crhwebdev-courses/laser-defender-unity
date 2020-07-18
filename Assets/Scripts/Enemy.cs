using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private float _health = 100;
    [SerializeField] private int _pointsPerKill = 60;
    [Header("Projectile")]
    [SerializeField] GameObject _projectile;
    [SerializeField] AudioClip _fireLaserSFX;
    [SerializeField] [Range(0, 1)] private float _fireLaserSFXVolume = 0.25f;
    [SerializeField] float _projectileSpeed = 10f;
    [SerializeField] float _shotCounter;
    [SerializeField] float _minTimeBetweenShots = 0.2f;
    [SerializeField] float _maxTimeBetweenShots = 3f;        
    [Header("Explosion")]
    [SerializeField] GameObject _explosion;
    [SerializeField] float _durationOfExplosion = 1f;
    [SerializeField] AudioClip _deathSFX;
    [SerializeField] [Range(0, 1)] private float _deathSFXVolume = 0.7f;

    private ScoreBoard _scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        _shotCounter = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;     
        if(_shotCounter < 0f)
        {            
            Fire();
            _shotCounter = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        
        GameObject laser = Instantiate(
                _projectile,
                transform.position,
                Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);

        // Play fire SFX Here
        AudioSource.PlayClipAtPoint(_fireLaserSFX, Camera.main.transform.position, _fireLaserSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){ return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        damageDealer.Hit();

        if (_health <= 0)
        {
            //increment score here
            _scoreBoard.Score += _pointsPerKill;

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
    }
    
}
