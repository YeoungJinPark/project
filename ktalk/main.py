import sys

from PyQt5 import QtWidgets
from PyQt5.QtWidgets import QWidget, QApplication
from PyQt5.QtCore import Qt

from chatroom import ChatRoom
from ui.login_page import Ui_Form
from ui.list_box_ui import Ui_ListBox
from client import Client


class LoginPage(QWidget, Ui_Form):
    def __init__(self):
        super().__init__()
        self.setupUi(self)
        self.connected = False
        self.client = Client()
        self.sock = None
        self._ui = None
        self._id = None
        self.loginButton.clicked.connect(self.login)
        self.join_lbl.mousePressEvent = self.signup_btn_clicked

    def room_list(self):
        select = 2
        self.client.send_int_to_server(select)
        length = int.from_bytes(self.sock.recv(4), 'big')
        if length > 0:
            data = self.sock.recv(length).decode('utf-8')
            data_list = data.split(':')
            for room in data_list:
                _widget = QWidget()
                self._ui = Ui_ListBox()
                self._ui.setupUi(_widget)
                self._ui.label_2.setText(room)
                self.scrollAreaWidgetContents.layout().setAlignment(Qt.AlignTop)
                self.scrollAreaWidgetContents.layout().addWidget(_widget)
                #  마지막 수신 메세지 추가 필요
                _widget.mousePressEvent = lambda event: self.join_room(event, room=self._ui.label_2.text())

    def join_room(self, event, room):
        chat_room = ChatRoom(self._id, room, self.client)
        self.close()
        chat_room.exec_()

    def login(self):
        if not self.connected:
            self.connect_server()
        account = {}
        select = 0
        self.client.send_int_to_server(select)
        check_id = self.id_line.text()
        check_pw = self.pw_line.text()
        length = int.from_bytes(self.sock.recv(4), 'big')
        if length > 0:
            data = self.sock.recv(length).decode('utf-8')
            data_list = data.strip().split('\n')
            for user_list in data_list:
                id_, pw_, name_ = user_list.split(':')
                account[id_] = pw_
            if check_id in account:
                if account[check_id] == check_pw:  # id,pw 일치
                    self._id = check_id
                    self.room_list()
                    self.set_page(3)
                else:
                    self.label_4.setText("비밀번호가 일치하지 않습니다.")    # pw 불일치
            else:
                self.label_4.setText("ID가 존재하지 않습니다.")    # id 존재하지 않음
        else:
            self.label_4.setText("ID가 존재하지 않습니다.")    # 가입된 유저 없음

    def signup_btn_clicked(self, event):
        if not self.connected:
            self.connect_server()
        self.stackedWidget.setCurrentIndex(1)
        self.pushButton_3.clicked.connect(self.signup)

    def signup(self):
        makeid = self.make_id.text()
        makepw = self.make_pw.text()
        makecpw = self.check_pw.text()
        if makepw != makecpw:
            self.error_lbl.setText("비밀번호 확인이 일치하지 않습니다.")
            return
        makename = self.make_name.text()
        idlist = []
        select = 1
        self.client.send_int_to_server(select)
        length = self.sock.recv(4)
        length = int.from_bytes(length, 'big')
        if length > 0:
            data = self.sock.recv(length).decode('utf-8')
            data_list = data.strip().split('\n')
            for user_list in data_list:
                _id, _pw, _name = user_list.split(':')
                idlist.append(_id)
            if makeid in idlist:
                self.error_lbl.setText("이미 존재하는 ID입니다.")
                self.client.send_int_to_server(0)  # 종료신호
                return
            else:
                self.client.send_int_to_server(1)  # 성공신호
                data = ':'.join([makeid, makepw, makename])
                self.client.send_data_to_server(data)
                self.set_page(2)
                self.pushButton_4.clicked.connect(lambda: self.set_page(0))

        else:
            self.client.send_int_to_server(1)  # 성공신호
            data = ':'.join([makeid, makepw, makename])
            self.client.send_data_to_server(data)
            self.set_page(2)
            self.pushButton_4.clicked.connect(lambda: self.set_page(0))

    def connect_server(self):
        self.client.set_sock()
        self.connectLabel.setText("서버에 접속되었습니다.")
        self.sock = self.client.sock
        self.connected = True

    def set_page(self, index):
        self.stackedWidget.setCurrentIndex(index)


def show_error_message(message, traceback):
    msg_box = QtWidgets.QMessageBox()
    msg_box.setIcon(QtWidgets.QMessageBox.Critical)
    msg_box.setWindowTitle("Error")
    msg_box.setText(message)
    msg_box.exec()
    traceback.print_exc()

if __name__ == '__main__':
    sys.excepthook = lambda exctype, value, traceback: show_error_message(str(value), traceback)
    app = QApplication(sys.argv)
    main_page = LoginPage()
    main_page.show()
    app.exec_()
