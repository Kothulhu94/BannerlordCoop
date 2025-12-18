# Implementation Plan - MobileParty Sync & Tests

## Goal
Ensure `MobileParty` fields are synced correctly using integration tests and debug commands, strictly following the provided templates and "Definition of Done".

## User Review Required
> [!IMPORTANT]
> - **Server side only**: I will focus usage on Server -> Client propagation.
> - **Templates**: Using `TestTemplate.cs` for integration tests and `TemplateCommands.cs` for debug commands.
> - **Definition of Done**: Ensuring class-level comments and XUnit tests for non-game-logic methods.

## Proposed Changes

### GameInterface
#### [MODIFY] [MobilePartySync.cs](file:///d:/Bannerlord_Coop/Source/source/GameInterface/Services/MobileParties/MobilePartySync.cs)
- Reference current source of truth for synced fields.
- Ensure all relevant fields are uncommented if valid and safe.

#### [MODIFY] [MobilePartyDebugCommand.cs](file:///d:/Bannerlord_Coop/Source/source/GameInterface/Services/MobileParties/Commands/MobilePartyDebugCommand.cs)
- Implement `SetField` command following `TemplateCommands.cs` structure (though `TemplateCommands.cs` is very bare).
- Command: `coop.debug.mobileparty.set_field <PartyId> <FieldName> <Value>`
- This command allows changing private fields via reflection to trigger the AutoSync mechanism for testing.

### Coop.IntegrationTests
#### [MODIFY] [MobilePartySyncTest.cs](file:///d:/Bannerlord_Coop/Source/source/Coop.IntegrationTests/MobileParties/MobilePartySyncTest.cs)
- **Structure**: Adapt `TestTemplate.cs` logic.
- **Scope**: Create a specific test method for **each** synced field in `MobilePartySync.cs`.
- **Logic**:
    *   Setup Server and Clients.
    *   Simulate a `FieldAutoSyncPacket` on the Server (mimicking a field change detection).
    *   Assert that the packet is broadcast to all Clients.
    *   (Optionally) If possible, verify the `MobileParty` instance on client updates (requires mocking/more complex setup, but checking the packet transmission is the core infrastructure test).
- **Test Names**: `ServerChange_[FieldName]_PublishesToAllClients`.

## Verification Plan

### Automated Tests
- `dotnet build` to ensure compilation.
- `dotnet test` (if environment permits) to run `MobilePartySyncTest.cs`.

### Manual Verification
- User to run the debug command `coop.debug.mobileparty.set_field` in-game and verify value changes propagate (if they have a way to inspect it on client).
