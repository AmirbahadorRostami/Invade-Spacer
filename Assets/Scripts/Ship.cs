using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvader
{
    public enum ShipType
    {
        enemy,
        player
    }

    public abstract class Ship : MonoBehaviour
    {   
        
        [SerializeField] public ShipType type;
        [SerializeField] protected float MovmentSpeed;
        [SerializeField] protected int StartingHealth = 100;
        [SerializeField] public int Health;

        public Action<ShipType> ShipDiedEvent;
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            Health = StartingHealth;
        }
    
        protected virtual void TakeDamage(int amount)
        {
            Health =- amount;
            if(Health <= 0)
                Die();
        }
    
        private void Die()
        {
            ShipDiedEvent?.Invoke(type);
            Destroy(gameObject);
        }
    
    }

}
