using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : ChangeSceneManager
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ToFlappyScene();
    }
}
