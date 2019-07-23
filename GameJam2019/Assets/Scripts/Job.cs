using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Job")]
public class Job : ScriptableObject
{
  [SerializeField]
  private int requiredHacking;
  public int RequiredHacking => requiredHacking;

  [SerializeField]
  private int requiredStealth;
  public int RequiredStealth => requiredStealth;

  [SerializeField]
  private int requiredAssassination;
  public int RequiredAssasination => requiredAssassination;

  [SerializeField]
  private int difficulty;
  public int Difficulty => difficulty;

  [SerializeField]
  private int goldCost;
  public int GoldCost => goldCost;

  public int GoldReward { get; set; }

  public int personnelLimit { get; set; }

  [SerializeField]
  private string jobName;
  public string Name => jobName;

  public List<Employee> Staff { get; set; }

  public void StartJob()
  {

  }
}
