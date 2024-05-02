using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace MyWebSocket.Controller
{
    public class WSController : ControllerBase
    {
        [Route("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static async Task Echo(WebSocket webSocket)
        {
            //var buffer = new byte[1024 * 4];
            //var receiveResult = await webSocket.ReceiveAsync(
            //    new ArraySegment<byte>(buffer), CancellationToken.None);

            //while (!receiveResult.CloseStatus.HasValue)
            //{
            //    var rnd = new Random();
            //    var number = rnd.Next(1, 100);
            //    string message = string.Format("You luck Number is '{0}'. Dont't remember that", number.ToString());
            //    byte[] outMsg = Encoding.UTF8.GetBytes(message);

            //    await webSocket.SendAsync(
            //        new ArraySegment<byte>(outMsg, 0, receiveResult.Count),
            //        receiveResult.MessageType,
            //        receiveResult.EndOfMessage,
            //        CancellationToken.None);

            //    receiveResult = await webSocket.ReceiveAsync(
            //        new ArraySegment<byte>(buffer), CancellationToken.None);
            //}

            //await webSocket.CloseAsync(
            //    receiveResult.CloseStatus.Value,
            //    receiveResult.CloseStatusDescription,
            //    CancellationToken.None);

            var bag = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(bag), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var inComingMesage = Encoding.UTF8.GetString(bag, 0, result.Count);
                Console.WriteLine("\nClients says that: '{0}'", inComingMesage);
                var rnd = new Random();
                var number = rnd.Next(1, 100);
                string message = string.Format("You luck Number is '{0}'. Dont't remember that", number.ToString());
                byte[] outGoingMeesage = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(outGoingMeesage, 0, outGoingMeesage.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(bag), CancellationToken.None);

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

        }
    }
}
