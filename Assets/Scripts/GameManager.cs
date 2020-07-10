using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalMatchesRequired;//Set this first.
    public int currentMatches;
    //Master
    /// <summary>
    /// Parent object that holds the boxes
    /// </summary>
    public GameObject allBoxesMaster;
    /// <summary>
    /// Parent object that holds all the spawn points
    /// </summary>
    public GameObject allSpawnPosMaster;
    /// <summary>
    /// List of all the items available to spawn
    /// </summary>
    public List<GameObject> completeItemRepo;
    /// <summary>
    /// List of the current items spawned in the level
    /// </summary>
    public List<GameObject> currentItemsInUse;

    //Reference
    public List<Transform> allBoxes = new List<Transform>();
    public List<Transform> allSpawnPoints = new List<Transform>();
    public List<GameObject> allItemsInBoxes = new List<GameObject>();

    //Gameplay
    public bool playerCanInput = true;
    public Item firstItem; //First Item to be Selected for a match
    public Item secondItem; //Second Item to be selectedfor a match
    int boxSelection = 0; //Are we selecting the first or second box?

    //Could move this out to a scriptable object eventually.
    public List<Level> allLevels = new List<Level>();
    public Level currentLevel;

    private float levelTimer;
    public float LevelTimer { get => levelTimer; }

    private void Awake()
    {
        instance = this;

        //Put some error checking in here for Unity assignments.
        if (allBoxesMaster == null)
            Debug.LogError("Assign Parent object that holds Boxes"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        //Level initialisation
        StartCoroutine(LevelInit());
        //Item Setup;
        SetupItems();
        //Setup Level Boxes
        SetupBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        //Level state control
        if (currentLevel != null)
        {
            switch (currentLevel.currentLevelState)
            {
                case Level.LevelState.Pregame:
                    break;
                case Level.LevelState.Playing:
                    levelTimer -= Time.deltaTime;
                    if (levelTimer <= 0)
                        currentLevel.currentLevelState = Level.LevelState.GameOver;
                    break;
                case Level.LevelState.GameOver:
                    playerCanInput = false;
                    if (currentMatches == totalMatchesRequired)
                    {
                        EventManager.instance.GameOver(true);
                    }
                    else EventManager.instance.GameOver(false);
                    break;
            }
            if (playerCanInput)
            {
                //Main raycaster for selecting boxes
                if (Input.GetMouseButtonDown(0))
                {
                    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(myRay.origin, myRay.direction * 1000, Color.red, 100);
                    RaycastHit hitinfo;
                    if (Physics.Raycast(myRay, out hitinfo, 1000))
                    {
                        if (hitinfo.transform.tag == "Box")
                        {
                            if (boxSelection == 0) //We need to select first box
                            {
                                boxSelection = 1; //First box clicked.
                                hitinfo.transform.GetComponent<Collider>().enabled = false;//Disable the collider so it cant be clicked again.
                                int tempI = FindBoxInSet(hitinfo.transform.parent);
                                firstItem = allItemsInBoxes[tempI].GetComponent<Item>();
                                hitinfo.transform.parent.GetComponent<Animator>().SetTrigger("UncoverBox");
                            }
                            else if (boxSelection == 1)
                            {
                                int tempI = FindBoxInSet(hitinfo.transform.parent);
                                secondItem = allItemsInBoxes[tempI].GetComponent<Item>();
                                hitinfo.transform.parent.GetComponent<Animator>().SetTrigger("UncoverBox");
                                StartCoroutine(CheckBoxesSequence());
                            }

                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// Helper function - finds a particular box in the list of all boxes
    /// </summary>
    /// <param name="obj">Box you are looking for.</param>
    /// <returns></returns>
    int FindBoxInSet(Transform obj)
    {
        return allBoxes.IndexOf(obj);
    }
    /// <summary>
    /// Logic for checking a match, generates event based on result.
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckBoxesSequence()
    {
        //Prevent player from inputting
        playerCanInput = false;
        //Check for Match
        if (firstItem.itemID == secondItem.itemID) //Match!
        {
            EventManager.instance.PatternMatch();
            allBoxes[firstItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("LockBox");
            allBoxes[secondItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("LockBox");
            yield return new WaitForSeconds(1);
        }
        else //not a match
        {
            EventManager.instance.PatternMisMatch();
            yield return new WaitForSeconds(1);
            //Animate
            allBoxes[firstItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("CoverBox");
            allBoxes[secondItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("CoverBox");
            //Add delay here for Transition
            yield return StartCoroutine(IsTransitionDone(allBoxes[firstItem.currentPosition].GetComponentInChildren<Animator>(), "Idle"));
            //Reenable colliders
            allBoxes[firstItem.currentPosition].transform.GetComponentInChildren<Collider>().enabled = true;//Enable the collider so it can be clicked again.
            allBoxes[secondItem.currentPosition].transform.GetComponentInChildren<Collider>().enabled = true;//Enable the collider so it can be clicked again.

        }
        //Reset
        boxSelection = 0;
        //Reset input
        playerCanInput = true;
    }

    IEnumerator LevelInit()
    {
        Level lv = Instantiate(allLevels[0]);
        currentLevel = lv;
        currentLevel.currentLevelState = Level.LevelState.Pregame;
        yield return new WaitForSeconds(2);//Yield a new coroutine here for doing dialogue etc
        currentLevel.currentLevelState = Level.LevelState.Playing;

        //Set level timer
        switch (currentLevel.currentLevelDifficulty)
        {
            case Level.LevelDifficulty.Tutorial:
                levelTimer = GlobalSettings.Instance.TutorialDifficultyTimer;
                break;
            case Level.LevelDifficulty.Easy:
                levelTimer = GlobalSettings.Instance.EasyDifficultyTimer;
                break;
            case Level.LevelDifficulty.Medium:
                levelTimer = GlobalSettings.Instance.MediumDifficultyTimer;
                break;
            case Level.LevelDifficulty.Hard:
                levelTimer = GlobalSettings.Instance.HardDifficultyTimer;
                break;
            case Level.LevelDifficulty.Nightmare:
                levelTimer = GlobalSettings.Instance.NightMareDifficultyTimer;
                break;
        }
    }
    void SetupItems()
    {
        //Spawn a set of items to use from a repository of items
        completeItemRepo.Shuffle();

        for (int i = 0; i < totalMatchesRequired; i++)
        {
            currentItemsInUse.Add(completeItemRepo[i]);
        }

        for (int i = 0; i < currentItemsInUse.Count; i++)
        {
            currentItemsInUse[i].GetComponent<Item>().itemID = i;
        }
    }

    void SetupBoxes()
    {
        //Get all the spawn points in the master and store them in the SpawnPoint List
        allSpawnPosMaster.GetComponentsInChildren<Transform>(false, allSpawnPoints);
        allSpawnPoints.RemoveAt(0);

        for (int i = 0; i < allBoxes.Count; i++)
        {
            allSpawnPoints[i].position = allBoxes[i].position;
        }

        //Insert random spawner of items here.
        int counter = 2;

        foreach (GameObject ip in currentItemsInUse)
        {
            for (int i = 0; i < counter; i++)
            {
                GameObject go = Instantiate(ip) as GameObject;
                allItemsInBoxes.Add(go);
            }
        }

        allItemsInBoxes.Shuffle();

        for (int i = 0; i < allItemsInBoxes.Count; i++)
        {
            allItemsInBoxes[i].transform.position = allSpawnPoints[i].position;
            allItemsInBoxes[i].GetComponent<Item>().currentPosition = i;
        }
        //Execute start sequence.
        //Allow player Input
    }

    void SelectionMatch()
    {
        currentMatches++;
        if (currentMatches == totalMatchesRequired)
        {
            currentLevel.currentLevelState = Level.LevelState.GameOver;
        }
    }

    private void OnEnable()
    {
        EventManager.OnPatternMatch += SelectionMatch;
    }

    private void OnDisable()
    {
        EventManager.OnPatternMatch -= SelectionMatch;
    }

    //HelperCoroutine for transition delays.
    IEnumerator IsTransitionDone(Animator animator, string animation)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animation) || animator.IsInTransition(0))
        {
            
            yield return 0;
        }
     
    }

}
/// <summary>
/// ///https://stackoverflow.com/questions/273313/randomize-a-listt
/// </summary>
static class MyExtension
{
    private static System.Random rng = new System.Random();


    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
