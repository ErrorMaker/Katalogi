using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalogi.Readers
{
    public class CatalogueItemReader
    {
        private Form1 form;

        public CatalogueItemReader(Form1 form)
        {
            this.form = form;
        }

        internal dynamic ReadEventArgs(DataInterceptedEventArgs obj)
        {
            var packet = obj.Packet;
            dynamic itemData = new ExpandoObject();

            int itemId = packet.ReadInteger();
            itemData.itemId = itemId;

            string displayName = packet.ReadString();
            itemData.displayName = displayName;

            packet.ReadBoolean(); // ??

            int costCredits = packet.ReadInteger();
            itemData.costCredits = costCredits;

            int otherCurrencyCost = packet.ReadInteger();
            int otherCurrencyType = packet.ReadInteger();

            if (otherCurrencyCost > 0)
            {
                itemData.otherCurrencyType = otherCurrencyType;
                itemData.otherCurrencyCost = otherCurrencyCost;
            }

            bool allowGift = packet.ReadBoolean();
            itemData.allowGift = allowGift;

            bool hasBadge = (packet.ReadInteger() == 2);

            if (hasBadge)
            {
                string badgeType = packet.ReadString();
                itemData.badgeType = badgeType;

                string badgeCode = packet.ReadString();
                itemData.badgeCode = badgeCode;
            }

            string itemType = packet.ReadString();
            itemData.itemType = itemType;

            if (hasBadge)
            {
                string itemName = packet.ReadString();
                itemData.itemName = itemName;
            }
            else
            {
                int spriteId = packet.ReadInteger();
                itemData.spriteId = spriteId;

                string extraData = packet.ReadString();
                itemData.extraData = extraData;

                int amount = packet.ReadInteger();
                itemData.amount = amount;

                bool isLimited = packet.ReadBoolean();

                if (isLimited)
                {
                    int limitedTotal = packet.ReadInteger();
                    itemData.limitedTotal = limitedTotal;

                    int limitedRemaining = packet.ReadInteger();
                    itemData.limitedRemaining = limitedRemaining;
                }

            }
            int subscriptionStatus = packet.ReadInteger();
            itemData.subscriptionStatus = subscriptionStatus;

            bool purchaseMulitiple = packet.ReadBoolean();
            itemData.purchaseMultiple = purchaseMulitiple;

            packet.ReadBoolean(); // dunno m8
            packet.ReadString(); // this is important but forgot

            return itemData;
        }
    }
}
