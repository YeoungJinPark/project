import sys

from ui.chat_page import Ui_Form
from PyQt5.QtWidgets import QDialog, QApplication, QWidget
from PyQt5 import QtWidgets


class ChatRoom(QDialog, Ui_Form):
    def __init__(self, _id, roomname, client):
        super().__init__()
        self.setupUi(self)
        self.show()
        self._id = _id
        self.roomname = roomname
        self.room = None
        self.client = client
        self.load_room()

    def load_room(self):
        self.client.send_int_to_server(3)
        self.client.send_data_to_server(self.roomname)
        data = self.client.recv_from_server()
        self.room = data.split(':')
        self.people_lbl.setText(str(len(self.room)))

    def user_list(self):
        _widget = QWidget()



def show_error_message(message, traceback):
    msg_box = QtWidgets.QMessageBox()
    msg_box.setIcon(QtWidgets.QMessageBox.Critical)
    msg_box.setWindowTitle("Error")
    msg_box.setText(message)
    msg_box.exec()
    traceback.print_exc()


if __name__ == '__main__':
    sys.excepthook = lambda exctype, value, traceback: show_error_message(str(value), traceback)
    myapp = QApplication(sys.argv)
    sys.exit(myapp.exec())
