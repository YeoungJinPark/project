from PyQt5 import QtWidgets, QtGui
from PyQt5.QtGui import QPixmap, QPainter

from kiosk.kiosk_end import EndPage
from kiosk.pay_ui import Ui_PayForm

class PayClass(QtWidgets.QDialog, Ui_PayForm):
    def __init__(self, geo, total_pay, cursor):
        super().__init__()
        self.setupUi(self)
        self.resize(geo[0], geo[1])  # 메인페이지와 크기, 위치를 맞춤
        self.cursor = cursor
        self.price = total_pay+3000
        self.label_order.setText(f"{total_pay}원")
        self.label_pay.setText(f"{self.price}원")
        self.button_back.clicked.connect(self.accept)
        self.paint()

    def paint(self):
        pixmap = QPixmap("menu_img/total.jpg")
        painter = QPainter(pixmap)
        painter.setRenderHint(QPainter.Antialiasing)
        painter.setPen(QtGui.QColor(255, 255, 255))
        txt = f"{self.price}원 결제하기"
        metrics = painter.fontMetrics()
        painter.drawText((pixmap.width() - metrics.width(txt)) // 2,
                         (pixmap.height() - metrics.height()) // 2,
                         txt)
        painter.end()
        self.label_total.setPixmap(pixmap)

    def mousePressEvent(self, event):
        label = self.childAt(event.pos())
        if label is not self.label_total:
            return
        self.cursor.execute("DELETE FROM cartTbl")
        self.endpage()

    def endpage(self):
        geo = [self.width(), self.height()]
        endpage = EndPage(geo)
        self.hide()
        result = endpage.exec_()
        if result:
            self.accept()
        else:
            self.close()



if __name__ == '__main__':
    pass