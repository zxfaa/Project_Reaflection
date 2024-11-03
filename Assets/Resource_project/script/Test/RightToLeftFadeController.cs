using UnityEngine;

public class LeftToRightFadeController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeSpeed = 0.5f; // ����H�X�t��
    public bool fade = false;

    private Material material;
    private float fadeValue = 0.7f; // ��l�ȡA�T�O���䤣�z���B�k��z��

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            material = spriteRenderer.material;
            if (material != null)
            {
                material.SetFloat("_Fade", fadeValue);
            }
            else
            {
                Debug.LogError("SpriteRenderer does not have a valid material with the LeftToRightFadeOut shader.");
            }
        }
    }

    void Update()
    {
        if (fade)
            FadeShadow();
    }

    void FadeShadow()
    {
        if (material != null)
        {
            // �v���W�[ fadeValue�A���z���ױq�����k�H�X�A�̲ק����z��
            fadeValue -= fadeSpeed * Time.deltaTime;
            material.SetFloat("_Fade", fadeValue);
            if (fadeValue <= -0.5)
                gameObject.SetActive(false);
        }
    }
}
