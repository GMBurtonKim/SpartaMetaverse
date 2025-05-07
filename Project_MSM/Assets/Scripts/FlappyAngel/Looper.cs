using UnityEngine;

public class Looper : MonoBehaviour
{
    public int bgCount = 5;

    private Vector3 lastObstaclePosition = Vector3.zero;

    private GameObject[] bgObjects;
    private Vector3[] bgStartPositions;

    private Obstacle[] obstacles;

    private void Start()
    {
        CacheBackgrounds();
        CacheObstacles();
        PlaceInitialObstacles();
    }

    private void CacheBackgrounds()
    {
        bgObjects = GameObject.FindGameObjectsWithTag("BackGround");
        bgStartPositions = new Vector3[bgObjects.Length];

        for (int i = 0; i < bgObjects.Length; i++)
        {
            bgStartPositions[i] = bgObjects[i].transform.position;
        }
    }

    private void CacheObstacles()
    {
        obstacles = FindObjectsOfType<Obstacle>();
    }

    private void PlaceInitialObstacles()
    {
        if (obstacles == null || obstacles.Length == 0) return;

        lastObstaclePosition = Vector3.zero;

        foreach (var obstacle in obstacles)
        {
            lastObstaclePosition = obstacle.SetRandomPlace(lastObstaclePosition);
        }
    }

    public void ResetObstacles()
    {
        if (bgObjects != null && bgStartPositions != null)
        {
            for (int i = 0; i < Mathf.Min(bgObjects.Length, bgStartPositions.Length); i++)
            {
                bgObjects[i].transform.position = bgStartPositions[i];
            }
        }
        PlaceInitialObstacles();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            RecycleBackground(collision);
        }
        else
        {
            Obstacle obstacle = collision.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                lastObstaclePosition = obstacle.SetRandomPlace(lastObstaclePosition);
            }
        }
    }

    private void RecycleBackground(Collider2D bgCollider)
    {
        BoxCollider2D box = bgCollider as BoxCollider2D;
        if (box == null) return;

        Vector3 pos = bgCollider.transform.position;
        pos.x += box.size.x * bgCount;
        bgCollider.transform.position = pos;
    }
}
