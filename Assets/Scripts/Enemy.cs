using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] float _shotCounter;
    [SerializeField] float _minTimeBetweenShots = 0.2f;
    [SerializeField] float _maxTimeBetweenShots = 3f;
    [SerializeField] float _projectileSpeed = 10f;

    [SerializeField] GameObject _projectile;

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Shot counter: " + _shotCounter.ToString());
        if(_shotCounter < 0f)
        {
            Debug.Log("Fired!");
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        //damageDealer.Hit();

        if (_health <= 0)
        {
            Destroy(gameObject);            
        }
    }
}
