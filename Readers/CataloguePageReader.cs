using System;
using Sulakore.Communication;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using System.IO;

namespace Katalogi.Readers
{
    public class CataloguePageReader
    {
        private Form1 form;

        public CataloguePageReader(Form1 form)
        {
            this.form = form;
        }

        internal dynamic ReadEventArgs(DataInterceptedEventArgs obj)
        {
            List<String> texts = new List<String>();
            List<String> images = new List<String>();
            List<dynamic> items = new List<dynamic>();

            dynamic pageData = new ExpandoObject();
            var packet = obj.Packet;

            int pageId = packet.ReadInteger();
            pageData.pageId = pageId;

            string pageType = packet.ReadString();
            pageData.pageType = pageType;

            string pageLayout = packet.ReadString();
            pageData.pageLayout = pageLayout;

            int textsAmount = packet.ReadInteger();
            for (int i = 0; i < textsAmount; i++)
            {
                texts.Add(packet.ReadString());
            }

            int imageAmount = packet.ReadInteger();
            for (int i = 0; i < imageAmount; i++)
            {
                images.Add(packet.ReadString());
            }

            pageData.texts = texts;
            pageData.images = images;

            int itemAmount = packet.ReadInteger();

            for (int i = 0; i < itemAmount; i++)
            {
                dynamic catalogueItem = this.form.ItemReader.ReadEventArgs(obj);
                items.Add(catalogueItem);
            }

            pageData.items = items;

            return pageData;

            //this.form.AppendCataloguePage(pageData);
        }
    }
}