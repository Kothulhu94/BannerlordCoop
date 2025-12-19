using TaleWorlds.CampaignSystem;
using Xunit;
using System;
using System.Reflection;
using System.Linq;

namespace GameInterface.Tests
{
    public class TypeInspectionTests
    {
        [Fact(Skip = "Manual inspection tool")]
        public void FindMissingFields()
        {
            var types = new[] { 
                typeof(TaleWorlds.CampaignSystem.Clan),
                typeof(TaleWorlds.CampaignSystem.Party.MobilePartyAi),
                typeof(TaleWorlds.CampaignSystem.Kingdom),
                typeof(TaleWorlds.CampaignSystem.Roster.TroopRoster)
            };
            
            string found = "";

            foreach(var type in types)
            {
                found += $"--- {type.FullName} ---\n";
                var fields = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.FlattenHierarchy);
                foreach(var field in fields)
                {
                    found += $"{field.Name}: {field.FieldType.FullName}\n";
                }
                found += "\n";
            }
            
            Assert.Fail(found);
        }
    }
}
