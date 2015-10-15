#include "mainwindow.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    //QObject::connect(&w.ui->pushButtonCompute,SIGNAL(clicked()),&w,SLOT(printGraph()));

    return a.exec();
}

