using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmpty : MonoBehaviour
{
    [SerializeField] public GameObject playerPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.transform.position + new Vector3(0, -3, 0);
    }
}
