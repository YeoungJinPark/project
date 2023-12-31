# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'kiosk_cart.ui'
#
# Created by: PyQt5 UI code generator 5.15.9
#
# WARNING: Any manual changes made to this file will be lost when pyuic5 is
# run again.  Do not edit this file unless you know what you are doing.


from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_CartForm(object):
    def setupUi(self, CartForm):
        CartForm.setObjectName("CartForm")
        CartForm.resize(560, 900)
        self.verticalLayout = QtWidgets.QVBoxLayout(CartForm)
        self.verticalLayout.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout.setSpacing(0)
        self.verticalLayout.setObjectName("verticalLayout")
        self.widget = QtWidgets.QWidget(CartForm)
        self.widget.setMinimumSize(QtCore.QSize(560, 900))
        self.widget.setObjectName("widget")
        self.verticalLayout_4 = QtWidgets.QVBoxLayout(self.widget)
        self.verticalLayout_4.setContentsMargins(0, 0, 0, -1)
        self.verticalLayout_4.setSpacing(0)
        self.verticalLayout_4.setObjectName("verticalLayout_4")
        self.widget_top = QtWidgets.QWidget(self.widget)
        self.widget_top.setMaximumSize(QtCore.QSize(558, 70))
        self.widget_top.setObjectName("widget_top")
        self.horizontalLayout_2 = QtWidgets.QHBoxLayout(self.widget_top)
        self.horizontalLayout_2.setContentsMargins(0, -1, 0, -1)
        self.horizontalLayout_2.setObjectName("horizontalLayout_2")
        self.button_back = QtWidgets.QPushButton(self.widget_top)
        self.button_back.setMinimumSize(QtCore.QSize(70, 50))
        self.button_back.setMaximumSize(QtCore.QSize(70, 50))
        self.button_back.setText("")
        icon = QtGui.QIcon()
        icon.addPixmap(QtGui.QPixmap("../../kiosk/back.png"), QtGui.QIcon.Normal, QtGui.QIcon.Off)
        self.button_back.setIcon(icon)
        self.button_back.setIconSize(QtCore.QSize(69, 49))
        self.button_back.setFlat(True)
        self.button_back.setObjectName("button_back")
        self.horizontalLayout_2.addWidget(self.button_back)
        spacerItem = QtWidgets.QSpacerItem(500, 10, QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Minimum)
        self.horizontalLayout_2.addItem(spacerItem)
        self.verticalLayout_4.addWidget(self.widget_top)
        self.widget_2 = QtWidgets.QWidget(self.widget)
        self.widget_2.setObjectName("widget_2")
        self.verticalLayout_3 = QtWidgets.QVBoxLayout(self.widget_2)
        self.verticalLayout_3.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout_3.setSpacing(0)
        self.verticalLayout_3.setObjectName("verticalLayout_3")
        self.widget_3 = QtWidgets.QWidget(self.widget_2)
        self.widget_3.setMaximumSize(QtCore.QSize(558, 100))
        self.widget_3.setObjectName("widget_3")
        self.verticalLayout_2 = QtWidgets.QVBoxLayout(self.widget_3)
        self.verticalLayout_2.setContentsMargins(5, 0, 5, 0)
        self.verticalLayout_2.setSpacing(0)
        self.verticalLayout_2.setObjectName("verticalLayout_2")
        self.label = QtWidgets.QLabel(self.widget_3)
        self.label.setMaximumSize(QtCore.QSize(558, 100))
        self.label.setText("")
        self.label.setPixmap(QtGui.QPixmap("menu_img/cart_0.jpg"))
        self.label.setScaledContents(True)
        self.label.setObjectName("label")
        self.verticalLayout_2.addWidget(self.label)
        self.verticalLayout_3.addWidget(self.widget_3)
        self.widget_list = QtWidgets.QWidget(self.widget_2)
        self.widget_list.setMinimumSize(QtCore.QSize(0, 0))
        self.widget_list.setObjectName("widget_list")
        self.verticalLayout_5 = QtWidgets.QVBoxLayout(self.widget_list)
        self.verticalLayout_5.setObjectName("verticalLayout_5")
        self.verticalLayout_3.addWidget(self.widget_list)
        self.verticalLayout_4.addWidget(self.widget_2)
        self.widget_bottom = QtWidgets.QWidget(self.widget)
        self.widget_bottom.setMaximumSize(QtCore.QSize(16777215, 100))
        self.widget_bottom.setObjectName("widget_bottom")
        self.horizontalLayout = QtWidgets.QHBoxLayout(self.widget_bottom)
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.label_min = QtWidgets.QLabel(self.widget_bottom)
        self.label_min.setMaximumSize(QtCore.QSize(145, 70))
        self.label_min.setText("")
        self.label_min.setPixmap(QtGui.QPixmap("../../kiosk/min.jpg"))
        self.label_min.setScaledContents(True)
        self.label_min.setObjectName("label_min")
        self.horizontalLayout.addWidget(self.label_min)
        self.label_total = QtWidgets.QLabel(self.widget_bottom)
        self.label_total.setMaximumSize(QtCore.QSize(330, 16777215))
        self.label_total.setPixmap(QtGui.QPixmap("../../kiosk/total.jpg"))
        self.label_total.setScaledContents(True)
        self.label_total.setAlignment(QtCore.Qt.AlignCenter)
        self.label_total.setObjectName("label_total")
        self.horizontalLayout.addWidget(self.label_total)
        self.verticalLayout_4.addWidget(self.widget_bottom)
        self.verticalLayout.addWidget(self.widget)

        self.retranslateUi(CartForm)
        QtCore.QMetaObject.connectSlotsByName(CartForm)

    def retranslateUi(self, CartForm):
        _translate = QtCore.QCoreApplication.translate
        CartForm.setWindowTitle(_translate("CartForm", "Cart"))
