using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public class MealSourceIdFinder : EditorWindow
    {
        private string _mealSourceNameQuery = "";
        private string _mealSourceNameResult = "";
        private string _mealSourceIdResult = "";

        [MenuItem("Calorie Counter/Window/Meal Source ID Finder")]
        public static void ShowWindow()
        {
            GetWindow<MealSourceIdFinder>("Meal Source ID Finder");
        }

        public void OnGUI()
        {
            _mealSourceNameQuery = EditorGUILayout.TextField("Meal Source Name Query: ", _mealSourceNameQuery);
            _mealSourceNameResult = EditorGUILayout.TextField("Meal Source Name Result: ", _mealSourceNameResult);
            _mealSourceIdResult = EditorGUILayout.TextField("Meal Source ID Result: ", _mealSourceIdResult);

            if (GUILayout.Button("Find Meal Source ID"))
            {
                var mealSourceNamesDictionary = JsonConverter.ImportFile<Dictionary<MealSourceType, Dictionary<string, string>>>(GlobalPaths.JsonMealSourceNamesFileName);

                _mealSourceNameResult = "Not Found";
                _mealSourceIdResult = "Not Found";
                foreach (var mealSourceNames in mealSourceNamesDictionary.Values)
                {
                    foreach(var mealSourceNameKeyValuePair in mealSourceNames)
                    {
                        if (mealSourceNameKeyValuePair.Value.ToLower().Contains(_mealSourceNameQuery.ToLower()))
                        {
                            _mealSourceNameResult = mealSourceNameKeyValuePair.Value;
                            _mealSourceIdResult = mealSourceNameKeyValuePair.Key;
                            break;
                        }
                    }
                }
            }
        }
    }
}
