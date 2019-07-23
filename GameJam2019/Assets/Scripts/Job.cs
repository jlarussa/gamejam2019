using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Job")]
public class Job : ScriptableObject
{
  public int RequiredHacking { get; set; }
  public int RequiredStealth { get; set; }
  public int RequiredAssasination { get; set; }

  private int difficulty;
  public int Difficulty => difficulty;

  public int GoldCost { get; set; }

  public int GoldReward { get; set; }

  public int personnelLimit { get; set; }

  public string Name { get; set; }

  public List<Employee> Staff { get; set; }

  public void StartJob()
  {

  }
}
