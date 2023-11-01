from socket import *

from PyQt5.QtCore import QObject, pyqtSignal


class Client(QObject):
    recv_signal = pyqtSignal(bytes)

    def __init__(self):
        super().__init__()
        self.sock = None

    def set_sock(self):
        self.sock = socket(AF_INET, SOCK_STREAM)
        ip = "10.10.20.36"
        # ip = "172.16.5.164"
        port = 9192
        addr = (ip, port)
        self.sock.connect(addr)
        # data = f"{self.id}:{self.room}".encode('utf-8')
        # length = len(data).to_bytes(4, 'big')
        # self.sock.sendall(length+data)

    def recv_from_server(self):
        length = int.from_bytes(self.sock.recv(4), 'big')
        data = self.sock.recv(length).decode('utf-8')
        return data

    def send_data_to_server(self, data):
        data = data.encode('utf-8')
        length = len(data).to_bytes(4, 'big')
        self.sock.sendall(length)
        self.sock.sendall(data)

    def send_int_to_server(self, num):
        self.sock.send(num.to_bytes(4, 'big'))
