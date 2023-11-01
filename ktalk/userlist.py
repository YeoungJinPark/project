from PyQt5.QtWidgets import QDialog
from ui.user_list import Ui_Form

class UserList(QDialog, Ui_Form):
    def __init__(self):
        super().__init__()
        self.setupUi(self)