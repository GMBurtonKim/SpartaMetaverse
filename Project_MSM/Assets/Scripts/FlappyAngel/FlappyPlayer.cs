using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    Rigidbody2D playerRB;
    AnimeManager anime;
    Camera cam;

    [Header("점프 힘과 앞으로 향하는 속도")]
    public float flapForce = 5f;
    public float forwardSpeed = 3f;

    private bool IsDead = false;
    private bool IsFlap = false;
    private bool IsAlreadyDamaged = false;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<AnimeManager>();

        cam = Camera.main;

        playerRB.simulated = false;
    }

    private void Update()
    {
        if (!MiniGameManagerBase.Instance.IsGameStart) return;

        FollowCam();

        if (IsDead)
        {
            if (!IsAlreadyDamaged)
            {
                anime.Damage(true);
                FlappyGameManager.Instance.GameOver();
                IsAlreadyDamaged = true;
            }
            return;
        }
        else
        {
            playerRB.simulated = true;
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                IsFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsDead) return;

        Vector2 velocity = playerRB.velocity;
        velocity.x = forwardSpeed;

        if (IsFlap)
        {
            velocity.y = 0;
            velocity.y += flapForce;
            IsFlap = false;
        }

        playerRB.velocity = velocity;

        float angle = Mathf.Clamp((playerRB.velocity.y * 9), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FollowCam()
    {
        Vector3 camPos = cam.transform.position;
        camPos.x = transform.position.x;
        cam.transform.position = camPos;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsDead = true;
    }

    public void ResetPlayer()
    {
        IsDead = false;
        IsAlreadyDamaged = false;
        anime.Damage(false);
        transform.rotation = Quaternion.identity;
        playerRB.simulated  = true;
    }
}
