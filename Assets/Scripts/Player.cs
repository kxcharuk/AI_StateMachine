using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    [SerializeField] private float respawnDelay;
    
    private int lives;
    public int Lives
    {
        get => lives;
    }
    
    private int pickUp;
    public int PickUp
    {
        get => pickUp;
    }

    private int maxPickUps;
    public int MaxPickUps
    {
        get => maxPickUps;
    }

    private Vector3 respawnPoint;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        maxPickUps = FindObjectsOfType<PickUp>().Length;
        lives = 3;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        respawnPoint = transform.position;
        pickUp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUp >= maxPickUps)
        {
            gameManager.GetComponent<GameManager>().LoadGameWon();
        }
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
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            gameManager.GetComponent<GameManager>().LoadGameOver();
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
        rb.isKinematic = true;
        yield return new WaitForSeconds(respawnDelay);
        transform.position = respawnPoint;
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        rb.isKinematic = false;
    }
}
