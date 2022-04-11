using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = Color.green;
            player.GetComponent<ArrayCheckpoint>().AddingCheckpoint(new Vector2(player.transform.position.x,player.transform.position.y));
            gameObject.SetActive(false);
        }
    }
}
