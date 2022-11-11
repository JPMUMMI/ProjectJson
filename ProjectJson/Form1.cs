using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
//Using necesarios, sobre todo Newtonsoft.Json que debe ser importado ya que no viene predeterminado

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
                //API TOKEN DOLE
                var url = "http://appscl.dole.com/wsqc/Auth/Token";

                //Id & Secret
                string Id = "9f2d10ee-89f0-4ccc-a098-128a57b635ef";
                string Secret = "ad59b11e-d1a8-4729-a3cd-89a6ef56ba53";

                //Guardar los datos en la clase para convertirlos en JSON
                AuthJson Datos = new AuthJson();
                Datos.clientId = Id;
                Datos.clientSecret = Secret;
                AuthJson.ClientD.Add(Datos);

                //Convertir en JSON
                string JsonData = JsonConvert.SerializeObject(Datos);

                //HttpRequest + todos los metodos y requerimientos que solicita la URL
                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                HttpRequest.ContentType = "application/json";
                HttpRequest.KeepAlive = true;
                HttpRequest.Accept = "text/plain";

                //Envio JSON solicitado mediante BODY
                using (var StreamWriter = new StreamWriter(HttpRequest.GetRequestStream()))
                {
                    StreamWriter.Write(JsonData);
                }

                //Recibir TOKEN y guardarlo en una clase para desconvertir desde JSON
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
                MessageBox.Show("Error Token " + ex);
            }
        }

        private void ConsProceso(string NCaja)
        {
            TakeToken();
            try
            {
                //Se envia el N° Caja por URL
                var url = "http://appscl.dole.com/wsqc/QC/GetScannedInfoPackedFruit?info=" + NCaja;

                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                //Se solicita autorización, que se envia el TOKEN mediante una autorización BEARER
                HttpRequest.Headers["Authorization"] = "Bearer " + Token;
                HttpRequest.ContentLength = 0;
                HttpRequest.Accept = "*/*";
                HttpRequest.KeepAlive = true;

                var HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
                using (var StreamReader = new StreamReader(HttpResponse.GetResponseStream()))
                {
                    var Result = StreamReader.ReadToEnd();

                    ProcJson DataString = JsonConvert.DeserializeObject<ProcJson>(Result.ToString());

                    //Mostrar datos
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
                MessageBox.Show("Error Proceso" + ex);
            }
        }

        private void Inspeccion(string JsonData)
        {
            TakeToken();
            try
            {
                var url = "http://appscl.dole.com/wsqc/QC/RegisterFtoPInspection";

                var HttpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpRequest.Method = "POST";
                HttpRequest.Headers["Authorization"] = "Bearer " + Token;
                HttpRequest.Accept = "text/plain";
                HttpRequest.ContentType = "application/json";
                HttpRequest.KeepAlive = true;

                //Se envia JSON con los datos mediante BODY
                using (var StreamWriter = new StreamWriter(HttpRequest.GetRequestStream()))
                {
                    StreamWriter.Write(JsonData);
                }

                //Respuesta Envio
                var HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
                using (var StreamReader = new StreamReader(HttpResponse.GetResponseStream()))
                {
                    var Result = StreamReader.ReadToEnd();

                    //Respuesta: id enviado + Id registrado en DOLE
                    IdInspecJson DataString = JsonConvert.DeserializeObject<IdInspecJson>(Result.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Envio " + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Orden del JSON
            //Datos Base
            SendJson sendJson = new SendJson();
            sendJson.id = "Pruebaid10";
            sendJson.parentId = "PruebaparentId";
            sendJson.companyId = 3;
            sendJson.name = "Pruebaname";
            sendJson.creationTime = 5;
            sendJson.creationTimeISO = DateTime.Now;
            sendJson.externalProduceId = "PruebaexternalProduceId";
            sendJson.varietyId = 7;
            sendJson.externalVarietyId = "PruebaexternalVarietyId";
            sendJson.varietyName = "PruebavarietyName";
            sendJson.processId = 10;
            sendJson.processName = "PruebaprocessName";
            sendJson.standardId = 12;
            sendJson.standardName = "PruebastandardName";
            sendJson.grade = "Pruebagrade";
            sendJson.username = "Pruebausername";

            //For Properties
            for (int i = 1; i < 3; i++)
            {
                PropJson propJson = new PropJson();
                propJson.id = "prop" + i;
                propJson.externalId = "prop" + (i + 1);
                propJson.name = "prop" + (i + 2);
                propJson.type = "prop" + (i + 3);
                propJson.jsonpropdefault = "prop" + (i + 4);
                propJson.value = "prop" + (i + 5);
                sendJson.properties.Add(propJson);
            }

            //For Attributes
            for (int i = 1; i < 3; i++)
            {
                AtrJson atrJson = new AtrJson();
                atrJson.id = i;
                atrJson.externalId = "attri" + (i + 1);
                atrJson.name = "attri" + (i + 2);
                atrJson.value = (i + 3);
                atrJson.valueName = "attri" + (i + 4);
                atrJson.count = (i + 5);
                atrJson.grade = "attri" + (i + 6);

                //For Values de Attributes
                for (int j = 1; j < 4; j++)
                {
                    ValuesAtr valuesAtr = new ValuesAtr();
                    valuesAtr.valueName = "M" + (j * i);
                    valuesAtr.value = 5 * (j * i);
                    valuesAtr.count = 25 * (j * i);
                    atrJson.values.Add(valuesAtr);
                }
                sendJson.attributes.Add(atrJson);
            }

            //For Defects
            for (int i = 1; i < 3; i++)
            {
                DefJson defJson = new DefJson();
                defJson.id = i;
                defJson.externalId = "def" + (i + 1);
                defJson.name = "def" + (i + 2);
                defJson.count = (i + 3);
                defJson.percent = (i + 4);
                defJson.grade = "def" + (i + 5);
                sendJson.defects.Add(defJson);
            }

            string JsonData = JsonConvert.SerializeObject(sendJson).Replace("jsonpropdefault", "default");

            textBox1.Text = JsonData;

            Inspeccion(JsonData);
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
