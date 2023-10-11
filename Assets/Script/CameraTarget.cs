using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    
    public Transform target;
    [SerializeField]
    public float offset = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + target.forward * offset;
        transform.rotation = new Quaternion(0.0f, target.rotation.y, 0.0f, target.rotation.w);
    }
}
