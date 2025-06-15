using UnityEngine;

[CreateAssetMenu(fileName ="NewPlaneSO",menuName ="Plane/PlaneSO")]
public class PlaneSO : ScriptableObject
{
    public string planeName;
    public int price;
    public Sprite avatar;
    public GameObject prefab;
}
