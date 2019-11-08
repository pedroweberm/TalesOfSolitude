using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resource")]
public class Resource : ScriptableObject
{
    public string resource_name = "Resource";

    public int amount = 0;

    public int type = 0; 

}
