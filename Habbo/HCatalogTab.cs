using Sulakore.Protocol;
using System.Collections.Generic;

namespace Katalogi.Habbo
{
    public class HCatalogTab
    {
        public bool IsEnabled { get; set; }
        public int IconImage { get; set; }
        public int PageId { get; set; }
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Link { get; set; }
        public string Caption { get; set; }

        public List<int> Items { get; set; }

        public List<HCatalogTab> SubPages { get; set; }

        public HCatalogTab(HMessage packet, int parentId = -1, int incrementId = -1)
        {
            IsEnabled = packet.ReadBoolean();
            IconImage = packet.ReadInteger();
            PageId = packet.ReadInteger();

            ParentId = parentId;
            Id = incrementId++;

            Link = packet.ReadString();
            Caption = packet.ReadString();

            Items = new List<int>(packet.ReadInteger());
            for(int i = 0; i < Items.Capacity; i++)
            {
                Items.Add(packet.ReadInteger());
            }

            SubPages = new List<HCatalogTab>(packet.ReadInteger());
            for (int i = 0; i < SubPages.Capacity; i++)
            {
                SubPages.Add(new HCatalogTab(packet, Id, incrementId));
            }
        }
    }
}
