using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS.Model
{
    public class Datos_Botellas:Datos_Materiales
    {
        public string eanTM { set; get; }
        public string LoteFabTM { set; get; }
        public string FechaFabTM { set; get; }
        public Datos_Botellas()
        {
            refInt = "";
            whDescrip = "";
            ean = "";
            eanTM = "";
            Proveedor = "";
            FechaFab = "";
            FechaFabTM = "";
            LoteFabTM = "";
            LoteFab = "";
            SSCC = "";
            Cantidad = "";

        }
    }

}
