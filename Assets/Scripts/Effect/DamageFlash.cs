using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;

    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Init();
    }
    
    private void Init()
    {
        materials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }    

    }

    public void CallDamageFlash()
    {
        if (!gameObject.activeSelf) return;
        if (damageFlashCoroutine != null) StopCoroutine(damageFlashCoroutine);
        damageFlashCoroutine = StartCoroutine(DamageFlashCoroutine());
    }    

    private IEnumerator DamageFlashCoroutine()
    {

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount=Mathf.Lerp(1f,0f,elapsedTime/flashTime);
            SetFlashAmount(currentFlashAmount);
            yield return null;
        } 
            
    } 

    private void SetFlashColor()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_FlashColor", flashColor);
        }
    }   
    
    private void SetFlashAmount(float amount)
    {
        for(int i = 0;i < materials.Length;i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }    
    }

    private void OnDisable()
    {
        SetFlashAmount(0f);
    }
}
