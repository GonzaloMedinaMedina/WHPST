using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    class LanzamientoLinea
    {
        public static DataSet DBL2 = new DataSet();
        public static DataSet DBL3 = new DataSet();
        public static DataSet DBL5 = new DataSet();


        public static bool DBL2_bool = false;
        public static bool DBL3_bool = false;
        public static bool DBL5_bool = false;


        public string numLinea { get; set; }
        public string iDOrd { get; set; }
        public string iDLanz { get; set; }
        public string codProducto { get; set; }
        public string orden { get; set; }
        public string cliente { get; set; }
        public string producto { get; set; }
        public string caja { get; set; }
        public string formato { get; set; }
        public string pa { get; set; }
        public string referencia { get; set; }
        public string gdo { get; set; }
        public string tipo { get; set; }
        public string comentarios { get; set; }
        public string liquido { get; set; }
        public string observacionesLaboratorio { get; set; }
        public string materiales { get; set; }
        public string estado { get; set; }
        public string fechaInicio { get; set; }
        public string observacionesProduccion { get; set; }
        public string estadoExpedicion { get; set; }
        public string fechaExpedicion { get; set; }

        public LanzamientoLinea()
        {
            numLinea = "";
            iDOrd = "";
            iDLanz = "";
            codProducto = "";
            orden = "";
            cliente = "";
            producto = "";
            caja = "";
            formato = "";
            pa = "";
            referencia = "";
            gdo = "";
            tipo = "";
            comentarios = "";
            liquido = "";
            observacionesLaboratorio = "";
            materiales = "";
            estado = "";
            fechaInicio = "";
            observacionesProduccion = "";
            estadoExpedicion = "";
            fechaExpedicion = "";
        }

        public void GuardarInfoDeDataRow(DataRow dr)
        {
            iDOrd = dr[0].ToString();// "ID_Ord";
            iDLanz = dr[1].ToString();// "ID_Lanz";
            codProducto = dr[2].ToString();//"REFERENCIA";
            orden = dr[3].ToString();//"ORDEN";
            cliente = dr[4].ToString();//"CLIENTE";
            producto = dr[5].ToString();//"PRODUCTO";
            caja = dr[6].ToString();//"CAJAS";
            formato = dr[7].ToString();//"FORMATO";
            pa = dr[8].ToString();//"PA";
            referencia = dr[9].ToString();//"REF.";
            gdo = dr[10].ToString();//"GDO.";
            tipo = dr[11].ToString();//"TIPO";
            comentarios = dr[12].ToString();//"COMENTARIOS";
            liquido = dr[13].ToString();//"LÍQUIDOS";
            observacionesLaboratorio = dr[14].ToString();//"OBSERVACIONES";
            materiales = dr[15].ToString();//"MATERIALES";
            estado = dr[16].ToString();//"ESTADO";
            fechaInicio = dr[17].ToString();//"FECHA INICIO (doble click)";
            observacionesProduccion = dr[18].ToString();//"OBSERVACIONES PRODUCCIÓN";
            estadoExpedicion = dr[19].ToString();//"ESTADO2";
            fechaExpedicion = dr[20].ToString();//"Fecha";
        }

        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 21; i++)
            {
                dt.Columns.Add("");
            }

            DataRow dr = dt.NewRow();
            dr[0] = iDOrd;// "ID_Ord";
            dr[1] = iDLanz;// "ID_Lanz";
            dr[2] = codProducto;//"REFERENCIA";
            dr[3] = orden;//"ORDEN";
            dr[4] = cliente;//"CLIENTE";
            dr[5] = producto;//"PRODUCTO";
            dr[6] = caja;//"CAJAS";
            dr[7] = formato;//"FORMATO";
            dr[8] = pa;//"PA";
            dr[9] = referencia;//"REF.";
            dr[10] = gdo;//"GDO.";
            dr[11] = tipo;//"TIPO";
            dr[12] = comentarios;//"COMENTARIOS";
            dr[13] = liquido;//"LÍQUIDOS";
            dr[14] = observacionesLaboratorio;//"OBSERVACIONES";
            dr[15] = materiales;//"MATERIALES";
            dr[16] = estado;//"ESTADO";
            dr[17] = fechaInicio;//"FECHA INICIO (doble click)";
            dr[18] = observacionesProduccion;//"OBSERVACIONES PRODUCCIÓN";
            dr[19] = estadoExpedicion;//"ESTADO2";
            dr[20] = fechaExpedicion;//"Fecha";
            return dr;
        }
    }
}
