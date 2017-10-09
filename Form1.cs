using System;
using System.IO;
using System.Dynamic;
using System.Collections.Generic;
using System.Windows.Forms;

using Tangine;
using Sulakore.Modules;
using Sulakore.Communication;

using Newtonsoft.Json;
using Katalogi.Readers;

namespace Katalogi
{
    [Module("Katalogi", "Generates JSON data from packets received from the remote server.")]
    [GitHub("TheAmazingAussie", "Katalogi")]
    [Author("Alex", HabboName = "", Hotel = Sulakore.Habbo.HHotel.Com, ResourceName = "Follow on Twitter", ResourceUrl = "https://twitter.com/AmazingAussie")]
    public partial class Form1 : ExtensionForm
    {
        public readonly ushort CATALOGUE_PAGE_EVENT = 2412;
        public readonly ushort CATALOGUE_TAB_EVENT = 808;

        public readonly ushort CATALOGUE_PAGE_REQUEST = 2148;

        public CataloguePageReader PageReader { get; set; }
        public CatalogueItemReader ItemReader { get; set; }
        public CatalogueTabReader TabReader { get; set; }

        public List<int> PageIds = new List<int>();
        public Dictionary<int, int> ParentIds = new Dictionary<int, int>();

        private Dictionary<int, dynamic> pages = new Dictionary<int, dynamic>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Log("Katalogi: The Catalogue Ripper");
            this.Log("Written by Quackster", "");

            try
            {
                this.SetupTanji();
                this.Log("Successfully hooked into Tanji");
            }
            catch (Exception ex)
            {
                this.Log("Error occurred for whatever reason: ", ex);
            }
        }

        public void SetupTanji()
        {
            this.PageReader = new CataloguePageReader(this);
            this.ItemReader = new CatalogueItemReader(this);
            this.TabReader = new CatalogueTabReader(this);

            Triggers.InAttach(this.CATALOGUE_PAGE_EVENT, HandleCataloguePageMessageEvent);
            Triggers.InAttach(this.CATALOGUE_TAB_EVENT, HandleCatalogueTabMessageEvent);
        }

        public void HandleCataloguePageMessageEvent(DataInterceptedEventArgs obj)
        {
            dynamic cataloguePage = null;

            try
            {
                cataloguePage = this.PageReader.ReadEventArgs(obj);
            }
            catch (Exception ex)
            {
                this.Log("Could not parse the following catalogue page due to: ", ex);
            }

            if (cataloguePage != null)
            {
                this.AppendCataloguePage(cataloguePage);
            }
        }


        public void HandleCatalogueTabMessageEvent(DataInterceptedEventArgs obj)
        {
            dynamic catalogueTabs = null;

            try
            {
                catalogueTabs = this.TabReader.ReadEventArgs(obj);
            }
            catch (Exception ex)
            {
                this.Log("Could not parse the following catalogue tabs due to: ", ex);
            }

            if (catalogueTabs != null)
            {
                foreach (var kvp in ParentIds)
                {
                    Log("] > " + kvp.Key + " - " + kvp.Value);
                }

                this.AppendCatalogueTabs(catalogueTabs);
            }
        }


        public void AppendCataloguePage(dynamic obj)
        {
            this.pages[obj.pageId] = obj;

            dynamic cataloguePages = new ExpandoObject();
            cataloguePages.pages = this.pages.Values;

            var json = JsonConvert.SerializeObject(cataloguePages);
            var jsonPretty = JsonConvert.SerializeObject(cataloguePages, Formatting.Indented);

            File.WriteAllText("catalogue_pages.json", json);
            File.WriteAllText("catalogue_pages_beautify.json", jsonPretty);
        }

        public void AppendCatalogueTabs(dynamic obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText("catalogue_tabs.json", json);

            var jsonPretty = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText("catalogue_tabs_beautify.json", jsonPretty);
        }

        public void Log(params object[] text)
        {
            foreach (Object textWrite in text)
            {
                ThreadHelperClass.SetText(this, this.textBox1, " [" + DateTime.Now + "] " + textWrite.ToString() + Environment.NewLine);
            }
        }
    }

    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        public static void SetText(Form form, Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    textBox.AppendText(text);
                }
            }
        }
    }
}