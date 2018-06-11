using Sulakore.Protocol;
using System.Collections.Generic;

namespace Katalogi.Habbo
{
    public class HCatalogPage
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Layout { get; set; }

		public List<string> Images { get; set; }

        public List<string> Texts { get; set; }

		public List<HCatalogItem> Items { get; set; }

        public HCatalogPage(HMessage packet)
        {
            Id = packet.ReadInteger();
            Type = packet.ReadString();
            Layout = packet.ReadString();

            Images = new List<string>(packet.ReadInteger());
			for(int i = 0; i < Images.Capacity; i++)
            {
                Images.Add(packet.ReadString());
            }

            Texts = new List<string>(packet.ReadInteger());
            for (int i = 0; i < Texts.Capacity; i++)
            {
                Texts.Add(packet.ReadString());
            }

            Items = new List<HCatalogItem>(packet.ReadInteger());
            for (int i = 0; i < Items.Capacity; i++)
            {
                Items.Add(new HCatalogItem(packet));
            }
        }
    }
}
