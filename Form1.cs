using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
using System.Text.Json;

using WialonSharp;

using System.Diagnostics;
using RestSharp;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace GPS_GetData
{
    public partial class Form1 : Form
    {

        private string WS_URL = "https://hosting.wialon.com/";
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        ConexionBD objDB = null;
        DateTime proxEjec = DateTime.Now;
        string Ejecutando = "";

        //20240902 el 28 de agosto cambiaron el usuario y password, se colocan ahora como una variable global. 
        string usuario = "samuel.valdes";  //"DEMO GT"; //"DemoBienestar";
        string password = "G4NdR4d3.";

        int IDResource = 401079984;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            DateTime FI = DateTime.Now.AddDays(-30);
            this.dateTimePicker2.Value = FI;
            this.objDB = new ConexionBD(this.ConnectionString);
            Utilerias.WriteToLog("Incia Aplicacion", "Form1_Load", Application.StartupPath + "\\Log.txt");
            this.Ejecutando = "ObtencionUnidades";
            this.timer1.Enabled = true;
            this.timer1.Start();
        }


       






       

      
        /*
        public static async Task<HttpResponseMessage> PostAsJsonAsync<TModel>(this HttpClient client, string requestUrl, TModel model)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(requestUrl, stringContent);
        }*/

        class AccessToken
        {
            public string access_token { get; set; }
        }
        class TokenClient
        {
            public string client_id { get; set; }
            public string login { get; set; }
            public string passw { get; set; }
            public string response_type { get; set; }
            public string access_type { get; set; }
            public string activation_time { get; set; }
            public string duration { get; set; }
            public string flags { get; set; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int conectados = 0;
                int contadorvehi = 0;

                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar")  20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    List<Models.Wi_vehicles> vehiculos = objWi.GetUnits(Wi_Log);
                    if (vehiculos.Count > 0)
                    {
                        lbUnidades.Items.Clear();
                        foreach (Models.Wi_vehicles unidad in vehiculos)
                        {
                            contadorvehi += 1;

                            if (unidad.d.netconn.ToString().Trim() == "1")
                            {
                                conectados += 1;
                                lbUnidades.Items.Add(unidad.d.nm.ToString().Replace("-","_") + "* -- " + unidad.d.id.ToString());
                            }
                            else {
                                lbUnidades.Items.Add(unidad.d.nm.ToString().Replace("-", "_") + " -- " + unidad.d.id.ToString());
                            }
                        }
                        this.lblTotalUnidades.Text = "Total Unidades: " + contadorvehi.ToString().Trim();
                        this.lblConectados.Text = "Conectados: " + conectados.ToString().Trim();
                        this.lblDesconectados.Text = "Desconectados: " + (contadorvehi - conectados).ToString().Trim();
                    }
                    else
                    {
                        //MessageBox.Show("No se encontraron Vehiculos");
                        Utilerias.WriteToLog("No se encontraron Vehículos", "Obtener Unidades", Application.StartupPath + "\\Log.txt"); 
                    }
                }
                else {
                    //MessageBox.Show("No fue posible firmarse en Wi_alon con el token proporcionado");
                    Utilerias.WriteToLog("No fue posible firmarse en Wi_alon con el token proporcionado", "Obtener Unidades", Application.StartupPath + "\\Log.txt");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utilerias.WriteToLog(ex.Message, "Obtener Unidades", Application.StartupPath + "\\Log.txt");
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItem = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
                if (selectedItem.Trim() == "")
                {
                    MessageBox.Show("Seleccione la unidad a consultar");
                    return;
                }

                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    string unidadAConsultar = selectedItem.Substring(0, selectedItem.IndexOf("-") - 1).Trim();

                    Models.UnidadConsultada DatosUnidad = await objWi.GetUnidad_1(Wi_Log, unidadAConsultar);
                    if (DatosUnidad != null)
                    {
                        MessageBox.Show("Unidad Encontrada");
                        this.lvDatosUnidad.Items.Clear();
                        ListViewItem lvI = new ListViewItem("Nombre:");
                        lvI.SubItems.Add(DatosUnidad.items[0].nm.ToString());
                        lvDatosUnidad.Items.Add(lvI);

                        ListViewItem lvI0 = new ListViewItem("ID:");
                        lvI0.SubItems.Add(DatosUnidad.items[0].id.ToString());
                        lvDatosUnidad.Items.Add(lvI0);

                        ListViewItem lvI1 = new ListViewItem("Ultima posicion:");
                        lvDatosUnidad.Items.Add(lvI1);

                        ListViewItem lvI2 = new ListViewItem("Fecha:");
                        lvI2.SubItems.Add(UnixTimeStampToDateTime(DatosUnidad.items[0].pos.t).ToString());
                        lvDatosUnidad.Items.Add(lvI2);

                        ListViewItem lvI3 = new ListViewItem("longitud:");
                        lvI3.SubItems.Add(DatosUnidad.items[0].pos.x.ToString());
                        lvDatosUnidad.Items.Add(lvI3);

                        ListViewItem lvI4 = new ListViewItem("latitud:");
                        lvI4.SubItems.Add(DatosUnidad.items[0].pos.y.ToString());
                        lvDatosUnidad.Items.Add(lvI4);

                        ListViewItem lvI5 = new ListViewItem("altura snm:");
                        lvI5.SubItems.Add(DatosUnidad.items[0].pos.z.ToString());
                        lvDatosUnidad.Items.Add(lvI5);

                        ListViewItem lvI6 = new ListViewItem("estado de conección:");
                        if (DatosUnidad.items[0].netconn.ToString() == "1")
                        { lvI6.SubItems.Add("En Linea"); }
                        else
                        {
                            lvI6.SubItems.Add("Desconectado");
                        }
                        lvDatosUnidad.Items.Add(lvI6);

                        ListViewItem lvI7 = new ListViewItem("Id Unico:");
                        lvI7.SubItems.Add(DatosUnidad.items[0].uid.ToString());
                        lvDatosUnidad.Items.Add(lvI7);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        

     
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DateTime fecha = this.dateTimePicker1.Value;
            //DateTime finicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            //DateTime finicio = fecha.AddMinutes(-5); //consultamos los ultimos 5 minutos.
            //DateTime finicio = fecha.AddDays(-3); //consultamos los ultimos 3 dias
            DateTime finicio = this.dateTimePicker2.Value;

            string idUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            idUnidad = idUnidad.Substring(idUnidad.IndexOf("-") + 3);
            string NombreUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            NombreUnidad = NombreUnidad.Substring(0, NombreUnidad.IndexOf("-") - 1);

            try
            {
                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    Task<Models.Mensajes> MensajesUnidad = objWi.ObtenerMensajes(Wi_Log, idUnidad, finicio.ToString(), fecha.ToString(), 5);
                    //MessageBox.Show(MensajesUnidad.ToJsonString());  //  Mensajes misMensajes = JsonConvert.DeserializeObject<Mensajes>(responseString);
                    if (MensajesUnidad.Result.count > 0)
                    {
                        Models.Message myMens = MensajesUnidad.Result.messages[MensajesUnidad.Result.messages.Count - 1];
                        //Nombre, ID, FECHA, #Mensaje, LONGITUD, LATITUD, EDO CONEXION
                        ListViewItem lvI = new ListViewItem(NombreUnidad);
                        lvI.SubItems.Add(idUnidad);
                        if (myMens != null)
                        {
                            lvI.SubItems.Add(UnixTimeStampToDateTime(myMens.t).ToString());
                            lvI.SubItems.Add(myMens.p.msg_num.ToString());
                            lvI.SubItems.Add(myMens.pos.x.ToString());
                            lvI.SubItems.Add(myMens.pos.y.ToString());
                            lvI.SubItems.Add("En Linea");
                        }
                        else
                        {
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("Desconectado");
                        }
                        this.lvMensajes.Items.Add(lvI);
                    }
                    else {
                        ListViewItem lvI = new ListViewItem(NombreUnidad);
                        lvI.SubItems.Add(idUnidad);
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("Unidad Sin Conexion");
                        this.lvMensajes.Items.Add(lvI);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            try
            {
                /*string selectedItem = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
                if (selectedItem.Trim() == "")
                {
                    MessageBox.Show("Cargue las unidades a consultar");
                    return;
                }*/

                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    foreach (var unidad in this.lbUnidades.Items)
                    {
                        string unidadAConsultar = unidad.ToString().Substring(0, unidad.ToString().IndexOf("-") - 1).Trim();  //selectedItem.Substring(0, selectedItem.IndexOf("-") - 1).Trim();
                        Models.UnidadConsultada DatosUnidad = await objWi.GetUnidad_1(Wi_Log, unidadAConsultar);
                        if (DatosUnidad != null)
                        {
                            if (DatosUnidad.items[0].pos != null)
                            {
                                ListViewItem lvI = new ListViewItem(DatosUnidad.items[0].nm.ToString());
                                lvI.SubItems.Add(DatosUnidad.items[0].id.ToString());
                                lvI.SubItems.Add(UnixTimeStampToDateTime(DatosUnidad.items[0].pos.t).ToString()).ToString();
                                lvI.SubItems.Add(" "); //numero de mensaje
                                lvI.SubItems.Add(DatosUnidad.items[0].pos.x.ToString());
                                lvI.SubItems.Add(DatosUnidad.items[0].pos.y.ToString());
                                DateTime faux = UnixTimeStampToDateTime(DatosUnidad.items[0].pos.t);
                                if (faux >= DateTime.Now.AddMinutes(-5))
                                {
                                    lvI.SubItems.Add("En Linea");
                                }
                                else
                                {
                                    lvI.SubItems.Add("Desconectado");
                                }
                                this.lvMensajes.Items.Add(lvI);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int ResourceId =  this.IDResource; //20240902  400912751;

                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    foreach (var unidad in this.lbUnidades.Items)
                    {                        
                        string unidadAConsultar = unidad.ToString().Substring(0, unidad.ToString().IndexOf("-") - 1).Trim();  //selectedItem.Substring(0, selectedItem.IndexOf("-") - 1).Trim();
                        Models.UnidadConsultada DatosUnidad = await objWi.GetUnidad_1(Wi_Log, unidadAConsultar.Replace("_","-"));
                        if (DatosUnidad != null)
                        {
                            if (DatosUnidad.items[0].pos != null)
                            {
                                string EnGeocerca = await objWi.PuntoEnGeocerca(Wi_Log, DatosUnidad.items[0].pos.x.ToString(), DatosUnidad.items[0].pos.y.ToString(), DatosUnidad.items[0].id.ToString(), ResourceId.ToString());
                                if (EnGeocerca.Trim() != "")
                                {
                                    string geocercas = EnGeocerca.Substring(EnGeocerca.IndexOf("|") + 1);
                                    if (geocercas.Trim() != "[{}]")
                                    {
                                        string[] ids_geocercas = geocercas.Split(',');
                                        foreach (string id_geo in ids_geocercas)
                                        {
                                            string Q = "Delete UNIDADESENGEOCERCA where unidad_id=" + DatosUnidad.items[0].id.ToString().Trim();
                                            Q += " and Convert(char(8),fecha,112) = Convert(char(8),GetDate(),112)";
                                            Q += " and geocerca_id=" + id_geo.Trim();
                                            Q += " and usuario_contrato = '" + this.usuario.Trim() + "'";

                                            int rs = this.objDB.EjecUnaInstruccion(Q);
                                            if (rs == 0) //Si puede ser que no haya datos y borre 0 registros.
                                            {
                                                Debug.WriteLine(Q);
                                            }

                                            Q = "insert into UNIDADESENGEOCERCA(unidad,unidad_id,geocerca_id,fecha,x_lon,y_lat,usuario_contrato)";
                                            Q += " VALUES(";
                                            Q += "'" + DatosUnidad.items[0].nm.Trim() + "',";
                                            Q += DatosUnidad.items[0].id.ToString().Trim() + ",";
                                            Q += id_geo.Trim() + ",";
                                            Q += "GetDate(),";
                                            Q += DatosUnidad.items[0].pos.x.ToString().Trim() + ",";
                                            Q += DatosUnidad.items[0].pos.y.ToString().Trim() + ",";
                                            Q += "'" + this.usuario.Trim() + "')";
                                            int res = this.objDB.EjecUnaInstruccion(Q);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //MessageBox.Show("Posiciones en Geocerca Actualizadas");
                    Utilerias.WriteToLog("Posiciones en Geocerca Actualizadas", "ActualizaPosicionesGeocercas", Application.StartupPath + "\\Log.txt");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utilerias.WriteToLog(ex.Message, "ActualizaPoricionesGeocercas", Application.StartupPath + "\\Log.txt");
            }
        }


        private void button12_Click(object sender, EventArgs e)
        {
            string idUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            idUnidad = idUnidad.Substring(idUnidad.IndexOf("-") + 3);
            string NombreUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            NombreUnidad = NombreUnidad.Substring(0, NombreUnidad.IndexOf("-") - 1);

            try
            {
                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    Task<double> NotificacionesUnidad = objWi.GetNotificaciones(Wi_Log, idUnidad);
                    //MessageBox.Show(MensajesUnidad.ToJsonString());  //  Mensajes misMensajes = JsonConvert.DeserializeObject<Mensajes>(responseString);
/*                    if (NotificacionesUnidad.Result.count > 0)
                    {
                        Models.Message myMens = MensajesUnidad.Result.messages[MensajesUnidad.Result.messages.Count - 1];
                        //Nombre, ID, FECHA, #Mensaje, LONGITUD, LATITUD, EDO CONEXION
                        ListViewItem lvI = new ListViewItem(NombreUnidad);
                        lvI.SubItems.Add(idUnidad);
                        if (myMens != null)
                        {
                            lvI.SubItems.Add(UnixTimeStampToDateTime(myMens.t).ToString());
                            lvI.SubItems.Add(myMens.p.msg_num.ToString());
                            lvI.SubItems.Add(myMens.pos.x.ToString());
                            lvI.SubItems.Add(myMens.pos.y.ToString());
                            lvI.SubItems.Add("En Linea");
                        }
                        else
                        {
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("");
                            lvI.SubItems.Add("Desconectado");
                        }
                        this.lvMensajes.Items.Add(lvI);
                    }
                    else
                    {
                        ListViewItem lvI = new ListViewItem(NombreUnidad);
                        lvI.SubItems.Add(idUnidad);
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("");
                        lvI.SubItems.Add("Unidad Sin Conexion");
                        this.lvMensajes.Items.Add(lvI);
                    }
*/                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void button15_Click(object sender, EventArgs e)
        {
            DateTime fecha = this.dateTimePicker1.Value;
            //DateTime finicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            //DateTime finicio = fecha.AddMinutes(-5); //consultamos los ultimos 5 minutos.
            //DateTime finicio = fecha.AddDays(-3); //consultamos los ultimos 3 dias
            DateTime finicio = this.dateTimePicker2.Value;

            //string idUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            //idUnidad = idUnidad.Substring(idUnidad.IndexOf("-") + 3);
            //string NombreUnidad = this.lbUnidades.Items[this.lbUnidades.SelectedIndex].ToString();
            //NombreUnidad = NombreUnidad.Substring(0, NombreUnidad.IndexOf("-") - 1);

            try
            {
                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    foreach (var unidad in this.lbUnidades.Items)
                    {
                        string idUnidad = unidad.ToString().Substring(unidad.ToString().IndexOf("-") + 3).Trim();
                        string NombreUnidad = unidad.ToString().Substring(0, unidad.ToString().IndexOf("-") - 1).Trim();
                        Models.UnidadConsultada DatosUnidad = await objWi.GetUnidad_1(Wi_Log, NombreUnidad);
                        if (DatosUnidad != null)
                        {
                            Task<Models.TripOfUnit> TripsUnidad = objWi.ObtenerViajes(Wi_Log, idUnidad, finicio.ToString(), fecha.ToString(), 50);
                            //MessageBox.Show(MensajesUnidad.ToJsonString());  //  Mensajes misMensajes = JsonConvert.DeserializeObject<Mensajes>(responseString);
                            //Nombre, ID, FECHA, #Mensaje, LONGITUD, LATITUD, EDO CONEXION, KM REC, MAX VELO
                            ListViewItem lvI = new ListViewItem(NombreUnidad);
                            lvI.SubItems.Add(idUnidad);
                            if (TripsUnidad.Result.units != null && TripsUnidad.Result.units.Count > 0)
                            {
                                string metros = TripsUnidad.Result.units[0].mileage.ToString();
                                string velocidad = TripsUnidad.Result.units[0].max_speed.ToString();

                                lvI.SubItems.Add(UnixTimeStampToDateTime(TripsUnidad.Result.units[0].msgs.last.time).ToString());
                                lvI.SubItems.Add(TripsUnidad.Result.units[0].msgs.count.ToString());
                                lvI.SubItems.Add(TripsUnidad.Result.units[0].msgs.last.lat.ToString());
                                lvI.SubItems.Add(TripsUnidad.Result.units[0].msgs.last.lon.ToString());                                
                                
                                if (DatosUnidad.items[0].netconn.ToString() == "1")
                                {
                                    lvI.SubItems.Add("En Linea");
                                }
                                else
                                {
                                    lvI.SubItems.Add("Desconectado");
                                }
                                lvI.SubItems.Add(metros + " m");
                                lvI.SubItems.Add(velocidad + " Km/h");
                            }
                            this.lvMensajes.Items.Add(lvI);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            DateTime fecha = this.dateTimePicker1.Value;
            //DateTime finicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            //DateTime finicio = fecha.AddMinutes(-5); //consultamos los ultimos 5 minutos.
            //fecha = fecha.AddDays(-1); 
            DateTime finicio = fecha.AddDays(-30); //consultamos los ultimos 30 dias
            //DateTime finicio = this.dateTimePicker2.Value;
            int ResourceId = this.IDResource;  //401079984;   //400912751; //401079984; 20240902
            int plantillaId = 3; //12; //Es el reporte:"Kilometraje"; 20240902

            string Q = "";
            try
            {
                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    foreach (var unidad in this.lbUnidades.Items)
                    {
                        string idUnidad = unidad.ToString().Right(9).Trim(); //unidad.ToString().Substring(unidad.ToString().IndexOf("-") + 3).Trim();
                        string NombreUnidad = unidad.ToString().Substring(0, unidad.ToString().IndexOf("-") - 1).Trim();
                        List<Models.MyResRepKilo> resultset = objWi.Get_kms(finicio, fecha, Convert.ToInt32(idUnidad), Wi_Log, plantillaId, ResourceId);
                        if (resultset != null)
                        {
                            Debug.WriteLine(NombreUnidad + " " + idUnidad); 
                            for(int i = 0; i<resultset.Count;i++)
                            {                                
                                var cadena = JsonConvert.SerializeObject(resultset[i].c[1]);
                                Models.objP objPui = JsonConvert.DeserializeObject<Models.objP>(cadena.ToString());
                                Models.objP objPuf = JsonConvert.DeserializeObject<Models.objP>(JsonConvert.SerializeObject(resultset[i].c[2]));

                            string f_ini = objPui.t.Trim(); 
                            string f_fin = objPuf.t.Trim();
                            string calle = resultset[i].c[0].ToString().Trim();
                            string y_ini = objPui.y.ToString().Trim();
                            string x_ini = objPui.x.ToString().Trim();
                            string y_fin = objPuf.y.ToString().Trim();
                            string x_fin = objPuf.x.ToString().Trim();
                            string duracion = resultset[i].c[3].ToString().Trim();
                            string kilometraje = resultset[i].c[4].ToString().Trim();
                            string kilometraje_a = resultset[i].c[5].ToString().Trim();
                            string velocidad = resultset[i].c[6].ToString().Trim();
                            string vel_maxima = "";
                            if (JsonConvert.SerializeObject(resultset[i].c[7]).Substring(0, 1) == "{")
                            {
                              Models.objP objPuv = JsonConvert.DeserializeObject<Models.objP>(JsonConvert.SerializeObject(resultset[i].c[7]));
                              vel_maxima = objPuv.t.Trim();  
                            }
                            else {
                              vel_maxima = resultset[i].c[7].ToString().Trim();
                                }
                                kilometraje = kilometraje.Replace(" km", "");
                                kilometraje_a = kilometraje_a.Replace(" km", "");
                                velocidad = velocidad.Replace(" km/h", "");
                                vel_maxima = vel_maxima.Replace(" km/h", "");

                                   Q = "select Count(*) from REP_KILOMETRAJE";
                                   Q += " where unidad_id =" + idUnidad;
                                   Q += " and f_ini = Convert(datetime, '" + f_ini + "')";
                                   Q += " and f_fin = Convert(datetime, '" + f_fin + "')";
                                   Q += " and usuario_contrato = '" + this.usuario.Trim()  +"'";

                                if (this.objDB.ConsultaUnSoloCampo(Q).Trim() == "0")
                                {
                                    try
                                    {
                                        NombreUnidad = NombreUnidad.Replace("_", "-");

                                        Q = "INSERT INTO[dbo].[REP_KILOMETRAJE]([unidad],[unidad_id],[calle],[f_ini],[f_fin],[y_ini],[x_ini],[y_fin],[x_fin],[duracion],[kilometraje],[kilometraje_a],[velocidad],[vel_maxima],[usuario_contrato])";
                                        Q += "VALUES(";
                                        Q += "'" + NombreUnidad.Replace("*","") + "',";
                                        Q += idUnidad + ",";
                                        Q += "'" + calle.Replace("'","") + "',";
                                        Q += "'" + f_ini + "',";
                                        Q += "'" + f_fin + "',";
                                        Q += y_ini + ",";
                                        Q += x_ini + ",";
                                        Q += y_fin + ",";
                                        Q += x_fin + ",";
                                        Q += "'" + duracion + "',";
                                        Q += kilometraje + ",";
                                        Q += kilometraje_a + ",";
                                        Q += velocidad + ",";
                                        Q += vel_maxima + ",";
                                        Q += "'" + this.usuario.Trim() + "')";

                                        int cont = this.objDB.EjecUnaInstruccion(Q);
                                        if (cont == 0)
                                        {
                                            Debug.WriteLine(Q);
                                            Utilerias.WriteToLog(Q, "CargarRepKilometrajeaBD", Application.StartupPath + "\\Log.txt");
                                        }
                                    }
                                    catch (Exception ex1)
                                    {
                                        Debug.WriteLine(ex1 + " " + Q);
                                        Utilerias.WriteToLog(ex1.Message + " " + Q, "CargarRepKilometrajeaBD", Application.StartupPath + "\\Log.txt");
                                    }
                                }
                            }
                        }
                    }
                    //MessageBox.Show("Reporte Kilometraje Cargado");
                    Utilerias.WriteToLog("Reporte Kilometraje Cargado", "CargarRepKilometrajeaBD", Application.StartupPath + "\\Log.txt");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void button16_Click(object sender, EventArgs e)
        {
            try
            {
                Wi_api objWi = new WialonSharp.Wi_api();
                Models.Wi_Login Wi_Log = objWi.LoginWialon();
                //if (Wi_Log.au.ToString() == "DemoBienestar") //20240902
                if (Wi_Log.au.ToString() == this.usuario.Trim())
                {
                    object geocercas = await objWi.GetGeoCercas(Wi_Log);
                    if (geocercas != null)
                    {
                        string Q = "";
                        Models.GEO_cercas objGeocercas = JsonConvert.DeserializeObject<Models.GEO_cercas>(JsonConvert.SerializeObject(geocercas));

                        //Debug.WriteLine(NombreUnidad + " " + idUnidad); 
                        for (int i = 0; i < objGeocercas.items.Count; i++)
                        {
                            for (int j = 0; j < objGeocercas.items[i].zl.Count; j++)
                            {
                                //Models.objP objPui = JsonConvert.DeserializeObject<Models.objP>(cadena.ToString());
                                //Models.objP objPuf = JsonConvert.DeserializeObject<Models.objP>(JsonConvert.SerializeObject(resultset[i].c[2]));

                                string nombre = objGeocercas.items[i].zl[j].n.ToString().Trim();
                                string descripcion = objGeocercas.items[i].zl[j].d.ToString().Trim();
                                string identificador = objGeocercas.items[i].zl[j].id.ToString().Trim();
                                string b_min_x = objGeocercas.items[i].zl[j].b.min_x.ToString().Trim();
                                string b_min_y = objGeocercas.items[i].zl[j].b.min_y.ToString().Trim();
                                string b_max_x = objGeocercas.items[i].zl[j].b.max_x.ToString().Trim();
                                string b_max_y = objGeocercas.items[i].zl[j].b.max_y.ToString().Trim();
                                string b_cen_x = objGeocercas.items[i].zl[j].b.cen_x.ToString().Trim();
                                string b_cen_y = objGeocercas.items[i].zl[j].b.cen_y.ToString().Trim();
                                string creation_time = UnixTimeStampToDateTime(objGeocercas.items[i].zl[j].ct).ToString().Trim();
                                string modification_time = UnixTimeStampToDateTime(objGeocercas.items[i].zl[j].mt).ToString().Trim();

                                Q = "select Count(*) from GEOCERCAS";
                                Q += " where identificador =" + identificador;
                                Q += " and usuario_contrato='" + this.usuario.Trim() + "'";

                                if (this.objDB.ConsultaUnSoloCampo(Q).Trim() == "0")
                                {
                                    try
                                    {
                                        Q = "INSERT INTO[dbo].[GEOCERCAS](nombre,descripcion,identificador,b_min_x,b_min_y,b_max_x,b_max_y,b_cen_x,b_cen_y,creation_time,modification_time,usuario_contrato)";
                                        Q += "VALUES(";
                                        Q += "'" + nombre + "',";
                                        Q += "'" + descripcion + "',";
                                        Q += identificador + ",";
                                        Q += b_min_x + ",";
                                        Q += b_min_y + ",";
                                        Q += b_max_x + ",";
                                        Q += b_max_y + ",";
                                        Q += b_cen_x + ",";
                                        Q += b_cen_y + ",";
                                        Q += "Convert(datetime,'" + FormateaFecha(creation_time) + "'),";
                                        Q += "Convert(datetime,'" + FormateaFecha(modification_time) + "'),";
                                        Q += "'" + this.usuario.Trim() + "')";

                                        int cont = this.objDB.EjecUnaInstruccion(Q);
                                    }
                                    catch (Exception ex1)
                                    {
                                        Debug.WriteLine(ex1.Message + " " + Q);
                                        Utilerias.WriteToLog(ex1.Message + " " + Q, "CargarGeocercasaBD", Application.StartupPath + "\\Log.txt");
                                    }
                                }
                                //else { 
                                //TODO: Update geocerca con los valores actuales.
                                //}                                                                
                            }//del for j
                            //MessageBox.Show("Geocercas Actualizadas");
                            Utilerias.WriteToLog("Geocercas Actualizadas", "CargarGeocercasaBD", Application.StartupPath + "\\Log.txt");
                        }//del for i
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utilerias.WriteToLog(ex.Message, "CargarGeocercasaBD", Application.StartupPath + "\\Log.txt");
            }
        }

        public string FormateaFecha(string Fecha)
        {
            try
            {
                if (Fecha.IndexOf("a.") > -1) //'28/08/2023 04:24:45 a. m.' 
                {
                    Fecha = Fecha.Replace("a.", "");
                    Fecha = Fecha.Replace("m.", "");
                }
                if (Fecha.IndexOf("p.") > -1)
                {
                    Fecha = Fecha.Replace("p.", "").Trim();
                    Fecha = Fecha.Replace("m.", "").Trim();
                    string horac = Fecha.Substring(Fecha.IndexOf(" ") + 1);
                    int hora = Convert.ToInt32(horac.Substring(0, horac.IndexOf(":")));
                    hora += 12;
                    if (hora == 24)
                    {
                        hora = 0;
                    }

                    horac = horac.Substring(horac.IndexOf(":"));
                    Fecha = Fecha.Substring(0,Fecha.IndexOf(" ")) + " " + hora.ToString().Trim() + horac.Trim();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Fecha.Trim();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //string Q = "select nombre, Count(geocerca_id) as unidades from UNIDADESENGEOCERCA, GEOCERCAS";
            //Q += " where identificador *= geocerca_id";
            //Q += " and Convert(varchar(8),fecha,112) = (Select Convert(varchar(8), Max(fecha), 112) from UNIDADESENGEOCERCA)";
            //Q += " group by nombre";
            //Q += " order by unidades desc";

            string Q = "select nombre, Count(geocerca_id) as unidades from UNIDADESENGEOCERCA";
            Q += " RIGHT JOIN GEOCERCAS";
            Q += " ON  identificador = geocerca_id";
            Q += " and Convert(varchar(8),fecha,112) = (Select Convert(varchar(8), Max(fecha), 112) from UNIDADESENGEOCERCA)";
            Q += " group by nombre";
            Q += " order by unidades desc";

            DataSet ds = this.objDB.Consulta(Q);
            if (ds.Tables.Count > 0)
            {
                string resultado = "";
                foreach (DataRow reg in ds.Tables[0].Rows)
                {
                    resultado += reg["nombre"].ToString() + " " + reg["unidades"].ToString() + "\r" + "\n";                
                }
                MessageBox.Show(resultado);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string Q = "select unidad, sum(kilometraje) as km_recorrido from REP_KILOMETRAJE";            
            Q += " where Convert(char(8),f_ini,112) between Convert(char(8),getdate() - 30,112) and Convert(char(8),getdate(),112)";
            Q += " group by unidad";
            Q += " order by km_recorrido desc";

             DataSet ds = this.objDB.Consulta(Q);
            if (ds.Tables.Count > 0)
            {
                string resultado = "";
                foreach (DataRow reg in ds.Tables[0].Rows)
                {
                    resultado += reg["unidad"].ToString() + " " + reg["km_recorrido"].ToString() + " km " + "\r" + "\n";
                }
                MessageBox.Show(resultado);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

            string Q = "select unidad, Convert(char(12), f_ini, 113) as fecha, sum(kilometraje) as km_recorrido from REP_KILOMETRAJE";
            Q += " where Convert(char(8), f_ini, 112) between Convert(char(8),getdate() - 30,112) and Convert(char(8),getdate(),112)";
            Q += " group by unidad,Convert(char(12), f_ini, 113)";
            Q += " order by fecha";

            DataSet ds = this.objDB.Consulta(Q);
            if (ds.Tables.Count > 0)
            {
                string resultado = "";
                string auxfe = "";
                foreach (DataRow reg in ds.Tables[0].Rows)
                {
                    if (auxfe.Trim() != reg["fecha"].ToString().Trim())
                    {
                        resultado += "______________________________" + "\r" + "\n";
                        auxfe = reg["fecha"].ToString().Trim();
                    }

                    resultado += reg["fecha"].ToString() + " " + reg["unidad"].ToString() +  " " + reg["km_recorrido"].ToString() + " km " + "\r" + "\n";
                }
                MessageBox.Show(resultado);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            string Q = "select unidad, Convert(char(12), f_ini, 113) as fecha,vel_maxima from REP_KILOMETRAJE where vel_maxima >= 100";
            Q += " and Convert(char(8),f_ini,112) between Convert(char(8),getdate() - 30,112) and Convert(char(8),getdate(),112)";
            Q += " order by vel_maxima desc,unidad,f_ini";
            DataSet ds = this.objDB.Consulta(Q);
            if (ds.Tables.Count > 0)
            {
                string resultado = "";
                foreach (DataRow reg in ds.Tables[0].Rows)
                {
                    resultado += reg["fecha"].ToString() + " " + reg["unidad"].ToString() + " " + reg["vel_maxima"].ToString() + " km/h " + "\r" + "\n";
                }
                MessageBox.Show(resultado);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Ejecutando == "ObtencionUnidades")
                {
                    Utilerias.WriteToLog("Incia Consulta de Unidades a Wi", "timer1_Tick", Application.StartupPath + "\\Log.txt");
                    this.proxEjec = DateTime.Now.AddMinutes(1);
                    this.button5_Click(this.button5, e);
                    this.Ejecutando = "CargaUnidadesGeocerca";
                }

                if (this.proxEjec.Minute == DateTime.Now.Minute && this.Ejecutando=="CargaUnidadesGeocerca")
                {
                    Utilerias.WriteToLog("Incia Carga de Unidades por GEOCERCA Wi-->BD", "timer1_Tick", Application.StartupPath + "\\Log.txt");
                    this.button11_Click(this.button11, e);
                    this.proxEjec = DateTime.Now.AddMinutes(1);
                    this.Ejecutando = "CargaGeocerca";
                }

                if (this.proxEjec.Minute == DateTime.Now.Minute && this.Ejecutando == "CargaGeocerca")
                {
                    Utilerias.WriteToLog("Incia Carga de GEOCERCAS Wi-->BD", "timer1_Tick", Application.StartupPath + "\\Log.txt");
                    this.button16_Click(this.button16, e);
                    this.proxEjec = DateTime.Now.AddMinutes(1);
                    this.Ejecutando = "CargaKilometraje";
                }
                
                if (this.proxEjec.Minute == DateTime.Now.Minute && this.Ejecutando == "CargaKilometraje")
                {
                    Utilerias.WriteToLog("Incia Carga de REP KILOMETRAJE Wi-->BD", "timer1_Tick", Application.StartupPath + "\\Log.txt");
                    this.button14_Click(this.button14, e);
                    Utilerias.WriteToLog("Termina la carga diaria se cierra el aplicativo al terminar", "timer1_Tick", Application.StartupPath + "\\Log.txt");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Utilerias.WriteToLog(ex.Message, "timer1_Tick", Application.StartupPath + "\\Log.txt");
            }
        }

        //aqui debe ir el siguiente metodo
    }
    public static class Extensions
    {
        public static string Right(this string str, int n)
        {
            return str.Substring(str.Length < n ? 0 : (str.Length - n));
        }
    }
}
