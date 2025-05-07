using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    Camera cam;
    private Rigidbody2D playerRB;
    private SpriteRenderer spriteRenderer;
    public AnimeManager anime;

    //�̵� ����
    private Vector2 direction = Vector2.zero;
    public float moveSpeed = 0f;

    //ī�޶� ���� ����
    public float minX = -3.75f, maxX = 3.75f;
    public float minY = -3.3f, maxY = 3.5f;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anime = GetComponentInChildren<AnimeManager>();
        cam = Camera.main;
    }

    private void Update()
    {
        Movement();
        CameraFollow();
    }

    public void Movement()
    {
        // ������ �� �ֵ��� Ű �ο� �� �̵� ����
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;
        playerRB.velocity = direction * moveSpeed;

        //�̵� ���⿡ ���� ĳ���� ���� ���� ��ȯ
        if (horizontal == -1)
            spriteRenderer.flipX = true;
        else if (horizontal == 1)
            spriteRenderer.flipX = false;

        // ���������� �ִϸ��̼� ����
        bool IsMove = direction.sqrMagnitude > 0;
        anime.Moving(IsMove);
    }

    private void CameraFollow()
    {
        Vector3 playerPos = playerRB.position;
        Vector3 camPos = cam.transform.position;

        camPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(playerPos.y, minY, maxY);

        cam.transform.position = camPos;
    }
}
