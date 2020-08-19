using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public class TimeScaleEditorWindow : EditorWindow
    {
        private const int DefaultMinTimeScale = 0;
        private const int DefaultMaxTimeScale = 100;

        private float minTimeScale = DefaultMinTimeScale;
        private float maxTimeScale = DefaultMaxTimeScale;

        [MenuItem("Calorie Counter/Window/Time Scale Editor")]
        public static void ShowWindow()
        {
            GetWindow<TimeScaleEditorWindow>("Time Scale Editor");
        }

        private void OnGUI()
        {
            Time.timeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, minTimeScale, maxTimeScale);
            MinTimeScale = EditorGUILayout.FloatField("Min Time Scale", minTimeScale);
            MaxTimeScale = EditorGUILayout.FloatField("Max Time Scale", maxTimeScale);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Default"))
            {
                minTimeScale = DefaultMinTimeScale;
                maxTimeScale = DefaultMaxTimeScale;
                Time.timeScale = 1;
            }
            if (GUILayout.Button("Min Speed"))
            {
                Time.timeScale = minTimeScale;
            }
            if (GUILayout.Button("Max Speed"))
            {
                Time.timeScale = maxTimeScale;
            }
            GUILayout.EndHorizontal();
        }

        private float MinTimeScale {
            set {
                minTimeScale = value;
                if (minTimeScale < DefaultMinTimeScale)
                {
                    minTimeScale = DefaultMinTimeScale;
                }
            }
        }

        private float MaxTimeScale {
            set {
                maxTimeScale = value;
                if (maxTimeScale > DefaultMaxTimeScale)
                {
                    maxTimeScale = DefaultMaxTimeScale;
                }
            }
        }
    }
}