# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'user_list_item.ui'
#
# Created by: PyQt5 UI code generator 5.15.9
#
# WARNING: Any manual changes made to this file will be lost when pyuic5 is
# run again.  Do not edit this file unless you know what you are doing.


from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_Form(object):
    def setupUi(self, Form):
        Form.setObjectName("Form")
        Form.resize(362, 50)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Preferred, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(Form.sizePolicy().hasHeightForWidth())
        Form.setSizePolicy(sizePolicy)
        Form.setMinimumSize(QtCore.QSize(362, 50))
        Form.setMaximumSize(QtCore.QSize(362, 50))
        self.widget = QtWidgets.QWidget(Form)
        self.widget.setGeometry(QtCore.QRect(0, 0, 362, 50))
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Fixed, QtWidgets.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.widget.sizePolicy().hasHeightForWidth())
        self.widget.setSizePolicy(sizePolicy)
        self.widget.setMinimumSize(QtCore.QSize(362, 50))
        self.widget.setMaximumSize(QtCore.QSize(362, 50))
        self.widget.setStyleSheet("background-color: rgb(255, 255, 255);")
        self.widget.setObjectName("widget")
        self.label = QtWidgets.QLabel(self.widget)
        self.label.setGeometry(QtCore.QRect(0, 0, 56, 50))
        self.label.setText("")
        self.label.setPixmap(QtGui.QPixmap("../ui_img/koprofil.png"))
        self.label.setScaledContents(True)
        self.label.setObjectName("label")
        self.name_lbl = QtWidgets.QLabel(self.widget)
        self.name_lbl.setGeometry(QtCore.QRect(60, 13, 130, 25))
        font = QtGui.QFont()
        font.setFamily("G마켓 산스 TTF Medium")
        font.setPointSize(11)
        font.setBold(True)
        font.setWeight(75)
        self.name_lbl.setFont(font)
        self.name_lbl.setObjectName("name_lbl")
        self.checkBox = QtWidgets.QCheckBox(self.widget)
        self.checkBox.setGeometry(QtCore.QRect(330, 15, 21, 21))
        self.checkBox.setStyleSheet("color: rgb(255, 255, 0);")
        self.checkBox.setText("")
        self.checkBox.setIconSize(QtCore.QSize(20, 20))
        self.checkBox.setObjectName("checkBox")

        self.retranslateUi(Form)
        QtCore.QMetaObject.connectSlotsByName(Form)

    def retranslateUi(self, Form):
        _translate = QtCore.QCoreApplication.translate
        Form.setWindowTitle(_translate("Form", "Form"))
        self.name_lbl.setText(_translate("Form", "이승신"))
