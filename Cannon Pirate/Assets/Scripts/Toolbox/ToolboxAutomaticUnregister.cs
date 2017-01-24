using UnityEngine;
using System.Collections;

public class ToolboxAutomaticUnregister : MonoBehaviour
{
    public System.Type typeToUnregister;

    void OnDestroy()
    {
        var instance = Toolbox.Instance;
        if (instance)
        {
            Toolbox.UnregisterComponent(typeToUnregister);
        }
    }
}
