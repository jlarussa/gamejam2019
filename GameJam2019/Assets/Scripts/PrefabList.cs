using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PrefabList")]
public class PrefabList : ScriptableObject
{
  [SerializeField]
  private List<GameObject> prefabs;
  public List<GameObject> Prefabs => prefabs; 
}
