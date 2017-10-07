using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GroupCommand {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///
    
    /*    
        - 'Group Command' script, press CTRL + G to group selected objects
        in the hierarchy. press CTRL + Z to undo command.

        -  Functions the same as placing an empty game object in the world
        and having the selected objects as a child.
    */

    [MenuItem("GameObject/Group Selected %g")]
    private static void GroupSelected() {

        if (!Selection.activeTransform)
            return;

        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);

        foreach (var transform in Selection.transforms)
            Undo.SetTransformParent(transform, go.transform, "Group Selected");

        Selection.activeGameObject = go;
    }
}