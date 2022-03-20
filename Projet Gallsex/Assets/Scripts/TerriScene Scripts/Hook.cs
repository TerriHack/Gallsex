
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
        [SerializeField] private LineRenderer lineRen;
        public float _inputX;
        public float _inputY;
        private RaycastHit2D _hit;
        private float _hookTimeCounter;
        private float hookTime;
        

        void Update()
        {
            Debug.Log(hookTime);
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
            Vector2 direction = new Vector2(_inputX * PlayerData.hookRange,_inputY * PlayerData.hookRange);
            lineRen.SetPosition(0, gobelinTr.position);

            
            _hit = Physics2D.Raycast(origin, direction, PlayerData.hookMaxRange);
            if(_hit.collider == null) return;

            _hookTimeCounter -= Time.deltaTime;
            hookTime -= Time.deltaTime;

            if (_hit.collider.CompareTag("Ground") && _hookTimeCounter <= 0f)
            {
                hookTime = PlayerData.hookRenDuration;
                gobelinRb.AddForce((direction.normalized * PlayerData.hookForce),ForceMode2D.Impulse);
                _hookTimeCounter = PlayerData.hookTime;
            }
            
            if (hookTime >= 0f)
            {
                Vector3 line = _hit.point; 
                
                lineRen.SetPosition(1, line);
                lineRen.enabled = true;
            }
            else
            {
                Debug.Log("non");
                lineRen.enabled = false;
            }
        }
    }
}
