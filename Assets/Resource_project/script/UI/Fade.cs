using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject targetUI;  // �ݭn�T�Ϊ� UI ����
    public GameObject nextUI;

    // �T�� UI ����k
    public void DisableUI()
    {
        if (targetUI != null)
            targetUI.SetActive(false);  // �N�ؼ� UI �]�m���T�Ϊ��A
    }

    public void NextUI()
    {
        if(nextUI != null)
        {
            targetUI.SetActive(false);
            nextUI.SetActive(true);
        }
            
    }
}
