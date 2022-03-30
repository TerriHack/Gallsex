using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerdash : MonoBehaviour
{
    public ParticleSystem dashanim;
    public ParticleSystem dashanim2;
    [SerializeField] public PlayerBetterController playerBetterController;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (playerBetterController._facingRight == true)
            {
                dashanim.transform.localScale = new Vector3(-1f, 1f, 1f);
                dashanim2.transform.localScale = new Vector3(-2f, 0.6f, 1f);
            }
            if (playerBetterController._facingRight == false)
            {

            dashanim.transform.localScale = new Vector3(1f, 1f, 1f);
            dashanim2.transform.localScale = new Vector3(2f, 0.6f, 1f);

            }
            dashanim.Play();
            dashanim2.Play();
        }
    }

}
