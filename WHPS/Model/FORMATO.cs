using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    class FORMATO
    {
        public static DataSet BOM_REFERENCIA = new DataSet();
        public static DataSet BOM_DESCRIPCION = new DataSet();
        public static DataSet FORMATO_POSICION = new DataSet();

        public static bool BOM_REFERENCIA_Bool = false;
        public static bool BOM_DESCRIPCION_Bool = false;
        public static bool FORMATO_POSICION_Bool = false;

        public string DescMaterial { get; set; }
        public string CodMaterial { get; set; }
        public string Matundventa { get; set; }

        public FORMATO()
        {
            DescMaterial = "";
            CodMaterial = "";
            Matundventa = "";
        }

        public void GuardarInfoDeDataRow(DataRow dr)
        {
            DescMaterial = dr[0].ToString();// "DescMaterial";
            CodMaterial = dr[1].ToString();// "CodMaterial";
            Matundventa = dr[2].ToString();//"Matundventa";

        }

        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 21; i++)
            {
                dt.Columns.Add("");
            }

            DataRow dr = dt.NewRow();
            dr[5] = DescMaterial;// "DescMaterial";
            dr[4] = CodMaterial;// "CodMaterial";
            dr[6] = Matundventa;//"Matundventa";
            return dr;
        }
    }
}
