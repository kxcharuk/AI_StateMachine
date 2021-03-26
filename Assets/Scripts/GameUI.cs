using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text pickUpText;
    [SerializeField] private Text livesText;
    [SerializeField] private GameObject playerObj;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        pickUpText.text = (player.PickUp.ToString() + " / " + player.MaxPickUps.ToString());
        livesText.text = ("Lives: " + player.Lives.ToString());
    }
}
