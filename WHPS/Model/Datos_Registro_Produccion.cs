using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    class Datos_Registro_Produccion
    {
        //DataSets por Máquina
        public static DataSet Llenadora_Porduccion = new DataSet();
        public static DataSet Encajonadora_Porduccion = new DataSet();
        public static DataSet Etiquetadora_Porduccion = new DataSet();



        public string numLinea { get; set; }
        public string Maquinista { get; set; }
        public string CodigoProducion { get; set; }
        public string Producto { get; set; }
        public string NBotellasTotal { get; set; }
        public string Barra { get; set; }
        public string Deposito { get; set; }
        public string Frio { get; set; }
        public string Barra2 { get; set; }
        public string Deposito2 { get; set; }
        public string Frio2 { get; set; }




        public Datos_Registro_Produccion()
        {
            numLinea = "";
            Maquinista = "";
            CodigoProducion = "";
            Producto = "";
            NBotellasTotal = "";
            Barra = "";
            Deposito = "";
            Frio = "";
            Barra2 = "";
            Deposito2 = "";
            Frio2 = "";
        }

        public void GuardarInfoDeDataRow(DataRow dr)
        {
            Maquinista = dr[0].ToString();// "Maquinista";
            CodigoProducion = dr[1].ToString();// "Codigo de produccion";
            Producto = dr[2].ToString();//"Descripcion del producto";
            NBotellasTotal = dr[3].ToString();//"Numero total de botellas";
            Barra = dr[4].ToString();//"Barra del líquido";
            Deposito = dr[5].ToString();//"Deposito de procedencia";
            Frio = dr[6].ToString();//"Subdeposito de procedencia";
            Barra2 = dr[7].ToString();//"Barra del líquido";
            Deposito2 = dr[8].ToString();//"Deposito de procedencia";
            Frio2 = dr[9].ToString();//"Subdeposito de procedencia";
        }

        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 10; i++)
            {
                dt.Columns.Add("");
            }

            DataRow dr = dt.NewRow();
            dr[0] = Maquinista;
            dr[1] = CodigoProducion;
            dr[2] = Producto;
            dr[3] = NBotellasTotal;
            dr[4] = Barra;
            dr[5] = Deposito;
            dr[6] = Frio;
            dr[7] = Barra2;
            dr[8] = Deposito2;
            dr[9] = Frio2;
            return dr;
        }
    }
}
