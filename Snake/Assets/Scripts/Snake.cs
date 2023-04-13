using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public GameObject bodySegment;
    public GameObject resetButton;
    
    public List<Transform> bodySegments = new List<Transform>();

    private Vector2 direction = Vector2.right;
    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true) { return; }

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        if (gameOver == true) { return; }
        if (bodySegments.Count > 0)
        {
            // Move the last segment to the previous segment's position
            bodySegments[bodySegments.Count - 1].position = bodySegments[0].position;
            bodySegments.Insert(0, bodySegments[bodySegments.Count - 1]);
            bodySegments.RemoveAt(bodySegments.Count - 1);
        }

        if (bodySegments.Count > 0)
        {
            bodySegments[0].position = transform.position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y)+ direction.y, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            GameObject newSegment = Instantiate(bodySegment, bodySegments.Count > 0 ? bodySegments[bodySegments.Count - 1].position : transform.position, Quaternion.identity);
            bodySegments.Add(newSegment.transform);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        direction = Vector2.zero;
        resetButton.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene("Snake");        
    }
}
