using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHPS.Utiles
{
    public class DataEventArgs<T> : EventArgs
    {
        public T data { get; set; }
   //     public delegate void DataEventArgsHandler(object sender, DataEventArgs<T> e);
        public delegate void DataEventArgsHandler(object sender, EventArgs e);

        public DataEventArgs(T d) {
            data = d;
            Console.WriteLine("EVENTO CAPTURADOOOO: "+data);
        }



    }
}
