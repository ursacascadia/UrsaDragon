using System;
using XRL;
using XRL.Core;
using XRL.World;

namespace XRL.World.Parts {

[Serializable]
public class UrsaPostQuest : IPart
{
    public override bool WantEvent(int ID, int cascade)
    {
        if (!base.WantEvent(ID, cascade))
        {
            return ID == ZoneActivatedEvent.ID;
        }
        return true;
    }

    public override bool HandleEvent(ZoneActivatedEvent E)
    {
        if (The.Game.GetBooleanGameState("Ursa_PostQuestTimerFinished")) {
            ParentObject.CurrentZone.GetCell(36, 9).AddObject("Ursa_AzhdahakDenPainting");
            ParentObject.CurrentZone.GetCell(38, 8).AddObject("Ursa_AzhdahakDenOven");
            ParentObject.CurrentZone.GetCell(41, 8).GetFirstObject("Bedroll")?.Obliterate();
            ParentObject.CurrentZone.GetCell(41, 8).AddObject("Bed");
            ParentObject.RemovePart<UrsaPostQuest>();
        }
        return base.HandleEvent(E);
    }

    public override void Register(GameObject Object, IEventRegistrar Registrar)
    {
        Registrar.Register("StartPostQuestTimer");
        base.Register(Object, Registrar);
    }

    public override bool FireEvent(Event E)
    {
        if (E.ID == "StartPostQuestTimer")
        {
            var timerPart = new Sibs_BoolStateTimer();
            timerPart.startTurn = Calendar.TotalTimeTicks;
            timerPart.state = "Ursa_PostQuestTimerFinished";
            timerPart.targetTurns = Calendar.TurnsPerDay * 7;
            The.Player.AddPart(timerPart);
        }
        return base.FireEvent(E);
    }

}

}