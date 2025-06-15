using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Button button;
    private RectTransform rectTransform;
    private Tween idleTween;

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        rectTransform = button.GetComponent<RectTransform>();

        StartIdleAnimation();

    }

   
    private void StartIdleAnimation()
    {
        idleTween = rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1f), 1f)
                                 .SetLoops(-1, LoopType.Yoyo)
                                 .SetEase(Ease.InOutSine);
    }

    void OnDestroy()
    {
        idleTween?.Kill();
    }
}
