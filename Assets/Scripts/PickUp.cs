using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    //private MeshRenderer meshRenderer;
    //private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        //boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddPickUp();
            //meshRenderer.enabled = false;
            //boxCollider.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
