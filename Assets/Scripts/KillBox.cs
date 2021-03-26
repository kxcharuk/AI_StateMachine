using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.Death();
        }
        else { other.gameObject.SetActive(false); }
    }
}
