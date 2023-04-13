using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject snake;

    private Bounds bounds;


    private void Start()
    {
        bounds = gridArea.bounds; 
        RandomizePosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RandomizePosition();
        }
    }

    private bool IsTileEmpty(Vector3 foodPos)
    {
        if (snake.transform.position == foodPos)
        {
            Debug.Log("Snake Pos");
            return false;
        }
        
        foreach (Transform bodySegment in snake.GetComponent<Snake>().bodySegments)
        {
            if (bodySegment.position == foodPos)
            {
                Debug.Log("SnakeSeg Pos");
                return false;
            }
        }

        return true;
    }

    private void RandomizePosition()
    {
        Vector3 newPos;
        do
        {
            float xPos = Random.Range(bounds.min.x, bounds.max.x);
            float yPos = Random.Range(bounds.min.y, bounds.max.y);

            newPos = new Vector3(Mathf.Round(xPos), Mathf.Round(yPos), 0);
        }
        while (!IsTileEmpty(newPos));

        transform.position = newPos;
    }
}
