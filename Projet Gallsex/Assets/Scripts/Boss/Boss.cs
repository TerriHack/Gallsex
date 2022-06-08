using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

namespace Boss
{
    public class Boss : MonoBehaviour
    {
        #region Variables

        [Header("GameObjects")] 
        private Transform BossTransform;
        private Rigidbody2D bossRB;
        [SerializeField] Transform playerTransform;
        [SerializeField] private AudioSource bossSound;
        public Volume globalVolume;
        [SerializeField] private CameraBoss _bossCam;

        [Header("Vitesse de base du boss")]
        [SerializeField] private float bossBaseSpeed;
        [SerializeField] private float bossBaseSpeedY;
        [SerializeField] private float bossYSpeed;

        [Header("Multiplicateurs")]
        [SerializeField] private float multClose;
        [SerializeField] private float multMid;
        [SerializeField] private float multFar;
        [SerializeField] private float multPhase3;
        
        [Header("Distance et limites entre les zones")]
        [SerializeField] private float distance;
        [SerializeField] private float zone1;
        [SerializeField] private float zone2;
        [SerializeField] private float zone3;
        [SerializeField] private float zone4;

        [Header("Timer de randomisation")]
        private float timerRandom;
        [SerializeField] private float maxTimerRandom;

        [Header("Variation sinus")] 
        [Range(0.1f, 5f)]
        private float sinusSpeed;
        [Range(0.1f, 5f)]
        private float sinusAmplitude;

        [HideInInspector]
        public bool isGone;
        #endregion
        
        private void Awake()
        {
            bossSound = GetComponent<AudioSource>();
            BossTransform = gameObject.GetComponent<Transform>();
            bossRB = gameObject.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // Timer pour randomiser la vitesse du boss
            timerRandom -= Time.deltaTime;
            if (timerRandom <= 0)
            {
                Randomisation();
            }

            bossYSpeed = Mathf.Sin(Time.time * sinusSpeed) * sinusAmplitude;

            //Calcule la distance entre le boss et le joueur et choisit la méthode en fonction
            distance = Vector2.Distance(BossTransform.position, playerTransform.position);

            if (_bossCam.phaseCounter == 0)
            {
                MotionLess();
            }
            else if (_bossCam.phaseCounter == 6)
            {
                BossDefeated();
            }
            else if (_bossCam.phaseCounter != 3)
            {
                if (distance > 0f && distance < zone1) CloseMovement();
                else if (distance > zone1 && distance < zone2) MidMovement();
                else if (distance > zone2 && distance < zone3) FarMovement();
                else if (distance > zone3) AwayMovement();
            }
            else
            {
                Phase3Movement();
            }

            if (distance > 8)
            {
                globalVolume.weight = 8 / distance;
            }
            else
            {
                globalVolume.weight = 1;
            }
            
            //Debug des 3 zones
            DebugZone();
        }
        
        //Randomise les multiplicateurs de vitesse
        private void Randomisation()
        {
            //random de la vitesse X
            multClose = Random.Range(0.6f, 0.9f);
            multMid = Random.Range(1f, 1.4f);
            multFar = Random.Range(1.4f, 1.8f);
            multPhase3 = Random.Range(-0.3f, - 0.5f);
            
            //random de la trajectoire Y
            sinusSpeed = Random.Range(2f, 4f);
            sinusAmplitude = Random.Range(1f, 2.5f);
            
            timerRandom = maxTimerRandom;
        }
        
        #region Fonctions de mouvements
        void CloseMovement()
        {
            if (_bossCam.phaseCounter == 5)
            {
                bossSound.enabled = true;
                bossRB.velocity = new Vector2(bossYSpeed, bossBaseSpeedY * multClose);
            }
            else if(_bossCam.phaseCounter == 4) bossRB.velocity = Vector2.zero;
            else if(_bossCam.phaseCounter < 3) bossRB.velocity = new Vector2(bossBaseSpeed * multClose, bossYSpeed); 
        }
        void MidMovement()
        {
            if(_bossCam.phaseCounter == 5) bossRB.velocity = new Vector2(bossYSpeed, bossBaseSpeedY * multMid);
            else if(_bossCam.phaseCounter == 4) bossRB.velocity = Vector2.zero;
            else if(_bossCam.phaseCounter < 3) bossRB.velocity = new Vector2(bossBaseSpeed * multMid, bossYSpeed); 
        }
        void FarMovement()
        {
            if(_bossCam.phaseCounter == 5)bossRB.velocity = new Vector2(bossYSpeed, bossBaseSpeedY * multFar);
            else if(_bossCam.phaseCounter == 4) bossRB.velocity = Vector2.zero;
            else if(_bossCam.phaseCounter < 3) bossRB.velocity = new Vector2(bossBaseSpeed * multFar, bossYSpeed); 
        }
        void AwayMovement()
        {
            if(_bossCam.phaseCounter == 5) bossRB.velocity = new Vector2(bossYSpeed, bossBaseSpeedY * 3); 
            else if(_bossCam.phaseCounter == 4) bossRB.velocity = Vector2.zero;
            else if(_bossCam.phaseCounter < 3) bossRB.velocity = new Vector2(bossBaseSpeed * 3, bossYSpeed);
        }
        void Phase3Movement()
        {
            bossRB.velocity = new Vector2(bossBaseSpeed * multPhase3, bossYSpeed);

            if (distance > zone4)
            {
                BossTeleportation();
                isGone = true;
            }
        }

        void MotionLess()
        {
            bossRB.velocity = Vector2.zero;
        }
        
        void BossDefeated()
        {
            bossRB.velocity = Vector2.down * bossBaseSpeedY;
        }
        #endregion

        public void BossTeleportation()
        {
            transform.position = new Vector3(497.5f, -8, 0);
            bossSound.clip = null;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        public void ResetBoss()
        {
            BossTransform.position = new Vector3(-42.5f, 3, 0);
            _bossCam.phaseCounter = 0;
        }

        void DebugZone()
        {
            var position = playerTransform.position;
            var zone1Ray = new Vector2(position.x - zone1, -100);
            var zone2Ray = new Vector2(position.x - zone2, -100);
            var zone3Ray = new Vector2(position.x - zone3, -100);
            var zone4Ray = new Vector2(position.x - zone4, -100);
            var rayDirX = new Vector3(0, 200, 0);

            Debug.DrawRay(zone1Ray, rayDirX, Color.green);
            Debug.DrawRay(zone2Ray, rayDirX, Color.yellow);
            Debug.DrawRay(zone3Ray, rayDirX, Color.red);
            Debug.DrawRay(zone4Ray, rayDirX, Color.cyan);
        }
    }
}

