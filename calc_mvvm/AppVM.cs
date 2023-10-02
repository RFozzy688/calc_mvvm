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
        double _num1;
        double _num2;
        double _result;
        Commands _getContent;
        Commands _getAction;
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

            _chooseAction = false;
            _getContent = new Commands(SetValue);
            _getAction = new Commands(SetAction);
        }
        public Commands GetContent { get { return _getContent; } }
        public Commands GetAction { get { return _getAction; } }
        private void SetValue(object param)
        {
            if (!_chooseAction)
            {
                _num1 = Double.Parse((string)param);

                _str += _num1.ToString();

                _view.Text1.Text = _str;
                _num1 = Double.Parse(_str);
            }
            else
            {
                _num2 = Double.Parse((string)param);

                _str += _num2.ToString();
                _num2 = Double.Parse(_str);
            }

            _view.Text2.Text = _str;
        }
        private void SetAction(object param)
        {
            _view.Text1.Text = _num1.ToString() + " " + (string)param;

            _action = Char.Parse((string)param);
            _chooseAction = true;

            _str = null;
        }
    }
}
