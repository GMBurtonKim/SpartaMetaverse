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

    //이동 변수
    private Vector2 direction = Vector2.zero;
    public float moveSpeed = 0f;

    //카메라 제한 영역
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
        // 움직일 수 있도록 키 부여 및 이동 실행
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;
        playerRB.velocity = direction * moveSpeed;

        //이동 방향에 따라 캐릭터 보는 방향 전환
        if (horizontal == -1)
            spriteRenderer.flipX = true;
        else if (horizontal == 1)
            spriteRenderer.flipX = false;

        // 움직였을때 애니메이션 실행
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
