using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Breaker : MonoBehaviour
{
    public CubesBehaviour.CubeType Type;
    gameManager GM;
    AudioSource boxHit;
    AudioSource bladeHit;
    AudioSource bladeWrongHit;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("gameManager").GetComponent<gameManager>();
        boxHit = GameObject.Find("BoxHitSound").GetComponent<AudioSource>();
        bladeWrongHit = GameObject.Find("WrongHit").GetComponent<AudioSource>();

        if (Type == CubesBehaviour.CubeType.Red)
            bladeHit = GameObject.Find("RedBladeHit").GetComponent<AudioSource>();
        else
            bladeHit = GameObject.Find("BlueBladeHit").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    bool CheckDirection(GameObject Coll,GameObject Parent)
    {
        bool ret = false;
        CubesBehaviour.Direction Dir = Parent.GetComponent<CubesBehaviour>().direction;


        if (Coll.GetComponent<Collider_Dir>().Direction == Dir)
            ret = true;

        return ret;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject Parent = other.GetComponent<Collider_Dir>().Parent;
        //if (CheckDirection(other.gameObject, Parent))
        //{

        if (Type == Parent.GetComponent<CubesBehaviour>().type)
        {
            bladeHit.PlayOneShot(bladeHit.clip);
            boxHit.PlayOneShot(boxHit.clip);
            GM.DestroyCube(Parent);
            GM.AddPunctuation();
        }
        else
            bladeWrongHit.PlayOneShot(bladeWrongHit.clip);
        //}
    }
}
