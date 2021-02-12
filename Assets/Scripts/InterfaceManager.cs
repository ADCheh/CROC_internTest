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

    public GameObject tutorialPanel; //Начальная панель с обучением
    public GameObject instructionsPanel; //Панель с инструкциями к текущему действию
    public GameObject stageDescription; //Панель с описанием нового этапа
    public GameObject failurPanel; //Панель проигрыша
    public GameObject mistakesPanel; //Панель со счестчиком ошибок
    public GameObject сursorPointer; //Точка курсора в центре камеры
    public Text mistakesText; //Текст счетчика ошибок
    public Text instructionsText; //Текст инструкций к текущему действию
    public Text stageInfoText; //Текст с инструкцией к новому этапу

    private InteractableObject nextAction; //Информация о следующем действии
    private int[] currentState; //Информация о текущем этапе и действии

    void Update()
    {
        TextUpdate();
    }

    public void StartScenarion() //Запуск сценария
    {
        tutorialPanel.SetActive(false);
        stageDescription.SetActive(false);
        StageInfo();   
    }
    public void CloseDescription() //Запуск после окна описания этапа
    {
        stageDescription.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        instructionsPanel.SetActive(true);
        mistakesPanel.SetActive(true);
        сursorPointer.SetActive(true);
    }

    Dictionary<string, string> colors = new Dictionary<string, string> //Словарь для правильного отображения подсказок
    {
        {"Red","красного" },
        {"Green","зеленого" },
        {"Blue","синего" },
        {"Yellow","желтого" },
        {"Orange","оранжевого" },
        {"Violet","фиолетового" },
    };

    void TextUpdate() //Обновление текста в окнах
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

        mistakesText.text = "Ошибок допущено: " + ScenarioManager.instance.GetMistakes();
    }

    public void StageInfo() //Текст на начале нового этапа
    {
        instructionsPanel.SetActive(false);
        mistakesPanel.SetActive(false);
        сursorPointer.SetActive(false);
        currentState = ScenarioManager.instance.GetCurrentState();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        stageDescription.SetActive(true);

        var actionsOnStage = ScenarioManager.instance.GetStages()[currentState[0]];
        var stringListOfActions = new List<string>();
        foreach(var e in actionsOnStage)
        {
            if (e.isButton)
                stringListOfActions.Add("Нажать на кнопку " + colors[e.objectFromScene.GetComponent<Renderer>().sharedMaterial.name] + " цвета\n\n");
            else
                stringListOfActions.Add("Повернуть вентиль " + colors[e.objectFromScene.GetComponent<Renderer>().sharedMaterial.name] + " цвета\n\n");
        }

        string instructionsText = string.Format(@"Этап №{0}
На данном этапе Ваша задача заключается в совершении последовательности простых действий, описание действия, которое необходимо выполнить, Вы сможете увидеть в верхнем левом углу экрана.
Вам необходимо выполнить следующую последовательность действий:

",(currentState[0]+1));
        stageInfoText.text = instructionsText;
        foreach (var e in stringListOfActions)
            stageInfoText.text += e;
        stageInfoText.text += "Удачи!";
    }

    public void Failure() //Окно проигрыша
    {
        instructionsPanel.SetActive(false);
        сursorPointer.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        failurPanel.SetActive(true);
    }
}
