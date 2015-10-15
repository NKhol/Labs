#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "cmath"
#include <QMessageBox>



/**
            My function is f(x) = 3x - cos(x) - 1 ;
            it's understood that there is only one solve because f'(x) = 3 + cos(x)
  */

double f(double x)
{
    return 3.0*x - cos(x) - 1;
}

double f_st(double x)
{
    return 3.0 + sin(x);
}

double f_2st(double x)
{
    return cos(x);
}

double xn_next(double x)
{
    return x - f(x)/f_st(x);
}

double get_m1(double x,double eps)
{
    double min = fabs(f_st(x)),incr = (x-eps)/500.0,xi = x-eps;
    for(int i = 0; i < 1000 ; i++){
        if(fabs(f_st(xi)) < min) min = fabs(f_st(xi));
        xi+=incr;
    }
    return min;
}

double get_M2(double x,double eps)
{
    double max = fabs(f_2st(x)),incr = (x-eps)/500.0,xi = x-eps;
    for(int i = 0; i < 1000 ; i++){
        if(fabs(f_2st(xi)) > max) max = fabs(f_2st(xi));
        xi+=incr;
    }
    return max;
}

double get_q(double x, double eps, double x0, double xn)
{
    return get_M2(x,eps)*fabs(x0-xn)/(2.0*get_m1(x,eps));       // xn should be x*
}

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QObject::connect(this->ui->pushButtonCompute,SIGNAL(clicked()),this,SLOT(divderIntoMethods()));
    connect(this->ui->pushButtonClear,SIGNAL(clicked()),this->ui->textBrowser,SLOT(clear()));

    this->ui->plot->xAxis->setLabel("x");
    this->ui->plot->yAxis->setLabel("y");

    this->ui->plot->xAxis->setRange(-100,100);
    this->ui->plot->yAxis->setRange(-100,100);

    // make left and bottom axes always transfer their ranges to right and top axes:
    connect(this->ui->plot->xAxis, SIGNAL(rangeChanged(QCPRange)), this->ui->plot->xAxis2, SLOT(setRange(QCPRange)));
    connect(this->ui->plot->yAxis, SIGNAL(rangeChanged(QCPRange)), this->ui->plot->yAxis2, SLOT(setRange(QCPRange)));
    // it is in charge of going out of bounds
    connect(this->ui->plot,SIGNAL(beforeReplot()),this,SLOT(checkRanges()));
    // when draw praphs?
    connect(this->ui->checkBoxf,SIGNAL(clicked()),this,SLOT(printGraph()));
    connect(this->ui->checkBoxf_st,SIGNAL(clicked()),this,SLOT(printGraph()));

    this->ui->plot->setInteractions(QCP::iRangeDrag | QCP::iRangeZoom | QCP::iSelectPlottables);

    // work on input
    // SET validator
    QDoubleValidator *valD = new QDoubleValidator(-2000.0,2000.0,8,this);
    ui->lineEditBA->setValidator(valD);
    QDoubleValidator *valO = new QDoubleValidator(-2000.0,2000.0,8,this);
    ui->lineEditBB->setValidator(valO);
    QDoubleValidator *valE = new QDoubleValidator(0.000001,1,8,this);
    ui->lineEditBE->setValidator(valE);
    QDoubleValidator *valX = new QDoubleValidator(-2000.0,2000.0,8,this);
    ui->lineEditBX0->setValidator(valX);
    connect(this->ui->checkBoxf,SIGNAL(clicked()),this,SLOT(printGraph()));

    /**
        Now we will draw all graphs.

      */
    int sizeOfVector = 4000;
    //   f(x)
    QVector<double> x(sizeOfVector),y(sizeOfVector);

    for(int i = 0; i < sizeOfVector ; i++)
    {
        x[i] = i - 2000;
        y[i] = f((double)i-2000.0);
    }

    this->ui->plot->addGraph();
    this->ui->plot->graph(0)->addData(x,y);
    this->ui->plot->graph(0)->setPen(QPen(Qt::red));
    this->ui->plot->graph(0)->setVisible(false);


    // f'(x)
    QVector<double> x1(sizeOfVector),y1(sizeOfVector);

    for(int i = 0; i < sizeOfVector ; i++)
    {
        x1[i] = i - 2000;
        y1[i] = f_st((double)i-2000.0);
    }

    this->ui->plot->addGraph();
    this->ui->plot->graph(1)->addData(x1,y1);
    this->ui->plot->graph(1)->setPen(QPen(Qt::green));
    this->ui->plot->graph(1)->setVisible(false);


}

MainWindow::~MainWindow()
{
    delete ui;
}




void MainWindow::printGraph()
{
    if(this->ui->checkBoxf->isChecked())
    this->ui->plot->graph(0)->setVisible(true);else this->ui->plot->graph(0)->setVisible(false);

    if(this->ui->checkBoxf_st->isChecked())
    this->ui->plot->graph(1)->setVisible(true);else  this->ui->plot->graph(1)->setVisible(false);

    this->ui->plot->replot();

    //QMessageBox::information(this,"OK","i have it");
}

void MainWindow::checkRanges()
{
    if(this->ui->plot->xAxis->range().lower < -2000)this->ui->plot->xAxis->setRangeLower(-2000);
    if(this->ui->plot->xAxis->range().upper > 2000) this->ui->plot->xAxis->setRangeUpper(2000);

    if(this->ui->plot->yAxis->range().lower < -2000)this->ui->plot->yAxis->setRangeLower(-2000);
    if(this->ui->plot->yAxis->range().upper > 2000) this->ui->plot->yAxis->setRangeUpper(2000);
}


bool MainWindow::getParameters(double & left, double & right , double & epsilon)
{
    double a,b,eps;
    try{
            QString s = ui->lineEditBE->text();
            for(int i = 0 ; i < s.length() ; i++)
                if(s[i]==',')s[i]='.';
            eps = s.toDouble();

            s = ui->lineEditBA->text();
            for(int i = 0 ; i < s.length() ; i++)
                if(s[i]==',')s[i]='.';
            a =  s.toDouble();

            s  = ui->lineEditBB->text();
            for(int i = 0 ; i < s.length() ; i++)
                if(s[i]==',')s[i]='.';
            b = s.toDouble();

            if(b == a)
            {
                QMessageBox::information(this,"Problem","Please, type parameters carefully (a=b). they should not be the same");
            }else{
                if(a>b)
                {
                   QMessageBox::information(this,"Problem","a > b. We have swaped them.");
                   left = b;
                   right = a;
                }else
                {
                    left = a;
                    right = b;
                }
                if(fabs(eps - (int)eps)> 0)
                {
                    epsilon = fabs(eps - (int)eps);
                    /*
                    ui->textBrowser->append("Left bound: " + QString::number(left));
                    ui->textBrowser->append("Right bound: " + QString::number(right));
                    ui->textBrowser->append("Epsilon: " + QString::number(epsilon));
                    */
                    return true;
                }
                else
                {
                    QMessageBox::information(this,"Problem","epsilon є (0;1).Please, type parameters carefully.");
                }
            }
    }
    catch(...)
    {
        QMessageBox::information(this,"Problem","Please, type parameters carefully");
    }

    return false;
}

bool MainWindow::stepOfDix(double a,double b, double & epsilon, double & solution,int &cn)
{
    ui->textBrowser->append("# "+QString::number(cn)+" iteration:");
    cn++;
    /*
    ui->textBrowser->append("Left bound: " + QString::number(a));
    ui->textBrowser->append("Right bound: " + QString::number(b));
    ui->textBrowser->append("Epsilon: " + QString::number(epsilon));*/
    ui->textBrowser->append("Left bound: " + QString::number(a)+"  Right bound: " + QString::number(b));
    if(f(a)*f(b)==0)
    {
        ui->textBrowser->append("f(a)*f(b) = 0");
        f(a)==0 ? solution = a : solution = b;
        return true;
    }
    if(f(a)*f(b) > 0)
    {
        ui->textBrowser->append("f(a)*f(b) > 0");
        return false;
    }

    double m = (a+b)/2.0;
    ui->textBrowser->append("xn = "+QString::number(m)+" ; f(xn) = "+QString::number(f(m)));
    if(b - m <= epsilon)
    {
        solution = m;
        return true;
    }
    if(f(a)*f(m) <= 0) return stepOfDix(a,m,epsilon,solution,cn);

    return stepOfDix(m,b,epsilon,solution,cn);
}

void MainWindow::dixomomia()
{
    double a,b,eps;
    if( getParameters(a,b,eps)){


    double solution;
    int cn = 1;

    bool isFound = stepOfDix(a,b,eps,solution,cn);
    if(isFound)
        ui->textBrowser->append("Solution: "+QString::number(solution)+" ± "+QString::number(eps));
    else
        ui->textBrowser->append("There is no solution on ["+QString::number(a)+" ; "+QString::number(b)+"]");
    }else
    {
        ui->textBrowser->append("There is problem. Try again.");
    }

}


void MainWindow::divderIntoMethods()
{
    ui->textBrowser->clear();
    if(!(ui->radioButtonDix->isChecked()||ui->radioButtonM2->isChecked()))
    {
        QMessageBox::information(this,"Problem","you have to choose method!");
    }
    else{
            //ui->textBrowser->append("All is OK");
        if(ui->radioButtonDix->isChecked())
        {
            ui->textBrowser->append("The dixotomy method is chosen");
            dixomomia();
        }else
        {
            ui->textBrowser->append("The Newton's method is chosen");
            newton();
        }
    }
}



bool MainWindow::stepOfNewton(double& xn, double & epsilon,int & cn, double  x0)
{
    ui->textBrowser->append("# "+QString::number(cn)+" iteration:");
    ui->textBrowser->append("xn = "+QString::number(xn)+" f(x) = "+QString::number(f(xn)));
    double q = get_q(xn,epsilon,x0,xn);

    if(q<1){
        if(/*pow(q,pow(2.0,cn)-1.0)*fabs(x0 - xn)*/ fabs(x0-xn)< epsilon) // xn should be x*
            return true;
        cn++;x0 = xn; xn = xn - f(xn)/f_st(xn);
        return stepOfNewton(xn,epsilon,cn,x0);
    }else{
        cn++;x0 = xn; xn = xn - f(xn)/f_st(xn);
        return stepOfNewton(xn,epsilon,cn,x0);
    }


    return false;
}

bool MainWindow::getParametersForNewton(double &x0,double & epsilon)
{
    QString x = ui->lineEditBX0->text();
    for(int i= 0; i<x.length();i++)
        if(x[i]==',')x[i]='.';
    QString ep = ui->lineEditBE->text();
    for(int i= 0; i<ep.length();i++)
        if(ep[i]==',')ep[i]='.';
    double eps = ep.toDouble();
    x0 = x.toDouble();
    if(fabs(eps - (int)eps)> 0)
    {
        epsilon = fabs(eps - (int)eps);
        return true;
    }else{
        ui->textBrowser->append("Problem. Epsilon є (0;1).");
    }
    return false;
}

void MainWindow::newton()
{
    // double a,b,eps;
        double xn,eps;          // modern version
     if(/*getParameters(a,b,eps)*/getParametersForNewton(xn,eps))
     {
         int cn = 0;
         //double xn = (a+b)/2.0;
         double b = 2000.0;
         bool isDone = stepOfNewton(xn,eps,cn,b);
         if(isDone)
             ui->textBrowser->append("Solution: "+QString::number(xn)+" ± "+QString::number(eps));
     }
     else
     {
         ui->textBrowser->append("There is problem. Try again.");
     }
}

