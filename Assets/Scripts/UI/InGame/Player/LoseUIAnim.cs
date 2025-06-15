using UnityEngine;
using DG.Tweening;

public class LoseUIAnim : MonoBehaviour
{
    [SerializeField] private GameObject btnContainer;
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject loseTMP;

   
    private Sequence sequence;


    private void Awake()
    {
        

        btnContainer.transform.localScale = Vector3.zero;
        menuBtn.transform.localScale = Vector3.zero;
        restartBtn.transform.localScale = Vector3.zero;
        loseTMP.transform.localScale = Vector3.zero;
        btnContainer.SetActive(false);
        menuBtn.SetActive(false);
        restartBtn.SetActive(false);
        loseTMP.SetActive(false);
    }
    private void Start()
    {
        LevelManager.Instance.OnLose += ShowLoseUI;
    }
    public void ShowLoseUI()
    {
        btnContainer.SetActive(true);
        menuBtn.SetActive(true);
        restartBtn.SetActive(true);
        loseTMP.SetActive(true);
        sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);

        sequence.Append(loseTMP.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack));

        loseTMP.transform.DOShakeRotation(1.5f, 2, 5, 90, false)
            .SetLoops(-1, LoopType.Restart)
            .SetUpdate(true);

        sequence.Join(btnContainer.transform.DOScale(1, 0.4f).SetEase(Ease.OutBack));

        sequence.Append(menuBtn.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(restartBtn.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));

        sequence.Play();
    }
    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnLose -= ShowLoseUI;

        DOTween.Kill(loseTMP.transform);
        DOTween.Kill(btnContainer.transform);
        DOTween.Kill(menuBtn.transform);
        DOTween.Kill(restartBtn.transform);

        sequence.Kill();
    }

}
