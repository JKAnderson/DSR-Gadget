using Octokit;
using Semver;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private static Properties.Settings settings = Properties.Settings.Default;

        private DSRProcess dsrProcess = null;
        private bool loaded = false;
        private bool reading = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void enableTabs(bool enable)
        {
            foreach (TabPage tab in tclMain.TabPages)
                tab.Enabled = enable;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            Location = settings.WindowLocation;
            Text = "DSR Gadget " + System.Windows.Forms.Application.ProductVersion;
            enableTabs(false);
            initializeAll();

            llbUpdate.Visible = false;
            GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("DSR-Gadget"));
            try
            {
                Release release = await gitHubClient.Repository.Release.GetLatest("JKAnderson", "DSR-Gadget");
                if (SemVersion.Parse(release.TagName) > System.Windows.Forms.Application.ProductVersion)
                {
                    lblUpdate.Text = "New version available!";
                    LinkLabel.Link link = new LinkLabel.Link();
                    link.LinkData = release.HtmlUrl;
                    llbUpdate.Links.Add(link);
                    llbUpdate.Visible = true;
                }
                else
                {
                    lblUpdate.Text = "App up to date";
                }
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is ApiException || ex is ArgumentException)
            {
                lblUpdate.Text = "Current app version unknown";
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                settings.WindowLocation = Location;
            else
                settings.WindowLocation = RestoreBounds.Location;

            saveAll();
            if (dsrProcess != null)
            {
                resetAll();
                dsrProcess.Close();
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (dsrProcess == null)
            {
                DSRProcess result = DSRProcess.GetProcess();
                if (result != null)
                {
                    lblVersionValue.Text = result.Version;
                    lblVersionValue.ForeColor = result.Valid ? Color.DarkGreen : Color.DarkRed;
                    dsrProcess = result;
                    tmrUpdate.Interval = 16;
                }
            }
            else
            {
                if (dsrProcess.Alive())
                {
                    if (dsrProcess.Loaded())
                    {
                        if (!loaded)
                        {
                            lblLoadedValue.Text = "Yes";
                            dsrProcess.LoadPointers();
                            reloadAll();
                            enableTabs(true);
                            loaded = true;
                        }
                        else
                        {
                            updateAll();
                        }
                    }
                    else if (loaded)
                    {
                        lblLoadedValue.Text = "No";
                        enableTabs(false);
                        loaded = false;
                    }
                }
                else
                {
                    tmrUpdate.Interval = 1000;
                    dsrProcess.Close();
                    dsrProcess = null;
                    lblVersionValue.Text = "None";
                    lblVersionValue.ForeColor = Color.Black;
                    lblLoadedValue.Text = "No";
                    enableTabs(false);
                    loaded = false;
                }
            }
        }

        private void initializeAll()
        {
            reading = true;
            initPlayer();
            initStats();
            initItems();
            initCheats();
            initGraphics();
            initHotkeys();
            reading = true;
        }

        private void reloadAll()
        {
            reading = true;
            reloadPlayer();
            reloadStats();
            reloadItems();
            reloadCheats();
            reloadGraphics();
            reloadHotkeys();
            reading = false;
        }

        private void updateAll()
        {
            reading = true;
            updatePlayer();
            updateStats();
            updateItems();
            updateCheats();
            updateGraphics();
            updateHotkeys();
            reading = false;
        }

        private void saveAll()
        {
            savePlayer();
            saveStats();
            saveItems();
            saveCheats();
            saveGraphics();
            saveHotkeys();
        }

        private void resetAll()
        {
            resetPlayer();
            resetStats();
            resetItems();
            resetCheats();
            resetGraphics();
            resetHotkeys();
        }
    }
}
