using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FadeController : MonoBehaviour
{
    public CanvasGroup canvasGroup;   // �Ω�H�J�H�X�r���� CanvasGroup
    public float fadeDuration = 1.0f; // �H�J�H�X�һݪ��ɶ�
    private Coroutine currentCoroutine = null; // ��e���b�B�檺��{

    // ��ܦr����
    public void DisplaySubtitles(SubtitleData.SubtitleGroup subtitleGroup)
    {
        // �p�G����{���b�B��A���
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        // �}�l��ܷs���r����{
        currentCoroutine = StartCoroutine(DisplaySubtitleCoroutine(subtitleGroup.subtitles));
    }

    // ��{�G��ܤ@�Ӧr���դ����Ҧ��r��
    private IEnumerator DisplaySubtitleCoroutine(List<SubtitleData.Subtitle> subtitles)
    {
        Text textComponent = canvasGroup.GetComponentInChildren<Text>();

        foreach (var subtitle in subtitles)
        {
            textComponent.text = subtitle.text;

            // �H�J�r��
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));

            // ��ܦr���@�q�ɶ�
            yield return new WaitForSeconds(subtitle.displayDuration);

            // �H�X�r��
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));
        }

        // ����ܧ�����A���m��e��{�ޥ�
        currentCoroutine = null;
    }

    // ��{�G�H�J�βH�X CanvasGroup
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = end;
    }
}
