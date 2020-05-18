using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    // Start is called before the first frame update
    gameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
      
       GM.DestroyCube(other.transform.parent.gameObject);
   
    }
}
