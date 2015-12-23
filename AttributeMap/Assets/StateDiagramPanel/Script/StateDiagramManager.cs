using UnityEngine;
using System.Collections;

public class StateDiagramManager 
{
    private static StateDiagramManager s_instance = new StateDiagramManager();
    protected StateDiagramManager() { }

    public static StateDiagramManager instance
    {
        get
        {
            return s_instance;
        }
    }


}
