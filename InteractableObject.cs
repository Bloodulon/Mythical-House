using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Здесь больше ничего нет, если вам не нужно хранить информацию о объекте
    // или реализовать более сложные взаимодействия
    public void Interact()
    {
        Debug.Log("Взаимодействие с объектом: " + gameObject.tag);
        // Здесь можно добавить логику, которая будет выполняться при взаимодействии с объектом
    }
}