# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'kiosk.ui'
#
# Created by: PyQt5 UI code generator 5.15.9
#
# WARNING: Any manual changes made to this file will be lost when pyuic5 is
# run again.  Do not edit this file unless you know what you are doing.


from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_FormMain(object):
    def setupUi(self, FormMain):
        FormMain.setObjectName("FormMain")
        FormMain.resize(560, 888)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Fixed, QtWidgets.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(FormMain.sizePolicy().hasHeightForWidth())
        FormMain.setSizePolicy(sizePolicy)
        FormMain.setMaximumSize(QtCore.QSize(560, 905))
        self.verticalLayout = QtWidgets.QVBoxLayout(FormMain)
        self.verticalLayout.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout.setSpacing(0)
        self.verticalLayout.setObjectName("verticalLayout")
        self.widgetTop = QtWidgets.QWidget(FormMain)
        self.widgetTop.setMinimumSize(QtCore.QSize(0, 50))
        self.widgetTop.setMaximumSize(QtCore.QSize(16777215, 50))
        self.widgetTop.setObjectName("widgetTop")
        self.labelTop = QtWidgets.QLabel(self.widgetTop)
        self.labelTop.setGeometry(QtCore.QRect(10, 10, 261, 41))
        self.labelTop.setText("")
        self.labelTop.setPixmap(QtGui.QPixmap("../../kiosk/home_top1.jpg"))
        self.labelTop.setScaledContents(True)
        self.labelTop.setObjectName("labelTop")
        self.buttonCart = QtWidgets.QPushButton(self.widgetTop)
        self.buttonCart.setGeometry(QtCore.QRect(480, 0, 75, 51))
        self.buttonCart.setText("")
        icon = QtGui.QIcon()
        icon.addPixmap(QtGui.QPixmap("../../kiosk/cart_icon.png"), QtGui.QIcon.Normal, QtGui.QIcon.Off)
        self.buttonCart.setIcon(icon)
        self.buttonCart.setIconSize(QtCore.QSize(30, 30))
        self.buttonCart.setFlat(True)
        self.buttonCart.setObjectName("buttonCart")
        self.verticalLayout.addWidget(self.widgetTop)
        self.scrollArea = QtWidgets.QScrollArea(FormMain)
        self.scrollArea.setMinimumSize(QtCore.QSize(0, 0))
        self.scrollArea.setMaximumSize(QtCore.QSize(560, 16777215))
        self.scrollArea.setFrameShape(QtWidgets.QFrame.NoFrame)
        self.scrollArea.setLineWidth(0)
        self.scrollArea.setHorizontalScrollBarPolicy(QtCore.Qt.ScrollBarAlwaysOff)
        self.scrollArea.setWidgetResizable(True)
        self.scrollArea.setAlignment(QtCore.Qt.AlignCenter)
        self.scrollArea.setObjectName("scrollArea")
        self.scrollAreaWidgetContents = QtWidgets.QWidget()
        self.scrollAreaWidgetContents.setGeometry(QtCore.QRect(0, 0, 560, 3408))
        self.scrollAreaWidgetContents.setMaximumSize(QtCore.QSize(560, 16777215))
        self.scrollAreaWidgetContents.setObjectName("scrollAreaWidgetContents")
        self.verticalLayout_2 = QtWidgets.QVBoxLayout(self.scrollAreaWidgetContents)
        self.verticalLayout_2.setObjectName("verticalLayout_2")
        self.widgetStack = QtWidgets.QWidget(self.scrollAreaWidgetContents)
        self.widgetStack.setMaximumSize(QtCore.QSize(550, 300))
        self.widgetStack.setObjectName("widgetStack")
        self.verticalLayout_3 = QtWidgets.QVBoxLayout(self.widgetStack)
        self.verticalLayout_3.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout_3.setSpacing(0)
        self.verticalLayout_3.setObjectName("verticalLayout_3")
        self.stackedWidget = QtWidgets.QStackedWidget(self.widgetStack)
        self.stackedWidget.setObjectName("stackedWidget")
        self.page_0 = QtWidgets.QWidget()
        self.page_0.setObjectName("page_0")
        self.verticalLayout_4 = QtWidgets.QVBoxLayout(self.page_0)
        self.verticalLayout_4.setObjectName("verticalLayout_4")
        self.pBanner0 = QtWidgets.QLabel(self.page_0)
        self.pBanner0.setText("")
        self.pBanner0.setPixmap(QtGui.QPixmap("../../kiosk/home01.jpg"))
        self.pBanner0.setScaledContents(True)
        self.pBanner0.setAlignment(QtCore.Qt.AlignCenter)
        self.pBanner0.setObjectName("pBanner0")
        self.verticalLayout_4.addWidget(self.pBanner0)
        self.stackedWidget.addWidget(self.page_0)
        self.page_1 = QtWidgets.QWidget()
        self.page_1.setObjectName("page_1")
        self.verticalLayout_7 = QtWidgets.QVBoxLayout(self.page_1)
        self.verticalLayout_7.setObjectName("verticalLayout_7")
        self.pBanner1 = QtWidgets.QLabel(self.page_1)
        self.pBanner1.setText("")
        self.pBanner1.setPixmap(QtGui.QPixmap("../../kiosk/home02.jpg"))
        self.pBanner1.setScaledContents(True)
        self.pBanner1.setObjectName("pBanner1")
        self.verticalLayout_7.addWidget(self.pBanner1)
        self.stackedWidget.addWidget(self.page_1)
        self.page_2 = QtWidgets.QWidget()
        self.page_2.setObjectName("page_2")
        self.verticalLayout_8 = QtWidgets.QVBoxLayout(self.page_2)
        self.verticalLayout_8.setObjectName("verticalLayout_8")
        self.pBanner2 = QtWidgets.QLabel(self.page_2)
        self.pBanner2.setText("")
        self.pBanner2.setPixmap(QtGui.QPixmap("../../kiosk/home03.jpg"))
        self.pBanner2.setScaledContents(True)
        self.pBanner2.setObjectName("pBanner2")
        self.verticalLayout_8.addWidget(self.pBanner2)
        self.stackedWidget.addWidget(self.page_2)
        self.verticalLayout_3.addWidget(self.stackedWidget)
        self.verticalLayout_2.addWidget(self.widgetStack)
        self.widgetBottom = QtWidgets.QWidget(self.scrollAreaWidgetContents)
        self.widgetBottom.setObjectName("widgetBottom")
        self.verticalLayout_5 = QtWidgets.QVBoxLayout(self.widgetBottom)
        self.verticalLayout_5.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout_5.setSpacing(0)
        self.verticalLayout_5.setObjectName("verticalLayout_5")
        self.labelName = QtWidgets.QLabel(self.widgetBottom)
        self.labelName.setMaximumSize(QtCore.QSize(16777215, 165))
        self.labelName.setText("")
        self.labelName.setPixmap(QtGui.QPixmap("../../kiosk/home_name.jpg"))
        self.labelName.setScaledContents(True)
        self.labelName.setAlignment(QtCore.Qt.AlignCenter)
        self.labelName.setObjectName("labelName")
        self.verticalLayout_5.addWidget(self.labelName)
        self.labelTip = QtWidgets.QLabel(self.widgetBottom)
        self.labelTip.setMaximumSize(QtCore.QSize(16777215, 140))
        self.labelTip.setText("")
        self.labelTip.setPixmap(QtGui.QPixmap("../../kiosk/home_tip_1.jpg"))
        self.labelTip.setScaledContents(True)
        self.labelTip.setAlignment(QtCore.Qt.AlignCenter)
        self.labelTip.setObjectName("labelTip")
        self.verticalLayout_5.addWidget(self.labelTip)
        self.menuTab = QtWidgets.QTabWidget(self.widgetBottom)
        font = QtGui.QFont()
        font.setFamily("맑은 고딕")
        font.setPointSize(16)
        font.setBold(False)
        font.setWeight(50)
        self.menuTab.setFont(font)
        self.menuTab.setAutoFillBackground(False)
        self.menuTab.setStyleSheet("QTabBar::tab {\n"
"    min-width: 174.4px;\n"
"}")
        self.menuTab.setUsesScrollButtons(False)
        self.menuTab.setObjectName("menuTab")
        self.homeMenu = QtWidgets.QWidget()
        self.homeMenu.setObjectName("homeMenu")
        self.verticalLayout_9 = QtWidgets.QVBoxLayout(self.homeMenu)
        self.verticalLayout_9.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout_9.setSpacing(0)
        self.verticalLayout_9.setObjectName("verticalLayout_9")
        self.txtMenu = QtWidgets.QLabel(self.homeMenu)
        self.txtMenu.setMaximumSize(QtCore.QSize(540, 300))
        self.txtMenu.setContextMenuPolicy(QtCore.Qt.DefaultContextMenu)
        self.txtMenu.setLineWidth(1)
        self.txtMenu.setMidLineWidth(0)
        self.txtMenu.setText("")
        self.txtMenu.setPixmap(QtGui.QPixmap("../../kiosk/txt.jpg"))
        self.txtMenu.setScaledContents(True)
        self.txtMenu.setObjectName("txtMenu")
        self.verticalLayout_9.addWidget(self.txtMenu)
        self.banner_1 = QtWidgets.QLabel(self.homeMenu)
        self.banner_1.setMaximumSize(QtCore.QSize(520, 65))
        self.banner_1.setText("")
        self.banner_1.setPixmap(QtGui.QPixmap("../../kiosk/banner_1.jpg"))
        self.banner_1.setScaledContents(True)
        self.banner_1.setObjectName("banner_1")
        self.verticalLayout_9.addWidget(self.banner_1)
        self.menu_01 = QtWidgets.QLabel(self.homeMenu)
        self.menu_01.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_01.setText("")
        self.menu_01.setPixmap(QtGui.QPixmap("../../kiosk/rep_menu1.jpg"))
        self.menu_01.setScaledContents(True)
        self.menu_01.setObjectName("menu_01")
        self.verticalLayout_9.addWidget(self.menu_01)
        self.menu_02 = QtWidgets.QLabel(self.homeMenu)
        self.menu_02.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_02.setText("")
        self.menu_02.setPixmap(QtGui.QPixmap("../../kiosk/rep_menu2.jpg"))
        self.menu_02.setScaledContents(True)
        self.menu_02.setObjectName("menu_02")
        self.verticalLayout_9.addWidget(self.menu_02)
        self.menu_03 = QtWidgets.QLabel(self.homeMenu)
        self.menu_03.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_03.setText("")
        self.menu_03.setPixmap(QtGui.QPixmap("../../kiosk/rep_menu3.jpg"))
        self.menu_03.setScaledContents(True)
        self.menu_03.setObjectName("menu_03")
        self.verticalLayout_9.addWidget(self.menu_03)
        self.menu_04 = QtWidgets.QLabel(self.homeMenu)
        self.menu_04.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_04.setText("")
        self.menu_04.setPixmap(QtGui.QPixmap("../../kiosk/rep_menu4.jpg"))
        self.menu_04.setScaledContents(True)
        self.menu_04.setObjectName("menu_04")
        self.verticalLayout_9.addWidget(self.menu_04)
        self.line = QtWidgets.QFrame(self.homeMenu)
        self.line.setFrameShape(QtWidgets.QFrame.HLine)
        self.line.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line.setObjectName("line")
        self.verticalLayout_9.addWidget(self.line)
        self.banner_2 = QtWidgets.QLabel(self.homeMenu)
        self.banner_2.setMaximumSize(QtCore.QSize(540, 65))
        self.banner_2.setAutoFillBackground(False)
        self.banner_2.setText("")
        self.banner_2.setPixmap(QtGui.QPixmap("../../kiosk/banner_2.jpg"))
        self.banner_2.setScaledContents(True)
        self.banner_2.setObjectName("banner_2")
        self.verticalLayout_9.addWidget(self.banner_2)
        self.line_2 = QtWidgets.QFrame(self.homeMenu)
        self.line_2.setAutoFillBackground(False)
        self.line_2.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_2.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_2.setObjectName("line_2")
        self.verticalLayout_9.addWidget(self.line_2)
        self.menu_05 = QtWidgets.QLabel(self.homeMenu)
        self.menu_05.setMinimumSize(QtCore.QSize(0, 0))
        self.menu_05.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_05.setText("")
        self.menu_05.setPixmap(QtGui.QPixmap("../../kiosk/pretzel1.jpg"))
        self.menu_05.setScaledContents(True)
        self.menu_05.setObjectName("menu_05")
        self.verticalLayout_9.addWidget(self.menu_05)
        self.menu_06 = QtWidgets.QLabel(self.homeMenu)
        self.menu_06.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_06.setText("")
        self.menu_06.setPixmap(QtGui.QPixmap("../../kiosk/pretzel2.jpg"))
        self.menu_06.setScaledContents(True)
        self.menu_06.setObjectName("menu_06")
        self.verticalLayout_9.addWidget(self.menu_06)
        self.menu_07 = QtWidgets.QLabel(self.homeMenu)
        self.menu_07.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_07.setText("")
        self.menu_07.setPixmap(QtGui.QPixmap("../../kiosk/pretzel3.jpg"))
        self.menu_07.setScaledContents(True)
        self.menu_07.setObjectName("menu_07")
        self.verticalLayout_9.addWidget(self.menu_07)
        self.line_3 = QtWidgets.QFrame(self.homeMenu)
        self.line_3.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_3.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_3.setObjectName("line_3")
        self.verticalLayout_9.addWidget(self.line_3)
        self.banner_3 = QtWidgets.QLabel(self.homeMenu)
        self.banner_3.setMaximumSize(QtCore.QSize(540, 65))
        self.banner_3.setText("")
        self.banner_3.setPixmap(QtGui.QPixmap("../../kiosk/banner_3.jpg"))
        self.banner_3.setScaledContents(True)
        self.banner_3.setObjectName("banner_3")
        self.verticalLayout_9.addWidget(self.banner_3)
        self.line_4 = QtWidgets.QFrame(self.homeMenu)
        self.line_4.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_4.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_4.setObjectName("line_4")
        self.verticalLayout_9.addWidget(self.line_4)
        self.menu_08 = QtWidgets.QLabel(self.homeMenu)
        self.menu_08.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_08.setText("")
        self.menu_08.setPixmap(QtGui.QPixmap("../../kiosk/tteok1.jpg"))
        self.menu_08.setScaledContents(True)
        self.menu_08.setObjectName("menu_08")
        self.verticalLayout_9.addWidget(self.menu_08)
        self.menu_09 = QtWidgets.QLabel(self.homeMenu)
        self.menu_09.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_09.setText("")
        self.menu_09.setPixmap(QtGui.QPixmap("../../kiosk/tteok2.jpg"))
        self.menu_09.setScaledContents(True)
        self.menu_09.setObjectName("menu_09")
        self.verticalLayout_9.addWidget(self.menu_09)
        self.line_5 = QtWidgets.QFrame(self.homeMenu)
        self.line_5.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_5.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_5.setObjectName("line_5")
        self.verticalLayout_9.addWidget(self.line_5)
        self.banner_4 = QtWidgets.QLabel(self.homeMenu)
        self.banner_4.setMaximumSize(QtCore.QSize(540, 65))
        self.banner_4.setText("")
        self.banner_4.setPixmap(QtGui.QPixmap("../../kiosk/banner_4.jpg"))
        self.banner_4.setScaledContents(True)
        self.banner_4.setObjectName("banner_4")
        self.verticalLayout_9.addWidget(self.banner_4)
        self.line_6 = QtWidgets.QFrame(self.homeMenu)
        self.line_6.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_6.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_6.setObjectName("line_6")
        self.verticalLayout_9.addWidget(self.line_6)
        self.menu_10 = QtWidgets.QLabel(self.homeMenu)
        self.menu_10.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_10.setText("")
        self.menu_10.setPixmap(QtGui.QPixmap("../../kiosk/set1.jpg"))
        self.menu_10.setScaledContents(True)
        self.menu_10.setObjectName("menu_10")
        self.verticalLayout_9.addWidget(self.menu_10)
        self.menu_11 = QtWidgets.QLabel(self.homeMenu)
        self.menu_11.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_11.setText("")
        self.menu_11.setPixmap(QtGui.QPixmap("../../kiosk/set2.jpg"))
        self.menu_11.setScaledContents(True)
        self.menu_11.setObjectName("menu_11")
        self.verticalLayout_9.addWidget(self.menu_11)
        self.menu_12 = QtWidgets.QLabel(self.homeMenu)
        self.menu_12.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_12.setText("")
        self.menu_12.setPixmap(QtGui.QPixmap("../../kiosk/set3.jpg"))
        self.menu_12.setScaledContents(True)
        self.menu_12.setObjectName("menu_12")
        self.verticalLayout_9.addWidget(self.menu_12)
        self.menu_13 = QtWidgets.QLabel(self.homeMenu)
        self.menu_13.setMaximumSize(QtCore.QSize(540, 160))
        self.menu_13.setText("")
        self.menu_13.setPixmap(QtGui.QPixmap("../../kiosk/set4.jpg"))
        self.menu_13.setScaledContents(True)
        self.menu_13.setObjectName("menu_13")
        self.verticalLayout_9.addWidget(self.menu_13)
        self.line_7 = QtWidgets.QFrame(self.homeMenu)
        self.line_7.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_7.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_7.setObjectName("line_7")
        self.verticalLayout_9.addWidget(self.line_7)
        self.banner_5 = QtWidgets.QLabel(self.homeMenu)
        self.banner_5.setMaximumSize(QtCore.QSize(540, 65))
        self.banner_5.setText("")
        self.banner_5.setPixmap(QtGui.QPixmap("../../kiosk/banner_5.jpg"))
        self.banner_5.setScaledContents(True)
        self.banner_5.setObjectName("banner_5")
        self.verticalLayout_9.addWidget(self.banner_5)
        self.line_8 = QtWidgets.QFrame(self.homeMenu)
        self.line_8.setFrameShape(QtWidgets.QFrame.HLine)
        self.line_8.setFrameShadow(QtWidgets.QFrame.Sunken)
        self.line_8.setObjectName("line_8")
        self.verticalLayout_9.addWidget(self.line_8)
        self.menuTab.addTab(self.homeMenu, "")
        self.tab_2 = QtWidgets.QWidget()
        self.tab_2.setObjectName("tab_2")
        self.menuTab.addTab(self.tab_2, "")
        self.tab_3 = QtWidgets.QWidget()
        self.tab_3.setObjectName("tab_3")
        self.menuTab.addTab(self.tab_3, "")
        self.verticalLayout_5.addWidget(self.menuTab)
        self.verticalLayout_2.addWidget(self.widgetBottom)
        self.widget_4 = QtWidgets.QWidget(self.scrollAreaWidgetContents)
        self.widget_4.setObjectName("widget_4")
        self.verticalLayout_6 = QtWidgets.QVBoxLayout(self.widget_4)
        self.verticalLayout_6.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout_6.setSpacing(0)
        self.verticalLayout_6.setObjectName("verticalLayout_6")
        self.verticalLayout_2.addWidget(self.widget_4)
        self.scrollArea.setWidget(self.scrollAreaWidgetContents)
        self.verticalLayout.addWidget(self.scrollArea)

        self.retranslateUi(FormMain)
        self.stackedWidget.setCurrentIndex(0)
        self.menuTab.setCurrentIndex(0)
        QtCore.QMetaObject.connectSlotsByName(FormMain)

    def retranslateUi(self, FormMain):
        _translate = QtCore.QCoreApplication.translate
        FormMain.setWindowTitle(_translate("FormMain", "Home"))
        self.menuTab.setTabText(self.menuTab.indexOf(self.homeMenu), _translate("FormMain", "메뉴"))
        self.menuTab.setTabText(self.menuTab.indexOf(self.tab_2), _translate("FormMain", "정보"))
        self.menuTab.setTabText(self.menuTab.indexOf(self.tab_3), _translate("FormMain", "리뷰"))
