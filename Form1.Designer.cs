using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Avalonia.SRI.Download.XML
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Panels
            this.panelTop = new Panel();
            this.panelBottom = new Panel();

            // Buttons superiores
            this.btnVaciar = new Button();
            this.btnImportar1 = new Button();
            this.btnImportar2 = new Button();

            // DataGridView
            this.dataGridView1 = new DataGridView();
            this.documentType = new DataGridViewTextBoxColumn();
            this.document = new DataGridViewTextBoxColumn();
            this.provider = new DataGridViewTextBoxColumn();
            this.issueDate = new DataGridViewTextBoxColumn();
            this.authorizationDate = new DataGridViewTextBoxColumn();
            this.documentTotal = new DataGridViewTextBoxColumn();
            this.status = new DataGridViewTextBoxColumn();
            this.accessKey = new DataGridViewTextBoxColumn();

            // Panel inferior
            this.txtDirectorio = new TextBox();
            this.btnExaminar = new Button();
            this.btnDescargar = new Button();
            this.lblEstado = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ================= panelTop =================
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.Padding = new Padding(5);
            this.panelTop.Controls.Add(this.btnVaciar);
            this.panelTop.Controls.Add(this.btnImportar1);
            this.panelTop.Controls.Add(this.btnImportar2);

            // ================= Botones superiores =================
            this.btnVaciar.Text = "Vaciar Lista";
            this.btnVaciar.Width = 120;
            this.btnVaciar.Left = 5;
            this.btnVaciar.Top = 10;
            this.btnVaciar.Click += new System.EventHandler(this.btnVaciar_Click);

            this.btnImportar1.Text = "Importar TXT 1";
            this.btnImportar1.Width = 120;
            this.btnImportar1.Left = 130;
            this.btnImportar1.Top = 10;
            this.btnImportar1.Click += new System.EventHandler(this.btnImportar1_Click);

            this.btnImportar2.Text = "Importar TXT 2";
            this.btnImportar2.Width = 120;
            this.btnImportar2.Left = 255;
            this.btnImportar2.Top = 10;
            this.btnImportar2.Click += new System.EventHandler(this.btnImportar2_Click);

            // ================= DataGridView =================
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ScrollBars = ScrollBars.Both; // vertical + horizontal
            //this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            //this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //this.dataGridView1.EnableHeadersVisualStyles = false;

            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255); // azul intenso
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.dataGridView1.EnableHeadersVisualStyles = false;
            
            // Alternar color de filas
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250); // casi blanco

            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] {
                this.documentType,
                this.document,
                this.provider,
                this.issueDate,
                this.authorizationDate,
                this.documentTotal,
                this.status
            });

            // Columnas
            this.documentType.HeaderText = "Tipo Documento";
            this.documentType.Name = "documentType";
            this.documentType.ReadOnly = true;

            this.document.HeaderText = "N° Documento";
            this.document.Name = "document";
            this.document.ReadOnly = true;

            this.provider.HeaderText = "Proveedor / Emisor del Documento";
            this.provider.Name = "provider";
            this.provider.ReadOnly = true;

            this.issueDate.HeaderText = "Fecha Documento";
            this.issueDate.Name = "issueDate";
            this.issueDate.ReadOnly = true;

            this.authorizationDate.HeaderText = "Fecha Recepción";
            this.authorizationDate.Name = "authorizationDate";
            this.authorizationDate.ReadOnly = true;

            this.documentTotal.HeaderText = "Total Documento";
            this.documentTotal.Name = "documentTotal";
            this.documentTotal.ReadOnly = true;

            this.status.HeaderText = "Estado";
            this.status.Name = "status";
            this.status.ReadOnly = true;


            this.accessKey.Name = "accessKey";
            this.accessKey.HeaderText = "Clave Acceso";
            this.accessKey.Visible = false; // columna oculta
            this.dataGridView1.Columns.Add(this.accessKey);


            //// ================= panelBottom =================
            //this.panelBottom.Dock = DockStyle.Bottom;
            //this.panelBottom.Height = 50;
            //this.panelBottom.Padding = new Padding(5);
            //this.panelBottom.Controls.Add(this.txtDirectorio);
            //this.panelBottom.Controls.Add(this.btnExaminar);
            //this.panelBottom.Controls.Add(this.btnDescargar);
            //this.panelBottom.Controls.Add(this.lblEstado);

            //// ================= Ruta destino y botones =================
            //this.txtDirectorio.Left = 5;
            //this.txtDirectorio.Top = 15;
            //this.txtDirectorio.Width = 500;
            ////this.txtDirectorio.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            //this.btnExaminar.Text = "Examinar";
            //this.btnExaminar.Left = 510;
            //this.btnExaminar.Top = 15;
            //this.btnExaminar.Width = 80;
            //this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);

            //this.btnDescargar.Text = "Descargar";
            //this.btnDescargar.Left = 600;
            //this.btnDescargar.Top = 15;
            //this.btnDescargar.Width = 100;
            //this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);

            //this.lblEstado.Left = 710;
            //this.lblEstado.Top = 15;
            //this.lblEstado.Width = 150;
            //this.lblEstado.Text = "0 archivos en la lista";

            // ================= panelBottom =================
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Height = 70; // aumentar altura para dos filas
            this.panelBottom.Padding = new Padding(5);

            // TextBox ruta
            this.txtDirectorio.Height = 25;
            this.txtDirectorio.Top = 0;
            this.txtDirectorio.Left = 5;
            // Ancho dinámico hasta el botón Examinar
            this.txtDirectorio.Width = this.panelBottom.Width - 5 - this.btnExaminar.Width - 5 - this.btnDescargar.Width - 50;

            this.txtDirectorio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;


            // Botones Examinar y Descargar a la derecha
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.Width = 100;
            this.btnDescargar.Height = 25;
            this.btnDescargar.Top = 0;
            this.btnDescargar.Left = this.panelBottom.Width - this.btnDescargar.Width - 5;
            this.btnDescargar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);

            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.Width = 80;
            this.btnExaminar.Height = 25;
            this.btnExaminar.Top = 0;
            this.btnExaminar.Left = this.btnDescargar.Left - this.btnExaminar.Width - 5;
            this.btnExaminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);

            // Label estado debajo de la ruta
            this.lblEstado.Text = "0 archivos en la lista";
            this.lblEstado.Top = this.txtDirectorio.Bottom + 5;
            this.lblEstado.Left = 5;
            this.lblEstado.Width = 300;
            this.lblEstado.Height = 20;
            //this.lblEstado.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            // Agregar controles al panel
            this.panelBottom.Controls.Add(this.txtDirectorio);
            this.panelBottom.Controls.Add(this.btnExaminar);
            this.panelBottom.Controls.Add(this.btnDescargar);
            this.panelBottom.Controls.Add(this.lblEstado);

            // ================= Form1 =================
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Text = "Descarga de Documentos";


            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel panelTop;
        private Panel panelBottom;

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn documentType;
        private DataGridViewTextBoxColumn document;
        private DataGridViewTextBoxColumn provider;
        private DataGridViewTextBoxColumn issueDate;
        private DataGridViewTextBoxColumn authorizationDate;
        private DataGridViewTextBoxColumn documentTotal;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn accessKey;

        private Button btnVaciar;
        private Button btnImportar1;
        private Button btnImportar2;

        private TextBox txtDirectorio;
        private Button btnExaminar;
        private Button btnDescargar;
        private Label lblEstado;
    }
}
