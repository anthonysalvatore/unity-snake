using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private Bounds bounds;


    private void Start()
    {
        bounds = gridArea.bounds; 
        RandomizePosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }

    private void RandomizePosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }
}
