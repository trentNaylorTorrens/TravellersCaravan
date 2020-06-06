using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public int totalMatchesRequired;//Set this first.
    public int currentMatches;
    //Master
    public GameObject allBoxesMaster;
    public GameObject allSpawnPosMaster;
    public List<GameObject> completeItemRepo;
    public List<GameObject> currentItemsInUse;

    //Reference
    public List<Transform> allBoxes = new List<Transform>();
    public List<Transform> allSpawnPoints = new List<Transform>();
    public List<GameObject> allItemsInBoxes = new List<GameObject>();

    //Gameplay
    public bool playerCanInput = true;
    public Item firstItem;
    public Item secondItem;

    public int boxSelection = 0; //Are we selection the first of second box.
    
  

    // Start is called before the first frame update
    void Start()
    {
        //Item Setup;
        SetupItems();
        //Setup Level Boxes
        SetupBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCanInput)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(myRay.origin,myRay.direction * 1000,Color.red, 100);
                RaycastHit hitinfo;
                if(Physics.Raycast(myRay,out hitinfo, 1000))
                {
                    if(hitinfo.transform.tag == "Box")
                    {
                        if(boxSelection == 0) //We need to select first box
                        {
                            boxSelection = 1; //First box clicked.
                            int tempI = FindBoxInSet(hitinfo.transform.parent);
                            firstItem = allItemsInBoxes[tempI].GetComponent<Item>();
                            hitinfo.transform.parent.GetComponent<Animator>().SetTrigger("UncoverBox");
                        }
                        else if(boxSelection == 1)
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

    int FindBoxInSet(Transform obj)
    {
        return allBoxes.IndexOf(obj);
    }

    IEnumerator CheckBoxesSequence()
    {
        //Check for Match

        playerCanInput = false;

        bool isMatch = false;
        if(firstItem.itemID == secondItem.itemID)
        {
            isMatch = true; //isMatch Event
        }
        if(isMatch) //Match!
        {
            EventManager.instance.PatternMatch();
        }
        else //not a match
        {
            EventManager.instance.PatternMisMatch();
            yield return new WaitForSeconds(2);
            //Animate
            allBoxes[firstItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("CoverBox");
            allBoxes[secondItem.currentPosition].GetComponentInChildren<Animator>().SetTrigger("CoverBox");
        }
        //Reset
        boxSelection = 0;
        yield return new WaitForSeconds(1);
        playerCanInput = true;
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
      
        allSpawnPosMaster.GetComponentsInChildren<Transform>(false, allSpawnPoints);
        allSpawnPoints.RemoveAt(0);

        for (int i = 0; i < allBoxes.Count; i++)
        {
            allSpawnPoints[i].position = allBoxes[i].position;
        }

        //Insert random spawner of items here.
        int counter = 2;

        foreach(GameObject ip in currentItemsInUse)
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
        if(currentMatches == totalMatchesRequired)
        {
            EventManager.instance.GameOver(true);
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
