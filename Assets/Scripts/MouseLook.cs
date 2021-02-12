using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f; //Коэффициент чувствительности мыши
    public Transform playerBody; //Компонент Transform для вращениия

    float xRotation = 0f; //Переменная для расчета вращения

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
