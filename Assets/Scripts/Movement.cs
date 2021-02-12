using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f; //Скорость движения
    public float gravity = -9.81f; // Сила гравитации

    public Transform groundCheck; //Объект для проверки нахождения на земле
    public float groundDistance = 0.4f; //Расстояние проверки
    public LayerMask groundMask; //Маска для элементов поверхности

    Vector3 velocity; //Скорость перемещения
    bool isGrounded; //Проверка нахождения на земле

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x + transform.forward*z;

        controller.Move(move*speed*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
