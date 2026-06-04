using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.Effects;
using XRL.Rules;

namespace XRL.World.Skills.Cooking
{
	[Serializable]
	public class Ursa_AzhRofMeal : CookingRecipe
	{
		public Ursa_AzhRofMeal()
		{
			Components.Add(new PreparedCookingRecipieComponentBlueprint("Barkbiter Jerky", null, 1));
			Components.Add(new PreparedCookingRecipieComponentLiquid("lava", 1));
			Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { nameof(CookingDomainHP_UnitHP), nameof(Ursa_HeatResistEffect) })));
		}

		public override string GetDescription() => "warmth, bonus max HP";

		public override string GetApplyMessage() => string.Empty;

		public override string GetDisplayName() => "Azh's recipe";
	}
}

namespace XRL.World.Effects
{
    [Serializable]
    public class Ursa_HeatResistEffect : ProceduralCookingEffectUnit
    {
        public int Bonus;

        public override string GetDescription()
        {
            return Bonus.Signed() + " Heat Resistance";
        }

        public override string GetTemplatedDescription()
        {
            return "+70-80 Heat Resistance";
        }

        public override void Init(GameObject target)
        {
            Bonus = Stat.Random(70, 80);
        }

        public override void Apply(GameObject Object, Effect parent)
        {
            Object.Statistics["HeatResistance"].Bonus += Bonus;
        }

        public override void Remove(GameObject Object, Effect parent)
        {
            Object.Statistics["HeatResistance"].Bonus -= Bonus;
            Bonus = 0;
        }
    }

}