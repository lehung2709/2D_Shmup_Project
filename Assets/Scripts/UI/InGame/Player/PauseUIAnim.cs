using DG.Tweening;
using UnityEngine;

public class PauseUIAnim : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private Vector3 hiddenPos;
    private Vector3 visiblePos;
    private bool isPaused = false;

    private void Awake()
    {
        visiblePos = container.transform.localPosition;
        hiddenPos = visiblePos + new Vector3(600, 0, 0); 

        container.transform.localPosition = hiddenPos;
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseUI();
        }
    }

    public void TogglePauseUI()
    {
        isPaused = !isPaused;
        container.SetActive(true);

        if (isPaused)
        {
            Time.timeScale = 0;
            container.transform.DOLocalMove(visiblePos, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        }
        else
        {
            container.transform.DOLocalMove(hiddenPos, 0.5f).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() =>
                {
                    container.SetActive(false);
                    Time.timeScale = 1;

                });
        }
    }
}
