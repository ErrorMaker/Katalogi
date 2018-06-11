using System;
using System.IO;
using System.Dynamic;
using System.Collections.Generic;
using System.Windows.Forms;

using Tangine;
using Sulakore.Habbo;
using Sulakore.Modules;
using Sulakore.Communication;

using Katalogi.Habbo;

using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Katalogi
{
    [Module("Katalogi", "Generates JSON data from packets received from the remote server.")]
    [Author("Alex", HabboName = "", Hotel = HHotel.Com, ResourceName = "Follow on Twitter", ResourceUrl = "https://twitter.com/AmazingAussie")]
    public partial class MainFrm : ExtensionForm
    {
        public override bool IsRemoteModule => true;

        [MessageId("38546bede9212e61c174caac0e46eca9")]
        public ushort InitCatalog { get; set; }

        private bool _hasHeaders;
        private Dictionary<int, HCatalogPage> _pages;
        private TaskCompletionSource<HCatalogTab> _catalogTabCompletionSource;

        public MainFrm()
        {
            _pages = new Dictionary<int, HCatalogPage>();
            _catalogTabCompletionSource = new TaskCompletionSource<HCatalogTab>();

            InitializeComponent();

            if (_hasHeaders = (Game != null))
                SetCallbacks();
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            Log("Katalogi: The Catalogue Ripper");
            Log("Written by Quackster");
        }

        private void SetCallbacks()
        {
            Triggers.InAttach(In.CatalogPage, HandleCatalogPage);
            Triggers.InAttach(In.CatalogPagesList, HandleCatalogTabs);
        }

        private void HandleCatalogPage(DataInterceptedEventArgs e)
        {
            try {
                var page = new HCatalogPage(e.Packet);

                if (!_pages.ContainsKey(page.Id))
                    _pages.Add(page.Id, page);
                else Log($"Page already loaded..");

                Invoke((MethodInvoker)delegate
                {
                    SavePagesBtn.Text = $"Save Pages ({_pages.Count})";
                });

                Log($"Loaded page with id {page.Id}. Item count: {page.Items?.Count ?? 0}");
            }catch (Exception ex)
            {
                Log("Probably not supported catalog page.");
            }
            
        }
        public void HandleCatalogTabs(DataInterceptedEventArgs e)
        {
            _catalogTabCompletionSource.TrySetResult(new HCatalogTab(e.Packet));

            Log($"Loaded catalog tabs.");
        }

        private void Log(string text)
        {
            Invoke((MethodInvoker)delegate {
                LogTB.AppendText($"[{DateTime.Now}] {text}{Environment.NewLine}");
            });
        }

        private void SavePagesBtn_Click(object sender, EventArgs e)
        {
            SaveJson(_pages.Values, "catalogue_pages");

            MessageBox.Show("Saved all page data as JSON!", "Success!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void SaveTabTreeBtn_Click(object sender, EventArgs e)
        {
            await Connection.SendToServerAsync(InitCatalog, "NORMAL");

            var rootTab = await _catalogTabCompletionSource.Task;
            _catalogTabCompletionSource = new TaskCompletionSource<HCatalogTab>();

            SaveJson(rootTab, "catalogue_tabs");

            MessageBox.Show("Saved the tab tree as JSON!", "Success!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveJson(object @object, string fileName)
        {
            var json = JsonConvert.SerializeObject(@object);
            var jsonPretty = JsonConvert.SerializeObject(@object, Formatting.Indented);

            File.WriteAllText(fileName + ".json", json);
            File.WriteAllText(fileName + "_beautify.json", jsonPretty);
        }

        public override void ModifyGame(HGame game)
        {
            if (!_hasHeaders)
                SetCallbacks();

            base.ModifyGame(game);
        }
    }
}