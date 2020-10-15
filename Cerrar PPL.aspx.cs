using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;


public partial class Cerrar_PPL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string tabla = "";

        tabla = "<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>" +
                        "<script type ='text/javascript'>" +
                                "google.charts.load('current', {'packages':['table']" +
                                "});" +
                        "google.charts.setOnLoadCallback(drawTable);" +

                        "function drawTable()" +
                        "{" +

    "var data = new google.visualization.DataTable();" +
    "data.addColumn('string', 'PPL');" +
    "data.addColumn('string', 'Area');" +
    "data.addColumn('string', 'Origen');" +
    "data.addColumn('string', 'Comentarios');" +
    "data.addColumn('string', 'Inicio');" +
    "data.addColumn('string', 'Cerrar');" +
    "data.addColumn('string', 'Verificar');" +
    "data.addRows([";


       

        SqlConnection sql_conexion;
        string conexion_string;/*nombre o ip del servidor , */
        string servidor = "MXCOPNAPPS01";
        string instancia = "sqlexpress";
        string db = "LMX_PPL";

        conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
        sql_conexion = new SqlConnection(conexion_string);

        SqlDataReader rdr = null;

        SqlCommand command = new SqlCommand(db + ".dbo.list_ppl_cierre", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        sql_conexion.Open();
        rdr = command.ExecuteReader();

        while (rdr.Read())
        {
            if (rdr.GetValue(9).ToString() == "Abierto")
            {
                tabla = tabla + " ['" + rdr.GetValue(6).ToString() + "','" + rdr.GetValue(0).ToString() + "', '" + rdr.GetValue(1).ToString() + "','" + rdr.GetValue(7).ToString() + "' ,'" + rdr.GetValue(2).ToString() + ":" + rdr.GetValue(3).ToString() + "','<a href=./CierrePPL.aspx?PPL_id=" + rdr.GetValue(6).ToString() + ">Cerrar</a>',''],";
            }
            if (rdr.GetValue(9).ToString() == "Cerrado")
            {
                tabla = tabla + " ['" + rdr.GetValue(6).ToString() + "','" + rdr.GetValue(0).ToString() + "', '" + rdr.GetValue(1).ToString() + "','" + rdr.GetValue(7).ToString() + "' ,'" + rdr.GetValue(2).ToString() + ":" + rdr.GetValue(3).ToString() + "','Cerrado','<a href=./VerificacionPPL.aspx?PPL_id=" + rdr.GetValue(6).ToString() + ">Verificar</a>'],";
            }
            if (rdr.GetValue(9).ToString() == "Verificado")
            {
                tabla = tabla + " ['" + rdr.GetValue(6).ToString() + "','" + rdr.GetValue(0).ToString() + "', '" + rdr.GetValue(1).ToString() + "','" + rdr.GetValue(7).ToString() + "' ,'" + rdr.GetValue(2).ToString() + ":" + rdr.GetValue(3).ToString() + "','Cerrado','Verificado'],";
            }

        }

            tabla = tabla + " ]);" +

       " var table = new google.visualization.Table(document.getElementById('tabla_ppl'));" +

"table.draw(data, {showRowNumber: false,allowHtml: true , width: '100%', height: '100%'});}" +
  "</script>";




        Page.ClientScript.RegisterStartupScript(this.GetType(), "k1", tabla, true);
    }
}