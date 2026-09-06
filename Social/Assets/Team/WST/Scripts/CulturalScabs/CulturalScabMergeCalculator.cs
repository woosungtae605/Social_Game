using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public enum CulturalScabMergeOutcome
    {
        Success,
        Fail,
        Destroy
    }

    public readonly struct CulturalScabMergeResult
    {
        public readonly float TotalUniqueness;
        public readonly float ExtraPower;
        public readonly CulturalScabMergeOutcome Outcome;

        public bool Succeeded => Outcome == CulturalScabMergeOutcome.Success;
        public bool Destroyed => Outcome == CulturalScabMergeOutcome.Destroy;

        public CulturalScabMergeResult(
            float totalUniqueness,
            float extraPower,
            CulturalScabMergeOutcome outcome)
        {
            TotalUniqueness = totalUniqueness;
            ExtraPower = extraPower;
            Outcome = outcome;
        }
    }

    public static class CulturalScabMergeCalculator
    {
        public const float SuccessChance = 0.60f;
        public const float FailChance = 0.30f;
        public const float SuccessMinMultiplier = 1f;
        public const float SuccessMaxMultiplier = 1.5f;
        public const float FailWowMin = -0.6666f;

        public static CulturalScabMergeResult Combine(float selfUniqueness, float otherUniqueness)
        {
            float roll = Random.value;
            if (roll < SuccessChance)
                return RollSuccess(selfUniqueness, otherUniqueness);

            if (roll < SuccessChance + FailChance)
                return RollFail(selfUniqueness, otherUniqueness);

            return RollDestroy(selfUniqueness);
        }

        private static CulturalScabMergeResult RollSuccess(float selfUniqueness, float otherUniqueness)
        {
            float total =
                selfUniqueness * Random.Range(SuccessMinMultiplier, SuccessMaxMultiplier) +
                otherUniqueness * Random.Range(SuccessMinMultiplier, SuccessMaxMultiplier);

            return new CulturalScabMergeResult(
                total,
                total - selfUniqueness,
                CulturalScabMergeOutcome.Success);
        }

        private static CulturalScabMergeResult RollFail(float selfUniqueness, float otherUniqueness)
        {
            float extraPower = otherUniqueness * Random.Range(FailWowMin, 0f);
            float total = Mathf.Max(1f, selfUniqueness + extraPower);
            return new CulturalScabMergeResult(
                total,
                total - selfUniqueness,
                CulturalScabMergeOutcome.Fail);
        }

        private static CulturalScabMergeResult RollDestroy(float selfUniqueness)
        {
            return new CulturalScabMergeResult(
                0f,
                -selfUniqueness,
                CulturalScabMergeOutcome.Destroy);
        }
    }
}
