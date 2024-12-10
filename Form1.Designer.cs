
namespace GPS_GetData
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDesconectados = new System.Windows.Forms.Label();
            this.lblConectados = new System.Windows.Forms.Label();
            this.lblTotalUnidades = new System.Windows.Forms.Label();
            this.button19 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.lvMensajes = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DISTANCIA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VELOCIDAD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button9 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lvDatosUnidad = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbUnidades = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(187, 44);
            this.button5.TabIndex = 4;
            this.button5.Text = "wi Obtener Unidades de DemoBienestar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(200, 13);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(217, 44);
            this.button6.TabIndex = 5;
            this.button6.Text = "wi Consultar Unidad Seleccionada";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDesconectados);
            this.groupBox1.Controls.Add(this.lblConectados);
            this.groupBox1.Controls.Add(this.lblTotalUnidades);
            this.groupBox1.Controls.Add(this.button19);
            this.groupBox1.Controls.Add(this.button18);
            this.groupBox1.Controls.Add(this.button17);
            this.groupBox1.Controls.Add(this.button16);
            this.groupBox1.Controls.Add(this.button15);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.button14);
            this.groupBox1.Controls.Add(this.button13);
            this.groupBox1.Controls.Add(this.button12);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.lvMensajes);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.lvDatosUnidad);
            this.groupBox1.Controls.Add(this.lbUnidades);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Location = new System.Drawing.Point(97, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1105, 553);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unidades";
            // 
            // lblDesconectados
            // 
            this.lblDesconectados.Location = new System.Drawing.Point(13, 530);
            this.lblDesconectados.Name = "lblDesconectados";
            this.lblDesconectados.Size = new System.Drawing.Size(181, 23);
            this.lblDesconectados.TabIndex = 23;
            this.lblDesconectados.Text = "Desconectados:";
            // 
            // lblConectados
            // 
            this.lblConectados.Location = new System.Drawing.Point(13, 507);
            this.lblConectados.Name = "lblConectados";
            this.lblConectados.Size = new System.Drawing.Size(181, 23);
            this.lblConectados.TabIndex = 22;
            this.lblConectados.Text = "Conectados: ";
            // 
            // lblTotalUnidades
            // 
            this.lblTotalUnidades.Location = new System.Drawing.Point(10, 482);
            this.lblTotalUnidades.Name = "lblTotalUnidades";
            this.lblTotalUnidades.Size = new System.Drawing.Size(184, 23);
            this.lblTotalUnidades.TabIndex = 21;
            this.lblTotalUnidades.Text = "Total Unidades: ";
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(628, 498);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(187, 23);
            this.button19.TabIndex = 20;
            this.button19.Text = "Km recorridos por dia x unidad.";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(628, 469);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(187, 23);
            this.button18.TabIndex = 19;
            this.button18.Text = "Km recorridos por unidad (1 mes)";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(628, 411);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(187, 23);
            this.button17.TabIndex = 18;
            this.button17.Text = "Consultar Unidades x Geocerca";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(423, 440);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(199, 23);
            this.button16.TabIndex = 17;
            this.button16.Text = "wi Cargar Geocercas BD";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(441, 85);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(232, 23);
            this.button15.TabIndex = 16;
            this.button15.Text = "wi Cargar Viajes entre fechas";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(777, 18);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 15;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(423, 469);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(199, 23);
            this.button14.TabIndex = 14;
            this.button14.Text = "wi Cargar Rep Kilometraje BD";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(628, 440);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(187, 23);
            this.button13.TabIndex = 13;
            this.button13.Text = "Excesos de Velocidad > 100 km/h";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(441, 16);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(232, 23);
            this.button12.TabIndex = 12;
            this.button12.Text = "wi Obtener Notificaciones x Unidad";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(423, 411);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(199, 23);
            this.button11.TabIndex = 11;
            this.button11.Text = "wi Carga Unidades por Geocerca BD";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(441, 34);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(232, 23);
            this.button10.TabIndex = 10;
            this.button10.Text = "wi Cargar Reporte EDO CONEXION";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // lvMensajes
            // 
            this.lvMensajes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.DISTANCIA,
            this.VELOCIDAD});
            this.lvMensajes.HideSelection = false;
            this.lvMensajes.Location = new System.Drawing.Point(423, 116);
            this.lvMensajes.Name = "lvMensajes";
            this.lvMensajes.Size = new System.Drawing.Size(676, 289);
            this.lvMensajes.TabIndex = 9;
            this.lvMensajes.UseCompatibleStateImageBehavior = false;
            this.lvMensajes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nombre";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "FECHA";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#MENSAJE";
            this.columnHeader6.Width = 74;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "LONGITUD";
            this.columnHeader7.Width = 78;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "LATITUD";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "EDO CONEXION";
            this.columnHeader9.Width = 100;
            // 
            // DISTANCIA
            // 
            this.DISTANCIA.Text = "M. RECORRIDOS";
            this.DISTANCIA.Width = 106;
            // 
            // VELOCIDAD
            // 
            this.VELOCIDAD.Text = "MAX VELOC.";
            this.VELOCIDAD.Width = 76;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(441, 59);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(232, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "wi Cargar Mensajes entre fechas...";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(777, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // lvDatosUnidad
            // 
            this.lvDatosUnidad.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvDatosUnidad.FullRowSelect = true;
            this.lvDatosUnidad.GridLines = true;
            this.lvDatosUnidad.HideSelection = false;
            this.lvDatosUnidad.Location = new System.Drawing.Point(200, 59);
            this.lvDatosUnidad.Name = "lvDatosUnidad";
            this.lvDatosUnidad.Size = new System.Drawing.Size(217, 420);
            this.lvDatosUnidad.TabIndex = 6;
            this.lvDatosUnidad.UseCompatibleStateImageBehavior = false;
            this.lvDatosUnidad.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PROPIEDAD";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "VALOR";
            this.columnHeader2.Width = 200;
            // 
            // lbUnidades
            // 
            this.lbUnidades.FormattingEnabled = true;
            this.lbUnidades.Location = new System.Drawing.Point(7, 59);
            this.lbUnidades.Name = "lbUnidades";
            this.lbUnidades.Size = new System.Drawing.Size(187, 420);
            this.lbUnidades.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 567);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbUnidades;
        private System.Windows.Forms.ListView lvDatosUnidad;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ListView lvMensajes;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.ColumnHeader DISTANCIA;
        private System.Windows.Forms.ColumnHeader VELOCIDAD;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Label lblDesconectados;
        private System.Windows.Forms.Label lblConectados;
        private System.Windows.Forms.Label lblTotalUnidades;
        private System.Windows.Forms.Timer timer1;
    }
}

