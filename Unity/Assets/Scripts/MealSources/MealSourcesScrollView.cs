using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    [RequireComponent(typeof(ScrollView))]
    public class MealSourcesScrollView : MonoBehaviour
    {
        public ScrollView ScrollView { get; private set; }

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

            int siblingStartIndex = _nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) * ScrollView.Content.constraintCount;
            GameObject mealNameText = ScrollView.InstantiateScrollViewText(siblingStartIndex);
            GameObject servingSizeText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject descriptionText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.servingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.description;

            var percent = 1 - (_nonarchivedNamedMealSources.IndexOfKey(namedMealSource.name) / (float) (_nonarchivedNamedMealSources.Count - 1));
            ScrollView.ScrollToPercent(percent);
            ScrollView.InvokeRowAdded(siblingStartIndex);
        }


        public void RenameNamedMealSource(NamedMealSource oldNamedMealSource, NamedMealSource newNamedMealSource)
        {
            MealSources.Remove(oldNamedMealSource.mealSource.id);
            MealSourceNames.Remove(oldNamedMealSource.mealSource.id);
            ScrollView.DeleteRow(_nonarchivedNamedMealSources.IndexOfKey(oldNamedMealSource.name));
            _nonarchivedNamedMealSources.Remove(oldNamedMealSource.name);
            AddNamedMealSource(newNamedMealSource);
        }

        public void ShowRenameField(int rowIndex)
        {
            var parentTransform = ScrollView.ContentChildren[rowIndex * ScrollView.Content.constraintCount].transform;
            var oldNamedMealSource = _nonarchivedNamedMealSources.Values[rowIndex];
            _mealSourceRenameField.Show(parentTransform, oldNamedMealSource);
        }

        private void Awake()
        {
            ScrollView = GetComponent<ScrollView>();
        }

        private void Start()
        {
            ScrollView.RowRemoved += ScrollView_OnRowRemoved;
        }

        private void OnDestroy()
        {
            ScrollView.RowRemoved -= ScrollView_OnRowRemoved;
        }

        private void ScrollView_OnRowRemoved(object sender, ScrollView.RowChangedEventArgs e)
        {
            var highlightedMealSource = _nonarchivedNamedMealSources.Values[e.RowIndex].mealSource;
            var archivedMealSource = new MealSource(highlightedMealSource, true);
            MealSources[archivedMealSource.id] = archivedMealSource;
            _nonarchivedNamedMealSources.RemoveAt(e.RowIndex);
        }
    }
}
