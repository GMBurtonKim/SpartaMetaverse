using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Collider2D offCollider;
    private AnimeManager anime;

    [SerializeField] private GameObject fKeyTxt;

    private bool IsPlayerNearby = false;
    private bool IsDoorOpen = false;


    private void Awake()
    {
        offCollider = GetComponent<Collider2D>();
        anime = GetComponent<AnimeManager>();
    }

    private void Update()
    {
        if (IsPlayerNearby)
        {
            ShowUI(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (IsDoorOpen)
                    CloseDoor();
                else OpenDoor();

                IsDoorOpen = !IsDoorOpen;
            }
        }
        else
        {
            ShowUI(false);
        }
    }

    public void OpenDoor()
    {
        offCollider.enabled = false;
        anime.DoorOpening();
    }

    public void CloseDoor()
    {
        offCollider.enabled = true;
        anime.DoorClosing();
    }

    public void ShowUI(bool IsShow)
    {
        fKeyTxt.SetActive(IsShow);
    }
    public void SetNearby(bool isNear)
    {
        IsPlayerNearby = isNear;
    }
}
