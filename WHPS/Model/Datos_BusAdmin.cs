using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    public class Datos_BusAdmin
    {
        //DataSets por Máquina
        public static DataSet Despaletizador = new DataSet();
        public static DataSet Llenadora = new DataSet();
        public static DataSet Etiquetadora = new DataSet();
        public static DataSet Encajonadora = new DataSet();
        public static bool[] Despaletizador_est = new bool[4];
        public static bool[] Llenadora_est = new bool[4];
        public static bool[] Etiquetadora_est = new bool[4];
        public static bool[] Encajonadora_est = new bool[4];

        public static string[,] FDesp = new string[20, 2];
        public static string[,] FLlen = new string[20, 2];
        public static string[,] FEtiq = new string[20, 2];
        public static string[,] FEnc = new string[20, 2];

        //Lista de valores a filtrar
        public static List<string[]> valoresAFiltrar = new List<string[]>();
    }

}
