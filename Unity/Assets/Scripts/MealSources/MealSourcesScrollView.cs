using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourcesScrollView : AbstractScrollView
    {
        public Dictionary<string, MealSource> MealSources { get; private set; } = new Dictionary<string, MealSource>();

        public Dictionary<string, string> MealSourceNames { get; private set; } = new Dictionary<string, string>();

        [SerializeField]
        private MealSourceRenameField _mealSourceRenameField = default;

        private SortedList<string, NamedMealSource> _namedMealSources = new SortedList<string, NamedMealSource>();

        public void AddNamedMealSource(NamedMealSource namedMealSource)
        {
            var mealSource = namedMealSource.mealSource;
            MealSources.Add(mealSource.id, mealSource);
            MealSourceNames.Add(mealSource.id, namedMealSource.name);

            if (mealSource.archived)
                return;

            _namedMealSources.Add(namedMealSource.name, namedMealSource);

            GameObject mealNameText = InstantiateScrollViewText();
            GameObject servingSizeText = InstantiateScrollViewText();
            GameObject fatText = InstantiateScrollViewText();
            GameObject carbText = InstantiateScrollViewText();
            GameObject proteinText = InstantiateScrollViewText();
            GameObject calorieText = InstantiateScrollViewText();
            GameObject descriptionText = InstantiateScrollViewText();

            int siblingStartIndex = _namedMealSources.IndexOfKey(namedMealSource.name) * _content.constraintCount;
            mealNameText.transform.SetSiblingIndex(siblingStartIndex);
            servingSizeText.transform.SetSiblingIndex(siblingStartIndex + 1);
            fatText.transform.SetSiblingIndex(siblingStartIndex + 2);
            carbText.transform.SetSiblingIndex(siblingStartIndex + 3);
            proteinText.transform.SetSiblingIndex(siblingStartIndex + 4);
            calorieText.transform.SetSiblingIndex(siblingStartIndex + 5);
            descriptionText.transform.SetSiblingIndex(siblingStartIndex + 6);

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.servingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.description;

            var percent = 1 - (_namedMealSources.IndexOfKey(namedMealSource.name) / (float) (_namedMealSources.Count - 1));
            ScrollToPercent(percent);
        }


        public void RenameNamedMealSource(NamedMealSource oldNamedMealSource, NamedMealSource newNamedMealSource)
        {
            MealSources.Remove(oldNamedMealSource.mealSource.id);
            MealSourceNames.Remove(oldNamedMealSource.mealSource.id);
            base.DeleteRow(_namedMealSources.IndexOfKey(oldNamedMealSource.name));
            _namedMealSources.Remove(oldNamedMealSource.name);
            AddNamedMealSource(newNamedMealSource);
        }

        public override void ShowRenameField(int rowIndex)
        {
            var parentTransform = _content.transform.GetChild(rowIndex * _content.constraintCount);
            var oldNamedMealSource = _namedMealSources.Values[rowIndex];
            _mealSourceRenameField.Show(parentTransform, oldNamedMealSource);
        }

        public override void DeleteRow(int rowIndex)
        {
            var archivedMealSource = _namedMealSources.Values[rowIndex].mealSource;
            archivedMealSource.archived = true;
            MealSources[archivedMealSource.id] = archivedMealSource;
            _namedMealSources.RemoveAt(rowIndex);
            base.DeleteRow(rowIndex);
        }

    }
}
