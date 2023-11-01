import sys
import sqlite3

from PyQt5 import QtWidgets, QtCore, QtGui
from PyQt5.QtGui import QPixmap, QPainter
from PyQt5.QtWidgets import QApplication, QLabel
# from PyQt5.QtCore import Qt

from kiosk.detail_ui import Ui_FormDetail
from kiosk.kiosk_ui import Ui_FormMain
from kiosk.cart_class import CartClass


class KioskForm(QtWidgets.QWidget, Ui_FormMain):
    def __init__(self):
        super().__init__()
        self.setupUi(self)
        self.conn = sqlite3.connect("menu.db")
        self.cursor = self.conn.cursor()
        self.set_db()
        self.set_menu()
        self.currPage = 0
        self.timer = QtCore.QTimer(self)
        self.timer.timeout.connect(self.next_page)
        self.timer.start(3000)
        self.buttonCart.clicked.connect(self.cart_page)

    def set_db(self):
        list_menu = [
            ("정보", 0, "txt_1.jpg", None, None),
            ("대표메뉴", 0, "banner_1.jpg", None, None),
            ("꽈배기 세트", 4500, "list_1.jpg", "menu_1.jpg", "expln_1.jpg"),
            ("(New)라면맵땅 핫도그", 3000, "list_2.jpg", "menu_2.jpg", "expln_2.jpg"),
            ("푸지미 세트", 25000, "list_3.jpg", "menu_3.jpg", "expln_3.jpg"),
            ("딜리버리 세트", 12800, "list_4.jpg", "menu_4.jpg", "expln_4.jpg"),
            ("꽈배기배너", 0, "banner_2.jpg", None, None),
            ("꽈배기", 1500, "list_5.jpg", "menu_5.jpg", "expln_5.jpg"),
            ("꽈배기 세트", 4500, "list_6.jpg", "menu_6.jpg", "expln_6.jpg"),
            ("명랑 꽈배기", 1800, "list_7.jpg", "menu_7.jpg", "expln_7.jpg"),
            ("떡볶이배너", 0, "banner_3.jpg", None, None),
            ("로제 떡볶이", 9900, "list_8.jpg", "menu_8.jpg", "expln_8.jpg"),
            ("떡볶이", 8500, "list_9.jpg", "menu_9.jpg", "expln_9.jpg"),
            ("세트메뉴배너", 0, "banner_4.jpg", None, None),
            ("갓성비 세트", 14800, "list_10.jpg", "menu_10.jpg", "expln_10.jpg"),
            ("푸지미 세트", 25000, "list_11.jpg", "menu_11.jpg", "expln_11.jpg"),
            ("인기폭탄 세트", 15900, "list_12.jpg", "menu_12.jpg", "expln_12.jpg"),
            ("딜리버리 세트", 12800, "list_13.jpg", "menu_13.jpg", "expln_13.jpg"),
            ("핫도그배너", 0, "banner_5.jpg", None, None),
            ("(New)라면맵땅 핫도그", 3000, "list_14.jpg", "menu_14.jpg", "expln_14.jpg"),
            ("고구마통모짜 핫도그", 3300, "list_15.jpg", "menu_15.jpg", "expln_15.jpg"),
            ("감자통모짜 핫도그", 3300, "list_16.jpg", "menu_16.jpg", "expln_16.jpg"),
            ("모짜맵구마 핫도그", 3100, "list_17.jpg", "menu_17.jpg", "expln_17.jpg"),
            ("감자 핫도그", 3100, "list_18.jpg", "menu_18.jpg", "expln_18.jpg"),
            ("모짜체다 핫도그", 2900, "list_19.jpg", "menu_19.jpg", "expln_19.jpg"),
            ("통모짜 핫도그", 2800, "list_20.jpg", "menu_20.jpg", "expln_20.jpg"),
            ("반반모짜 핫도그", 2600, "list_21.jpg", "menu_21.jpg", "expln_21.jpg"),
            ("체다치즈 핫도그", 2600, "list_22.jpg", "menu_22.jpg", "expln_22.jpg"),
            ("점보 핫도그", 2600, "list_23.jpg", "menu_23.jpg", "expln_23.jpg"),
            ("맵반모짜 핫도그", 2600, "list_24.jpg", "menu_24.jpg", "expln_24.jpg"),
            ("할라피뇨 핫도그", 2600, "list_25.jpg", "menu_25.jpg", "expln_25.jpg"),
            ("통가래떡 핫도그", 2300, "list_26.jpg", "menu_26.jpg", "expln_26.jpg"),
            ("명랑 핫도그", 1800, "list_27.jpg", "menu_27.jpg", "expln_27.jpg"),
            ("사이드배너", 0, "banner_6.jpg", None, None),
            ("쫀달치즈볼", 5000, "list_28.jpg", "menu_28.jpg", "expln_28.jpg"),
            ("만두튀김", 5000, "list_29.jpg", "menu_29.jpg", "expln_29.jpg"),
            ("불닭치즈소떡", 3500, "list_30.jpg", "menu_30.jpg", "expln_30.jpg"),
            ("소스/시즈닝배너", 0, "banner_7.jpg", None, None),
            ("소스보따리", 1500, "list_31.jpg", "menu_31.jpg", "expln_31.jpg"),
            ("스위트칠리 소스", 300, "list_32.jpg", "menu_32.jpg", "expln_32.jpg"),
            ("핫칠리 소스", 300, "list_33.jpg", "menu_33.jpg", "expln_33.jpg"),
            ("치즈맛머스타드 소스", 300, "list_34.jpg", "menu_34.jpg", "expln_34.jpg"),
            ("허니머스타드 소스", 300, "list_35.jpg", "menu_35.jpg", "expln_35.jpg"),
            ("체다치즈맛 소스", 300, "list_36.jpg", "menu_36.jpg", "expln_36.jpg"),
            ("토마토케첩(2개) 소스", 300, "list_37.jpg", "menu_37.jpg", "expln_37.jpg"),
            ("오니언크림 시즈닝", 300, "list_38.jpg", "menu_38.jpg", "expln_38.jpg"),
            ("버터갈릭 시즈닝", 300, "list_39.jpg", "menu_39.jpg", "expln_39.jpg"),
            ("음료배너", 0, "banner_8.jpg", None, None),
            ("명랑 쌀 식혜", 2000, "list_40.jpg", "menu_40.jpg", "expln_40.jpg"),
            ("콜라/사이다/환타배너", 0, "banner_9.jpg", None, None),
            ("코카콜라", 1500, "list_41.jpg", "menu_41.jpg", "expln_41.jpg"),
            ("코카콜라", 2000, "list_42.jpg", "menu_42.jpg", "expln_42.jpg"),
            ("코카콜라", 3000, "list_43.jpg", "menu_43.jpg", "expln_43.jpg"),
            ("코카콜라 제로", 1500, "list_44.jpg", "menu_44.jpg", "expln_44.jpg"),
            ("스프라이트", 1500, "list_45.jpg", "menu_45.jpg", "expln_45.jpg"),
            ("스프라이트", 3000, "list_46.jpg", "menu_46.jpg", "expln_46.jpg"),
            ("환타 오렌지", 1500, "list_47.jpg", "menu_47.jpg", "expln_47.jpg"),
            ("환타 파인애플", 1500, "list_48.jpg", "menu_48.jpg", "expln_48.jpg"),
            ("탄산음료배너", 0, "banner_10.jpg", None, None),
            ("밀크소다 암바사", 1500, "list_49.jpg", "menu_49.jpg", "expln_49.jpg"),
            ("델몬트/미닛메이드배너", 0, "banner_11.jpg", None, None),
            ("미닛메이드 사과", 700, "list_50.jpg", "menu_50.jpg", "expln_50.jpg"),
            ("미닛메이드 오렌지", 700, "list_51.jpg", "menu_51.jpg", "expln_51.jpg"),
            ("탄산수/이온음료배너", 0, "banner_12.jpg", None, None),
            ("파워에이드", 1500, "list_52.jpg", "menu_52.jpg", "expln_52.jpg"),
            ("생수/차음료배너", 0, "banner_13.jpg", None, None),
            ("휘오 다이아몬드EC", 1000, "list_53.jpg", "menu_53.jpg", "expln_53.jpg"),
            ("원산지 표기", 0, "txt_2.jpg", None, None),
            ("유의사항", 0, "txt_3.jpg", None, None),
        ]

        self.cursor.execute("DROP TABLE IF EXISTS menuTbl")
        self.cursor.execute(
            "CREATE TABLE menuTbl (menuID INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL,"
            "price INTEGER NOT NULL, homeIMG TEXT, menuIMG TEXT, explnIMG TEXT)")
        self.conn.commit()

        self.cursor.execute("DROP TABLE IF EXISTS cartTbl")
        self.cursor.execute(
            "CREATE TABLE cartTbl (orderID INTEGER PRIMARY KEY AUTOINCREMENT, menuID INTEGER NOT NULL, "
            "totalPrice INTEGER NOT NULL, cnt INTEGER NOT NULL, "
            "FOREIGN KEY (menuID) REFERENCES menuTbl(menuID))")
        for menu_list in list_menu:
            name, price, home, menu, expln = menu_list
            self.cursor.execute("INSERT INTO menuTbl (name, price, homeIMG, menuIMG, explnIMG) VALUES (?, ?, ?, ?, ?)",
                                (name, price, home, menu, expln))
        self.conn.commit()

    def set_menu(self):
        self.cursor.execute("SELECT menuID, homeIMG FROM menuTbl")
        result = self.cursor.fetchall()
        layout = QtWidgets.QVBoxLayout()
        self.tab_1.setLayout(layout)
        for home_menu in result:
            menu_id, home_img = home_menu
            pixmap = QPixmap(f"menu_img/{home_img}")
            label = QLabel()
            if "list_" in home_img:
                label.setObjectName(f"menu_{menu_id}")
                label.setMaximumSize(540, 160)
            elif "txt_" in home_img:
                label.setMaximumSize(540, 500)
            else:
                label.setMaximumSize(540, 70)
            label.setPixmap(pixmap)
            label.setScaledContents(True)
            self.tab_1.layout().addWidget(label)

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
            result = detail_page.exec_()
            if result:
                self.cart_page()
            self.show()

    def cart_page(self):
        geo = [self.width(), self.height()]
        cartpage = CartClass(geo, self.cursor)
        self.hide()
        result = cartpage.exec_()
        if result:
            self.conn.commit()
        self.show()



    def closeEvent(self, event):
        self.cursor.close()
        self.conn.close()


class DetailPage(QtWidgets.QDialog, Ui_FormDetail):
    def __init__(self, menu, geo):
        super().__init__()
        # DB
        self.setupUi(self)
        self.conn = sqlite3.connect("menu.db")
        self.cursor = self.conn.cursor()
        self.set_option()
        # 세부페이지
        self.price_cnt = 1      # 수량
        self.price_menu = 0     # 메뉴 가격
        self.price_opt = 0      # 옵션 가격
        self.price_total = 0    # 합산 가격
        self.name_menu = ""
        self.option = []        # 생성된 옵션들의 체크박스를 담고있는 리스트
        self.group_box = []     # 생성된 옵션의 그룹박스들을 담고있는 리스트
        self.check_boxes = []
        self.selec_min = []
        self.selec_max = []
        self.font = QtGui.QFont()   # 폰트
        self.font.setFamily("맑은 고딕")    # 기본 폰트
        self.resize(geo[0], geo[1])  # 메인페이지와 크기, 위치를 맞춤
        self.menu = menu        # 메인페이지에서 선택한 메뉴
        self.button_back.clicked.connect(self.close)   # 뒤로(메인페이지)가기 버튼
        self.button_cart.clicked.connect(self.accept)     # 장바구니 버튼
        self.label_cnt = QLabel("label_cnt")            # 수량 라벨
        self.result = False
        self.set_page()     # 세부 페이지 UI 생성 메서드

    def set_option(self):
        self.cursor.execute("DROP TABLE IF EXISTS optTypeTbl")
        self.cursor.execute("CREATE TABLE optTypeTbl (typeID INTEGER PRIMARY KEY, name TEXT NOT NULL,"
                            "min INTEGER NOT NULL, max INTEGER NOT NULL)")

        self.cursor.execute("DROP TABLE IF EXISTS idTbl")
        self.cursor.execute(
            "CREATE TABLE idTbl (menuID INTEGER NOT NULL, typeID INTEGER NOT NULL,"
            "PRIMARY KEY (menuID, typeID),"
            "FOREIGN KEY (menuID) REFERENCES menuTbl(menuID),"
            "FOREIGN KEY (typeID) REFERENCES optTypeTbl(typeID))"
        )

        self.cursor.execute("DROP TABLE IF EXISTS optionTbl")
        self.cursor.execute(
            "CREATE TABLE optionTbl (optID INTEGER PRIMARY KEY, typeID INTEGER NOT NULL,"
            "name TEXT NOT NULL, price INTEGER NOT NULL,"
            "FOREIGN KEY(typeID) REFERENCES optTypeTbl(typeID) ON DELETE NO ACTION)"
        )

        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('설탕 선택', 1, 1)")   # 1
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('설탕 선택1', 1, 1)")  # 2
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('설탕 선택2', 1, 1)")  # 3
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('설탕 선택3', 1, 1)")  # 4
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('설탕 선택4', 1, 1)")  # 5
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('떡볶이 선택', 1, 1)")  # 6
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('맵기 선택', 1, 1)")   # 7
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('떡볶이 토핑 선택1', 0, 2)")  # 8
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('떡볶이 토핑 선택2', 0, 2)")  # 9
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('점보 핫도그 설탕 선택', 1, 1)")    # 10
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('점보 핫도그 토핑 선택', 0, 1)")    # 11
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('반반모짜 핫도그 설탕 선택', 1, 1)")    # 12
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('반반모짜 핫도그 토핑 선택', 0, 1)")    # 13
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('통모짜 핫도그 설탕 선택', 1, 1)")   # 14
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('통모짜 핫도그 토핑 선택', 0, 1)")   # 15
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('통가래떡 핫도그 설탕 선택', 1, 1)")  # 16
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('통가래떡 핫도그 토핑 선택', 0, 1)")  # 17
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('할라피뇨 핫도그 설탕 선택', 1, 1)")  # 18
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('할라피뇨 핫도그 토핑 선택', 0, 1)")  # 19
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('감자통모짜 핫도그 설탕 선택', 1, 1)")  # 20
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('고구마통모짜 핫도그 설탕 선택', 1, 1)")  # 21
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('감자 핫도그 설탕 선택', 1, 1)")    # 22
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('핫도그 토핑 선택', 0, 1)")   # 23
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('소스 선택', 1, 1)")   # 24
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('소스 선택', 1, 1)")   # 25
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('소스/시즈닝 선택1', 3, 3)")  # 26
        self.cursor.execute("INSERT INTO optTypeTbl (name, min, max) VALUES ('소스/시즈닝 선택2', 3, 3)")  # 27

        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (3, 2)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (3, 3)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (3, 4)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (3, 5)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (4, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (4, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 6)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 7)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 8)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 9)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 10)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 12)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (5, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 16)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 17)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 10)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 11)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 15)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 18)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 19)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 12)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (6, 13)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (8, 1)")     # 꽈배기
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (9, 2)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (9, 3)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (9, 4)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (9, 5)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (12, 7)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (12, 8)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (12, 9)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (13, 7)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (13, 8)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (13, 9)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (15, 6)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (15, 7)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (15, 8)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (15, 9)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (15, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 6)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 7)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 8)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 9)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 10)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 12)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (16, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (17, 20)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (17, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (17, 21)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (17, 12)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (17, 22)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 16)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 17)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 10)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 11)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 14)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 15)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 18)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 19)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 12)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (18, 13)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (20, 1)")    # 라면맵땅
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (20, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (21, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (21, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (22, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (22, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (23, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (23, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (24, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (24, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (25, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (25, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (25, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (26, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (26, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (26, 24)")   # 통모짜
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (27, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (27, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (27, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (28, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (28, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (28, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (29, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (29, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (29, 24)")   # 점보
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (30, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (30, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (30, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (31, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (31, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (31, 24)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (32, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (32, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (32, 24)")   # 통가래떡
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (33, 1)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (33, 23)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (33, 24)")   # 명랑
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (39, 26)")
        self.cursor.execute("INSERT INTO idTbl (menuID, typeID) VALUES (39, 27)")

        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 뿌림(O)', 0, 1)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 안뿌림(X)', 0, 1)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 뿌림(O)', 0, 2)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 안뿌림(X)', 0, 2)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 뿌림(O)', 0, 3)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 안뿌림(X)', 0, 3)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 뿌림(O)', 0, 4)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 안뿌림(X)', 0, 4)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 뿌림(O)', 0, 5)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('설탕 안뿌림(X)', 0, 5)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('떡볶이', 0, 6)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('로제 떡볶이', 1400, 6)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('보통 맛', 0, 7)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('매운 맛', 0, 7)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('아주 매운 맛', 0, 7)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보소시지(1개)', 1000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨소시지(1개)', 1000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('체다치즈(2장)', 1000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('모짜렐라치즈(60g)', 2000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('어묵 추가(100g)', 2000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('만두튀김(3개)', 3000, 8)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보소시지(1개)', 1000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨소시지(1개)', 1000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('체다치즈(2장)', 1000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('모짜렐라치즈(60g)', 2000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('어묵 추가(100g)', 2000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('만두튀김(3개)', 3000, 9)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보 핫도그 설탕 뿌림(O)', 0, 10)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보 핫도그 설탕 안뿌림(X)', 0, 10)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보 핫도그 [토핑]감자', 500, 11)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보 핫도그 [토핑]고구마', 500, 11)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('점보 핫도그 [토핑]라면사리', 300, 11)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('반반모짜 핫도그 설탕 뿌림(O)', 0, 12)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('반반모짜 핫도그 설탕 안뿌림(X)', 0, 12)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('반반모짜 핫도그 [토핑]감자', 500, 13)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('반반모짜 핫도그 [토핑]고구마', 500, 13)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('반반모짜 핫도그 [토핑]라면사리', 300, 13)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통모짜 핫도그 설탕 뿌림(O)', 0, 14)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통모짜 핫도그 설탕 안뿌림(X)', 0, 14)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통모짜 핫도그 [토핑]감자', 500, 15)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통모짜 핫도그 [토핑]고구마', 500, 15)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통모짜 핫도그 [토핑]라면사리', 300, 15)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통가래떡 핫도그 설탕 뿌림(O)', 0, 16)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통가래떡 핫도그 설탕 안뿌림(X)', 0, 16)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통가래떡 핫도그 [토핑]감자', 500, 17)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통가래떡 핫도그 [토핑]고구마', 500, 17)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('통가래떡 핫도그 [토핑]라면사리', 300, 17)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨 핫도그 설탕 뿌림(O)', 0, 18)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨 핫도그 설탕 안뿌림(X)', 0, 18)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨 핫도그 [토핑]감자', 500, 19)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨 핫도그 [토핑]고구마', 500, 19)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('할라피뇨 핫도그 [토핑]라면사리', 300, 19)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('감자통모짜 핫도그 설탕 뿌림(O)', 0, 20)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('감자통모짜 핫도그 설탕 안뿌림(X)', 0, 20)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('고구마통모짜 핫도그 설탕 뿌림(O)', 0, 21)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('고구마통모짜 핫도그 설탕 안뿌림(X)', 0, 21)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('감자 핫도그 설탕 뿌림(O)', 0, 22)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('감자 핫도그 설탕 안뿌림(X)', 0, 22)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[토핑]감자', 500, 23)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[토핑]고구마', 500, 23)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[토핑]라면사리', 300, 23)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]토마토케첩(9g)', 0, 24)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]필요 없음(X)', 0, 24)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]토마토케첩(9g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]스위트칠리(10g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]핫칠리(10g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]치즈맛머스타드(10g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]허니머스타드(10g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]체다치즈맛(10g)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]필요 없음(X)', 0, 25)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]스위트칠리(10g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]핫칠리(10g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]치즈맛머스타드(10g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]허니머스타드(10g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]체다치즈맛(10g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]토마토케첩(9g) 2개', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]오니언크림 시즈닝(4g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]버터갈릭 시즈닝(4g)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]필요 없음(X)', 0, 26)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]스위트칠리(10g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]핫칠리(10g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]치즈맛머스타드(10g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]허니머스타드(10g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]체다치즈맛(10g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]토마토케첩(9g) 2개', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]오니언크림 시즈닝(4g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]버터갈릭 시즈닝(4g)', 0, 27)")
        self.cursor.execute("INSERT INTO optionTbl (name, price, typeID) VALUES ('[소스]필요 없음(X)', 0, 27)")

        self.conn.commit()

    def set_page(self):
        self.cursor.execute("SELECT menuIMG, explnIMG, name, price FROM menuTbl WHERE menuID=?", (self.menu,))
        home_menu = self.cursor.fetchall()
        menu_img, expln, menu_name, menu_price = home_menu[0]
        label_menu = QPixmap(f"menu_img/{menu_img}")
        label_expln = QPixmap(f"menu_img/{expln}")
        self.label_img.setPixmap(label_menu)
        self.label_expln.setPixmap(label_expln)
        self.label_name.setText(f"{menu_name}")
        self.label_price3.setText(f"{menu_price}")
        self.price_menu = menu_price

        self.cursor.execute("SELECT type.name, opt.name, opt.price, type.min, type.max "
                            "FROM optTypeTbl AS type "
                            "JOIN optionTbl AS opt ON type.typeID = opt.typeID "
                            "JOIN idTbl AS idT ON type.typeID = idT.typeID "
                            "WHERE idT.menuID=?", (self.menu,))
        options = self.cursor.fetchall()
        # opt = QtWidgets.QVBoxLayout()
        layout_opt = self.widget_option.layout()
        prev_type = None
        # 옵션 생성
        for option in options:
            type_name, opt_name, opt_price, type_min, type_max = option

            self.font.setPointSize(20)
            cbox = QtWidgets.QCheckBox()
            cbox.setFont(self.font)
            cbox.setText(opt_name)

            if type_name != prev_type:
                # 1번 옵션 체크상태, 그룹 박스
                if type_min == 1:
                    cbox.setChecked(True)
                    gbox = QtWidgets.QButtonGroup()
                    self.group_box.append(gbox)
                else:
                    cboxes = []
                    self.check_boxes.append(cboxes)
                    self.selec_min.append(type_min)
                    self.selec_max.append(type_max)
                # 옵션 카테고리 라벨
                label_opt = QLabel(type_name)
                label_opt.setFont(self.font)
                layout_opt.addWidget(label_opt)
                prev_type = type_name
            if type_min == 1:
                gbox.addButton(cbox)
            else:
                cboxes.append(cbox)

            hbox_line = QtWidgets.QHBoxLayout()
            hspacer = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)

            self.font.setPointSize(16)
            label_price = QLabel()
            label_price.setFont(self.font)
            label_price.setText(str(f"{opt_price}원"))

            cbox.stateChanged.connect(lambda state, price=opt_price:
                                      self.checkbox(state, price=price))

            hbox_line.addWidget(cbox)
            hbox_line.addItem(hspacer)
            hbox_line.addWidget(label_price)
            layout_opt.addLayout(hbox_line)
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

        # opt.addLayout(layout_cnt)
        layout_opt.addLayout(layout_cnt)
        self.paint()
        # self.label_total.setPixmap(self.label_total.pixmap())

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

    def checkbox(self, state, price):
        if state == 0:
            self.price_opt -= price
        elif state == 2:
            self.price_opt += price
        else:
            pass
        self.paint()

    def plus_minus(self):
        btn = self.sender()
        if btn.objectName() == "button_plus" and self.price_cnt <= 99:
            self.price_cnt += 1
        elif btn.objectName() == "button_minus" and self.price_cnt > 1:
            self.price_cnt -= 1
        self.label_cnt.setText(f"{self.price_cnt}개")
        self.paint()

    def mousePressEvent(self, event):
        label = self.childAt(event.pos())
        if label is not self.label_total:
            return
        for cboxes, min, max in zip(self.check_boxes, self.selec_min, self.selec_max):
            cnt = 0
            for cbox in cboxes:
                if cbox.isChecked():
                    cnt += 1
            if cnt < min:
                popup_msg("필수 옵션을 선택해주세요.")
                return
            elif cnt > max:
                popup_msg("최대 옵션 수를 초과했습니다.")
                return
        self.cursor.execute("INSERT INTO cartTbl (menuID, totalPrice, cnt) VALUES (?, ?, ?)",
                            (self.menu, self.price_total, self.price_cnt))
        self.conn.commit()
        self.close()


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
    sys.excepthook = lambda exctype, value, traceback: show_error_message(str(value), traceback)
    myapp = QApplication(sys.argv)
    kiosk = KioskForm()
    kiosk.show()
    sys.exit(myapp.exec())
