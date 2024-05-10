using Camera2D.CameraExtensions;
using UnityEditor;
using UnityEngine;
using Assets.Extensions;

[CustomEditor(typeof(CinemachineTransposer2d))]
[CanEditMultipleObjects]
public class CinemachineTransposer2dEditor : Editor
{
    //SerializedProperty follow;
    //SerializedProperty smooth;
    //SerializedProperty stepped;
    //private SerializedProperty applyAfter;
    public bool showPosition;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        //follow = serializedObject.FindProperty("Follow");
        //smooth = serializedObject.FindProperty("smooth");
        //stepped = serializedObject.FindProperty("stepped");
        //applyAfter = serializedObject.FindProperty("ApplyAfter");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        //serializedObject.Update();
        DrawDefaultInspector();
        var target = (CinemachineTransposer2d)this.target;
        showPosition = EditorGUILayout.Foldout(showPosition, "Debug");
        if (EditorGUILayout.BeginFadeGroup(showPosition ? 1 : 0))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Debug Units");
            EditorGUILayout.Vector2Field("Offset",target.offset);
            EditorGUILayout.Vector2Field("Distance",target.distance);
            EditorGUILayout.Vector2Field("Velocity",target.velocity);
            EditorGUILayout.FloatField("Speed",target.speed);
            EditorGUILayout.LabelField("Debug Pixels");
            EditorGUILayout.Vector2Field("Offset", target.offset.ToPixels(target.PixelPerUnit));
            EditorGUILayout.Vector2Field("Distance", target.distance.ToPixels(target.PixelPerUnit));
            EditorGUILayout.Vector2Field("Velocity", target.velocity.ToPixels(target.PixelPerUnit));
            EditorGUILayout.FloatField("Speed", target.speed * target.PixelPerUnit);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();


        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        //serializedObject.ApplyModifiedProperties();

    }
}
