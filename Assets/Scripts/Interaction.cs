using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool isClickable; //Проверка для кнопок
    public bool isRotatable; //Проверка для вентилей
    public float rotationSpeed = 3f; //Скорость поворота вентилей

    private float rotationX = 0f; //Расчет поворота
    private Color defaultColor; //Стартовый цвет объекта
    private Color emissionColor; //Цвет для подсветки
     
    void Start()
    {
        defaultColor = gameObject.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        emissionColor = new Color(70, 70, 70);
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",emissionColor);  
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultColor);
    }

    private void OnMouseDown()
    {
        if (isClickable)
            ScenarioManager.instance.MakeAction(gameObject);
    }

    private void OnMouseDrag()
    {
        if (isRotatable)
        {
            if (Input.GetKey(KeyCode.Q))
                rotationX -= rotationSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.E))
                rotationX += rotationSpeed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }

    private void OnMouseUp()
    {
        if(isRotatable)
            ScenarioManager.instance.MakeAction(gameObject);
    }
}
