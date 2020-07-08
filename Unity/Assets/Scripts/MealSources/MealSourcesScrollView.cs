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

        public MealSourceType MealSourceType => _mealSourceType;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private MealSourceRenameField _mealSourceRenameField = default;

        private SortedList<string, NamedMealSource> _nonarchivedNamedMealSources = new SortedList<string, NamedMealSource>();

        public void AddNamedMealSource(NamedMealSource namedMealSource)
        {
            var mealSource = namedMealSource.MealSource;
            MealSources.Add(mealSource.Id, mealSource);
            MealSourceNames.Add(mealSource.Id, namedMealSource.Name);

            if (mealSource.Archived)
                return;

            _nonarchivedNamedMealSources.Add(namedMealSource.Name, namedMealSource);

            int siblingStartIndex = _nonarchivedNamedMealSources.IndexOfKey(namedMealSource.Name) * ScrollViewAssistant.Content.constraintCount;
            GameObject mealNameText = ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex);
            GameObject servingSizeText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject descriptionText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.Name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.ServingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.Calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.Description;

            var percent = 1 - (_nonarchivedNamedMealSources.IndexOfKey(namedMealSource.Name) / (float) (_nonarchivedNamedMealSources.Count - 1));
            ScrollViewAssistant.ScrollToPercent(percent);
            ScrollViewAssistant.InvokeRowAdded(siblingStartIndex);
        }


        public void RenameNamedMealSource(NamedMealSource oldNamedMealSource, NamedMealSource newNamedMealSource)
        {
            MealSources.Remove(oldNamedMealSource.MealSource.Id);
            MealSourceNames.Remove(oldNamedMealSource.MealSource.Id);

            ScrollViewAssistant.RowRemoved -= ScrollViewAssistant_OnRowRemoved;
            ScrollViewAssistant.RemoveRow(_nonarchivedNamedMealSources.IndexOfKey(oldNamedMealSource.Name));
            ScrollViewAssistant.RowRemoved += ScrollViewAssistant_OnRowRemoved;

            _nonarchivedNamedMealSources.Remove(oldNamedMealSource.Name);
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
            var highlightedMealSource = _nonarchivedNamedMealSources.Values[e.RowIndex].MealSource;
            var archivedMealSource = new MealSource(highlightedMealSource, true);
            MealSources[archivedMealSource.Id] = archivedMealSource;
            _nonarchivedNamedMealSources.RemoveAt(e.RowIndex);
        }
    }
}
