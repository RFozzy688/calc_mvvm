using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc_mvvm
{
    // VM
    public delegate double CalcDelegate(double x, double y);
    internal class AppVM
    {
        Calc _calc;
        Dictionary<char, CalcDelegate> _actions;
        MainWindow _view;
        bool _chooseAction;
        bool _isNum1;
        bool _isNum2;
        double _num1;
        double _num2;
        double _result;
        Commands _getContent;
        Commands _getAction;
        Commands _getResult;
        Commands _getActionOneOperand;
        string _str;
        char _action;
        public AppVM(MainWindow view)
        {
            _view = view;
            _calc = new Calc();
            _actions = new Dictionary<char, CalcDelegate>();

            _actions['+'] = _calc.Add;
            _actions['-'] = _calc.Sub;
            _actions['*'] = _calc.Mult;
            _actions['/'] = _calc.Div;
            _actions['p'] = _calc.Pow;
            _actions['s'] = _calc.Sqrt;
            _actions['i'] = _calc.InverseProportionality;

            _isNum1 = false;
            _isNum2 = false;
            _chooseAction = false;
            _getContent = new Commands(SetValue);
            _getAction = new Commands(ChooseAction);
            _getResult = new Commands(Result);
            _getActionOneOperand = new Commands(ActionOneOperand);
        }
        public Commands GetContent { get { return _getContent; } }
        public Commands GetAction { get { return _getAction; } }
        public Commands GetResult { get { return _getResult; } }
        public Commands GetActionOneOperand { get {  return _getActionOneOperand; } }
        private void SetValue(object param)
        {
            if (!_isNum1)
            {
                _num1 = Double.Parse((string)param);

                _str += _num1.ToString();

                //_view.Text1.Text = _str;
                _num1 = Double.Parse(_str);
            }
            else
            {
                _num2 = Double.Parse((string)param);
                _isNum2 = true;

                _str += _num2.ToString();
                _num2 = Double.Parse(_str);
            }

            _view.Text2.Text = _str;
        }
        private void ChooseAction(object param)
        {
            if (_isNum2)
            {
                _result = Calculation();

                _action = Char.Parse((string)param);

                ShowResult(_action);

                _num1 = _result;
                _isNum2 = false;

                _str = null;
            }
            else
            {
                _action = Char.Parse((string)param);
                _view.Text1.Text = _num1.ToString() + " " + _action;

                _isNum1 = true;

                _str = null;
            }
        }
        private double Calculation()
        {
            return _actions[_action](_num1, _num2);
        }
        private void ShowResult(char ch)
        {
            if (ch == '=')
            {
                _view.Text1.Text = String.Format("{0} {1} {2} =", _num1, _action, _num2);
                _view.Text2.Text = _result.ToString();
            }
            else if (ch == 'p')
            {
                _view.Text1.Text += String.Format(" pow({0})", _num1);
                _view.Text2.Text = _result.ToString();
            }
            else if (ch == 's')
            {
                _view.Text1.Text += String.Format(" sqrt({0})", _num1);
                _view.Text2.Text = _result.ToString();
            }
            else if (ch == 'i')
            {
                _view.Text1.Text += String.Format(" 1 / {0}", _num1);
                _view.Text2.Text = _result.ToString();
            }
            else 
            {
                _view.Text1.Text = String.Format("{0} {1} ", _result, _action);
                _view.Text2.Text = _result.ToString();
            }
        }
        private void Result(object param)
        {
            double x = _num2;
            _result = Calculation();

            ShowResult(Char.Parse((string)param));

            _num1 = _result;
            _isNum2 = false;

            _str = null;
        }
        private void ActionOneOperand(object param)
        {
            double value = Double.Parse(_view.Text2.Text);
            double tempNum;

            char previousAction = _action;
            _action = Char.Parse((string)param);

            if (_isNum2)
            {
                tempNum = _num1;
                _num1 = value;

                _result = Calculation();
                ShowResult(_action);

                _action = previousAction;

                _num2 = _result;
                _num1 = tempNum;
            }
            else
            {
                _view.Text1.Text = "";
                _result = Calculation();
                ShowResult(_action);
                _num1 = _result;
            }
        }
    }
}
