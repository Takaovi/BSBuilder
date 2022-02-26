using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBuilder
{
    class Confuser
    {
        // Random bullshit commands that serve no actual purpose and will be skipped
        string[] bullshitCommandArr = {
          "IF %F%==1 IF %C%==1",
          "   ELSE IF %F%==1 IF %C%==0",
          "ELSE IF %F%==0 IF %C%==1",
          " ELSE IF %F%==0 IF %C%==1",
          "goto endoftests",
          "   goto workdone",
          "set F=%date%",
          "   :: Fixed",
          "if errorlevel 0 (set r=true, %when%) else (set r=failed, %when%, correct.)",
          "   if %user_agrees% do",
          "set /p var=fd:" + "FOR /R %fo% %%G IN (.) DO (" + "   set h=%%~dpa" + "\n)" + "%t% & dir & echo %tme%",
          "\npause" + "\n)",
          "   set fi=%%~fG",
          "set fi=%%~fG & set /p f=f: & set F=%da% & for /f \"delims=[] t=2\" %%a ",
          "   set for /f \"delims=[] t=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr%",
          "ELSE IF %F%==0 IF %C%==1 & set C=cls & set /A spl =1",
          "   timeout /t 2 /nobreak > NUL & if errorlevel 0 (set r=true, %when%)",
          "timeout /t 2 /nobreak > NUL & if errorlevel 1(set r = true, % then %)",
          "   set C=cls",
          "   set /A sample =1",
          "   fc /l %comp1% %comp2%",
          "for /f \"delims=[] t=2\" %%a in ('2^>NUL b -4 -n 1 %cmptr% ^| fndst [') do set d=%%a",
          "   timeout /t 2 /nobreak > NUL",
          "   if errorlevel 1 (@echo off) else",
          "whoami",
          "   if %ERRORLEVEL% EQU 1 goto w",
          "move \"%source%\" \"%fs%\"",
          "   move \"%source%\" \"%s%\"",
          "cls",
          "if 0==1 0",
          "   if 0==1 0",
          "cd.",
          "call",
          "   call",
          "setlocal",
          "2>NUL Info > %temp%",
          "   2>NUL Info > %temp%",
          "set \"input=%~1\"",
          "if \"!input:~0,1!\" equ \"-\"",
          "set \"input=!input:~1!\"",
          "if \"!input:~0,1!\" equ \"+\" set \"input = !input:~1!\"",
          "set result=false",
          "   set result=true",
          "set wait=true",
          "   set wait=false",
          "endlocal & if \"%~2\" neq \"\" (set %~2=%result%) else echo %result%",
          "   echo %~1 | find /i \"help\" >nul 2>&1 && ( goto :help )",
          "set \"verp=%%~O\"",
          "   rem Please wait",
          "echo %~n0 [RtnVar]",
          "   if \"%s%\" equ \"#%~1\" endl& if \"%~3\" neq \"\" (set %~3=-1&exit /b 0)",
          " set \"len=0\"",
          "set \"s=!s:~%% A!\"",
          "   set /a \"len+=%%A\"",
          "set /A md=2*%%A",
          "   set /A md=2*%%A",
          "::set \"th=%~1\"",
          "call ::insert %~1r %value%",
          "   rem exit /b 0",
          "echo <wait>",
          "set \"hos=%~2\"",
          "   set \"record=%~2\"",
          "set \"c=%~2\"",
          "   goto :argPad",
          ":evr",
          "set \"us=/dR:% user %\"",
          "   goto :eof",
          "echo Started",
          "   echo %%1=%1 N=%N%",
          "echo %%1=%1 %%2=%2 %%3=%3",
          "set \"[=rem/||(\" & set \"]=)\"",
          "set \"[:=goto :]%% \"",
          "",
          "",
          "",
          "",
          "",
          "",
          "",
          "",
        };

        string genFakeCopyrightNotice()
        {
            Random rand = new Random();

            string[] firstnames = {
                "Liam",
                "Noah",
                "Oliver",
                "Elijah",
                "Olivia",
                "Ava",
                "Emma",
                "Charlotte",
                "William",
                "James",
                "Benjamin",
                "Lucas",
                "Henry",
                "Alexander",
                "Sophia",
                "Amelia",
                "Isabella",
                "Mia",
                "Evelyn",
                "Harper",
            };
            string[] lastnames = {
                "Smith",
                "Johnson",
                "Williams",
                "Brown",
                "Jones",
                "Garcia",
                "Miller",
                "Davis",
                "Rodriguez",
                "Martinez",
                "Hernandez",
                "Lopez",
                "Wilson",
                "Moore",
                "Lee",
                "Robinson",
                "King",
                "Wright"
            };

            string rndFirstname = firstnames[rand.Next(1, firstnames.Length)];
            string rndLastname = lastnames[rand.Next(1, lastnames.Length)];
            string rndYear = $"201{rand.Next(6, 9)}";
            string rndVersion = rand.Next(1, 10) + "." + rand.Next(1, 69);

            string fakeCopyrightNotice =
            $":: Copyright © {rndFirstname} {rndLastname}, {rndYear}\n"
            + $":: Version: {rndVersion}\n"
            + $":: All rights reserved. The moral rights of the author have been asserted.";

            return fakeCopyrightNotice;
        }

        string genBullshitCommands(int lineAmount, string flag)
        {
            Random rand = new Random();
            string genRndBullshitCommand() => bullshitCommandArr[rand.Next(0, bullshitCommandArr.Length)];
            string bullshitCommands = string.Empty;
            string lastCommand = string.Empty;

            bullshitCommands += $"\ngoto {flag}";

            for (int i = 0; i <= lineAmount; i++)
            {
                while (true)
                {
                    string rndBullshitCommand = genRndBullshitCommand();

                    if (rndBullshitCommand != lastCommand)
                    {
                        bullshitCommands += "\n" + rndBullshitCommand;
                        lastCommand = rndBullshitCommand;

                        break;
                    }
                }
            }

            bullshitCommands += $"\n:{flag}";

            return bullshitCommands;
        }

        public string confuseBatch(string batch)
        {
            Random rand = new Random();
            string confusedBatch = string.Empty;
            confusedBatch = genFakeCopyrightNotice() + "\n@echo off\ncd.";

            confusedBatch += genBullshitCommands(
                rand.Next(100, 1000), // line amount
                "temp" // flag name
            );

            confusedBatch += "\n" + batch;

            confusedBatch += genBullshitCommands(
                rand.Next(100, 500), // line amount
                "tmp" // flag name
            );

            return confusedBatch;
        }
    }
}