using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinUIAnim : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject winTMP;
    [SerializeField] private GameObject dTDMissionTMP;
    [SerializeField] private GameObject cAEMissionTMP;
    [SerializeField] private Image dTDMissionStatusImg;
    [SerializeField] private Image cAEMissionStatusImg;
    [SerializeField] private Sprite doneSprite;
    [SerializeField] private Sprite failSprite;

    private Sequence sequence;



    private void Awake()
    {
        
        container.transform.localScale = Vector3.zero;
        menuBtn.transform.localScale = Vector3.zero;
        restartBtn.transform.localScale = Vector3.zero;
        winTMP.transform.localScale = Vector3.zero;
        dTDMissionTMP.transform.localScale = Vector3.zero;
        dTDMissionStatusImg.transform.localScale = Vector3.zero;
        cAEMissionTMP.transform.localScale = Vector3.zero;
        cAEMissionStatusImg.transform.localScale = Vector3.zero;

        container.SetActive(false);
        menuBtn.SetActive(false);
        restartBtn.SetActive(false);
        winTMP.SetActive(false);
        dTDMissionTMP.SetActive(false);
        dTDMissionStatusImg.gameObject.SetActive(false);
        cAEMissionTMP.SetActive(false);
        cAEMissionStatusImg.gameObject.SetActive(false);

    }

    private void Start()
    {
        LevelManager.Instance.OnWin += ShowWinUI;

    }

    public void ShowWinUI()
    {
        container.SetActive(true);
        menuBtn.SetActive(true);
        restartBtn.SetActive(true);
        winTMP.SetActive(true);
        dTDMissionTMP.SetActive(true);
        dTDMissionStatusImg.gameObject.SetActive(true);
        cAEMissionTMP.SetActive(true);
        cAEMissionStatusImg.gameObject.SetActive(true);

        dTDMissionStatusImg.sprite = LevelManager.Instance.DTDStatus ? doneSprite : failSprite;
        cAEMissionStatusImg.sprite = LevelManager.Instance.CAEStatus ? doneSprite : failSprite;

        sequence = DOTween.Sequence();

        sequence.AppendInterval(3f);

        sequence.Append(container.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack));
        sequence.Append(winTMP.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));

        sequence.Append(restartBtn.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(menuBtn.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));


        sequence.Append(dTDMissionTMP.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(dTDMissionStatusImg.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(cAEMissionTMP.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(cAEMissionStatusImg.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));

        sequence.Play();
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnWin -= ShowWinUI;

        DOTween.Kill(winTMP.transform); 
        DOTween.Kill(container.transform);
        DOTween.Kill(restartBtn.transform);
        DOTween.Kill(menuBtn.transform);
        DOTween.Kill(dTDMissionTMP.transform);
        DOTween.Kill(dTDMissionStatusImg.transform);
        DOTween.Kill(cAEMissionTMP.transform);
        DOTween.Kill(cAEMissionStatusImg.transform);

        sequence.Kill();
    }
}
