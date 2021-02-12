using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] buttons = new GameObject[6];
    public GameObject[] wheels = new GameObject[5];

    public ScenarioScript scenario;

    private List<GameObject[]> stages;

    private static List<GameObject> playerActions;

    private GameObject nextAction;
    private int currentStage = 0;
    private int currentAction = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        stages = new List<GameObject[]>();
        playerActions = new List<GameObject>();
        List<InteractableObject> listOfActions = new List<InteractableObject>();
        foreach (var e in scenario.Actions)
            listOfActions.Add(e);

        for (var i = 0; i < scenario.stages.Length; i++)
        {
            var stage = new GameObject[scenario.stages[i]];
            
            for(var j = 0;j< scenario.stages[i]; j++)
            {
                if (listOfActions[j].isButton)
                {
                    stage[j] = buttons[listOfActions[j].id];
                }
                else
                {
                    stage[j] = wheels[listOfActions[j].id];
                }
            }
            listOfActions.RemoveRange(0, scenario.stages[i]);
            stages.Add(stage);

        }
        
        foreach(var e in stages)
        {
            Debug.Log("Stage Started");
            foreach(var b in e)
            {
                Debug.Log("Action " + b.name);
            }
            Debug.Log("Stage Finished");
        }

        nextAction = stages[currentStage][currentAction];

    }

    

    // Update is called once per frame
    void Update()
    {


        if (currentAction >= stages[currentStage].Length)
        {
            currentStage++;
            currentAction = 0;
        }

        if (currentStage > scenario.stages.Length)
            Debug.Log("YOU WIN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
    public void MakeAction(GameObject actionObject)
    {
        playerActions.Add(actionObject);
        Debug.Log("Action performed on " + actionObject.name);
        if (actionObject == nextAction)
        {
            Debug.Log("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            currentAction++;
            nextAction = stages[currentStage][currentAction];
        }
        else
        {
            Debug.Log("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
        }
    }
}
