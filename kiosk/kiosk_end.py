from PyQt5 import QtWidgets

from kiosk.end_ui import Ui_Form


class EndPage(QtWidgets.QDialog, Ui_Form):
    def __init__(self, geo):
        super().__init__()
        self.setupUi(self)
        self.resize(geo[0], geo[1])  # 메인페이지와 크기, 위치를 맞춤

    def mousePressEvent(self, event):
        self.accept()

    def closeEvent(self, event):
        self.close()

if __name__ == '__main__':
    pass