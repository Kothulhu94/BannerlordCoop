# Bannerlord Coop Research Findings

## 1. Initialization Flow

The entry point for the mod is defined in `SubModule.xml` (standard Bannerlord module structure), which points to the `Coop.CoopMod` class.

**Flow:**
1.  **Entry Point**: `Coop.CoopMod` (extends `NoHarmonyLoader`, which extends `MBSubModuleBase`).
2.  **OnSubModuleLoad**:
    *   Redirects assembly bindings (`AssemblyHellscape.CreateAssemblyBindingRedirects`).
    *   Parses command line args (`/server`, `/client`).
    *   Sets up Logging (`SetupLogging`).
    *   Calls `NoHarmonyLoad()`.
3.  **NoHarmonyLoad**:
    *   Initializes `CoopartiveMultiplayerExperience`.
    *   Adds `GameLoopRunner` to updateables.
    *   **Debug Mode**: Automatically sets "Splash Screen Played" to true.
    *   **Menu Setup**: Adds "CoOp Campaign" and "Join Coop Game" options to the main menu via `InitialStateOption`.
4.  **Connection Init**:
    *   When "CoOp Campaign" or "Join" is clicked, it calls `Coop.StartAsServer()` or `Coop.StartAsClient()`.
    *   `CoopartiveMultiplayerExperience` creates a **Dependency Injection Container** (Autofac).
    *   Resolves `INetwork` and `ILogic`.
    *   Starts the logic (`logic.Start()`).

## 2. Harmony Patches

The mod uses a mix of manual patching (via `NoHarmonyLoader` logic) and standard Harmony patches.

**Key Patches by System:**

### **Time & State**
*   `GameInterface.Services.Time.Patches.MapStatePatch`: Patches `MapState.OnMapModeTick` to prevent pausing.
*   `GameInterface.Services.Time.Patches.TimeControlModePatch`
*   `GameInterface.Services.Time.Patches.GameStateManagerPatches`

### **Movement & Party**
*   `Missions.Services.Agents.Packets.MovementHandler` (Packet handling, likely patches agent movement)
*   `GameInterface.Services.MobileParties.Patches.PartyBasePatch`
*   `Missions.Services.Agents.Patches.AgentInteractionPatches`

### **Campaign Mechanics (Disabled/Modified)**
*   `GameInterface.Services.CraftingService.Patches.DisableCraftingCampaignBehavior`
*   `GameInterface.Services.Battles.Patches.DisableDisorganizedStateCampaignBehavior`
*   `GameInterface.Services.Kingdoms.Patches.DisableKingdomDecisionProposalBehavior`
*   `GameInterface.Services.Kingdoms.Patches.DisableVassalAndMercenaryOfferCampaignBehavior`
*   `GameInterface.Services.Villages.Patches.DisableExtortionByDesertersIssueBehavior`
*   `GameInterface.Services.Workshops.Patches.DisableWorkshopsCampaignBehavior`
*   `GameInterface.Services.Towns.Patches.DailyTickTownPatch`

### **Sync & Network**
*   `GameInterface.Services.MobileParties.MobilePartySync`: Defines fields to sync for `MobileParty`.

## 3. Sync Logic & Serialization

The mod handles synchronization using a custom `AutoSync` system built on top of `LiteNetLib` and `ProtoBuf`.

*   **Manager**: `MobilePartySync` (in `GameInterface.Services.MobileParties`).
*   **Mechanism**: Implements `IAutoSync`. Uses `IAutoSyncBuilder` to register specific **Fields** and **Properties** of the `MobileParty` class to be monitored.
    *   *Examples*: `_attachedTo`, `HasUnpaidWages`, `_isDisorganized`, `Ai`, `CurrentSettlement`.
*   **Packet**: `FieldAutoSyncPacket` (in `GameInterface.AutoSync.Fields`).
    *   **Serialization**: Uses `ProtoBuf` (`[ProtoContract]`, `[ProtoMember]`).
    *   **Content**: `instanceId`, `classId`, `fieldId`, `value` (byte array).
*   **Transmission**:
    *   Delivery Method: `ReliableOrdered`.
    *   Network Library: `LiteNetLib`.
