using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    // --- Game Stuff ---
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
    public Text CubesPassedText;
    public Text MaxCubesPassedText;
    public Text HiScoreText;
    public int MaxCubesPassed = 3;
    public int CurrentCubesPassed = 0;
    public int HighScore = 0;

    private bool stopCreating = false;

    // --- UI Stuff ---    
    public enum GameScreens { MAIN_MENU = 0, GAME, END_SCREEN };
    public GameScreens current_screen = GameScreens.MAIN_MENU;
    public GameObject WinTxtObj;
    public GameObject ScoreTxtObj;
    public GameObject PointsTxtObj;

    // --- UI ---
    public void ChangeToMainMenuScreen()
    {
        // --- Re-Activate EndScreen stuff so it can be used again ---
        WinTxtObj.SetActive(true);
        ScoreTxtObj.SetActive(true);
        PointsTxtObj.SetActive(true);

        // --- Deactivate EndScreen ---
        GameObject.Find("EndScreen").SetActive(false);

        // Show Main Menu Screen & its stuff
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.name == "StartScreen" || go.name == "MenuSabers")
                go.SetActive(true);
        }

        // --- Set Main Menu Screen Values ---
        current_screen = GameScreens.MAIN_MENU;
    }

    public void ChangeToGameScreen()
    {
        // Just in case, we activate too the end screen
        WinTxtObj.SetActive(true);
        ScoreTxtObj.SetActive(true);
        PointsTxtObj.SetActive(true);

        // --- "Change" Screens ---
        // Hide Main Menu Screen & show Game Screen & its stuff
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.name == "GameSabers" || go.name == "GameScreen")
                go.SetActive(true);
            if (go.name == "EndScreen" || go.name == "StartScreen" || go.name == "MenuSabers")
                go.SetActive(false);
        }

        // --- Set Game Values ---
        current_screen = GameScreens.GAME;
        stopCreating = false;
        CurrentCubesPassed = 0;
        TimeController = 0.0f;
        Score = 0;
        TexttoChange.text = Score.ToString();
        CubesPassedText.text = CurrentCubesPassed.ToString();
    }

    public void ClearSceneCubes()
    {
        for(int i = CubeList.Count-1; i >= 0; i--)
            DestroyCube(CubeList[i].gameObject);

        CubeList.Clear();
    }

    public void ChangeToEndScreen()
    {
        // --- Destroy all cubes ---
        ClearSceneCubes();

        // --- "Change" Screens ---
        // Hide Game Screen & its stuff
        GameObject.Find("GameScreen").SetActive(false);
        GameObject.Find("GameSabers").SetActive(false);

        // --- Show End Screen ---
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.name == "EndScreen" || go.name == "MenuSabers")
                go.SetActive(true);
        }

        // --- Show Win or Lose ---
        ScoreTxtObj.SetActive(true);
        ScoreTxtObj.GetComponent<Text>().text = TexttoChange.text;
        PointsTxtObj.SetActive(true);
        WinTxtObj.SetActive(true);

        if (Score > HighScore)
        {
            HighScore = Score;
            HiScoreText.text = TexttoChange.text;
        }

        // --- Set End Screen Values ---
        current_screen = GameScreens.END_SCREEN;
    }

    // --- Generic Stuff ---
    // Start is called before the first frame update
    void Start()
    {
        MaxCubesPassedText.text = MaxCubesPassed.ToString();
        TexttoChange.text = Score.ToString();
        CubesPassedText.text = CurrentCubesPassed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (current_screen == GameScreens.GAME)
        {
            if (TimeController >= time_to_Spawn && !stopCreating)
            {
                CubeList.Add(SpawnCube());
                TimeController = 0.0f;
            }

            TimeController += Time.deltaTime;

            if(CurrentCubesPassed > MaxCubesPassed)
            {
                stopCreating = true;
                ChangeToEndScreen();
            }
        }
    }

    // Score
    public void AddPunctuation()
    {
        Score += CubeValue;
        TexttoChange.text = Score.ToString();
    }

    // --- Cubes Handling ---
    CubesBehaviour SpawnCube()
    {
        int rand = Random.Range(0, 100000000);
        int type = 0;
        if (rand % 2 == 0)
            type = 1;

        CubesBehaviour.Direction direct = (CubesBehaviour.Direction)Random.Range(0, 5);
        Vector3 Area = new Vector3(Random.Range(-Max_Area_To_Draw.x, Max_Area_To_Draw.x), Random.Range(-Max_Area_To_Draw.y, Max_Area_To_Draw.y), 0);
        GameObject CubeTrans = Instantiate(Cubes_GO[type], Ar_Camera.transform.position + Ar_Camera.transform.forward * Distance+Area, Ar_Camera.transform.rotation);
        
        CubesBehaviour cubeBeh = CubeTrans.GetComponent<CubesBehaviour>();
        ChangeDirection(direct, CubeTrans);
        cubeBeh.direction = direct;
        cubeBeh.ID = rand;

        if (type == 1)
            cubeBeh.type = CubesBehaviour.CubeType.Red;
        else
            cubeBeh.type = CubesBehaviour.CubeType.Blue;

        cubeBeh.Setup(CubeTrans);
        return cubeBeh;
    }

    public void DestroyCube(GameObject toDestroy)
    {
        CubeList.Remove(toDestroy.GetComponent<CubesBehaviour>());
        Destroy(toDestroy);
    }

    // Direction Change
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
}
