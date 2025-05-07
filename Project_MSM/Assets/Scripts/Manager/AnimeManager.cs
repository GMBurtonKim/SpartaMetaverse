using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeManager : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Moving(bool IsMove)
    {
        animator.SetBool("IsMove", IsMove);
    }

    public void Damage(bool IsDamage)
    {
        animator.SetBool("IsDamage", IsDamage);
    }

    public void DoorOpening()
    {
        animator.SetBool("IsOpen", true);
    }

    public void DoorClosing()
    {
        animator.SetBool("IsOpen", false);
    }
}
