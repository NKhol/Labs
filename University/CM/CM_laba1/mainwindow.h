#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    bool getParameters(double & left, double & right , double & epsilon);
    bool getParametersForNewton(double &x0,double & epsilon);
    bool stepOfDix(double a,double b, double & epsilon, double & solution,int &cn);
    bool stepOfNewton(double &xn, double & epsilon,int & cn, double  x0);


    ~MainWindow();
public slots:
    void printGraph();
    void checkRanges();
    void divderIntoMethods();
    void dixomomia();
    void newton();


signals:
    //LetsBuildGraph();

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
