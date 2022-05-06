using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private GameObject dash;   
    [SerializeField] private ParticleSystemRenderer dashVFX;
    [SerializeField] private GameObject landing;

    private Vector2 _feetPos;
    
    public bool isLanding;

    private void Update()
    {
        if(isLanding) LandingVFX();
    }
    
    private void LandingVFX()
    {
        _feetPos = new Vector2(playerTr.position.x, playerTr.position.y - 1f);
        Instantiate(landing, _feetPos, playerTr.rotation);
        isLanding = false;
    }

    private void DashVFX()
    {
        Instantiate(_dashVFXGameObject, playerTr.position, _dashVFX.transform.rotation);
    }
}
