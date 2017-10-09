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
        private static int increment = -1;

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

            tabData.pageId = packet.ReadInteger();

            tabData.parentId = -1;
            tabData.id = increment;
            increment++;

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
                dynamic tab = this.ReadTabData(obj);
                tab.parentId = tabData.id;

                tabData.tabs.Add(tab);
            }

            return tabData;
        }

        /*        private dynamic ReadTabData(DataInterceptedEventArgs obj)
        {
            var packet = obj.Packet;
            dynamic tabData = new ExpandoObject();
       
            tabData.tabs = new List<dynamic>();
     
            tabData.isEnabled = packet.ReadBoolean();
            tabData.iconImage = packet.ReadInteger();

            tabData.parentId = -1;
            tabData.id = packet.ReadInteger();

            if (tabData.id == -1)
            {
                tabData = incrementId;
                incrementId++;
            }

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
                dynamic newTab = this.ReadTabData(obj);
                newTab.parentId = tabData.id;
                tabData.tabs.Add(newTab);
            }

            return tabData;
        }*/

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
