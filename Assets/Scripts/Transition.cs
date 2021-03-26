using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    [SerializeField] private GameObject gameManager;
    [SerializeField] private float transitionTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunSplashScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RunSplashScreen()
    {
        yield return new WaitForSeconds(transitionTimer);
        gameManager.GetComponent<GameManager>().LoadMainMenu();
    }
}
