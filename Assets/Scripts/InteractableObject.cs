using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableObject
{
    public bool isButton; //Проверка на кнопку
    public int id; //Идентификатор объекта
    public GameObject objectFromScene; //Ссылка на объект
}
