using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Rpgwo_Server.Networking
{
    public class Listener : IDisposable
    {
        private Socket _Listener;
        private readonly AsyncCallback _OnAccept;

        public Listener(IPEndPoint endPoint)
        {
            BindIPEndPoint(endPoint);

            if (_Listener == null)
                return;

            _OnAccept = OnAccept;

            StartAccepting();
        }

        private void StartAccepting()
        {
            try
            {
                _Listener.BeginAccept(_OnAccept, _Listener);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Logging.
            }
        }

        private void OnAccept(IAsyncResult asyncResult)
        {
            Socket listener = (Socket)asyncResult.AsyncState;
            Socket socket = null;

            try
            {
                socket = listener.EndAccept(asyncResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Logging.
            }

            if (socket != null)
            {
                Console.WriteLine("Accepted socket connection: " + socket.RemoteEndPoint);

                NetClient nc = new NetClient(socket);
            }
            else
            {
                Console.WriteLine("Socket was null."); // TODO :: Logging.
            }
            
            try
            {
                listener.BeginAccept(_OnAccept, listener);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Logging.
            }
        }

        private void BindIPEndPoint(IPEndPoint endPoint)
        {
            try
            {
                _Listener = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                _Listener.Bind(endPoint);

                _Listener.Listen(10);
            } 
            catch (Exception e)
            {
                _Listener = null;

                Console.WriteLine(e); // TODO :: Logging.
            }
        }

        public void Dispose()
        {
            if (_Listener == null)
                return;

            _Listener.Close();
        }
    }
}
