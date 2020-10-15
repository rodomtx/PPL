using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class PPL : System.Web.UI.Page
{

 

protected void Page_Load(object sender, EventArgs e)
    {
       

        Response.AppendHeader("Refresh", 30 + "; URL=./PPL.aspx");
    
    DateTime hoy = DateTime.Now;

        fecha.InnerHtml = hoy.ToString("dd MMMM yyyy");
        
        

        SqlConnection sql_conexion;
        string conexion_string;/*nombre o ip del servidor , */
        string servidor="MXCOPNAPPS01";
        string instancia= "sqlexpress";
        string db="LMX_PPL";

        conexion_string = "Data Source=" + servidor + @"\" + instancia + ";Initial Catalog=" + db + "; Integrated Security=SSPI;";
        /*conexion_string = @"Data Source=mxcopnapps01\sqlexpress ;Initial Catalog=plexus_audit;Integrated Security=True";*/
        sql_conexion = new SqlConnection(conexion_string);

        SqlDataReader rdr = null;

        string[] areas = new string[20];
        
        
        /*  try {*/

        SqlCommand command = new SqlCommand(db + ".dbo.list_areas", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();
        int i = 0;
        while (rdr.Read())
        {
            areas[i] = rdr.GetValue(0).ToString();
            i++;
        }

        sql_conexion.Close();






        /********************************* SCRIPT ENSAMBLADO *************************************/

        string grafica = "google.charts.load('current', { packages:['timeline']});" +
        "google.charts.setOnLoadCallback(drawChart);" +
        "function drawChart() {" +

            "var container = document.getElementById('example5.1');" +
            "var chart = new google.visualization.Timeline(container);" +
            "var dataTable = new google.visualization.DataTable();" +
            "dataTable.addColumn({ type: 'string', id: 'Room' });" +
            "dataTable.addColumn({ type: 'string', id: 'Name' });" +
            "dataTable.addColumn({ type: 'date', id: 'Start' });" +
            "dataTable.addColumn({ type: 'date', id: 'End' });" +
            "dataTable.addRows([";

        foreach (string area in areas)
            {

            if (area == null)
            { }
            else { 
            grafica = grafica + "['" + area + "', '', new Date(0, 0, 0, 7, 0, 0), new Date(0, 0, 0, 7, 0, 0)]," +
                 "['" + area + "', '', new Date(0, 0, 0, " + hoy.Hour + ", " + hoy.Minute + ", 0), new Date(0, 0, 0," + hoy.Hour + ", " + hoy.Minute + ", 0)]," +
      "['" + area + "', '', new Date(0, 0, 0, 17, 6, 0), new Date(0, 0, 0, 17,6, 0)],";


            }
        }

        /*************** seccion de eventos */////////////
            

       command = new SqlCommand(db + ".dbo.list_ppl", sql_conexion);
        command.CommandType = System.Data.CommandType.StoredProcedure;

        sql_conexion.Open();
        rdr = command.ExecuteReader();
        
        while (rdr.Read())
        {
            /* rdr.GetValue(0).ToString();*/
            
            grafica = grafica + "['" + rdr.GetValue(0).ToString()  + "', '" + rdr.GetValue(1).ToString() + "', new Date(0, 0, 0, " + rdr.GetValue(2) + ", " + rdr.GetValue(3) + ", 0), new Date(0, 0, 0, " + rdr.GetValue(4) + "," + rdr.GetValue(5) + ", 0)],";
        }

        sql_conexion.Close();


        
     
      
      
      /**************************************************/  
      
      grafica=grafica+
      
      "]);" +

    "var options = {" +
      "timeline: { colorByRowLabel: true }" +
    "};" +

    "chart.draw(dataTable, options);" +
  "}";



        /***************************************************/
        Page.ClientScript.RegisterStartupScript(this.GetType(), "k1", grafica, true);
    }





}