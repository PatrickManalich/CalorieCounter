using UnityEngine;
using UnityEditor;

public class CopyPasteAllComponents : EditorWindow
{

    private static Component[] _copiedComponents;

    [MenuItem("GameObject/Copy All Components %&C")]
    private static void CopyAllComponents()
    {
        _copiedComponents = Selection.activeGameObject.GetComponents<Component>();
    }

    [MenuItem("GameObject/Paste All Components %&P")]
    private static void PasteAllComponents()
    {
        foreach (var targetGameObject in Selection.gameObjects)
        {
            if (!targetGameObject || _copiedComponents == null) continue;
            foreach (var copiedComponent in _copiedComponents)
            {
                if (!copiedComponent) continue;
                UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponent);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
            }
        }
    }

}