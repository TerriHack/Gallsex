using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.color = Color.green;
        //Player.GetComponent<ArrayCheckpoint>().checkpointArray.Un;
        player.GetComponent<ArrayCheckpoint>().AddingCheckpoint(new Vector2(transform.position.x,transform.position.y));
    }
}
