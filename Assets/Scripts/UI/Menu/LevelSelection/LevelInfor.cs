using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfor : MonoBehaviour
{
    public static LevelInfor Instance { get; private set; }

    private int levelNumber = 0;

    [SerializeField] private GameObject levelInforPanel;
    [SerializeField] private TextMeshProUGUI levelNumberTMP;
    [SerializeField] private Image dTDStatusImg;
    [SerializeField] private Image cAEStatusImg;

    [SerializeField] private Sprite doneSprite;
    [SerializeField] private Sprite failSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
    }

    public void ShowLevelInfor(int levlelNumber)
    {
        this.levelNumber=levlelNumber;
        LevelData data=GameManager.Instance.GetLevelData(levlelNumber);
        levelNumberTMP.text = "LEVEL" + levlelNumber;
        dTDStatusImg.sprite = data.isDontTakeDmg ? doneSprite : failSprite;
        cAEStatusImg.sprite = data.isClearAllEnemy ? doneSprite : failSprite;
        levelInforPanel.SetActive(true);
    }    

    public void Play()
    {
        GameManager.Instance.LoadLevel(levelNumber);
    }    
    
}
