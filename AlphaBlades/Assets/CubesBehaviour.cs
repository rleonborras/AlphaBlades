using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesBehaviour : MonoBehaviour
{

    GameObject m_gameObject;
    GameObject m_Right_Hand;

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
   
    void Start()
    {
        
    }
    public void Setup(GameObject ownGameObject)
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        Move();
    }

    void Move()
    {
        transform.Translate(new Vector3(0, 0, -Speed));
    }

}
