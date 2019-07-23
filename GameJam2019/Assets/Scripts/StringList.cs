using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StringList")]
public class StringList : ScriptableObject
{
  [SerializeField]
  private List<string> strings;
  public List<string> Strings => strings; 
}
