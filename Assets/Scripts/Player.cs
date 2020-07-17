using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _padding = 1f;
    [SerializeField] int _health = 200;
    [Header("Projectile")]
    [SerializeField] GameObject _projectile;
    [SerializeField] float _projectileSpeed = 10f;
    [SerializeField] float _projectileFiringPeriod = 0.1f;
        
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
            GameObject laser = Instantiate(
                _projectile,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);

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

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        damageDealer.Hit();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
