using UnityEngine;

public class pillierGrade : MonoBehaviour
{
    private float timerEnter;
    public GameObject timer;
    public TextMesh text;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = timer.GetComponent<prefabTimer>().currentTime.ToString();
        }
    }
}
