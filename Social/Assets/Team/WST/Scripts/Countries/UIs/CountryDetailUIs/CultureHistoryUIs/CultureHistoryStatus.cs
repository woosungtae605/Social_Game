using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CultureHistoryUIs
{
    public class CultureHistoryStatus : MonoBehaviour
    {
        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private CultureHistoryStatusPrefab cultureHistoryStatusPrefab;
        [SerializeField] private int initCount;
        
        private GenericObjectPool<CultureHistoryStatusPrefab> _pool;

        public void Init()
        {
            _pool = new GenericObjectPool<CultureHistoryStatusPrefab>(cultureHistoryStatusPrefab, spawnTransform, initCount);
        }
        
        public void Show(ICultureShowUI iCultureShowUI)
        {
            var histories = iCultureShowUI.CultureHistory;
            _pool.Clear();
            
            if (histories == null)
                return;
            
            foreach (HistoryEventSO history in histories)
            {
                if (history == null)
                    continue;
                
                CultureHistoryStatusPrefab historyPrefab = _pool.Get();
                historyPrefab.SetTitle(history.Title);
                historyPrefab.SetContent(history.Content);
            }
        }
    }
}
