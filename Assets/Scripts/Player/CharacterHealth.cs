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
      //sets the health bar to max health at the start
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
      
      currentKey = setKey;
   }

   private void Update()
   {
      if (currentHealth <= 0)
      {
         gameObject.SetActive(false);
        pause.SetActive(true);
      }
      
      //sets health back to 100 if health goes over with pickup. 
      if (currentHealth > maxHealth)
      {
         currentHealth = 100;
      }
      
      //DoorUnlock checks this to see if the player has a key
      if (currentKey == 1)
      {
         hasAKey = true;
      }
      
      
   }
   
   //takes damage if hit by bullet and sets it inactive
   void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("bullet"))
      {
         TakeDamage(5);
        other.gameObject.SetActive(false);
      }
   }

   //updates the health bar and health from damage
   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
   }
   
   //updates the health bar and health from pickup
   public void HealthPickup(int health)
   {
      currentHealth += health;
      healthBar.SetHealth(currentHealth);
   }

   //adds a key
   public void KeyPickup(int key)
   {
      currentKey += key;
   }
   
}

