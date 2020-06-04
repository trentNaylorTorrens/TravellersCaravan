using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class GameManager : MonoBehaviour
{
    //Master
    public GameObject allBoxesMaster;
    public GameObject allSpawnPosMaster;
    public List<GameObject> itemRepo;

    //Reference
    public List<Transform> allBoxes = new List<Transform>();
    public List<Transform> allSpawnPoints = new List<Transform>();
    public List<GameObject> allItems = new List<GameObject>();

    //Gameplay
    public bool playerCanInput = true;
    public int boxSelectionOne = -1; //Default -1 = nothing.
    public int boxSelectionTwo = -1; //Default -1 = nothing.

    // Start is called before the first frame update
    void Start()
    {
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
                        if(boxSelectionOne < 0) //We need to select first box
                        {
                            boxSelectionOne = FindBoxInSet(hitinfo.transform.parent);
                            hitinfo.transform.parent.GetComponent<Animator>().SetTrigger("UncoverBox");
                        }
                        else if(boxSelectionTwo < 0)
                        {
                            boxSelectionTwo = FindBoxInSet(hitinfo.transform.parent);
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
        playerCanInput = false;
        yield return new WaitForSeconds(2);
        //Animate
        allBoxes[boxSelectionOne].GetComponentInChildren<Animator>().SetTrigger("CoverBox");
        allBoxes[boxSelectionTwo].GetComponentInChildren<Animator>().SetTrigger("CoverBox");

        //Reset
        boxSelectionOne = -1;
        boxSelectionTwo = -1;

        yield return new WaitForSeconds(1);
        playerCanInput = true;
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
        int counter = 3;

        foreach(GameObject ip in itemRepo)
        {
            for (int i = 0; i < counter; i++)
            {
                GameObject go = Instantiate(ip) as GameObject;
                allItems.Add(go);
            }
        }

        allItems.Shuffle();

        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].transform.position = allSpawnPoints[i].position;
        }
        //Execute start sequence.
        //Allow player Input
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
