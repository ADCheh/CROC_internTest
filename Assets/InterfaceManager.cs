using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject tutorialPanel;
    public GameObject instructionsPanel;
    public GameObject stageDescription;
    public Text instructionsText;
    public Text stageInfoText;

    private InteractableObject nextAction;
    private int[] currentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
    }

    public void StartScenarion()
    {
        tutorialPanel.SetActive(false);
        stageDescription.SetActive(false);

        StageInfo();
        
    }

    public void CloseDescription()
    {
        stageDescription.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        
        instructionsPanel.SetActive(true);
    }

    Dictionary<string, string> colors = new Dictionary<string, string>
    {
        {"Red","красного" },
        {"Green","зеленого" },
        {"Blue","синего" },
        {"Yellow","желтого" },
        {"Orange","оранжевого" },
        {"Violet","фиолетового" },
    };

    void TextUpdate()
    {
        currentState = ScenarioManager.instance.GetCurrentState();
        nextAction = ScenarioManager.instance.GetNextAction();
        string objectColor = colors[nextAction.objectFromScene.GetComponent<Renderer>().sharedMaterial.name];
        string objectType;
        string objectAction;
        if (nextAction.isButton)
        {
            objectType = "нажать на кнопку";
            objectAction = "нажатия наведите камеру на кнопку и нажмите ЛКМ";
        }
        else
        {
            objectType = "повернуть вентиль";
            objectAction = "поворота наведите камеру на вентиль, зажмите ЛКМ и удерживайте Q для поворота против часовойц стрелки или E для поворота по часовой стрелке";
        }            
        string text = string.Format(
            @" Этап № {0}
Задача № {1}
Вам необходимо {2} {3} цвета.
Для {4}", (currentState[0]+1), (currentState[1]+1), objectType, objectColor, objectAction);
        instructionsText.text = text;
    }

    public void StageInfo()
    {
        currentState = ScenarioManager.instance.GetCurrentState();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        stageDescription.SetActive(true);
        string instructionsText = string.Format(@"Этап №{0}
На данном этапе Ваша задача заключается в совершении последовательности простых действий, описание действия, которое необходимо выполнить, Вы сможете увидеть в верхнем левом углу экрана. 
Удачи!",(currentState[0]+1));
        stageInfoText.text = instructionsText; 
    }
}
