using UnityEngine;
using UnityEngine.UI;

public class LevelsListView : MonoBehaviour
{
    [SerializeField] private LevelOption[] levelOptions;
    [SerializeField] private Button preLevelBtn;
    [SerializeField] private Button nextLevelBtn;

    [SerializeField] private int maxLevel;

    private int optionLength;
    private int unlockedLevel;
    int startLevel;

  
    void Start()
    {
        preLevelBtn.onClick.AddListener (OnPreLevelBtn);
        nextLevelBtn.onClick.AddListener (OnNextLevelBtn);
        unlockedLevel = GameManager.Instance.GetUnlockedLevel();
        optionLength = levelOptions.Length;
        startLevel = ((unlockedLevel-1)/ optionLength) * optionLength + 1;
        SetLevel();
    }
    private void  SetLevel()
    {   
        int levelNumber=startLevel;
        for (int i = 0; i < optionLength; i++)
        {
            if (levelNumber > maxLevel)
            {
                levelOptions[i].gameObject.SetActive(false);
            }
            else
            {
                levelOptions[i].gameObject.SetActive(true);

            }
            levelOptions[i].SetLevel(levelNumber,levelNumber<=unlockedLevel);
            levelNumber++;
        } 
            
    }    

    private void OnPreLevelBtn()
    {
        AudioManager.Instance.PlayBtnSound();
        if (startLevel <= 1) return;
        startLevel-=optionLength;
        SetLevel();
    }    

    private void OnNextLevelBtn()
    {
        AudioManager.Instance.PlayBtnSound();
        if (startLevel+optionLength-1 >= maxLevel) return;
        startLevel+=optionLength;
        SetLevel();
    }    

}
