using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = targetTransform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothTime * Time.deltaTime);
        transform.position = smoothPos;
    }
}
