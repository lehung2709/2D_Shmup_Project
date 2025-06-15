using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float existTime;

    private string keyName;

    public void SetKeyName(string keyName)
    {
        this.keyName = keyName;
    }    
    public void RunAnim()
    {
        StartCoroutine(RunAnimateCoroutine());
    }    
    private IEnumerator RunAnimateCoroutine()
    {
        int index = 0;
        spriteRenderer.sprite = sprites[index];
        index++;
        float frameInterval = existTime/(sprites.Length-1);
        WaitForSeconds waitForSeconds = new WaitForSeconds(frameInterval);
        while (index < sprites.Length)
        {
            yield return waitForSeconds;
            spriteRenderer.sprite = sprites[index];
            index++;
            
        }
        yield return new WaitForSeconds(0.02f);

        EffectsSpawner.Instance.Return2Pool(keyName,this);

    }    
}
