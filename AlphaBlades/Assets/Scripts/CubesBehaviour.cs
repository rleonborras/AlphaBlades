using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesBehaviour : MonoBehaviour
{

    GameObject m_gameObject;
    GameObject m_Right_Hand;
    gameManager GM;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public enum CubeType
    {
        Red,
        Blue
    }

    public int ID;
    // Start is called before the first frame update
    public float Speed;
    public CubeType type;
    public Direction direction;
    public Vector3 spawnPos;

    void Start()
    {
        
    }
    public void Setup(gameManager gm)
    {
        GM = gm;
    }
    // Update is called once per frame
    void Update()
    {

        Move();
    }

    void Move()
    {
        //transform.Translate(new Vector3(0, 0, -Speed));
        transform.position = Vector3.Lerp(transform.position, spawnPos * GM.endPosMultiplier, Time.deltaTime*Speed*6.0f);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * GM.endScaleMultiplier, Time.deltaTime);
    }
}
