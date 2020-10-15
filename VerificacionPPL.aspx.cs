using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;


public partial class VerificacionPPL : System.Web.UI.Page
{
    public string seguridad;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PPL_id"]))
            {

                ViewState["seguridad"] = pin().ToString();

                SqlConnection sql_conexion;
                string conexion_string;/*nombre o ip del servidor , */
                string servidor = "MXCOPNAPPS01";
                string instancia = "sqlexpress";
                string db = "LMX_PPL";

                conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
                /*conexion_string = @"Data Source=mxcopnapps01\sqlexpress ;Initial Catalog=plexus_audit;Integrated Security=True";*/
                sql_conexion = new SqlConnection(conexion_string);

                SqlDataReader rdr = null;

                /*  try {*/

                SqlCommand command = new SqlCommand(db + ".dbo.detail_ppl", sql_conexion);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@PPL_id", Int32.Parse(Request.QueryString["PPL_id"])));
                ViewState["PPL_id"] = Int32.Parse(Request.QueryString["PPL_id"]);

                sql_conexion.Open();
                rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                   area.Text = rdr.GetValue(0).ToString();
                    causa.Text = rdr.GetValue(1).ToString();
                    observaciones.Text = rdr.GetValue(2).ToString();
                    quienreporta.Text = rdr.GetValue(3).ToString();
                }
                enviar_correo(ViewState["seguridad"].ToString());

            }
            else
            {
              
            }


        }

    }

    public string pin()
    {
        Random _random = new Random();
        string ret_pin = "";

        for (int j = 0; j <= 5; j++)
        {
            ret_pin = ret_pin + _random.Next(0, 9).ToString();
        }
        return ret_pin;
    }

    protected void VerificarPPL_Click(object sender, EventArgs e)
    {
        string mensaje = "";
        if (codigo.Text == ViewState["seguridad"].ToString())

        {


            /*** aqui va la consulta para cerrarlo , el correo para la persona que va verificar ***/
            SqlConnection sql_conexion;
            string conexion_string;/*nombre o ip del servidor , */
            string servidor = "MXCOPNAPPS01";
            string instancia = "sqlexpress";
            string db = "LMX_PPL";

            conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
            /*conexion_string = @"Data Source=mxcopnapps01\sqlexpress ;Initial Catalog=plexus_audit;Integrated Security=True";*/
            sql_conexion = new SqlConnection(conexion_string);

            /*  try {*/

            SqlCommand command = new SqlCommand(db + ".dbo.verificar_ppl", sql_conexion);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id_PPL", Int32.Parse(ViewState["PPL_id"].ToString())));

            sql_conexion.Open();
            command.ExecuteNonQuery();





            /*** aqui va la consulta para cerrarlo , el correo para la persona que va verificar ***/
            mensaje = "alert('El PPL ha sido verificado'); windows.location='CerrarPPL.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "k1", mensaje, true);
            /*aqui debe reenviar a la pagina de cerrado*/
            Response.Redirect("Cerrar PPL.aspx");
        }
        else
        { mensaje = "alert('Codigo de seguridad invalido, por favor intente nuevamente');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "k1", mensaje, true);
        }
        
    }

    void enviar_correo(string _pin)
    {
        SmtpClient smtp_server = new SmtpClient();
        MailMessage email = new MailMessage();
        smtp_server.Port = 587;
        smtp_server.EnableSsl = true;
        smtp_server.Host = "smtp.office365.com";
        smtp_server.Credentials = new System.Net.NetworkCredential("noreply@lancerworldwide.com", "Lancer2013!");

        email.From = new MailAddress("noreply@lancerworldwide.com");
        /****agregar los meros buenos y copiar a todo mundo*/
        SqlConnection sql_conexion;
        string conexion_string;/*nombre o ip del servidor , */
        string servidor = "MXCOPNAPPS01";
        string instancia = "sqlexpress";
        string db = "LMX_PPL";

        conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
        /*conexion_string = @"Data Source=mxcopnapps01\sqlexpress ;Initial Catalog=plexus_audit;Integrated Security=True";*/
        sql_conexion = new SqlConnection(conexion_string);

        SqlDataReader rdr = null;

        /*  try {*/

        SqlCommand command = new SqlCommand(db + ".dbo.get_verificador_mail", sql_conexion);
        command.Parameters.Add(new SqlParameter("@area", area.Text));
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();

        while (rdr.Read())
        {
            email.To.Add(rdr.GetValue(1).ToString());
        }
        email.CC.Add("esmeralda.perales@lancerworldwide.com");
        email.CC.Add("rodolfo.martinez@lancerworldwide.com");
        sql_conexion.Close();

                email.Subject = "Codigo para verificar PPL";
        email.IsBodyHtml = false;

        email.Body = "\n";
        email.Body += "\n\n";
        email.Body += "PIN : " + _pin;
        email.Body += "\n\n";
        email.Body += "\n\n";
        email.Body += "Powered by IT Mexico";
        smtp_server.Send(email);

    }
}