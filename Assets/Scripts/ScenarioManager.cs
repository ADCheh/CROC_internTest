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

    public ScenarioScript scenario; //Объект сценария
    public int maximumMistakes = 3; //Максимально число ошибок
    public AudioSource failSound;
    public AudioSource successSound;

    private List<InteractableObject[]> stages; //Лист этапов с действиями
    private List<GameObject> playerActions; //Список действий игрока
    private InteractableObject nextAction; //Следующее необходимое действие
    private int currentStage = 0; //Текущий этап
    private int currentAction = 0; //Текущее действие на этапе
    private int mistakesCounter; // Счетчик ошибок

    void Start()
    {
        Time.timeScale = 0f;

        stages = new List<InteractableObject[]>();
        playerActions = new List<GameObject>();
        List<InteractableObject> listOfActions = new List<InteractableObject>();

        foreach (var e in scenario.Actions)
            listOfActions.Add(e);

        for (var i = 0; i < scenario.stages.Length; i++) //Инициализация этапов
        {
            var stage = new InteractableObject[scenario.stages[i]];
            for (var j = 0; j < scenario.stages[i]; j++)
            {
                if (listOfActions[j].isButton)
                    stage[j] = listOfActions[j];
                else
                    stage[j] = listOfActions[j];
            }
            listOfActions.RemoveRange(0, scenario.stages[i]);
            stages.Add(stage);
        }
        nextAction = stages[currentStage][currentAction];    
    }

    public void MakeAction(GameObject actionObject) //Обработка действия
    {
        playerActions.Add(actionObject);
        if (actionObject.name == nextAction.objectFromScene.name)
        {
            if (currentAction == scenario.stages[currentStage] - 1)
            {
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
            {
                currentAction++;
                successSound.Play();
            } 
            nextAction = stages[currentStage][currentAction];
        }
        else
        {
            failSound.Play();
            mistakesCounter++;
            if (mistakesCounter == maximumMistakes)
                InterfaceManager.instance.Failure();
        }
    }

    public InteractableObject GetNextAction() //Доступ к следующему действию
    {
        return nextAction;
    }

    public int[] GetCurrentState() //Доступ к номеру текущего этапа и действия
    {
        return new int[2] { currentStage, currentAction };
    }

    public int GetMistakes() //Доступ к счетчику ошибок
    {
        return mistakesCounter;
    }

    public List<InteractableObject[]> GetStages() //Доступ к списку этапов и действий
    {
        return stages;
    }



}
