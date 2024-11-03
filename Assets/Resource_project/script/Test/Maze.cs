using UnityEngine;
using UnityEngine.EventSystems;

public class Maze : MonoBehaviour, IPointerEnterHandler
{
    private Item mazeKey;

    void Start()
    {
        mazeKey = GetComponent<Item>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��즲����i�J�ؼаϰ��Ĳ�o
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            Debug.Log("Dropped object");
            // �b�o�̳B�z���骺�ƥ��޿�
            mazeKey.Interact();
        }
        else
        {
            Debug.Log("Dropped object is null");
        }
    }
}
