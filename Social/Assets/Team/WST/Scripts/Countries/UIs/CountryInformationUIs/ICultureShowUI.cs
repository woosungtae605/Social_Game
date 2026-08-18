using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.Informations.Histories;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryInformationUIs
{
    public interface ICultureShowUI
    {
        public string DisplayName { get; }
        int AllCulturePower { get; }
        IReadOnlyDictionary<CountryType, int> CulturePowerDict { get; }
        IReadOnlyList<HistoryEventSO> CultureHistory { get; }
        Sprite DisplaySprite { get; }
        Color DisplayColor { get; }
        CountryType CountryType { get; }
    }
}