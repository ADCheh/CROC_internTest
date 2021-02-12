using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario",menuName = "Scenario")]
public class ScenarioScript : ScriptableObject
{
    public string title; //Название
    public string description; //Описание
    public InteractableObject[] Actions; //Набор действий в сценарии
    public int[] stages;  //Массив с информацией об этапах: размер массива - количество этапов, элементы массива - количество действий в соответствующем этапе
}
