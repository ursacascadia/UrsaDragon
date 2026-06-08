using System;
using XRL;
using Qud.API;
using XRL.World;
using XRL.World.Conversations;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;

namespace UrsaCompanionHelpers {

[HasConversationDelegate]
public static class UrsaDelegates
{
    public static Type GetRecipeType(string Name)
    {
        return ModManager.ResolveType("XRL.World.Skills.Cooking." + Name);
    }

    // Share recipe e.g. ShareRecipe="AppleMatz"
    [ConversationDelegate]
    public static void ShareRecipe(DelegateContext Context)
    {
        CookingRecipe Recipe = Activator.CreateInstance(GetRecipeType(Context.Value.ToString())) as CookingRecipe;
        Popup.Show(The.Speaker.Does("share") + " the recipe for {{W|" + Recipe.GetDisplayName() + "}}!");
        JournalAPI.AddRecipeNote(Recipe, null, revealed: true, silent: true);
        return;
    }
}

}