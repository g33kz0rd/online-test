using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VariableTrackerController : MonoBehaviour
{
    private class TackedVariable
    {
        public GameObject Context { get; set; }
        public string Text { get; set; }
        public string Varname { get; internal set; }
    }

    private static Dictionary<string, List<TackedVariable>> trackedVariables = new Dictionary<string, List<TackedVariable>>();
    private static List<string> enabledGroups = new List<string>();

    public static void TrackVariable(string group, GameObject context, string varname, string text)
    {
        if (trackedVariables.ContainsKey(group))
        {
            var trackedVariable = trackedVariables[group].FirstOrDefault(x => x.Varname == varname && x.Context.GetInstanceID() == context.GetInstanceID());
            if (trackedVariable != null)
            {
                trackedVariable.Text = text;
            }
            else
            {
                AddVariable(group, context, varname, text);
                enabledGroups.Add(group);
            }
        }
        else
        {
            trackedVariables.Add(group, new List<TackedVariable>());
            AddVariable(group, context, varname, text);
        }
    }

    private static void AddVariable(string group, GameObject context, string varname, string text)
    {
        trackedVariables[group].Add(new TackedVariable()
        {
            Context = context,
            Varname = varname,
            Text = text,
        });
    }

    private void OnGUI()
    {
        int groupCount = 0;
        foreach (var kvp in trackedVariables)
        {
            if (!enabledGroups.Contains(kvp.Key))
            {
                RenderGroupDisabled(kvp.Key, groupCount);
            }
            else
            {
                RenderGroupEnabled(kvp.Key, kvp.Value, groupCount);
            }
            groupCount++;
        }
    }

    const int CW = 140;
    const int CH = 20;
    const int CP = 5;

    private void RenderGroupDisabled(string group, int groupCount)
    {
        GUI.Label(new Rect(groupCount * (CW + CP) + CP, CP, 80, CH), group);
        if (GUI.Button(new Rect(groupCount * (CW + CP) + CP + 85, CP, 20, CH), "+"))
            enabledGroups.Add(group);
    }

    private void RenderGroupEnabled(string group, List<TackedVariable> variables, int groupCount)
    {
        int currentGroupItem = 0;
        GUI.Label(new Rect(groupCount * (CW + CP) + CP, currentGroupItem * (CH + CP) + CP, 80, CH), group);
        if (GUI.Button(new Rect(groupCount * (CW + CP) + CP + 85 + CP, currentGroupItem * (CH + CP) + CP, 20, CH), "-"))
            enabledGroups.Remove(group);

        currentGroupItem++;

        foreach (var variable in variables)
        {
            GUI.Label(new Rect(groupCount * (CW + CP) + CP, currentGroupItem * (CH + CP) + CP, 105, CH), variable.Text);
            currentGroupItem++;
        }
    }
}
