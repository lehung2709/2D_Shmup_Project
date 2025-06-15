using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject planeSelectionPanel;
    [SerializeField] private TextMeshProUGUI planeNameTMP;
    [SerializeField] private Image planeAvatarImage;
    [SerializeField] private Button prePlaneBtn;
    [SerializeField] private Button nextPlaneBtn;
    [SerializeField] private Button buy_Select_Btn;
    [SerializeField] private TextMeshProUGUI buy_Select_TMP;
    private int currentPlaneIndex;
    private int maxPlaneIndex;
    void Start()
    {
        maxPlaneIndex = GameManager.Instance.GetMaxPlaneIndex();
        prePlaneBtn.onClick.AddListener(prePlaneBtnAction);
        nextPlaneBtn.onClick.AddListener(nextPlaneBtnAction);
        buy_Select_Btn.onClick.AddListener(Buy_Select_Btn_Action);
    }

    private void nextPlaneBtnAction()
    {
        currentPlaneIndex++;
        if (currentPlaneIndex > maxPlaneIndex)
            currentPlaneIndex = 0;
        SetUI();

    }
    private void prePlaneBtnAction()
    {
        currentPlaneIndex--;
        if (currentPlaneIndex < 0)
            currentPlaneIndex = maxPlaneIndex;
        SetUI();

    }

    private void SetUI()
    {
        PlaneSO planeSO = GameManager.Instance.GetPlaneSO(currentPlaneIndex);
        planeNameTMP.text = planeSO.name;
        planeAvatarImage.sprite = planeSO.avatar;
        planeAvatarImage.SetNativeSize();

        buy_Select_Btn.interactable = true;

        if (GameManager.Instance.IsPlanePurchased(currentPlaneIndex))
        {
            if (GameManager.Instance.IsPlaneSelected(currentPlaneIndex))
            {
                buy_Select_TMP.text = "SELECTED";
                buy_Select_Btn.interactable = false;
            }
            else
            {
                buy_Select_TMP.text = "SELECT";

            }
        }
        else
        {
            buy_Select_TMP.text = planeSO.price.ToString();

        }
    }    

    private void Buy_Select_Btn_Action()
    {
        if (GameManager.Instance.IsPlanePurchased(currentPlaneIndex))
        {
            if (!GameManager.Instance.IsPlaneSelected(currentPlaneIndex))
            {
                AudioManager.Instance.PlayBtnSound();
                GameManager.Instance.SelectPlane(currentPlaneIndex);
                buy_Select_TMP.text = "SELECTED";
                buy_Select_Btn.interactable = false;

            }
        }
        else
        {
            
           if( GameManager.Instance.PurchasePlane(currentPlaneIndex))
            {
                AudioManager.Instance.SpawnSoundEmitter(null, "Buy", Vector3.zero);
                buy_Select_TMP.text = "SELECT";

            }

        }
    }    

    public void SetActivePlaneSelectionPanel()
    {
        planeSelectionPanel.SetActive(!planeSelectionPanel.activeSelf);
        if(planeSelectionPanel.activeSelf )
        {
            currentPlaneIndex = GameManager.Instance.GetSelectedPlaneIndex();
            SetUI();
        }    

    }    
}
