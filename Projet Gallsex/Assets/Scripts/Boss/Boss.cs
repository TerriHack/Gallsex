using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

namespace Boss
{
    public class Boss : MonoBehaviour
    {
        #region Variables

        [Header("GameObjects")]
        private Transform bossTransform;
        private Rigidbody2D bossRB;
        [SerializeField] Transform playerTransform;
        public Volume globalVolume;

        [Header("Vitesse de base du boss")]
        [SerializeField] private float bossBaseSpeed;
        [SerializeField] private float bossYSpeed;

        [Header("Multiplicateurs")]
        [SerializeField] private float multClose;
        [SerializeField] private float multMid;
        [SerializeField] private float multFar;
        
        [Header("Distance et limites entre les zones")]
        [SerializeField] private float distance;
        [SerializeField] private float zone1;
        [SerializeField] private float zone2;
        [SerializeField] private float zone3;

        [Header("Timer de randomisation")]
        private float timerRandom;
        [SerializeField] private float maxTimerRandom;

        [Header("Variation sinus")] 
        [Range(0.1f, 5f)]
        private float sinusSpeed;
        [Range(0.1f, 5f)]
        private float sinusAmplitude;

        #endregion
        
        private void Awake()
        {
            bossTransform = gameObject.GetComponent<Transform>();
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
            distance = Vector2.Distance(bossTransform.position, playerTransform.position);

            if (distance > 0f && distance < zone1) CloseMovement();
            else if (distance > zone1 && distance < zone2) MidMovement();
            else if (distance > zone2 && distance < zone3) FarMovement();
            else if (distance > zone3) AwayMovement();

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
            
            //random de la trajectoire Y
            sinusSpeed = Random.Range(2f, 4f);
            sinusAmplitude = Random.Range(1f, 2.5f);
            
            timerRandom = maxTimerRandom;
        }
        
        #region Fonctions de mouvements
        void CloseMovement()
        {
            bossRB.velocity = new Vector2(bossBaseSpeed * multClose, bossYSpeed);
        }
        
        void MidMovement()
        {
            bossRB.velocity = new Vector2(bossBaseSpeed * multMid, bossYSpeed);
        }
        
        void FarMovement()
        {
            bossRB.velocity = new Vector2(bossBaseSpeed * multFar, bossYSpeed);
        }
        
        void AwayMovement()
        {
            bossRB.velocity = new Vector2(bossBaseSpeed * 3, bossYSpeed);
        }
        #endregion
        
        void DebugZone()
        {
            var position = playerTransform.position;
            var zone1Ray = new Vector2(position.x - zone1, -100);
            var zone2Ray = new Vector2(position.x - zone2, -100);
            var zone3Ray = new Vector2(position.x - zone3, -100);
            var rayDir = new Vector3(0, 200, 0);
            
            Debug.DrawRay(zone1Ray, rayDir, Color.green);
            Debug.DrawRay(zone2Ray, rayDir, Color.yellow);
            Debug.DrawRay(zone3Ray, rayDir, Color.red);
        }
    }
}

