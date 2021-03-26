using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Vector3 direction;
    private float horizontal;
    private float vertical;

    [SerializeField] private float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        //speed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        direction = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
