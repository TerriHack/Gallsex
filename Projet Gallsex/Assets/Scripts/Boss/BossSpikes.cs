using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boss
{
    public class BossSpikes : MonoBehaviour
    {
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log(3);
                SceneManager.LoadScene("Level_Boss_Scene");
            }
        }
    }
}
