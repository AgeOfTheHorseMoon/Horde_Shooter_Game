using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Test Scriptable Objects/Items")]
public class Test_Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    [Range(1,99)]
    public int MaximumStackSize = 1;
    public Sprite Icon;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Test_Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }
}
