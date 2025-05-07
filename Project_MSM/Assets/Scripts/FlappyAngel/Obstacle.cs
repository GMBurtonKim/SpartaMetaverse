using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Y ��ġ ���� ����")]
    public float highPosY = 0.5f;
    public float lowPosY = -0.5f;

    [Header("���� ũ�� ����")]
    public float holeSizeMax = 3f;
    public float holeSizeMin = 1f;

    [Header("������Ʈ ����")]
    public Transform topObject;
    public Transform bottomObject;

    [Header("��ֹ� �� ����")]
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