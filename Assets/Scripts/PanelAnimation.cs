using System.Collections;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Vector3 normalScale;
    Coroutine animationRoutine;

    [SerializeField] float duration = 0.15f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        normalScale = transform.localScale;
    }

    void OnEnable()
    {
        if (animationRoutine != null)
            StopCoroutine(animationRoutine);

        animationRoutine = StartCoroutine(OpenAnimation());
    }


    IEnumerator OpenAnimation()
    {
        transform.localScale = normalScale * 0.75f;
        canvasGroup.alpha = 0f;

        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            float p = t / duration;

            transform.localScale = Vector3.Lerp(
                normalScale * 0.75f,
                normalScale,
                p
            );

            canvasGroup.alpha = p;

            yield return null;
        }

        transform.localScale = normalScale;
        canvasGroup.alpha = 1f;
    }


}