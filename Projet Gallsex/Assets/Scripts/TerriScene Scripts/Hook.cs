
using System;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class Hook : MonoBehaviour
    {

        [SerializeField] private Transform directionTr;
        [SerializeField] private Transform originTr;
        [SerializeField] private Transform gobelinTr;
        [SerializeField] private Rigidbody2D gobelinRb;
        [SerializeField] private PlayerControler pC;
        public float _inputX;
        public float _inputY;
        private RaycastHit2D _hit;
        private float hookTimeCounter;
        

        void Update()
        {
            _inputX = Input.GetAxisRaw("Mouse X");
            _inputY = Input.GetAxisRaw("Mouse Y");

            Aim();
            Shoot();
        }

        private void Aim()
        {
            Vector2 position = gobelinTr.position;
            directionTr.position = new Vector2(position.x +_inputX * 1.5f, position.y + _inputY * 1.5f);
            originTr.position = new Vector2(position.x +_inputX, position.y + _inputY);
        }

        private void Shoot()
        {
            Vector2 origin = originTr.position;
            Vector2 direction = new Vector2(_inputX * 2,_inputY * 2);

            Debug.DrawRay(origin, direction, Color.blue);
            _hit = Physics2D.Raycast(origin, direction, PlayerData.hookRange);
            if(_hit.collider == null) return;

            hookTimeCounter -= Time.deltaTime;
                
            if (_hit.collider.CompareTag("Ground") && hookTimeCounter <= 0f)
            {
                Debug.Log("oui");
                gobelinRb.AddForce(new Vector2(_inputX,_inputY) * PlayerData.hookForce ,ForceMode2D.Impulse);
                hookTimeCounter = PlayerData.hookTime;
            }
        }
    }
}
