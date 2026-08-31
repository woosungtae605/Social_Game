using System.Collections;
using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.NewsUIs
{
    public class NewsStatus : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private NewsItemView newsView;

        [Header("ticker")]
        [SerializeField] private float pixelsPerSecond = 80f;

        private readonly Queue<QueuedNews> _queue = new();
        private Coroutine _playRoutine;

        private readonly struct QueuedNews
        {
            public readonly string Title;
            public readonly string Content;

            public QueuedNews(string title, string content)
            {
                Title = title;
                Content = content;
            }
        }

        private void Awake()
        {
            Bus<AddHistoryEvent>.OnEvent += HandleAddHistory;
        }

        private void OnDestroy()
        {
            Bus<AddHistoryEvent>.OnEvent -= HandleAddHistory;
            if (_playRoutine != null)
                StopCoroutine(_playRoutine);
        }

        private void HandleAddHistory(AddHistoryEvent evt)
        {
            HistoryEventSO history = evt.HistoryEvent;
            if (history == null || newsView == null)
                return;

            if (countryManager == null || !countryManager.TryGetCountry(evt.CountryType, out AbstractCountry country))
                return;

            _queue.Enqueue(new QueuedNews($"({country.DisplayName}) {history.Title}", history.Content));
            if (_playRoutine == null)
                _playRoutine = StartCoroutine(PlayQueue());
        }

        private IEnumerator PlayQueue()
        {
            while (_queue.Count > 0)
            {
                QueuedNews news = _queue.Dequeue();
                newsView.SetTitle(news.Title);
                newsView.SetContent(news.Content);
                yield return newsView.PlayTickers(pixelsPerSecond);
            }

            newsView.SetTitle(string.Empty);
            newsView.SetContent(string.Empty);
            _playRoutine = null;
        }
    }
}
