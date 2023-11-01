import sys

from PyQt5 import QtWidgets, QtCore, QtGui
from PyQt5.QtGui import QPixmap, QPainter
from PyQt5.QtWidgets import QApplication, QLabel
from PyQt5.QtCore import Qt

from kiosk.detail_ui import Ui_FormDetail
from kiosk.kiosk_ui import Ui_FormMain


class KioskForm(QtWidgets.QWidget, Ui_FormMain):
    def __init__(self):
        super().__init__()
        self.setupUi(self)
        self.tabWidget = self.menuTab.widget(0).layout()
        self.load_ui()
        self.timer = QtCore.QTimer(self)
        self.timer.timeout.connect(self.next_page)
        self.timer.start(3000)
        self.currPage = 0

    def load_ui(self):
        self.add_img("hotdog", 1, 14)
        self.add_banner("banner", 6)
        self.add_img("side", 1, 3)
        self.add_banner("banner", 7)
        self.add_img("sauce", 1, 9)
        self.add_banner("banner", 8)
        self.add_drink("sikhye", 1, 1, 100)
        self.add_banner("banner", 9)
        self.add_drink("drink", 1, 8, 100)
        self.add_banner("banner", 10)
        self.add_drink("ambasa", 1, 1, 100)
        self.add_banner("banner", 11)
        self.add_drink("minute", 1, 2, 100)
        self.add_banner("banner", 12)
        self.add_drink("power", 1, 1, 100)
        self.add_banner("banner", 13)
        self.add_drink("dia", 1, 1, 100)
        self.add_drink("txt", 2, 3, 420)

    def add_img(self, f_name, start, end):
        for i in range(start, end+1):
            menu_name = f"{f_name}_{i}"
            pixmap = QPixmap(f"menu_img/{menu_name}.jpg")
            label = QLabel(menu_name)
            label.setPixmap(pixmap)
            label.setScaledContents(True)
            # label.setObjectName(f"{f_name}_{where}")
            label.setMaximumSize(540, 160)
            self.tabWidget.addWidget(label)

    def add_banner(self, f_name, index):
        pixmap = QPixmap(f"menu_img/{f_name}_{index}.jpg")
        label = QLabel(f"{f_name}_{index}")
        label.setPixmap(pixmap)
        label.setScaledContents(True)
        label.setMaximumSize(540, 65)
        line_open = QtWidgets.QFrame()
        line_open.setFrameShape(QtWidgets.QFrame.HLine)
        line_open.setFrameShadow(QtWidgets.QFrame.Sunken)
        line_close = QtWidgets.QFrame()
        line_close.setFrameShape(QtWidgets.QFrame.HLine)
        line_close.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.tabWidget.addWidget(line_open)
        self.tabWidget.addWidget(label)
        self.tabWidget.addWidget(line_close)

    def add_drink(self, f_name, start, end, h):
        for i in range(start, end+1):
            menu_name = f"{f_name}_{i}"
            pixmap = QPixmap(f"menu_img/{menu_name}.jpg")
            label = QLabel(menu_name)
            label.setPixmap(pixmap)
            label.setScaledContents(True)
            label.setMaximumSize(540, h)
            self.tabWidget.addWidget(label)

    def next_page(self):
        self.currPage += 1
        if self.currPage >= self.stackedWidget.count():
            self.currPage = 0

        self.stackedWidget.setCurrentIndex(self.currPage)

    def mousePressEvent(self, event):
        label = self.childAt(event.pos())
        if isinstance(label, QLabel) and "menu_" in label.objectName():
            geo = [self.width(), self.height()]
            detail_page = DetailPage(int("_".join(label.objectName().split("_")[1:])), geo)
            self.hide()
            detail_page.exec_()
            self.show()


class DetailPage(QtWidgets.QDialog, Ui_FormDetail):
    def __init__(self, menu, geo):
        super().__init__()
        self.setupUi(self)
        self.price_cnt = 1
        self.price_menu = 0
        self.price_opt = 0
        self.price_total = 0
        self.option = []
        self.group_box = []
        self.font = QtGui.QFont()
        self.resize(geo[0], geo[1])
        self.menu = menu
        self.button_back.clicked.connect(self.accept)
        self.button_cart.clicked.connect(self.cart)
        self.label_cnt = QLabel("label_cnt")
        self.what_menu()
        self.cart = []
        # 메뉴별 set_menu를 사용할 코드 작성 if

    def set_page(self, data):
        page = data[4]
        img_label = QPixmap(f"menu_img/{data[0]}.jpg")
        expln = QPixmap(f"menu_img/{data[1]}.jpg")

        self.label_img.setPixmap(img_label)
        self.label_expln.setPixmap(expln)

        self.label_name.setText(f"{data[2]}")
        self.label_price3.setText(f"{data[3]}")

        layout_opt = self.widget_option.layout()
        cnt = 0
        self.font.setFamily("맑은 고딕")
        opt = QtWidgets.QVBoxLayout()

        # 옵션 생성
        for i in range(len(page)):
            self.font.setPointSize(20)
            label_opt = QLabel(f"cmt_{i}")
            label_opt.setText(page[i][0])
            label_opt.setFont(self.font)
            opt.addWidget(label_opt)
            gbox = QtWidgets.QButtonGroup()

            for j in range(2, len(page[i])):
                option = []
                hbox_line = QtWidgets.QHBoxLayout()
                hspacer = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)
                self.font.setPointSize(16)
                label_price = QLabel()
                label_price.setFont(self.font)
                cnt += 1
                cbox = QtWidgets.QCheckBox()
                cbox.setFont(self.font)
                option.append(cbox)
                for key, value in page[i][j].items():
                    cbox.setText(key)
                    label_price.setText(f"+{value}원")
                    option.append(value)
                if page[i][1]:
                    cbox.setAutoExclusive(True)
                    gbox.addButton(cbox)
                hbox_line.addWidget(cbox)
                hbox_line.addItem(hspacer)
                hbox_line.addWidget(label_price)
                opt.addLayout(hbox_line)
                self.option.append(option)
            self.group_box.append(gbox)

        # H레이아웃
        layout_cnt = QtWidgets.QHBoxLayout()
        # 수량 라벨
        label_tag = QLabel("label_tag")
        self.font.setPointSize(20)
        label_tag.setFont(self.font)
        label_tag.setText("수량")
        layout_cnt.addWidget(label_tag)
        # 스페이서
        hspacer = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)
        layout_cnt.addItem(hspacer)
        # -버튼
        minus_btn = QtWidgets.QPushButton("-")
        minus_btn.setFont(self.font)
        minus_btn.setObjectName("button_minus")
        layout_cnt.addWidget(minus_btn)
        minus_btn.clicked.connect(self.plus_minus)
        # 갯수 라벨
        self.price_cnt = 1
        self.label_cnt.setFont(self.font)
        self.label_cnt.setText(f"{self.price_cnt}개")
        layout_cnt.addWidget(self.label_cnt)
        # +버튼
        plus_btn = QtWidgets.QPushButton("+")
        plus_btn.setObjectName("button_plus")
        plus_btn.setFont(self.font)
        layout_cnt.addWidget(plus_btn)
        plus_btn.clicked.connect(self.plus_minus)

        opt.addLayout(layout_cnt)
        layout_opt.addLayout(opt)

        self.price_menu = data[3]
        self.set_checkbox()
        self.paint()
        self.label_total.setPixmap(self.label_total.pixmap())

    def paint(self):
        pixmap = QPixmap("menu_img/total.jpg")
        painter = QPainter(pixmap)
        painter.setRenderHint(QPainter.Antialiasing)
        self.font.setPointSize(23)
        painter.setFont(self.font)
        painter.setPen(QtGui.QColor(255, 255, 255))
        self.price_total = self.price_menu * self.price_cnt + self.price_opt
        txt = f"{self.price_total}원"
        metrics = painter.fontMetrics()
        painter.drawText((pixmap.width() - metrics.width(txt)) // 2,
                         (pixmap.height() - metrics.height()) // 2 + metrics.height() // 2,
                         txt)
        painter.end()
        self.label_total.setPixmap(pixmap)

    def set_checkbox(self):
        for cbox in self.option:
            cbox[0].stateChanged.connect(lambda state, price=cbox[1]: self.checkbox(state, price))

    def checkbox(self, state, price):
        if state == 0:
            self.price_opt = 0
        elif state == 2:
            self.price_opt = price
        self.paint()

    def set_menu(self, img, expln, name, price, opt):
        page = [img, expln, name, price, opt]
        self.set_page(page)

    def what_menu(self):
        if self.menu == 1:
            self.set_menu("menu_01", "expln_01", "꽈배기 세트", 4500,
                          [["설탕 선택1", True, {"설탕 뿌림(O)": 200}, {"설탕 안뿌림(X)": 0}],
                           ["설탕 선택2", True, {"설탕 뿌림(O)": 0}, {"설탕 안뿌림(X)": 0}]])

    def plus_minus(self):
        btn = self.sender()

        if btn.objectName() == "button_plus" and self.price_cnt <= 99:
            self.price_cnt += 1
        elif btn.objectName() == "button_minus" and self.price_cnt > 1:
            self.price_cnt -= 1
        self.label_cnt.setText(f"{self.price_cnt}개")
        self.paint()

    def cart(self):
        pass

    def closeEvent(self, event):
        sys.exit()


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
    kiosk = KioskForm()
    kiosk.show()
    sys.exit(myapp.exec())
