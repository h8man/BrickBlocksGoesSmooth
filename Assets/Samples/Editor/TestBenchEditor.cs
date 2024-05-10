using Unity.Cinemachine;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Samples.Editors
{
    [CustomEditor(typeof(TestBench))]

    internal class TestBenchEditor: Editor
    {
        CinemachineCamera active;
        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            //serializedObject.Update();
            var target = (TestBench)this.target;
            DrawDefaultInspector();
            EditorGUILayout.Space();
            if (GUILayout.Button($"Apply"))
            {
                Application.targetFrameRate = target.frameRate;
                Time.timeScale = target.timeScale;
                Time.fixedDeltaTime = target.fixedTime;
            }

            if (active == null)
            {
                active = target.cameras.Where(x => CinemachineCore.IsLive(x)).FirstOrDefault();
            }
            int i = 0;
            EditorGUILayout.LabelField("Cameras:");
            foreach (var camera in target.cameras)
            {
                if (GUILayout.Button($"{camera.Name} - {i}"))
                {
                    active = camera;
                    camera.Prioritize();
                }
                i++;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Actions:");
            if (GUILayout.Button($"Warp Cahracter"))
            {
                var pos = target.character.transform.position;
                if (Random.value > 0.5)
                {
                    target.character.transform.position = target.viewPoint1.position;
                }
                else
                {
                    target.character.transform.position = target.viewPoint2.position;
                }
                active.OnTargetObjectWarped(target.character.transform, target.character.transform.position - pos);
            }
            if (GUILayout.Button($"Transit Cahracter"))
            {
                if (Random.value > 0.5)
                {
                    target.character.transform.position = target.viewPoint1.position;
                }
                else
                {
                    target.character.transform.position = target.viewPoint2.position;
                }
            }
            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            //serializedObject.ApplyModifiedProperties();

        }
    }
}
