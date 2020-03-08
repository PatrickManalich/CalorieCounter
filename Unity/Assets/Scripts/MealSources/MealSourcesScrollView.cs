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

        private SortedList<string, NamedMealSource> _nonarchivedNamedMealSources = new SortedList<string, NamedMealSource>();

        public void AddNamedMealSource(NamedMealSource namedMealSource)
        {
            var mealSource = namedMealSource.mealSource;
            MealSources.Add(mealSource.id, mealSource);
            MealSourceNames.Add(mealSource.id, namedMealSource.name);

            if (mealSource.archived)
                return;

            _nonarchivedNamedMealSources.Add(namedMealSource.name, namedMealSource);

            int siblingStartIndex = _nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) * _content.constraintCount;
            GameObject mealNameText = InstantiateScrollViewText(siblingStartIndex);
            GameObject servingSizeText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject descriptionText = InstantiateScrollViewText(++siblingStartIndex);

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.servingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.description;

            var percent = 1 - (_nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) / (float) (_nonarchivedNamedMealSources.Count - 1));
            ScrollToPercent(percent);
        }


        public void RenameNamedMealSource(NamedMealSource oldNamedMealSource, NamedMealSource newNamedMealSource)
        {
            MealSources.Remove(oldNamedMealSource.mealSource.id);
            MealSourceNames.Remove(oldNamedMealSource.mealSource.id);
            base.DeleteRow(_nonarchivedNamedMealSources.IndexOfKey(oldNamedMealSource.name));
            _nonarchivedNamedMealSources.Remove(oldNamedMealSource.name);
            AddNamedMealSource(newNamedMealSource);
        }

        public override void ShowRenameField(int rowIndex)
        {
            var parentTransform = _contentChildren[rowIndex * _content.constraintCount].transform;
            var oldNamedMealSource = _nonarchivedNamedMealSources.Values[rowIndex];
            _mealSourceRenameField.Show(parentTransform, oldNamedMealSource);
        }

        public override void DeleteRow(int rowIndex)
        {
            base.DeleteRow(rowIndex);
            var archivedMealSource = _nonarchivedNamedMealSources.Values[rowIndex].mealSource;
            archivedMealSource.archived = true;
            MealSources[archivedMealSource.id] = archivedMealSource;
            _nonarchivedNamedMealSources.RemoveAt(rowIndex);
        }
    }
}
