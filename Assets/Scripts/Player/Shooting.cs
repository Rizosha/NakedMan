using System;
using UnityEngine;

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

        public Transform aim;
        //public MousePoint mousePoint;
        public Transform player;
        public Vector3 direction;

        private void Start()
        {
            ammoUI = GameObject.Find("Ammo");
            gunshot = GetComponent<AudioSource>();
            //mousePoint = FindFirstObjectByType<MousePoint>(); // Get the MousePoint script
        }

        public void Update()
        {
            // Timer for reloading
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
                    // Adds more bullets when reloaded, resets the timer and updates ammo UI
                    bulletsLeft += 12;
                    reloadDelta = 0;
                    ammoUI.GetComponent<Ammo>().ammo = 12;
                }
            }

            direction = (aim.transform.position - player.position).normalized;
            direction = player.TransformDirection(direction);
            
            
            if (shootReloading)
            {
                if (bulletsLeft >= 1)
                {
                    reloading = false;
                    shootTime += Time.deltaTime;
                    if (shootTime >= shootReloadTime && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GameObject pbullet = PlayerPooling.SharedInstance.GetPooledObject();
                        if (pbullet != null)
                        {
                            gunshot.PlayOneShot(gunClip);

                            Rigidbody rb = pbullet.GetComponent<Rigidbody>();
                            pbullet.transform.position = transform.position;
                            pbullet.SetActive(true);

                            // Calculate direction from player to mouse point
                           
                            rb.AddForce(direction.normalized * velocity, ForceMode.Impulse);
                            //rb.AddForce(finalDirection.normalized * velocity, ForceMode.Impulse);

                            bulletsLeft -= 1;
                            ammoUI.GetComponent<Ammo>().ammo -= 1;
                        }

                        shootReloading = false;
                        shootTime = 0;
                    }
                }
            }
        }
    }
}
    
    
