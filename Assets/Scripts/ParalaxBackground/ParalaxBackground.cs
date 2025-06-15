using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    private class ParallaxLayer
    {
        public SpriteRenderer layerSprite;
        [Range(0, 1)] public float speed;
        [HideInInspector] public Material spriteMaterial;
    }

    [SerializeField] private ParallaxLayer[] parallaxLayers;
    [SerializeField] private float speedMultiplier;

    private void Awake()
    {
        foreach (var layer in parallaxLayers)
        {
            layer.spriteMaterial = layer.layerSprite.material;

        }
    }

    void FixedUpdate()
    {
        foreach (var layer in parallaxLayers)
        {
           
                Vector2 offset = layer.spriteMaterial.mainTextureOffset;

                offset.x += layer.speed * speedMultiplier  * Time.fixedDeltaTime;

                layer.spriteMaterial.mainTextureOffset = offset;
            
        }
    }
}
