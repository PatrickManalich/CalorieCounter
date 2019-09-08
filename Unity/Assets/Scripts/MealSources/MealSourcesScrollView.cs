using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourcesScrollView : AbstractScrollView
    {
        public SortedList<string, MealSource> MealSources { get; private set; } = new SortedList<string, MealSource>();

        public SortedList<string, string> MealSourceNames { get; private set; } = new SortedList<string, string>();

        public override event TextAddedEventHandler TextAddedEvent;

        protected override ScrollViewRowHighlighter ScrollViewRowHighlighter { get { return _scrollViewRowHighlighter; } }

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private MealSourceInputFields _mealSourceInputFields = default;

        [SerializeField]
        private ScrollViewRowHighlighter _scrollViewRowHighlighter = default;

        private SortedList<string, NamedMealSource> _namedMealSources = new SortedList<string, NamedMealSource>();

        public void AddNamedMealSourceFromInputFields()
        {
            AddNamedMealSource(_mealSourceInputFields.GetNamedMealSourceFromInputFields());
        }

        public void AddNamedMealSource(NamedMealSource namedMealSource)
        {
            var mealSource = namedMealSource.mealSource;
            MealSources.Add(mealSource.id, mealSource);
            MealSourceNames.Add(mealSource.id, namedMealSource.name);

            if (mealSource.archived)
                return;

            _namedMealSources.Add(namedMealSource.name, namedMealSource);

            GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

            int siblingStartIndex = _namedMealSources.IndexOfKey(namedMealSource.name) * _content.constraintCount;
            mealNameText.transform.SetSiblingIndex(siblingStartIndex);
            servingSizeText.transform.SetSiblingIndex(siblingStartIndex + 1);
            fatText.transform.SetSiblingIndex(siblingStartIndex + 2);
            carbText.transform.SetSiblingIndex(siblingStartIndex + 3);
            proteinText.transform.SetSiblingIndex(siblingStartIndex + 4);
            calorieText.transform.SetSiblingIndex(siblingStartIndex + 5);
            descriptionText.transform.SetSiblingIndex(siblingStartIndex + 6);

            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(mealNameText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(servingSizeText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(fatText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(carbText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(proteinText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(calorieText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(descriptionText.GetComponent<ScrollViewText>()));

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

        protected override void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e)
        {
            var archivedMealSource = _namedMealSources.Values[e.DestroyedRowIndex].mealSource;
            archivedMealSource.archived = true;
            MealSources.RemoveAt(MealSources.IndexOfKey(archivedMealSource.id));
            MealSources.Add(archivedMealSource.id, archivedMealSource);
            _namedMealSources.RemoveAt(e.DestroyedRowIndex);
        }
    }
}
