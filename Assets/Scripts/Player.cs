using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    [SerializeField] private float respawnDelay;

    [SerializeField] private Text livesText;
    private int lives;

    [SerializeField] private Text pickUpText;
    private int pickUp;

    private Vector3 respawnPoint;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        respawnPoint = transform.position;
        pickUp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        if(lives > 1)
        {
            lives--;
            StartCoroutine(Respawn());
        }
        else
        {
            gameManager.GetComponent<GameManager>().GameOver();
        }
    }

    public void AddPickUp()
    {
        pickUp++;
    }

    IEnumerator Respawn()
    {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        transform.position = respawnPoint;
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }
}
