var btnConnect = document.getElementById("btnConnect");
var btnSendMessage = document.getElementById("btnSendMessage");
var lblMessage = document.getElementById("lblMessage");
var btnDisconnect = document.getElementById("btnDisconnect");
var socket;

btnConnect.onclick = function () {
    socket = new WebSocket("ws://localhost:8888/ws");
    socket.onopen = function (e) {
        console.log("Connected", e);
    };
    socket.onclose = function (e) {
        console.log("Disconnected", e);
    };
    socket.onerror = function (e) {
        console.error(e.data);
    };
    socket.onmessage = function (e) {
        console.log(e.data);
    };
}

btnSendMessage.onclick = function () {
    if (!socket || socket.readyState != WebSocket.OPEN) {
        console.error("Houston we have a problem! Socket not connected.");
    }
    var data = lblMessage.value;
    socket.send(data);
    console.log(data);
}

btnDisconnect.onclick = function () {
    if (!socket || socket.readyState != WebSocket.OPEN) {
        console.error("Houston we have a problem! Socket not connected.");
    }
    socket.close(1000, "Closing from Apollo 13");
}
