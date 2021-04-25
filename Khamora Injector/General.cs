using ManualMapInjection.Injection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Khamora_Injector
{
    public partial class General : Form
    {
        public General()
        {
            InitializeComponent();
        }
        private List<String> CheatList;
        private String FilePath = "";
        public async Task UpdateColors()
        {
            guna2VSeparator1.FillColor = Properties.Settings.Default.AccentColor;
            ProcessBox.ForeColor = Properties.Settings.Default.AccentColor;
            SelectedProcessLabel.ForeColor = Properties.Settings.Default.AccentColor;
            SelectedDLLLabel.ForeColor = Properties.Settings.Default.AccentColor;
            CloseAfterInjectionToggle.CheckMarkColor = Properties.Settings.Default.AccentColor;
            TopMostToggle.CheckMarkColor = Properties.Settings.Default.AccentColor;
            logInStatusBar20.RectangleColor = Properties.Settings.Default.AccentColor;
            label1.ForeColor = Properties.Settings.Default.AccentColor;
            AccentColorPanel.BackColor = Properties.Settings.Default.AccentColor;

            logInStatusBar20.Hide();
            logInStatusBar20.Show();

        }
        private void GrabProcessesButton_Click(object sender, EventArgs e)
        {
            string[] excludeProcesses = 
            { 
                "vcredist", 
                "opera",
                "dllhost",
                "node",
                "Code",
                "PerfWatson2",
                "svchost",
                "unsecapp",
                "MSBuild",
                "WavesSysSvc62",
                "Microsoft.ServiceHub.Controller",
                "SearchFilterHost",
                "ServiceHub.Host.CLR.x86",
                "AdobeIPCBroker",
                "SecurityHealthService",
                "Time",
                "Skype",
                "QALockHandler",
                "csrss",
                "SgrmBroker",
                "wininit",
                "SearchProtocolHost",
                "Secure System",
                "ServiceHub.RoslynCodeAnalysisService",
                "ctfmon",
                "PSAdminAgent",
                "igfxCUIService",
                "MsMpEng",
                "NVIDIA Share",
                "ShareX",
                "sqlwriter",
                "ePowerButton_NB",
                "ServiceHub.ThreadedWaitDialog",
                "StandardCollector.Service",
                "vpnclient_x64",
"HelpPane",
"winlogon",
"ServiceHub.SettingsHost",
"httpd",
"devenv",
"explorer",
"ServiceHub.VCDetouredHost",
"Spotify",
"conhost",
"ServiceHub.IdentifyHost",
"RuntimeBroker",
"Registry",
"TextInputHost",
"SearchApp",
"nvsphelper64",
"YourPhone",
"wlanext",
"PSAGent",
"lsass",
"Lealso",
"RtkAudUService64",
"PresentationFontCache",
"System",
"Idle",
"idle",
"Memory Compression",
"taskhostw",
"nvcontainer",
"CCXProcess",
"ACCStd",
"smartscreen",
"UBTService",
"Video.UI",
"jhi_service",
"NVDisplay.Container",
"fontdrvhost",
"services",
"RstMwService",
"ServiceHub.IdentityHost",
"ServiceHub.TestWindowStoreHost",
"SecurityHealthSystray",
"rundll32",
"SecurityHealthHost",
"ServiceHub.VSDetouredHost",
"openvpnserv",
"igfxEM",
"IAStorDataMgrSvc",
"igfxext",
"AcerRegistrationBackGroundTask",
"uUserOOBEBroker",
"Music.UI",
"notepad",
"IAStorIcon",
"spoolsv",
"PSSvc",
"SecHealthUI",
"WinStore.App",
"StartMenuExperienceHost",
"SearchIndexer",
"VBCSCompiler",
"QAAdminAgent",
"Lsalso",
"PSAGent",
"GoogleCrashHandler",
"QASvc",
"ScriptedSandbox64",
"ApplicationFrameHost",
"AppMonitorPlugin",
"WmiPrvSE",
"MoUsoCoreWorker",
"IntelCpHeciSVC",
"dwm",
"LMS",
"LMIGuardianSvc",
"hamachi-2",
"NVIDIA Web Helper",
"smss",
"ACCSvc",
"IntelCpHDCPSvc",
"opera_crashreporter",
"SystemSettings",
"WavesSysSvc64",
"RtkUWP",
"QAAgent",
"ServiceHub.DataWarehouseHost",
"sihost",
"xampp-control",
"audiodg",
"UserOOBEBroker",
"ShellExperienceHost",
"AppMonitorPlugIn",
"IntelCpHeciSVC",
"IntelCpHecISVC",
"GoogleCrashHandler64",
"PSAgent.",
"SettingsSyncHost",
"LsaLso",
"Lsalso",
"LsaIso",
"Lsaiso",
"SettingSyncHost",
"IntelCpHeciSvc",
"intelCpHeciSvc",
"IntelCpHecLSvc",
"IntelCpHeclSvc",
"IntelCpHecISvc",
"WavesSvc64",
"PSAgent",
"steamwebhelper",
"cmd",
"SteamService",
"GameBar",
"GameBarPresenceWriter",
"ffmpeg",
"GameBarFTServer",
"GameOverlayUI",
"obs-browser-page",
"get-graphics-offsets64",
"obs64",
"FileCoAuth"
          };

            Process[] allProc = Process.GetProcesses();
            foreach (Process p in allProc)
            {
                if (!(excludeProcesses.Contains(p.ProcessName)))
                {
                    ProcessBox.Items.Add(p.ProcessName);
                    var _items = this.ProcessBox.Items.Cast<string>().Distinct().ToArray();
                    this.ProcessBox.Items.Clear();
                    foreach (var item in _items)
                    {
                        this.ProcessBox.Items.Add(item);
                    }
                }
            }
        }

        private void ProcessBox_Click(object sender, EventArgs e)
        {
            if (ProcessBox.SelectedItem != null)
            {
                SelectedProcessLabel.Text = ProcessBox.SelectedItem.ToString();
            }
        }

        private void ProcessBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProcessBox.SelectedItem != null)
            {
                SelectedProcessLabel.Text = ProcessBox.SelectedItem.ToString();
            }
            //SelectedProcessLabel.Text = ProcessBox.SelectedItem.ToString();
        }
        private bool buttonisclicked = false;
        public string DLL;
        private string path = string.Empty;
        private void InjectToProcessButton_Click(object sender, EventArgs e)
        {
            if (buttonisclicked == true)
            {
                var target = Process.GetProcessesByName(ProcessBox.SelectedItem.ToString()).FirstOrDefault();
                if (target != null)
                {
                    var file = File.ReadAllBytes(path);
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    logInLabel4.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                    return;
                    if (CloseAfterInjectionToggle.Checked == true)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show($"Process [{SelectedDLLLabel.Text} Not Found!\r\nPlease Start The process and try again!");
                }
            }
        }

        private void SelectCustomDLLButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Desktop";
                openFileDialog.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                    path = openFileDialog.FileName;

                    var file = File.ReadAllBytes(path);

                    SelectedDLLLabel.Text = openFileDialog.SafeFileName;
                    buttonisclicked = true;
                }
            }
        }

        private void TopMostToggle_CheckedChanged(object sender, EventArgs e)
        {
            switch (TopMostToggle.Checked)
            {
                case true:
                    this.TopMost = true;
                    break;
                case false:
                    this.TopMost = false;
                    break;
            }
        }

        private async void General_Load(object sender, EventArgs e) => await UpdateColors();

        private async void AccentColorPanel_Click(object sender, EventArgs e)
        {
            ColorDialog MD = new ColorDialog();
            MD.AnyColor = true;
            MD.AllowFullOpen = true;

            if (MD.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.AccentColor = MD.Color;
                Properties.Settings.Default.Save();
                await UpdateColors();
            }
        }
    }
}
