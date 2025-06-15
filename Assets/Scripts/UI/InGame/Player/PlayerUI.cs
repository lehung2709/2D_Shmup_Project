using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject healthPointObj;
    [SerializeField] private GameObject skillPointObj;
    [SerializeField] private GameObject healthPointContainer;
    [SerializeField] private GameObject skillPointContainer;
    [SerializeField] private Image skillImg;
    [SerializeField] private TextMeshProUGUI skillDes;

    public static PlayerUI Instance { get; private set; }

    private List<GameObject> healthPoints = new List<GameObject>();
    private List<GameObject> skillPoints = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void Init(int healthPoint, int skillPoint)
    {
        
        foreach (Transform child in healthPointContainer.transform)
            Destroy(child.gameObject);
        foreach (Transform child in skillPointContainer.transform)
            Destroy(child.gameObject);


        for (int i = 0; i < healthPoint; i++)
        {
            GameObject obj = Instantiate(healthPointObj, healthPointContainer.transform);
            healthPoints.Add(obj);
        }

        for (int i = 0; i < skillPoint; i++)
        {
            GameObject obj = Instantiate(skillPointObj, skillPointContainer.transform);
            skillPoints.Add(obj);
        }
        var healthFitter = healthPointContainer.GetComponent<ContentSizeFitter>();
        var skillFitter = skillPointContainer.GetComponent<ContentSizeFitter>();

        StartCoroutine(FixContainerSizeNextFrame(healthPointContainer, healthFitter));
        StartCoroutine(FixContainerSizeNextFrame(skillPointContainer, skillFitter));
    }

    private IEnumerator<UnityEngine.WaitForEndOfFrame> FixContainerSizeNextFrame(GameObject container, ContentSizeFitter fitter)
    {
        yield return new WaitForEndOfFrame(); 

        if (fitter != null) fitter.enabled = false;

        RectTransform rect = container.GetComponent<RectTransform>();
        if (rect != null)
        {
            Vector2 size = rect.sizeDelta;
            rect.sizeDelta = size; 
        }
    }


    public void DecreaseHealPoint(int index)
    {
        if (index >= 0 && index < healthPoints.Count)
        {
            healthPoints[index].SetActive(false);
        }
    }

    public void IncreaseHealPoint(int index)
    {
        if (index >= 0 && index < healthPoints.Count)
        {
            healthPoints[index].SetActive(true);
        }
    }

    public void DecreaseSkillPoint(int index)
    {
        if (index >= 0 && index < skillPoints.Count)
        {
            skillPoints[index].SetActive(false);
        }
    }

    public void IncreaseSkillPoint(int index)
    {
        if (index >= 0 && index < skillPoints.Count)
        {
            skillPoints[index].SetActive(true);
        }
    }

    public void SetSkillDes(Sprite sprite,string des)
    {
        skillImg.sprite = sprite;
        skillDes.text = des;
    }    
}
