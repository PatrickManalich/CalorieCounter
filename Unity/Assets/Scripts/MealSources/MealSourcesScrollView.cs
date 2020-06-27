using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    [RequireComponent(typeof(ScrollViewAssistant))]
    public class MealSourcesScrollView : MonoBehaviour
    {
        public ScrollViewAssistant ScrollViewAssistant { get; private set; }

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

            int siblingStartIndex = _nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) * ScrollViewAssistant.Content.constraintCount;
            GameObject mealNameText = ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex);
            GameObject servingSizeText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject descriptionText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.servingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.description;

            var percent = 1 - (_nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) / (float) (_nonarchivedNamedMealSources.Count - 1));
            ScrollViewAssistant.ScrollToPercent(percent);
            ScrollViewAssistant.InvokeRowAdded(siblingStartIndex);
        }


        public void RenameNamedMealSource(NamedMealSource oldNamedMealSource, NamedMealSource newNamedMealSource)
        {
            MealSources.Remove(oldNamedMealSource.mealSource.id);
            MealSourceNames.Remove(oldNamedMealSource.mealSource.id);
            ScrollViewAssistant.DeleteRow(_nonarchivedNamedMealSources.IndexOfKey(oldNamedMealSource.name));
            _nonarchivedNamedMealSources.Remove(oldNamedMealSource.name);
            AddNamedMealSource(newNamedMealSource);
        }

        public void ShowRenameField(int rowIndex)
        {
            var parentTransform = ScrollViewAssistant.ContentChildren[rowIndex * ScrollViewAssistant.Content.constraintCount].transform;
            var oldNamedMealSource = _nonarchivedNamedMealSources.Values[rowIndex];
            _mealSourceRenameField.Show(parentTransform, oldNamedMealSource);
        }

        private void Awake()
        {
            ScrollViewAssistant = GetComponent<ScrollViewAssistant>();
        }

        private void Start()
        {
            ScrollViewAssistant.RowRemoved += ScrollViewAssistant_OnRowRemoved;
        }

        private void OnDestroy()
        {
            ScrollViewAssistant.RowRemoved -= ScrollViewAssistant_OnRowRemoved;
        }

        private void ScrollViewAssistant_OnRowRemoved(object sender, ScrollViewAssistant.RowChangedEventArgs e)
        {
            var highlightedMealSource = _nonarchivedNamedMealSources.Values[e.RowIndex].mealSource;
            var archivedMealSource = new MealSource(highlightedMealSource, true);
            MealSources[archivedMealSource.id] = archivedMealSource;
            _nonarchivedNamedMealSources.RemoveAt(e.RowIndex);
        }
    }
}
