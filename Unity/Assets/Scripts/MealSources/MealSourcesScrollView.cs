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
            var mealSource = namedMealSource.MealSource;
            MealSources.Add(mealSource.Id, mealSource);
            MealSourceNames.Add(mealSource.Id, namedMealSource.Name);

            if (mealSource.Archived)
                return;

            _namedMealSources.Add(namedMealSource.Name, namedMealSource);

            GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

            int siblingStartIndex = _namedMealSources.IndexOfKey(namedMealSource.Name) * _content.constraintCount;
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

            mealNameText.GetComponent<TextMeshProUGUI>().text = namedMealSource.Name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.ServingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.Calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.Description;

            var percent = 1 - (_namedMealSources.IndexOfKey(namedMealSource.Name) / (float) (_namedMealSources.Count - 1));
            ScrollToPercent(percent);
        }

        protected override void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e)
        {
            var archivedMealSource = _namedMealSources.Values[e.DestroyedRowIndex].MealSource;
            archivedMealSource.Archived = true;
            MealSources.RemoveAt(MealSources.IndexOfKey(archivedMealSource.Id));
            MealSources.Add(archivedMealSource.Id, archivedMealSource);
            _namedMealSources.RemoveAt(e.DestroyedRowIndex);
        }
    }
}
