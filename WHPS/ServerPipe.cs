using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHPS
{
    class ServerPipe
    {
        public static string nombre = "Gonzalo";
        public static bool login = false;
        private string orden;
        private string producto;
        private string cliente;
        private string botellas;
        private string graduacion;
        private string capacidad;
        private string lote;

        public ServerPipe()
        {

            PipeSecurity pse = new PipeSecurity();
            pse.SetAccessRule(new PipeAccessRule("Usuarios", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow));//Set access rules

            using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("testpipe", PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 1024, 1024, pse, HandleInheritability.None))//new NamedPipeServerStream("testpipe", PipeDirection.In))
            {
                Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                Console.Write("Waiting for client connection...");
                pipeServer.WaitForConnection();

                Console.WriteLine("Client connected.");
                try
                {
                    StreamReader sr = new StreamReader(pipeServer);
                    StreamWriter sw = new StreamWriter(pipeServer);
                    sw.AutoFlush = true;

                    sw.WriteLine("true");
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        public ServerPipe(string ord, string prod, string cli, string bot, string grad, string cap, string lot)
        {
            this.orden = ord;
            this.producto = prod;
            this.cliente = cli;
            this.botellas = bot;
            this.graduacion = grad;
            this.capacidad = cap;
            this.lote = lot;

            PipeSecurity pse = new PipeSecurity();
            pse.SetAccessRule(new PipeAccessRule("Usuarios", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow));//Set access rules

            using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("testpipe", PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 1024, 1024, pse, HandleInheritability.None))//new NamedPipeServerStream("testpipe", PipeDirection.In))
            {
                Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                Console.Write("Waiting for client connection...");
                pipeServer.WaitForConnection();

                Console.WriteLine("Client connected.");
                try
                {
                    StreamReader sr = new StreamReader(pipeServer);
                    StreamWriter sw = new StreamWriter(pipeServer);
                    sw.AutoFlush = true;



                    sw.WriteLine(orden);
                    sw.WriteLine(producto);
                    sw.WriteLine(cliente);
                    sw.WriteLine(botellas);
                    sw.WriteLine(graduacion);
                    sw.WriteLine(capacidad);
                    sw.WriteLine(lote);

                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

}
