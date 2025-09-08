//using System;
//using System.ComponentModel;
//using System.Windows.Forms;

//namespace Desktop.WinForm.SRI.Download.XML
//{
//    public partial class Form1 : Form
//    {
//        private BindingList<ArchivoItem> archivos = new BindingList<ArchivoItem>();

//        public Form1()
//        {
//            InitializeComponent();

//            dataGridView1.AutoGenerateColumns = false; // usa las columnas del Designer
//            dataGridView1.DataSource = archivos;

//            // Eventos de botones
//            btnVaciar.Click += (s, e) => { archivos.Clear(); lblEstado.Text = "0 archivos en la lista"; };
//            btnImportar1.Click += (s, e) => { AgregarArchivo("001-001-0001", "Proveedor A", "100.50"); };
//            btnImportar2.Click += (s, e) => { AgregarArchivo("002-002-0002", "Proveedor B", "200.75"); };
//            btnExaminar.Click += BtnExaminar_Click;
//            btnDescargar.Click += BtnDescargar_Click;
//        }

//        private void AgregarArchivo(string numero, string proveedor, string total)
//        {
//            archivos.Add(new ArchivoItem
//            {
//                documentType = "Factura",
//                document = numero,
//                provider = proveedor,
//                issueDate = DateTime.Now.ToString("dd-MM-yyyy"),
//                authorizationDate = DateTime.Now.ToString("dd-MM-yyyy"),
//                documentTotal = total,
//                status = "Pendiente"
//            });
//            lblEstado.Text = $"{archivos.Count} archivos en la lista";
//        }

//        private void BtnExaminar_Click(object sender, EventArgs e)
//        {
//            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
//            {
//                if (fbd.ShowDialog() == DialogResult.OK)
//                    txtDirectorio.Text = fbd.SelectedPath;
//            }
//        }

//        private void BtnDescargar_Click(object sender, EventArgs e)
//        {
//            MessageBox.Show($"Descargando {archivos.Count} archivos en: {txtDirectorio.Text}");
//        }
//    }

//    public class ArchivoItem
//    {
//        public string documentType { get; set; }
//        public string document { get; set; }
//        public string provider { get; set; }
//        public string issueDate { get; set; }
//        public string authorizationDate { get; set; }
//        public string documentTotal { get; set; }
//        public string status { get; set; }
//    }
//}

using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Desktop.WinForm.SRI.Download.XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Esto llama al Designer y carga los controles
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UltimaRuta))
            {
                txtDirectorio.Text = Properties.Settings.Default.UltimaRuta;
            }
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            lblEstado.Text = "0 archivos en la lista";
        }

        private void CargarArchivoEnDataGrid(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                notify("Archivo no encontrado", MessageBoxIcon.Error);
                return;
            }

            var lineas = File.ReadAllLines(rutaArchivo);

            // Limpiar la tabla
            dataGridView1.Rows.Clear();

            // La primera línea son los encabezados
            for (int i = 1; i < lineas.Length; i++)
            {
                var campos = lineas[i].Split('\t'); // Tab como separador

                if (campos.Length >= 12)
                {
                    var documentType = campos[2];
                    var document = campos[3];
                    var provider = campos[0] + " - " + campos[1];
                    var issueDate = DateTime.TryParse(campos[6], out DateTime issue) ? issue.ToString("dd/MM/yyyy") : "";
                    var authorizationDate = DateTime.TryParse(campos[5], out DateTime authorization) ? authorization.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    var documentTotal = string.Format("${0:0.00}", campos[10]);
                    var accessKey = campos[4];

                    dataGridView1.Rows.Add(
                        documentType,  // TIPO_COMPROBANTE -> documentType
                        document,  // NUMERO_DOCUMENTO_MODIFICADO -> document
                        provider,  // RAZON_SOCIAL_EMISOR -> provider
                        issueDate,  // FECHA_EMISION -> issueDate
                        authorizationDate,  // FECHA_AUTORIZACION -> authorizationDate
                        documentTotal, // IMPORTE_TOTAL -> documentTotal
                        "Pendiente", // status
                        accessKey
                    );
                }
            }

            lblEstado.Text = $"{dataGridView1.Rows.Count} archivos en la lista";
        }


        private void btnImportar1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos TXT|*.txt|Archivos CSV|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CargarArchivoEnDataGrid(ofd.FileName);
            }
        }

        private void btnImportar2_Click(object sender, EventArgs e)
        {
            // Otra lógica de importar
            //AgregarFila("Factura", "002-002-0002", "Proveedor B", "03-01-2025", "04-01-2025", "200.75", "Pendiente", "");
            notify("En construcción", MessageBoxIcon.Warning); // cargar TXt con pura Claves de Acceso o Abrir Formulario para digitar Claves de Acceso (Se recomienda la 1ra opción)
        }




        public async Task<string> DescargarArchivoAsync(string claveAcceso)
        {
            string xml = null;

            string url = "https://cel.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantesOffline?wsdl";

            string soapXml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.autorizacion"">
                <soapenv:Header/>
                <soapenv:Body>
                    <ec:autorizacionComprobante>
                       <claveAccesoComprobante>{claveAcceso}</claveAccesoComprobante>
                    </ec:autorizacionComprobante>
                </soapenv:Body>
            </soapenv:Envelope>
            ";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("Accept", "text/xml");
                client.DefaultRequestHeaders.Add("User-Agent", "MiAppWindowsForms");
                client.DefaultRequestHeaders.Add("Host", "cel.sri.gob.ec");

                var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");

                var response = await client.PostAsync(url, content);

                response.EnsureSuccessStatusCode();

                xml = await response.Content.ReadAsStringAsync();

                var doc = new XmlDocument();
                doc.LoadXml(xml);

                //var respuesta = new RespuestaSri();

                // Caso: No existe
                var numeroNode = doc.SelectSingleNode("//numeroComprobantes");
                if (numeroNode != null && numeroNode.InnerText == "0")
                {
                    //"NO EXISTE";
                    //"El comprobante no está registrado en el SRI.";
                    return null;
                }

                // Buscar estado (AUTORIZADO o RECHAZADA)
                var estadoNode = doc.SelectSingleNode("//autorizacion/estado");

                if (estadoNode == null)
                {
                    //"ERROR";
                    //"No se encontró el nodo de estado en la respuesta.";
                    return null;
                }

                var status = estadoNode.InnerText;

                if (status == "AUTORIZADO")
                {
                    //var xmlAutorizacionNode = doc.SelectSingleNode("//autorizacion");
                    //if (xmlAutorizacionNode != null)
                    //{
                    //    xmlDoc = xmlAutorizacionNode.InnerXml;
                    //}
                    return xml;
                }

                return null;
                //else if (status == "RECHAZADA")
                //{
                //    var mensajeNode = doc.SelectSingleNode("//autorizacion/mensajes/mensaje/mensaje");
                //    var infoNode = doc.SelectSingleNode("//autorizacion/mensajes/mensaje/informacionAdicional");

                //mensajeNode?.InnerText ?? "Error desconocido";
                //infoNode?.InnerText ?? "";
                //}
            }

        }



        public async Task<bool> ProbarServicioAsync()
        {
            try
            {

                string url = "https://cel.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantesOffline?wsdl";

                string soapXml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.autorizacion"">
                    <soapenv:Header/>
                    <soapenv:Body>
                        <ec:autorizacionComprobante>
                           <claveAccesoComprobante>0000000000000000000000000000000000000000000000000</claveAccesoComprobante>
                        </ec:autorizacionComprobante>
                    </soapenv:Body>
                </soapenv:Envelope>
                ";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Accept", "text/xml");
                    client.DefaultRequestHeaders.Add("User-Agent", "MiAppWindowsForms");
                    client.DefaultRequestHeaders.Add("Host", "cel.sri.gob.ec");

                    var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        return true; // Servicio activo aunque la clave sea inválida

                    return false; // Responde pero con error 500, 400, etc.
                }
            }
            catch
            {
                return false; // No responde el servidor
            }
        }


        private void GuardarArchivo(string xml, string rutaDestino, string nombreArchivo)
        {
            try
            {
                // unify route end
                string pathCompleto = Path.Combine(rutaDestino, nombreArchivo);
                // save file xml
                File.WriteAllText(pathCompleto, xml, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException ex)
            {
                notify("Error al guardar XML: " + ex.Message, MessageBoxIcon.Error);
            }
        }



        private void btnExaminar_Click(object sender, EventArgs e)
        {
            //using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            //{
            //    if (fbd.ShowDialog() == DialogResult.OK)
            //        txtDirectorio.Text = fbd.SelectedPath;
            //}

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.UltimaRuta))
                {
                    fbd.SelectedPath = Properties.Settings.Default.UltimaRuta;
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDirectorio.Text = fbd.SelectedPath;

                    // Guardar la ruta en Settings
                    Properties.Settings.Default.UltimaRuta = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void notify(string message, MessageBoxIcon icon)
        {
            // Información general

            // Error

            // Éxito / Operación completada
            // también puedes usar MessageBoxIcon.Information si prefieres

            switch (icon)
            {
                //case MessageBoxIcon.None:
                //    break;
                //case MessageBoxIcon.Hand:
                //    break;
                //case MessageBoxIcon.Question:
                //    break;
                //case MessageBoxIcon.Exclamation:
                //    break;
                case MessageBoxIcon.Asterisk:
                    MessageBox.Show($"{message}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                //case MessageBoxIcon.Stop:
                //    break;
                case MessageBoxIcon.Error:
                    MessageBox.Show($"{message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case MessageBoxIcon.Warning:
                    MessageBox.Show($"{message}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show($"{message}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            // disabled order columns - temp
            dataGridView1.AllowUserToOrderColumns = false;

            //// verify selected rows 
            //if (dataGridView1.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("Seleccione al menos una fila para descargar.");
            //    return;
            //}

            // get route to save xml
            string rutaDestino = txtDirectorio.Text;

            // verify route to save xml
            if (string.IsNullOrWhiteSpace(rutaDestino) || !Directory.Exists(rutaDestino))
            {
                notify("Ruta de destino inválida.", MessageBoxIcon.Information);
                return;
            }

            // invoce service sri is active
            bool activo = await ProbarServicioAsync();

            // verify service sri is active
            if (!activo)
            {
                notify("El servicio del SRI no está disponible en este momento.", MessageBoxIcon.Error);
                return;
            }

            // loop of table to download xml
            //foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // skip row empty
                if (fila.IsNewRow)
                    continue;

                // get document status 
                string status = fila.Cells["status"].Value?.ToString();
                // skip document in status ok
                if (status == "Descargado")
                    continue;

                // get access key
                string claveAcceso = fila.Cells["accessKey"].Value.ToString();

                // change status
                fila.Cells["status"].Value = "Descargado...";

                // change status color
                //fila.DefaultCellStyle.BackColor = Color.LightCyan;
                fila.DefaultCellStyle.BackColor = Color.FromArgb(173, 216, 230); // azul claro
                fila.DefaultCellStyle.ForeColor = Color.Black;

                // invoke service sri - download xml
                string xml = await DescargarArchivoAsync(claveAcceso);

                // verify xml empty
                if (!string.IsNullOrEmpty(xml))
                {
                    // get document type
                    string documentType = fila.Cells["documentType"].Value.ToString();
                    // get document number 
                    string document = fila.Cells["document"].Value.ToString();
                    // document name structure
                    string documentName = $"{documentType} {document}.xml";

                    // save file
                    GuardarArchivo(xml, rutaDestino, documentName);

                    // change status
                    fila.Cells["status"].Value = "Descargado";
                    // change status color
                    //fila.DefaultCellStyle.BackColor = Color.LightGreen;
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144); // verde claro
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // change status
                    fila.Cells["status"].Value = "Error";
                    // change status color
                    //fila.DefaultCellStyle.BackColor = Color.LightCoral;
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193); // rojo claro
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            notify("Descarga finalizada.", MessageBoxIcon.Information);
            
            // enabled order columns
            dataGridView1.AllowUserToOrderColumns = true;
        }

        //private void AgregarFila(string tipo, string doc, string proveedor, string fechaDoc, string fechaRec, string total, string estado, string accessKey)
        //{
        //    dataGridView1.Rows.Add(tipo, doc, proveedor, fechaDoc, fechaRec, total, estado, accessKey);
        //    lblEstado.Text = $"{dataGridView1.Rows.Count} archivos en la lista";
        //}
    }
}

