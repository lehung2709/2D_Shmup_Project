using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelOption : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private int levelNumber;
    private TextMeshProUGUI levelNumberTMP;

    private void Awake()
    {
       levelNumberTMP=GetComponentInChildren<TextMeshProUGUI>(); 
    }
    public void SetLevel(int levelNumber,bool isUnlocked)
    {
        this.levelNumber = levelNumber;
        levelNumberTMP.text=levelNumber.ToString();
        if (!isUnlocked)
        {
            GetComponent<Image>().color = Color.black;
            this.enabled = false;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            this.enabled = true;
        }    

    }    
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelInfor.Instance.ShowLevelInfor(levelNumber);
        AudioManager.Instance.PlayBtnSound();
    }
}
