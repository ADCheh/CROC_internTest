using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text instructionsText;
    public int maximumMistakes = 3;

    private List<GameObject[]> stages;
    private List<InteractableObject[]> stages_2;

    private static List<GameObject> playerActions;

    private InteractableObject nextAction;
    private int currentStage = 0;
    private int currentAction = 0;
    private int mistakesCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        stages = new List<GameObject[]>();

        stages_2 = new List<InteractableObject[]>();

        playerActions = new List<GameObject>();
        List<InteractableObject> listOfActions = new List<InteractableObject>();
        foreach (var e in scenario.Actions)
            listOfActions.Add(e);


        for (var i = 0; i < scenario.stages.Length; i++)
        {
            var stage = new InteractableObject[scenario.stages[i]];

            for (var j = 0; j < scenario.stages[i]; j++)
            {
                if (listOfActions[j].isButton)
                {
                    stage[j] = listOfActions[j];
                }
                else
                {
                    stage[j] = listOfActions[j];
                }
            }
            listOfActions.RemoveRange(0, scenario.stages[i]);
            stages_2.Add(stage);

        }

        //for (var i = 0; i < scenario.stages.Length; i++)
        //{
        //    var stage = new GameObject[scenario.stages[i]];
            
        //    for(var j = 0;j< scenario.stages[i]; j++)
        //    {
        //        if (listOfActions[j].isButton)
        //        {
        //            stage[j] = buttons[listOfActions[j].id];
        //        }
        //        else
        //        {
        //            stage[j] = wheels[listOfActions[j].id];
        //        }
        //    }
        //    listOfActions.RemoveRange(0, scenario.stages[i]);
        //    stages.Add(stage);

        //}
        
        //foreach(var e in stages)
        //{
        //    Debug.Log("Stage Started");
        //    foreach(var b in e)
        //    {
        //        Debug.Log("Action " + b.name);
        //    }
        //    Debug.Log("Stage Finished");
        //}

        //nextAction = stages[currentStage][currentAction];
        nextAction = stages_2[currentStage][currentAction];
        Debug.Log(nextAction.objectFromScene.name);
        
    }

    

    // Update is called once per frame
    void Update()
    {

        //TextUpdate();
        //if (currentAction >= stages_2[currentStage].Length)
        //{
        //    currentStage++;
        //    currentAction = 0;
        //}

        //if (currentStage > scenario.stages.Length)
        //    Debug.Log("YOU WIN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
    public void MakeAction(GameObject actionObject)
    {
        string objectType;
        if (nextAction.isButton)
            objectType = "кнопкой";
        else
            objectType = "вентилем";
        playerActions.Add(actionObject);
        Debug.Log("Action performed on " + actionObject.name);
        if (actionObject.name == nextAction.objectFromScene.name)
        {
            Debug.Log("Вы выполнили действие с " + objectType + "№" + nextAction.id);
            if (currentAction == scenario.stages[currentStage] - 1)
            {
                Debug.Log("Вы завершили Этап №" + (currentStage + 1));
                currentStage++;
                
                currentAction = 0;
                if (currentStage == scenario.stages.Length)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    gameObject.GetComponent<SceneManager>().NextScene();
                    return;
                }
                InterfaceManager.instance.StageInfo();
            }
            else
                currentAction++;
            //nextAction = stages[currentStage][currentAction];
            nextAction = stages_2[currentStage][currentAction];
        }
        else
        {
            mistakesCounter++;
            if (mistakesCounter == maximumMistakes)
                InterfaceManager.instance.Failure();
            Debug.Log("CRITICAL ERROR RYAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }

    public InteractableObject GetNextAction()
    {
        return nextAction;
    }

    public int[] GetCurrentState()
    {
        return new int[2] { currentStage, currentAction };
    }

    public int GetMistakes()
    {
        return mistakesCounter;
    }


    
}
