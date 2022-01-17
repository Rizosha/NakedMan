using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
   public int maxHealth = 100;
   public int currentHealth;

   public HealthBar healthBar;

   public int setKey = 0;
   public int currentKey;
   public bool hasAKey;
   public GameObject pause;
   
   
   
   private void Start()
   {
      currentHealth = maxHealth;
      
      healthBar.SetMaxHealth(maxHealth);

      currentKey = setKey;
      
   }

   private void Update()
   {
      
     

      if (currentHealth <= 0)
      {
        // Destroy(gameObject);
        gameObject.SetActive(false);
        pause.SetActive(true);
        
        
      }
      if (currentHealth > maxHealth)
      {
         currentHealth = 100;
      }
      
      
      if (currentKey == 1)
      {
         hasAKey = true;
      }
   }

   void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("bullet"))
      {
         TakeDamage(5);
        other.gameObject.SetActive(false);
      }

   }

   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
   }

   public void HealthPickup(int health)
   {
      currentHealth += health;
      healthBar.SetHealth(currentHealth);
   }

   public void KeyPickup(int key)
   {
      currentKey += key;
   }
   
}

