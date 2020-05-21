using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Trough_Camera : MonoBehaviour
{
    public GameObject Cubes;
    public float Distance;
    public Vector2 Max_Area_To_Draw;
    private Vector3 DistancetoDraw = Vector3.zero;
    public float time_to_Spawn;
    private float TimeController = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        DistancetoDraw = transform.forward;
        DistancetoDraw.z += Distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeController >= time_to_Spawn)
        {
            Vector3 Area = new Vector3(Random.Range(-Max_Area_To_Draw.x, Max_Area_To_Draw.x), Random.Range(-Max_Area_To_Draw.y, Max_Area_To_Draw.y), 0);
            Transform.Instantiate(Cubes, transform.position + transform.forward * Distance + Area, transform.rotation);
            TimeController = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            
        }
        TimeController += Time.deltaTime;
    }
}
