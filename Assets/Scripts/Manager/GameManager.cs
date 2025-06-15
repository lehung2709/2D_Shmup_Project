using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData
{
    public bool isDontTakeDmg;
    public bool isClearAllEnemy;

    public LevelData(bool isDontTakeDmg, bool isClearAllEnemy)
    {
        this.isDontTakeDmg = isDontTakeDmg;
        this.isClearAllEnemy = isClearAllEnemy;
    }
}

[System.Serializable]
public class GameData
{
    public int unlockedLevels;
    public int playerMoney;
    public List<LevelData> levels;
    public List<bool> purchasedPlaneBoolList;
    public int selectedPlaneIndex;

    public GameData()
    {
        unlockedLevels = 1;
        playerMoney = 0;
        levels = new List< LevelData>();
        levels.Add(new LevelData(false,false));
        purchasedPlaneBoolList = new List<bool>();
        purchasedPlaneBoolList.Add(true);
    }

    public void UnlockNextLevel()
    {
        unlockedLevels++;
        levels.Add(new LevelData(false, false));
        Debug.Log(levels.Count);

    }




}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string savePath;
    [SerializeField] private PlaneLibSO planeLibSO;

    public Action<int> OnMoneyChangeAction;

    private GameData gameData;
    private int currentLevel = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Application.persistentDataPath + "/gameData.json";
        LoadGame();
    }

    private void SaveGame()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved: " + savePath);
    }

    public int GetMoney()
    {
        return gameData.playerMoney;
    }    

    private void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            gameData = JsonUtility.FromJson<GameData>(json);

        }
        else
        {
            gameData = new GameData();
            Debug.Log("No Save Found, Creating New Data");
        }
    }


    //Level Related
    public int GetUnlockedLevel()
    {
        return gameData.unlockedLevels;
    }    
    public void CompleteLevel( bool isDontTakeDmg,bool isClearAllEnemy,int moneyEarned)
    {
        if(currentLevel==gameData.unlockedLevels)
        {
            gameData.UnlockNextLevel();            
        }
        gameData.levels[currentLevel - 1].isClearAllEnemy |= isClearAllEnemy;
        gameData.levels[currentLevel - 1].isDontTakeDmg |= isDontTakeDmg;

        gameData.playerMoney += moneyEarned;


        SaveGame();
    }

    public LevelData GetLevelData(int levelNumber)
    {
        return gameData.levels[levelNumber-1];
    }
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        Time.timeScale = 1;
        AsyncLoader.Instance.LoadScene("Level" + currentLevel);
    }



    //Delete Save Data
    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save data deleted!");
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }




    //Plane Related
    public bool PurchasePlane(int index)
    {
        if (index < 0 || index >= planeLibSO.planeSOs.Count())
            return false;
        if(planeLibSO.planeSOs[index].price>gameData.playerMoney)return false;

        while (gameData.purchasedPlaneBoolList.Count <= index)
        {
            gameData.purchasedPlaneBoolList.Add(false);
        }

        gameData.purchasedPlaneBoolList[index] = true;
        gameData.playerMoney-=planeLibSO.planeSOs[index].price;
        OnMoneyChangeAction(gameData.playerMoney);
        SaveGame();
        return true;
    }
    public void SelectPlane(int index)
    {
        if (!IsPlanePurchased(index))
        {
            Debug.LogWarning("Plane not purchased yet!");
            return;
        }

        gameData.selectedPlaneIndex = index;
        SaveGame();
    }
    public PlaneSO GetPlaneSO(int index)
    {
        return planeLibSO.planeSOs[index];
    }    
    public bool IsPlanePurchased(int index)
    {
        if (index < 0 || index >= planeLibSO.planeSOs.Count())
            return false;

        if (index >= gameData.purchasedPlaneBoolList.Count)
            return false;

        return gameData.purchasedPlaneBoolList[index];
    }
    public bool IsPlaneSelected(int index)
    {
        return gameData.selectedPlaneIndex == index;
    }    
    public int GetMaxPlaneIndex()
    {
        return planeLibSO.planeSOs.Length-1;
    }    
    public int GetSelectedPlaneIndex()
    {
        return gameData.selectedPlaneIndex;
    }    
    public void SpawnPlane(Vector2 planePos)
    {
        Instantiate(planeLibSO.planeSOs[gameData.selectedPlaneIndex].prefab,planePos, Quaternion.identity);
    }    


}
