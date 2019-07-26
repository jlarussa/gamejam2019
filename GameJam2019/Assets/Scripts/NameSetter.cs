using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class NameSetter : MonoBehaviour
{
  [SerializeField]
  private Text textBox;

  public void SetName()
  {
    Manager.Current.SetName( textBox.text );
  }
  
}
