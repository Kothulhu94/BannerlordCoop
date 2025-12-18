# MobileParty Sync Verification Walkthrough

## 1. Changes Implemented
- **Integration Tests**: Added `Coop.IntegrationTests.MobileParties.MobilePartySyncTest`.
    - Covers 12Synced Fields (e.g., `_attachedTo`, `HasUnpaidWages`, `_disorganizedUntilTime`).
    - Verifies that when these fields change on the Server, a `FieldAutoSyncPacket` is broadcast to all clients.
- **Debug Command**: Added `coop.debug.mobileparty.set_field`.
    - Allows manual modification of private fields via reflection to trigger sync packets in-game.

## 2. Manual Verification (In-Game)

### Prerequisites
- Launch the game with **Bannerlord.Coop** module enabled.
- Loads into a Campaign (Host or Client).
- Enable "Cheat Mode" to access the Developer Console (`Alt + ~`).

### Steps
1.  **Identify a Party**:
    - Use `coop.debug.mobileparty.list` (if available) or standard game commands to find a MobileParty StringID (e.g., `mobileparty_1`).
    - *Tip*: You can often use the player's party ID if you know it, or spawn one.

2.  **Modify a Field (Server Side)**:
    - Run the command to change a synced field.
    - **Command**: `coop.debug.mobileparty.set_field <PartyId> <FieldName> <Value>`
    - **Example**:
        ```bash
        coop.debug.mobileparty.set_field mobileparty_1 _partyTradeGold 5000
        ```
    - **Expected Result**:
        - Server console should confirm the set.
        - `FieldAutoSyncPacket` should be generated (visible in logs if Verbose logging is on).

3.  **Verify on Client**:
    - Check the same party on the Client.
    - The value (e.g., `_partyTradeGold`) should update to `5000`.

## 3. Automated Verification

- The class `MobilePartySyncTest.cs` contains `[Fact]` tests for all enabled fields.
- These verify the **Network Infrastructure** (Server -> Broadcast) is correctly wired for the `MobileParty` class.
