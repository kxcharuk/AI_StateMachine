using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideInstructions();
        }
    }

    public void DisplayInstructions()
    {
        instructions.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }
}
