using System;
using System.Collections;
using System.Collections.Generic;
using SpaceInvader;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explosion;

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosion.Play();
        
        if (other.tag == "Player")
        {
            //Damage the player
            other.GetComponent<PlayerShip>().takeDamage(10);
            
        }
        else if(other.tag == "Enemy")
        {
            //Damage the enemy 
            other.GetComponent<PlayerShip>().takeDamage(10);
        }
        
        //Play a Explosion particle
    }
}
