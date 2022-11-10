using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ProjectJson
{
    public partial class Form1 : Form
    {
        public string Token;

        public Form1()
        {
            InitializeComponent();
        }

        private void TakeToken()
        {
            try
            {
                var url = "http://appscl.dole.com/wsqc/Auth/Token";

                string Id = "9f2d10ee-89f0-4ccc-a098-128a57b635ef";
                string Secret = "ad59b11e-d1a8-4729-a3cd-89a6ef56ba53";

                AuthJson Datos = new AuthJson();
                Datos.clientId = Id;
                Datos.clientSecret = Secret;
                AuthJson.ClientD.Add(Datos);

                string JsonData = JsonConvert.SerializeObject(Datos);

                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                HttpRequest.ContentType = "application/json";
                HttpRequest.KeepAlive = true;
                HttpRequest.Accept = "text/plain";

                using (var StreamWriter = new StreamWriter(HttpRequest.GetRequestStream()))
                {
                    StreamWriter.Write(JsonData);
                }

                var HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
                using (var StreamReader = new StreamReader(HttpResponse.GetResponseStream()))
                {
                    var Result = StreamReader.ReadToEnd();

                    TokenClass TokenString = JsonConvert.DeserializeObject<TokenClass>(Result.ToString());

                    Token = TokenString.token.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void ConsProceso(string NCaja)
        {
            try
            {
                var url = "http://appscl.dole.com/wsqc/QC/GetScannedInfoPackedFruit?info=" + NCaja;

                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                HttpRequest.Headers["Authorization"] = "Bearer " + TokenClass.TokenC.ToString();
                HttpRequest.ContentLength = 0;
                HttpRequest.Accept = "*/*";
                HttpRequest.KeepAlive = true;

                var HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
                using (var StreamReader = new StreamReader(HttpResponse.GetResponseStream()))
                {
                    var Result = StreamReader.ReadToEnd();

                    ProcJson DataString = JsonConvert.DeserializeObject<ProcJson>(Result.ToString());

                    textBox2.Text = DataString.requestID.ToString();
                    textBox3.Text = DataString.season.ToString();
                    textBox4.Text = DataString.packingCode.ToString();
                    textBox5.Text = DataString.packingName.ToString();
                    textBox6.Text = DataString.growerCode.ToString();
                    textBox7.Text = DataString.growerName.ToString();
                    textBox8.Text = DataString.csg.ToString();
                    textBox9.Text = DataString.ranchCode.ToString();
                    textBox10.Text = DataString.ranchName.ToString();
                    textBox11.Text = DataString.commodityCode.ToString();
                    textBox12.Text = DataString.commodityName.ToString();
                    textBox13.Text = DataString.varietyCode.ToString();
                    textBox14.Text = DataString.varietyName.ToString();
                    textBox15.Text = DataString.packageCode.ToString();
                    textBox16.Text = DataString.sizeCode.ToString();
                    textBox17.Text = DataString.colorCode.ToString();
                    textBox18.Text = DataString.processGuideNumber.ToString();
                    textBox19.Text = DataString.turn.ToString();
                    textBox20.Text = DataString.line.ToString();
                    textBox21.Text = DataString.harvestDate.ToString();
                    textBox22.Text = DataString.packedDate.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void Inspeccion()
        {
            try
            {
                var url = "http://appscl.dole.com/wsqc/QC/GetScannedInfoPackedFruit";

                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                HttpRequest.Headers["Authorization"] = "Bearer " + Token;
                HttpRequest.Accept = "text/plain";
                HttpRequest.ContentType = "application/json";
                HttpRequest.KeepAlive = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TakeToken();

            AtrJson atrJson = new AtrJson();
            atrJson.id = 2;
            atrJson.externalId = "String3";
            atrJson.name = "String4";
            atrJson.value = 5;
            atrJson.valueName = "String6";
            atrJson.count = 7;
            atrJson.grade = "String8";

            ValuesAtr valuesAtr = new ValuesAtr();
            valuesAtr.valueName = "M1";
            valuesAtr.value = 25;

            atrJson.values.Add(valuesAtr);

            textBox1.Text = JsonConvert.SerializeObject(atrJson);
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.IsInputKey = true;
                ConsProceso(textBox1.Text);
            }
        }
    }
}
