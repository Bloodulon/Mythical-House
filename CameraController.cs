using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Скорость движения персонажа
    public float gravity = -9.81f; // Гравитация (можно адаптировать)

    private CharacterController controller;
    private Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        // Получаем ввод от игрока только по оси "Vertical" (вперед/назад)
        float verticalInput = Input.GetAxis("Vertical");

        // Вычисляем направление движения (только вперед)
        moveDirection = transform.forward * verticalInput;

        // Проверка на то, находится ли персонаж на земле
        if (controller.isGrounded)
        {
            // Если на земле, сбрасываем вертикальную скорость
            moveDirection.y = 0f;
        }

        // Применяем гравитацию
        moveDirection.y += gravity * Time.deltaTime; // Убедитесь, что g отрицательная для падения вниз

        // Двигаем персонажа
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
