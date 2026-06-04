using XRL.World.Parts;

namespace XRL.World.Conversations.Parts {

public class UrsaStartPostQuest : IConversationPart
{
    public override bool WantEvent(int ID, int Propagation)
    {
        if (!base.WantEvent(ID, Propagation))
        {
            return ID == EnteredElementEvent.ID;
        }
        return true;
    }

    public override bool HandleEvent(EnteredElementEvent E)
    {
        The.Speaker.RequirePart<UrsaPostQuest>(); // Give Azh this part
        The.Speaker.FireEvent("StartPostQuestTimer");
        Pettable part = The.ActiveZone.FindFirstObject("Ursa_Rofwufufuf")?.RequirePart<Pettable>();
         if (part is not null) {
            part.UseFactionForFeelingFloor  = "Pariahs";
        }
        return base.HandleEvent(E);
    }
}

}