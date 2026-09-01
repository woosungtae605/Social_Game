using Team.WST.Scripts.Countries.Interfaces;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class BaseCountry : AbstractCountry, ICultureReceiver
    {
        [SerializeField] private float spreadInterval = 2f;
        private float _elapsed;
        private void Update()
        {
            _elapsed += Time.deltaTime;
            
            if (_elapsed < spreadInterval)
                return;
            
            _elapsed = 0f;
            Spread();
        }
    }
}