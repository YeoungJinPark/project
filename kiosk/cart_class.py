from PyQt5 import QtWidgets, QtGui
from PyQt5.QtGui import QPixmap, QPainter
from PyQt5.QtWidgets import QApplication, QLabel

from kiosk.cart_ui import Ui_CartForm
from kiosk.kiosk_pay import PayClass


class CartClass(QtWidgets.QDialog, Ui_CartForm):
    def __init__(self, geo, cursor):
        super().__init__()
        self.setupUi(self)
        self.resize(geo[0], geo[1])  # 메인페이지와 크기, 위치를 맞춤
        self.cursor = cursor
        self.font = QtGui.QFont()  # 폰트
        self.font.setFamily("맑은 고딕")  # 기본 폰트
        self.font.setPointSize(20)
        self.button_back.clicked.connect(self.accept)
        self.cart_total = 0
        self.setlist()

    def setlist(self):
        orderlist = self.cursor.execute("SELECT menu.menuIMG, menu.name, cart.totalPrice, cart.cnt "
                                        "FROM cartTbl cart JOIN menuTbl menu ON cart.menuID = menu.menuID")
        self.widget_list.setLayout(QtWidgets.QVBoxLayout())
        for img, name, price, cnt in orderlist:
            spacer1 = QtWidgets.QSpacerItem(20, 40, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)
            spacer2 = QtWidgets.QSpacerItem(20, 40, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)
            hlayout = QtWidgets.QHBoxLayout()
            vlayout = QtWidgets.QVBoxLayout()
            pixmap = QPixmap(f"menu_img/{img}")
            self.cart_total += price

            label_name = QLabel(name)
            label_name.setFont(self.font)
            vlayout.addWidget(label_name)

            label_img = QLabel()
            label_img.setScaledContents(True)
            label_img.setMaximumSize(145, 70)
            label_img.setPixmap(pixmap)
            hlayout.addWidget(label_img)
            hlayout.addItem(spacer1)

            label_price = QLabel(f"{price}원")
            label_price.setFont(self.font)
            hlayout.addWidget(label_price)
            hlayout.addItem(spacer2)

            label_cnt = QLabel(f"{cnt}개")
            label_cnt.setFont(self.font)
            hlayout.addWidget(label_cnt)
            vlayout.addLayout(hlayout)
            self.widget_list.layout().addLayout(vlayout)

        spacer = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Expanding)
        self.widget_list.layout().addItem(spacer)
        self.paint()


    def paint(self):
        pixmap = QPixmap("menu_img/total.jpg")
        painter = QPainter(pixmap)
        painter.setRenderHint(QPainter.Antialiasing)
        self.font.setPointSize(23)
        painter.setFont(self.font)
        painter.setPen(QtGui.QColor(255, 255, 255))
        txt = f"배달 주문하기     {self.cart_total}원"
        metrics = painter.fontMetrics()
        painter.drawText((pixmap.width() - metrics.width(txt)) // 2,
                         (pixmap.height() - metrics.height()) // 2 + metrics.height() // 2,
                         txt)
        painter.end()
        self.label_total.setPixmap(pixmap)

    def mousePressEvent(self, event):
        label = self.childAt(event.pos())
        if label is not self.label_total:
            return
        if self.cart_total <= 0:
            popup_msg("메뉴를 추가해주세요")
            return
        self.payments()

    def payments(self):
        geo = [self.width(), self.height()]
        paypage = PayClass(geo, self.cart_total, self.cursor)
        self.hide()
        result = paypage.exec_()
        if result:
            self.accept()
        else:
            self.setlist()
            self.show()


def show_error_message(message, traceback):
    msg_box = QtWidgets.QMessageBox()
    msg_box.setIcon(QtWidgets.QMessageBox.Critical)
    msg_box.setWindowTitle("Error")
    msg_box.setText(message)
    msg_box.exec()
    traceback.print_exc()

def popup_msg(message):
    msg_box = QtWidgets.QMessageBox()
    msg_box.setIcon(QtWidgets.QMessageBox.Warning)
    msg_box.setWindowTitle("Warning")
    msg_box.setText(message)
    msg_box.exec()


if __name__ == '__main__':
    pass
