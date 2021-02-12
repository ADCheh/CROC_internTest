using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario",menuName = "Scenario")]
public class ScenarioScript : ScriptableObject
{
    public string title;
    public string description;

    public InteractableObject[] Actions;

    public int[] stages;



}
