using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    public class Datos_Volumen
    {
        public string Proveedor { set; get; }
        public string Volumen { set; get; }
        public string refInt { set; get; }
        public string Cantidad { set; get; }
        public string temperatura { set; get; }
        public string capacidad { set; get; }
        public string graduacion { set; get; }
        public string SSCC { set; get; }
        public string whDescrip { set; get; }
        public string LoteFab { set; get; }
        public string estado { set; get; }
        public string FechaFab { set; get; }
        public decimal error { set; get; }

        public Datos_Volumen()
        {
            refInt = "";
            Volumen = "";
            Proveedor = "";
            temperatura = "";
            Cantidad = "";
            capacidad = "";
            error=0;
            graduacion = "";
            whDescrip = "";
            LoteFab = "";
            Cantidad = "";
            estado = ""; 
            FechaFab = "";

        }
    }

}
