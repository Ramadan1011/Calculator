using Calculator.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _keyPressedString;

        public string KeyPressedString
        {
            get
            {
                return _keyPressedString;
            }
            set
            {
                _keyPressedString = value;
                OnPropertyChanged("KeyPressedString");
            }
        }

        private string _enteredNumber;

        public string EnteredNumber
        {
            get
            {
                return _enteredNumber;
            }
            set
            {
                _enteredNumber = value;
                OnPropertyChanged("EnteredNumber");
            }
        }


        private ButtonPressedCommand? _buttonPressedCommand;

        public ButtonPressedCommand buttonPressedCommand
        {
            get
            {
                return _buttonPressedCommand;
            }
            set
            {
                _buttonPressedCommand = value;
            }
        }

        List<string> EnteredKeys;
        double Number = 0;
        bool FirstNumberEntered = true;
        bool EqualToFlag = true;
        bool FunctionPressed = false;
        string SelectedFunctions = "";
        public string PreviousEnteredKey = "";

        public MainViewModel()
        {
            EnteredNumber = "0";
            KeyPressedString = "";
            EnteredKeys = new List<string>();
            buttonPressedCommand = new ButtonPressedCommand(this);
        }

        void UpdateEnteredKeysOnGui()
        {
            string temp = "";

            for (int i = 0; i < EnteredKeys.Count; i++)
                temp += EnteredKeys[i];

            KeyPressedString = temp;
        }

        void Addition()
        {
            if (!double.TryParse(EnteredNumber, out double d))
            {
                EnteredNumber = "it is impossible to divide by zero";
                return;
            }
            double number = Convert.ToDouble(EnteredNumber);
            if (number == 0)
            {
                EnteredNumber = "it is impossible to divide by zero";
            }
            else
            {
                Number /= number;
                EnteredNumber = Number.ToString();
            }
        }
        
        void Subtraction()
        {
            if (!double.TryParse(EnteredNumber, out double d))
            {
                EnteredNumber = "it is impossible to divide by zero";
                return;
            }
            double number = Convert.ToDouble(EnteredNumber);
            if (number == 0)
            {
                EnteredNumber = "it is impossible to divide by zero";
            }
            else
            {
                Number /= number;
                EnteredNumber = Number.ToString();
            }
        }

        void Multiplication()
        {
            if (!double.TryParse(EnteredNumber, out double d))
            {
                EnteredNumber = "it is impossible to divide by zero";
                return;
            }
            double number = Convert.ToDouble(EnteredNumber);
            if (number == 0)
            {
                EnteredNumber = "it is impossible to divide by zero";
            }
            else
            {
                Number /= number; 
                EnteredNumber = Number.ToString();
            }
        }

        void Division()
        {
            if (!double.TryParse(EnteredNumber, out double d))
            {
                EnteredNumber = "it is impossible to divide by zero";
                return;
            }
            double number = Convert.ToDouble(EnteredNumber);
            if (number == 0)
            {
                EnteredNumber = "it is impossible to divide by zero";
            }
            else
            {
                Number /= number;
                EnteredNumber = Number.ToString();
            }
        }

        void EqualTo()
        {
            EnteredKeys.Clear();
            EnteredKeys.Add(EnteredNumber);
            EqualToFlag = false;
        }

        void Clear()
        {
            EnteredKeys.Clear();
            KeyPressedString = "";
            EnteredNumber = "0";
            Number = 0;
            FirstNumberEntered = true;
            EqualToFlag = false;
        }

        bool PressedButtonOperator(string pressedButton)
        {
            if (pressedButton == "0" || pressedButton == "1" || pressedButton == "2" || pressedButton == "3" || pressedButton == "4" || pressedButton == "5" ||
                pressedButton == "6" || pressedButton == "7" || pressedButton == "8" || pressedButton == "9" || pressedButton == ".")
            {
                if (EqualToFlag)
                    Clear();

                EnteredKeys.Add(pressedButton);
                UpdateEnteredKeysOnGui();

                PreviousEnteredKey = pressedButton;

                if (FunctionPressed)
                {
                    if (EnteredNumber == "it is impossible to divide by zero")
                    {
                        EnteredNumber = "it is impossible to divide by zero";
                        FunctionPressed = false;
                        return false;
                    }
                    EnteredNumber = "0";
                    FunctionPressed = false;
                }

                if (EnteredNumber == "0")
                    EnteredNumber = pressedButton;
                else
                    EnteredNumber += pressedButton;

                return false;
            }
            else
                return true;
        }

        public void GetPressedButton(string pressedButton)
        {
            if (PressedButtonOperator(pressedButton))
            {
                if (PreviousEnteredKey != pressedButton && PreviousEnteredKey != "+" && PreviousEnteredKey != "-" && PreviousEnteredKey != "/" && PreviousEnteredKey != "*")
                {
                    if (EnteredKeys.Count == 0)
                    {
                        EnteredKeys.Add(EnteredNumber);
                        UpdateEnteredKeysOnGui();
                    }

                    if (FirstNumberEntered)
                    {
                        Number = Convert.ToDouble(EnteredNumber);
                        EnteredNumber = Number.ToString();
                        FirstNumberEntered = false;
                    }
                    else
                    {
                        switch (SelectedFunctions)
                        {
                            case "Addition": Addition(); break;
                            case "Subtraction": Subtraction(); break;
                            case "Multiplication": Multiplication(); break;
                            case "Division": Division(); break;
                            case "EqualTo": EqualTo(); break;
                        }
                    }

                    switch (pressedButton)
                    {
                        case "+":
                            SelectedFunctions = "Addition";
                            EnteredKeys.Add(pressedButton);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedButton;
                            FunctionPressed = true;
                            break;
                        case "-":
                            SelectedFunctions = "Subtraction";
                            EnteredKeys.Add(pressedButton);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedButton;
                            FunctionPressed = true;
                            break;
                        case "*":
                            SelectedFunctions = "Multiplication";
                            EnteredKeys.Add(pressedButton);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedButton;
                            FunctionPressed = true;
                            break;
                        case "/":
                            SelectedFunctions = "Division";
                            EnteredKeys.Add(pressedButton);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedButton;
                            FunctionPressed = true;
                            break;
                        case "=":
                            SelectedFunctions = "EqualTo";
                            EnteredKeys.Add(pressedButton);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedButton;
                            FunctionPressed = true;
                            break;
                        case "Clr":
                            Clear();
                            FunctionPressed = true;
                            PreviousEnteredKey = pressedButton;
                            break;                          
                    }
                }
                else if (PreviousEnteredKey == "+" || PreviousEnteredKey == "*" || PreviousEnteredKey == "-" || PreviousEnteredKey == "/" || PreviousEnteredKey == "Clr")
                {
                    EnteredKeys.RemoveAt(EnteredKeys.Count - 1);
                    EnteredKeys.Add(pressedButton);
                    UpdateEnteredKeysOnGui();

                    PreviousEnteredKey = pressedButton;
                    FunctionPressed = true;

                    switch(pressedButton)
                    {
                         case "+": SelectedFunctions = "Addition"; break;
                         case "-": SelectedFunctions = "Subtraction"; break;
                         case "*": SelectedFunctions = "Multiplication"; break;
                         case "/": SelectedFunctions = "Division"; break;
                         case "=": SelectedFunctions = "EqualTo"; break;
                        case "Clr": Clear(); break;
                    }
                }
            }
        }
    }
}
