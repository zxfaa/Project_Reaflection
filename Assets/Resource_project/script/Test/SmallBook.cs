using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SmallBook : MonoBehaviour
{
    [Header("��o�D��C��")]
    public ItemData itemData;
    public int[] itemIndices;

    private string requireItem = "�ȱ�";
    private Button button;
    private DragAndDrop dragAndDrop;
    private InventorySystem inventorySystem;
    private UnityAction action;

    private void Start()
    {
        button = GetComponent<Button>();
        dragAndDrop = FindObjectOfType<DragAndDrop>();
        inventorySystem = FindObjectOfType<InventorySystem>();
        action = () => UseItem(requireItem);
        button.onClick.AddListener(action);
    }
    public void UseItem(string itemName)
    {
        if (inventorySystem.isDragging)
        {
            if (dragAndDrop.item.itemName == itemName)
            {
                dragAndDrop.StopDragItem();
                if (itemName == "�ȱ�")
                {
                    requireItem = "�]��";
                    ChangeListener();
                    PaperUsed();
                }
                else if (itemName == "�]��")
                {
                    requireItem = null;
                    ChangeListener();
                    PenUsed();
                }
            } 
        }
    }

    private void ChangeListener()
    {
        button.onClick.RemoveAllListeners();
        action = () => UseItem(requireItem);
        button.onClick.AddListener(action);
    }

    private void PaperUsed()
    {
        Debug.Log("Use �ȱ�");
    }

    private void PenUsed()
    {
        Debug.Log("Use �]��");
        PickUp();
    }

    private void PickUp()
    {
        foreach (int index in itemIndices)
        {
            if (index >= 0 && index < itemData.items.Count)
            {
                // �u���B�zEncyclopediaUI
                FindObjectOfType<InventorySystem>().PickUp(itemData.items[index]);

                // �B�z�����sIsInteration
                StartCoroutine(UpdateItemInteraction(index));
            }
            else
            {
                Debug.LogError("Item index out of range in PickUpItem");
            }
        }
        // ��s��ŲUI
        if (EncyclopediaUI.Instance != null)
        {
            Debug.Log("InteractionHandler: EncyclopediaUI.Instance found, calling UpdateUI");
            EncyclopediaUI.Instance.UpdateUI();
        }
        else
        {
            Debug.LogError("InteractionHandler: EncyclopediaUI.Instance is null");
        }
        StartCoroutine(WaitForDialogToCloseAndPickUpItem());
    }

    private void UpdateUI()
    {
        // ��s��ŲUI
        if (EncyclopediaUI.Instance != null)
        {
            Debug.Log("InteractionHandler: EncyclopediaUI.Instance found, calling UpdateUI");
            EncyclopediaUI.Instance.UpdateUI();
        }
        else
        {
            Debug.LogError("InteractionHandler: EncyclopediaUI.Instance is null");
        }
        StartCoroutine(WaitForDialogToCloseAndPickUpItem());
    }

    private IEnumerator WaitForDialogToCloseAndPickUpItem()
    {
        // ���ݤ@�q�ɶ��A�H�T�O��ܮؤw�g�}�l���
        yield return new WaitForSeconds(0.1f); // �u�ȵ���

        // �ˬd��ܮجO�_�s�b
        while (GameObject.Find("DialogPanel"))
        {
            yield return null; // �C�V�ˬd�@��
        }

        // ��ܮخ�����A����߬B�ާ@        
        ImageDisplayManager.Instance.QueueImagesWithFade(itemData, itemIndices);
    }

    private IEnumerator UpdateItemInteraction(int index)
    {
        //�T�OEncyclopediaUI�B�z����
        yield return null;

        //����ç�sItemData
        ItemData.Item currentItem = itemData.items[index];
        currentItem.IsInteration = true;
        itemData.items[index] = currentItem;

        Debug.Log($"Item '{currentItem.itemName}' interaction updated.");
    }
}
