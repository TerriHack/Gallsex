
using System.Collections;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class LevelBeginner : MonoBehaviour
{
    [SerializeField] private Animator blackScreenAnim;
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private Image blackScreen;
    
    

    private void Start()
    {
        blackScreenAnim.SetTrigger("isBeginning");
        StartCoroutine(Begining());
    }

    IEnumerator Begining()
    {
        playerController.levelBeginning = true;
        yield return new WaitForSeconds(1.3f);
        playerController.levelBeginning = false;
    }
}
