using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Camera Ar_Camera;
    public GameObject Cubes;
    public float Distance;
    public Vector2 Max_Area_To_Draw;
    public GameObject[] Cubes_GO = new GameObject[2];
    public float time_to_Spawn;
    public List<CubesBehaviour> CubeList = new List<CubesBehaviour>();
    private float TimeController = 0.0f;
    public int Score = 0;
    public int CubeValue;
    public Text TexttoChange;
    
    
    // Start is called before the first frame update
    void Start()
    {
        TexttoChange.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()

    {
        if (TimeController >= time_to_Spawn)
        {
            CubeList.Add(SpawnCube());

            TimeController = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
        TimeController += Time.deltaTime;
    }

    CubesBehaviour SpawnCube()
    {
        int rand = Random.Range(0, 100000000);
        int type = 0;
        if (rand % 2 == 0)
        {
            type = 1;
        }
        CubesBehaviour.Direction direct = (CubesBehaviour.Direction)Random.Range(0, 5);
        Vector3 Area = new Vector3(Random.Range(-Max_Area_To_Draw.x, Max_Area_To_Draw.x), Random.Range(-Max_Area_To_Draw.y, Max_Area_To_Draw.y), 0);
        GameObject CubeTrans = Instantiate(Cubes_GO[type], Ar_Camera.transform.position + Ar_Camera.transform.forward * Distance+Area, Ar_Camera.transform.rotation);
        CubesBehaviour cubeBeh = CubeTrans.GetComponent<CubesBehaviour>();
        ChangeDirection(direct, CubeTrans);
        cubeBeh.direction = direct;
        cubeBeh.ID = rand;

        if (type == 1)
        {
            cubeBeh.type = CubesBehaviour.CubeType.Red;
        }
        else
        {
            cubeBeh.type = CubesBehaviour.CubeType.Blue;

        }

        cubeBeh.Setup(CubeTrans);

        return cubeBeh;
    }

    public void DestroyCube(GameObject toDestroy)
    {
        CubeList.Remove(toDestroy.GetComponent<CubesBehaviour>());
        Destroy(toDestroy);
    }
    private void ChangeDirection(CubesBehaviour.Direction Dir,GameObject Change)
    {
        GameObject child =  Change.transform.GetChild(1).gameObject;
        float Rotate = 0;
        switch (Dir)
        {
            case CubesBehaviour.Direction.Up:
                Rotate = -270;
                break;
            case CubesBehaviour.Direction.Down:
                Rotate = -90;
                break;
            case CubesBehaviour.Direction.Left:
                break;
            case CubesBehaviour.Direction.Right:
                Rotate = -180;
                break;
            default:
                break;
        }

        child.transform.Rotate(0, 0, Rotate);
    }
    public void AddPunctuation()
    {
        Score += CubeValue;
        TexttoChange.text = Score.ToString();
    }
}
