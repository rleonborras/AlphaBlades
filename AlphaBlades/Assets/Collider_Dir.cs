using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Dir : MonoBehaviour
{
    // Start is called before the first frame update
    public CubesBehaviour.CubeType type;
    public CubesBehaviour.Direction Direction;
    public GameObject Parent;

    void Start()
    {
        Parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
