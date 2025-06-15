using UnityEngine;

public class SimpleSpriteAnim : MonoBehaviour
{
    [SerializeField] private Sprite[] animSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float frameRate = 0.1f;
    private int currentFrame = 0;
    private float timer = 0f;


    private void Update()
    {
        AnimateSprite();
    }
    private void AnimateSprite()
    {
        if (animSprites == null || animSprites.Length == 0 || spriteRenderer == null) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % animSprites.Length;
            spriteRenderer.sprite = animSprites[currentFrame];
        }
    }
}
