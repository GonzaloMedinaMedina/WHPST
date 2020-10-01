using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    class Datos_Produccion
    {
        //DataSets por Máquina
        public static DataSet EncajonadoraL5 = new DataSet();
        public static DataSet Llenadora = new DataSet();
        public static DataSet EncajonadoraL3 = new DataSet();
        public static DataSet EncajonadoraL2 = new DataSet();
        public static bool[] EncajonadoraL5_est = new bool[4];
        public static bool[] Llenadora_est = new bool[4];
        public static bool[] EncajonadoraL3_est = new bool[4];
        public static bool[] EncajonadoraL2_est = new bool[4];

        public static string[,] FEncL5 = new string[20, 2];
        public static string[,] FLlen = new string[20, 2];
        public static string[,] FEncL3 = new string[20, 2];
        public static string[,] FEncL2 = new string[20, 2];

        //Lista de valores a filtrar
        public static List<string[]> valoresAFiltrar = new List<string[]>();
    }
}
