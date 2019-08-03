using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealSources {

    public class MealSourcesScrollView : AbstractScrollView
    {
        public SortedList<string, MealSource> ArchivedMealSources { get; private set; } = new SortedList<string, MealSource>();

        public override event TextAddedEventHandler TextAddedEvent;

        protected override GridLayoutGroup Content { get { return _content; } }

        protected override ScrollViewRowHighlighter ScrollViewRowHighlighter { get { return _scrollViewRowHighlighter; } }

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private MealSourceInputFields _mealSourceInputFields = default;

        [SerializeField]
        private ScrollViewRowHighlighter _scrollViewRowHighlighter = default;

        private SortedList<string, MealSource> _nonArchivedMealSources = new SortedList<string, MealSource>();

        public void AddMealSourceFromInputFields()
        {
            AddMealSource(_mealSourceInputFields.GetMealSourceFromInputFields());
        }

        public void AddMealSource(MealSource mealSource)
        {
            ArchivedMealSources.Add(mealSource.Name, mealSource);

            if (mealSource.Archived)
            {
                return;
            }
            _nonArchivedMealSources.Add(mealSource.Name, mealSource);

            GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

            int siblingStartIndex = (_nonArchivedMealSources.IndexOfKey(mealSource.Name) * _content.constraintCount) + _content.constraintCount;
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

            mealNameText.GetComponent<TextMeshProUGUI>().text = mealSource.Name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.ServingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.Calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.Description;
        }

        protected override void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e)
        {
            var archivedMealSource = _nonArchivedMealSources.Values[e.DestroyedRowIndex];
            archivedMealSource.Archived = true;
            ArchivedMealSources.RemoveAt(ArchivedMealSources.IndexOfKey(archivedMealSource.Name));
            ArchivedMealSources.Add(archivedMealSource.Name, archivedMealSource);
            _nonArchivedMealSources.RemoveAt(e.DestroyedRowIndex);
        }
    }
}
