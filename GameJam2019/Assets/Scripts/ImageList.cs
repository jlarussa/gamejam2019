using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ImageList")]
public class ImageList : ScriptableObject
{
  [SerializeField]
  private List<Sprite> images;
  public List<Sprite> Images => images; 
}
