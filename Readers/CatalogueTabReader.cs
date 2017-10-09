using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sulakore.Protocol;

namespace Katalogi.Readers
{
    public class CatalogueTabReader
    {
        private static Random random = new Random();
        private Form1 form;

        public CatalogueTabReader(Form1 form)
        {
            this.form = form;
        }

        internal dynamic ReadEventArgs(DataInterceptedEventArgs obj)
        {
            dynamic tab = this.ReadTabData(obj);
            return tab;
        }

        private dynamic ReadTabData(DataInterceptedEventArgs obj)
        {
            var packet = obj.Packet;
            dynamic tabData = new ExpandoObject();

            tabData.tabs = new List<dynamic>();

            tabData.isEnabled = packet.ReadBoolean();

            tabData.iconImage = packet.ReadInteger();
            tabData.id = packet.ReadInteger();

            if (tabData.id == -1)
            {
                tabData.id = random.Next(10000, 30000);
            }

            this.form.PageIds.Add(tabData.id);//.RequestPage(tabData.id, obj);

            tabData.pageLink = packet.ReadString();
            tabData.pageCaption = packet.ReadString();

            int items = packet.ReadInteger();
            for (int i = 0; i < items; i++)
            {
                packet.ReadInteger();
            }

            int subPages = packet.ReadInteger();
            for (int i = 0; i < subPages; i++)
            {
                dynamic nextTab = this.ReadTabData(obj);
                tabData.tabs.Add(nextTab);
            }

            return tabData;
        }

        /*private void ReadChildTabs(dynamic parentTab, HMessage packet)
        {
            List<dynamic> tabs = new List<dynamic>();

            int childTabs = packet.ReadInteger();

            for (int i = 0; i < childTabs; i++)
            {
                dynamic tab = this.ReadTabData(packet);
                tabs.Add(tab);

                if (tab.isEnabled)
                {
                    this.ReadChildTabs(tab, packet);
                }
            }

            parentTab.tabs = tabs;
        }*/

    }
}
