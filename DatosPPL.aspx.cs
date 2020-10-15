using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class DatosPPL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



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

        SqlCommand command = new SqlCommand(db + ".dbo.list_areas", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();
        int i = 0;
        while (rdr.Read())
        {
            listaAreas.Items.Add(rdr.GetValue(0).ToString());
            i++;
        }

        sql_conexion.Close();

        /*  try {*/

        command = new SqlCommand(db + ".dbo.list_rubros", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();
        i = 0;
        while (rdr.Read())
        {
            listaRubro.Items.Add(rdr.GetValue(0).ToString());
            i++;
        }

        sql_conexion.Close();


   



        /********************************* SCRIPT ENSAMBLADO *************************************/
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    { 
        enviar_correo();
        iniciar_ppl();
        Response.Redirect("PPL.aspx");
    }

 
    
    void enviar_correo()
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

        SqlCommand command = new SqlCommand(db + ".dbo.get_mail_addresses", sql_conexion);
        command.Parameters.Add(new SqlParameter("@area", listaAreas.Text));
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();
        
        while (rdr.Read())
        {
            email.To.Add(rdr.GetValue(1).ToString());
        }

        sql_conexion.Close();

        /****agregar los meros buenos y copiar a todo mundo*/

        /****al final cambiar de cc a mi a todo el mundo y quitar lo de SIMULACRO*/
        email.CC.Add("rodolfo.martinez@lancerworldwide.com");
        email.CC.Add("lmx_staff_mexico@lancerworldwide.com");
        email.CC.Add("pn_all@lancercorp.com");
        email.Subject = "Alerta de Paro de Linea " + listaAreas.Text;
        email.IsBodyHtml = false;

        email.Body = "\n";
        email.Body += "\n\n";
        email.Body += "La linea " + listaAreas.Text + "entro en paro de linea";
        email.Body += "\n\n";
        email.Body = "Detalles";
        email.Body += "\n\n";
        email.Body += "Rubro: " + listaRubro.Text;
        email.Body += "\n\n";
        email.Body += "Comentarios: " + observacioones.Text;
        email.Body += "\n\n";
        email.Body += "Reporto:" + quienreporta.Text;
        email.Body += "\n\n";
        email.Body += "Inicio: " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        email.Body += "\n\n";
        email.Body += "\n\n";
        email.Body += "\n\n";
        email.Body += "Powered by IT Mexico";
        smtp_server.Send(email);

    }

    void iniciar_ppl()
    {
        SqlConnection sql_conexion;

        string conexion_string;/*nombre o ip del servidor , */
        string servidor = "MXCOPNAPPS01";
        string instancia = "sqlexpress";
        string db = "LMX_PPL";

        conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
        /*conexion_string = @"Data Source=mxcopnapps01\sqlexpress ;Initial Catalog=plexus_audit;Integrated Security=True";*/
        sql_conexion = new SqlConnection(conexion_string);


        SqlCommand command = new SqlCommand(db + ".dbo.iniciar_PPL", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@area", listaAreas.Text));
        command.Parameters.Add(new SqlParameter("@rubro", listaRubro.Text));
        command.Parameters.Add(new SqlParameter("@observaciones", observacioones.Text));
        command.Parameters.Add(new SqlParameter("@reporto", quienreporta.Text));

        sql_conexion.Open();
        command.ExecuteNonQuery();




        /*}
        catch (Exception ex)
        {


        }*/
        if (sql_conexion != null)
        {
            sql_conexion.Close();
        }
    }

}
