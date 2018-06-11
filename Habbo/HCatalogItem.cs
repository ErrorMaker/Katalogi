using Sulakore.Protocol;

namespace Katalogi.Habbo
{
    public class HCatalogItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public int CostCredits { get; set; }

        public int OtherCurrencyCost { get; set; }
        public int OtherCurrencyType { get; set; }

        public bool AllowGift { get; set; }

        public bool HasBadge { get; set; }
        public string BadgeType { get; set; }
        public string BadgeCode { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }

        public int SpriteId { get; set; }
        public string ExtraData { get; set; }

        public int Amount { get; set; }

        public bool IsLimited { get; set; }

        public int LimitedTotal { get; set; }
        public int LimitedRemaining { get; set; }

        public int SubscriptionStatus { get; set; }

        public bool AllowMultiple { get; set; }

        public HCatalogItem(HMessage packet)
        {
            Id = packet.ReadInteger();
            DisplayName = packet.ReadString();
            packet.ReadBoolean();
            CostCredits = packet.ReadInteger();
            OtherCurrencyCost = packet.ReadInteger();
            OtherCurrencyType = packet.ReadInteger();
            AllowGift = packet.ReadBoolean();
            if(HasBadge = (packet.ReadInteger() == 2))
            {
                BadgeType = packet.ReadString();
                BadgeCode = packet.ReadString();
            }

            Type = packet.ReadString();

            if (HasBadge) Name = packet.ReadString();
            else
            {
                SpriteId = packet.ReadInteger();
                ExtraData = packet.ReadString();
                Amount = packet.ReadInteger();
                if(IsLimited = packet.ReadBoolean())
                {
                    LimitedTotal = packet.ReadInteger();
                    LimitedRemaining = packet.ReadInteger();
                }
            }

            SubscriptionStatus = packet.ReadInteger();
            AllowMultiple = packet.ReadBoolean();

            packet.ReadBoolean();
            packet.ReadString();
        }
    }
}
