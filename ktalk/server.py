from socket import *

import threading

import PyQt5.QtCore

import file as f


class Server:
    def __init__(self, port):
        self.port = port
        self._serverSock = None
        self.clients = []
        self.lock = threading.Lock()
        threading.Thread(target=self.start_server).start()

    def set_socket(self):
        self._serverSock = socket(AF_INET, SOCK_STREAM)
        self._serverSock.setsockopt(SOL_SOCKET, SO_REUSEADDR, 1)
        self._serverSock.bind(('', self.port))
        self._serverSock.listen()

    def start_server(self):
        self.set_socket()
        while True:
            sock, addr = self._serverSock.accept()
            print(f'접속IP:{addr}')
            client = ClientHandler(sock, addr)
            self.clients.append(client)
            threading.Thread(target=client.recv_from_client).start()


class ClientHandler(PyQt5.QtCore.QObject):
    received = PyQt5.QtCore.pyqtSignal(bytes, str)

    def __init__(self, sock, addr):
        super().__init__()
        self.sock = sock
        self.addr = addr
        self.id = ''
        self.room = ''

    def send_user_data(self):
        data = f.read_user()
        if data is not False:
            self.send_to_client(data)
        else:
            length = 0
            self.sock.send(length.to_bytes(4, 'big'))

    def signup(self):
        self.send_user_data()
        result = int.from_bytes(self.sock.recv(4), 'big')
        if result == 0:
            return
        length = int.from_bytes(self.sock.recv(4), 'big')
        data = self.sock.recv(length).decode('utf-8')
        f.write_user(data)

    def send_room_list(self):
        roomlist = f.read_list()
        self.send_to_client(roomlist)

    def get_userinfo(self):
        length = self.sock.recv(4)
        self.id, self.room = self.sock.recv(length).decode('utf-8').split(':')

    def send_roominfo(self):
        length = int.from_bytes(self.sock.recv(4), 'big')
        roomname = self.sock.recv(length).decode('utf-8')
        roominfo = f.read_room(roomname)
        self.send_to_client(roominfo)

    def recv_from_client(self):
        while True:
            select = int.from_bytes(self.sock.recv(4), 'big')
            if select == 0:  # 로그인
                self.send_user_data()
            if select == 1:  # 회원가입
                self.signup()
            if select == 2:  # 방 목록
                self.send_room_list()
            if select == 3:  # 방 정보
                self.send_roominfo()
            if select == 4:  # 메세지
                pass

    def send_to_client(self, data):
        data = data.encode('utf-8')
        length = len(data)
        self.sock.sendall(length.to_bytes(4, 'big'))
        if length > 0:
            self.sock.sendall(data)


if __name__ == '__main__':
    server = Server(9192)
