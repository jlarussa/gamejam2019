using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
  public int RequiredHacking { get; set; }
  public int RequiredStealth { get; set; }
  public int RequiredAssasination { get; set; }

  public int GoldCost { get; set; }

  public int GoldReward { get; set; }

  public int personnelLimit { get; set; }

  public List<Employee> Staff { get; set; }
}
