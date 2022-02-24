using System;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Shooting : MonoBehaviour
    {
      
        public float shootTime;
        [SerializeField] public float shootReloadTime;
        public bool shootReloading = true;
        public float velocity;

        public int bulletsLeft = 6;
        public float reloadDelta;
        public float reloadTime;
        public bool reloading;

        private GameObject ammoUI;
        public AudioSource gunshot;
        public AudioClip gunClip;

        /// <summary>
        /// responsible for most of the players fire rate and players reload time
        /// </summary>

        private void Start()
        {
            ammoUI = GameObject.Find("Ammo");
            gunshot = GetComponent<AudioSource>();
        }


        public void Update()
        {
            //timer for reloading
            if (shootTime < shootReloadTime)
            {
                shootReloading = true;
            }

            if (bulletsLeft <= 0)
            {
                reloading = true;
                reloadDelta += Time.deltaTime;
                if (reloadDelta >= reloadTime)
                {
                    //adds more bullets when reloaded, resets the timer and updates ammo UI
                    bulletsLeft += 12;
                    reloadDelta = 0;
                    ammoUI.GetComponent<Ammo>().ammo = 12;
                }
            }
            
            if (shootReloading)
            {
                //sets the fire rate of the player if you have bullets
                if (bulletsLeft >= 1)
                {
                    reloading = false;
                    
                    shootTime += Time.deltaTime;
                    if (shootTime >= shootReloadTime && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GameObject pbullet = PlayerPooling.SharedInstance.GetPooledObject();
                        if (pbullet != null)
                        {   
                            //sound
                            gunshot.PlayOneShot(gunClip);

                            //sets velocity, position and active based on fire rate 
                            Rigidbody rb = pbullet.GetComponent<Rigidbody>();
                            pbullet.transform.position = transform.position;
                            pbullet.SetActive(true);
                            rb.AddForce(transform.forward * velocity, ForceMode.Impulse);
                            
                            //updates UI 
                            bulletsLeft -= 1;
                            ammoUI.GetComponent<Ammo>().ammo -= 1;
                        }
                        //reset rate
                        shootReloading = false;
                        shootTime = 0;
                    }
                }
            }
        }
    }
    }

    
    
