using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // private config params
    [SerializeField] private int _damage = 100;

    // properties
    public int Damage => _damage;
    

    // TODO This is here for backwards compatibility
    public int GetDamage()
    {
        return _damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
