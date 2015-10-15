/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 5.1.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QCheckBox>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QRadioButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTextBrowser>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>
#include "qcustomplot.h"

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralWidget;
    QCustomPlot *plot;
    QPushButton *pushButtonCompute;
    QLabel *label;
    QLineEdit *lineEditBA;
    QLabel *label_2;
    QLabel *label_3;
    QLabel *label_4;
    QLineEdit *lineEditBB;
    QLabel *label_5;
    QLineEdit *lineEditBE;
    QLabel *label_6;
    QCheckBox *checkBoxf;
    QCheckBox *checkBoxf_st;
    QTextBrowser *textBrowser;
    QPushButton *pushButtonClear;
    QRadioButton *radioButtonDix;
    QRadioButton *radioButtonM2;
    QLabel *label_7;
    QLabel *label_8;
    QLineEdit *lineEditBX0;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QStringLiteral("MainWindow"));
        MainWindow->resize(838, 615);
        centralWidget = new QWidget(MainWindow);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        plot = new QCustomPlot(centralWidget);
        plot->setObjectName(QStringLiteral("plot"));
        plot->setGeometry(QRect(10, 10, 601, 401));
        pushButtonCompute = new QPushButton(centralWidget);
        pushButtonCompute->setObjectName(QStringLiteral("pushButtonCompute"));
        pushButtonCompute->setGeometry(QRect(630, 470, 131, 41));
        label = new QLabel(centralWidget);
        label->setObjectName(QStringLiteral("label"));
        label->setGeometry(QRect(630, 10, 171, 16));
        lineEditBA = new QLineEdit(centralWidget);
        lineEditBA->setObjectName(QStringLiteral("lineEditBA"));
        lineEditBA->setGeometry(QRect(630, 90, 201, 23));
        lineEditBA->setMaxLength(22);
        label_2 = new QLabel(centralWidget);
        label_2->setObjectName(QStringLiteral("label_2"));
        label_2->setGeometry(QRect(630, 70, 101, 16));
        label_3 = new QLabel(centralWidget);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setGeometry(QRect(630, 50, 71, 16));
        QFont font;
        font.setPointSize(10);
        font.setBold(true);
        font.setWeight(75);
        label_3->setFont(font);
        label_4 = new QLabel(centralWidget);
        label_4->setObjectName(QStringLiteral("label_4"));
        label_4->setGeometry(QRect(630, 120, 131, 16));
        lineEditBB = new QLineEdit(centralWidget);
        lineEditBB->setObjectName(QStringLiteral("lineEditBB"));
        lineEditBB->setGeometry(QRect(630, 140, 201, 23));
        lineEditBB->setMaxLength(22);
        label_5 = new QLabel(centralWidget);
        label_5->setObjectName(QStringLiteral("label_5"));
        label_5->setGeometry(QRect(630, 180, 111, 16));
        label_5->setFont(font);
        lineEditBE = new QLineEdit(centralWidget);
        lineEditBE->setObjectName(QStringLiteral("lineEditBE"));
        lineEditBE->setGeometry(QRect(630, 200, 201, 23));
        lineEditBE->setMaxLength(22);
        label_6 = new QLabel(centralWidget);
        label_6->setObjectName(QStringLiteral("label_6"));
        label_6->setGeometry(QRect(630, 300, 111, 16));
        label_6->setFont(font);
        checkBoxf = new QCheckBox(centralWidget);
        checkBoxf->setObjectName(QStringLiteral("checkBoxf"));
        checkBoxf->setGeometry(QRect(630, 320, 141, 21));
        checkBoxf_st = new QCheckBox(centralWidget);
        checkBoxf_st->setObjectName(QStringLiteral("checkBoxf_st"));
        checkBoxf_st->setGeometry(QRect(630, 350, 141, 21));
        textBrowser = new QTextBrowser(centralWidget);
        textBrowser->setObjectName(QStringLiteral("textBrowser"));
        textBrowser->setGeometry(QRect(10, 420, 601, 131));
        textBrowser->setAutoFillBackground(false);
        pushButtonClear = new QPushButton(centralWidget);
        pushButtonClear->setObjectName(QStringLiteral("pushButtonClear"));
        pushButtonClear->setGeometry(QRect(630, 520, 80, 23));
        radioButtonDix = new QRadioButton(centralWidget);
        radioButtonDix->setObjectName(QStringLiteral("radioButtonDix"));
        radioButtonDix->setGeometry(QRect(630, 400, 100, 21));
        radioButtonM2 = new QRadioButton(centralWidget);
        radioButtonM2->setObjectName(QStringLiteral("radioButtonM2"));
        radioButtonM2->setGeometry(QRect(630, 430, 181, 21));
        label_7 = new QLabel(centralWidget);
        label_7->setObjectName(QStringLiteral("label_7"));
        label_7->setGeometry(QRect(630, 380, 111, 16));
        label_7->setFont(font);
        label_8 = new QLabel(centralWidget);
        label_8->setObjectName(QStringLiteral("label_8"));
        label_8->setGeometry(QRect(630, 240, 181, 16));
        label_8->setFont(font);
        lineEditBX0 = new QLineEdit(centralWidget);
        lineEditBX0->setObjectName(QStringLiteral("lineEditBX0"));
        lineEditBX0->setGeometry(QRect(630, 260, 201, 23));
        lineEditBX0->setMaxLength(22);
        MainWindow->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(MainWindow);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 838, 20));
        MainWindow->setMenuBar(menuBar);
        mainToolBar = new QToolBar(MainWindow);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        MainWindow->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(MainWindow);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        MainWindow->setStatusBar(statusBar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QApplication::translate("MainWindow", "MainWindow", 0));
        pushButtonCompute->setText(QApplication::translate("MainWindow", "Compute", 0));
        label->setText(QApplication::translate("MainWindow", "f(x) = 3*x - cos(x) - 1 = 0", 0));
        label_2->setText(QApplication::translate("MainWindow", "Left bound (a):", 0));
        label_3->setText(QApplication::translate("MainWindow", "Bounds:", 0));
        label_4->setText(QApplication::translate("MainWindow", "Right bound (b):", 0));
        label_5->setText(QApplication::translate("MainWindow", "epsilon:", 0));
        label_6->setText(QApplication::translate("MainWindow", "Graphs:", 0));
        checkBoxf->setText(QApplication::translate("MainWindow", "f (x)", 0));
        checkBoxf_st->setText(QApplication::translate("MainWindow", "f ' (x)", 0));
        pushButtonClear->setText(QApplication::translate("MainWindow", "Clear", 0));
        radioButtonDix->setText(QApplication::translate("MainWindow", "Dixomomy", 0));
        radioButtonM2->setText(QApplication::translate("MainWindow", "Newton's Method", 0));
        label_7->setText(QApplication::translate("MainWindow", "Method:", 0));
        label_8->setText(QApplication::translate("MainWindow", "X0 (Newton's method):", 0));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
