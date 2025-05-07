using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Y 위치 랜덤 범위")]
    public float highPosY = 0.5f;
    public float lowPosY = -0.5f;

    [Header("구멍 크기 범위")]
    public float holeSizeMax = 3f;
    public float holeSizeMin = 1f;

    [Header("오브젝트 연결")]
    public Transform topObject;
    public Transform bottomObject;

    [Header("장애물 간 간격")]
    public float spacingBetween = 4f;

    private bool hasScored = false;

    public Vector3 SetRandomPlace(Vector3 lastPosition)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector2(0, halfHoleSize);
        bottomObject.localPosition = new Vector2(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(spacingBetween, 0, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);
        transform.position = placePosition;

        hasScored = false;

        return placePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasScored) return;

        FlappyPlayer player = collision.GetComponent<FlappyPlayer>();
        if (player != null && FlappyGameManager.Instance != null)
        {
            FlappyGameManager.Instance.AddScore(1);
            hasScored = true;
        }
    }
}