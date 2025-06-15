using UnityEngine;

public class ShieldAnimation : MonoBehaviour
{
    [SerializeField] private GameObject shield1;
    [SerializeField] private GameObject shield2;

    [SerializeField] private float rotationSpeed = 100f; 

    private void FixedUpdate()
    {
        
        shield1.transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
        

        
        shield2.transform.Rotate(0f, 0f, -rotationSpeed * Time.fixedDeltaTime);
        
    }
}
