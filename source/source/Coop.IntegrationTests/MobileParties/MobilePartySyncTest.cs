using Coop.IntegrationTests.Environment;
using Coop.IntegrationTests.Environment.Instance;
using GameInterface.AutoSync.Fields;
using TaleWorlds.CampaignSystem.Party;
using Xunit;
using System.Linq;

namespace Coop.IntegrationTests.MobileParties;

/// <summary>
/// Verified that MobileParty fields are correctly synced from Server to Clients.
/// </summary>
public class MobilePartySyncTest
{
    internal TestEnvironment TestEnvironment { get; } = new TestEnvironment();

    [Fact]
    public void ServerChange_AttachedTo_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _attachedTo (MobileParty)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 0, new byte[] { 0x01 }); 

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_HasUnpaidWages_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: HasUnpaidWages (float)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 1, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_DisorganizedUntilTime_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _disorganizedUntilTime (CampaignTime)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 2, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_PartySizeRatioLastCheckVersion_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _partySizeRatioLastCheckVersion (int)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 3, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_CachedPartySizeRatio_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _cachedPartySizeRatio (float)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 4, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_DoNotAttackMainParty_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _doNotAttackMainParty (int)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 5, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_CustomHomeSettlement_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _customHomeSettlement (Settlement)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 6, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_IsDisorganized_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _isDisorganized (bool)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 7, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_IsCurrentlyUsedByAQuest_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _isCurrentlyUsedByAQuest (bool)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 8, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_PartyTradeGold_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _partyTradeGold (int)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 9, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_IgnoredUntilTime_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _ignoredUntilTime (CampaignTime)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 10, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }

    [Fact]
    public void ServerChange_BesiegerCampResetStarted_PublishesToAllClients()
    {
        var partyId = "MobileParty_1";
        // Field: _besiegerCampResetStarted (bool)
        var triggerMessage = new FieldAutoSyncPacket(partyId, 0, 11, new byte[] { 0x01 });

        var server = TestEnvironment.Server;
        server.SimulateMessage(this, triggerMessage);

        Assert.Equal(1, server.NetworkSentMessages.GetMessageCount<FieldAutoSyncPacket>());
        foreach (EnvironmentInstance client in TestEnvironment.Clients)
        {
            Assert.Equal(1, client.InternalMessages.GetMessageCount<FieldAutoSyncPacket>());
        }
    }
}
