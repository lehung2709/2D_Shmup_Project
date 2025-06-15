using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject healthBarObj;
    private Enemy enemy;
    public void RegisterEvent(Enemy enemy)
    {
        healthBarObj.SetActive(true);
        if (enemy != null )
        {
            enemy.heahthChangeAction -= OnHealthChange;
        }
        this.enemy = enemy;
        enemy.heahthChangeAction += OnHealthChange;
        
    }    
    private void OnHealthChange(int health,int maxHealth)
    {
        healthBarSlider.value = (float)health/maxHealth;
        if (health <= 0)
        {
            enemy.heahthChangeAction -= OnHealthChange;
            enemy = null;
            healthBarObj.SetActive(false);
        }
    }
    
}
