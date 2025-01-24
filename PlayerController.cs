using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector3 MousePositionViewport = Vector3.zero;
    private Quaternion DesiredRotation = Quaternion.identity; // Установлено в Quaternion.identity по умолчанию
    private float RotationSpeed = 9f; // Скорость поворота
    private float VerticalRotationLimit = 45f; // Ограничение по вертикали

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        var center = Screen.safeArea.center;
        Mouse.current.WarpCursorPosition(center);
    }

    void Update()
    {
        // Получаем позицию мыши в координатах экрана и конвертируем в Viewport
        MousePositionViewport = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        // Проверяем, находится ли мышь близко к краю экрана
        if (MousePositionViewport.x < 0.01f || MousePositionViewport.x > 0.99f || MousePositionViewport.y < 0.01f || MousePositionViewport.y > 0.99f)
        {
            // Если мышка близко к краю, телепортируем ее в центр
            var center = Screen.safeArea.center;
            Mouse.current.WarpCursorPosition(center);
        }

        // Определяем желаемую ориентацию по осям Y и X
        float desiredYRotation = 0f; // Поворот по оси Y
        float desiredXRotation; // Поворот по оси X

        // Логика для поворота по оси Y в зависимости от горизонтального положения мыши
        if (MousePositionViewport.x >= 0.75f)
        {
            desiredYRotation = 270f;  // Поворот на 270 градусов по оси Y (вправо)
        }
        else if (MousePositionViewport.x < 0.75f && MousePositionViewport.x >= 0.5f)
        {
            desiredYRotation = 180f;  // Поворот на 180 градусов по оси Y
        }
        else if (MousePositionViewport.x < 0.5f && MousePositionViewport.x >= 0.25f)
        {
            desiredYRotation = 90f;   // Поворот на 90 градусов по оси Y (влево)
        }

        // Логика для поворота по оси X в зависимости от вертикального положения мыши
        if (MousePositionViewport.y >= 0.75f)
        {
            desiredXRotation = VerticalRotationLimit;  // Поворот вверх на 45 градусов
        }
        else if (MousePositionViewport.y < 0.25f)
        {
            desiredXRotation = -VerticalRotationLimit; // Поворот вниз на 45 градусов
        }
        else
        {
            desiredXRotation = 0f; // Если мышь находится в среднем диапазоне, не меняем поворот
        }

        // Создаем желаемую ориентацию
        DesiredRotation = Quaternion.Euler(-desiredXRotation, desiredYRotation, 0); // Инвертируем значений для камеры

        // Плавный переход между текущей и желаемой ориентацией
        transform.rotation = Quaternion.Slerp(transform.rotation, DesiredRotation, RotationSpeed * Time.deltaTime);
    }
}
