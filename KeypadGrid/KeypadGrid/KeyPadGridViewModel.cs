using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Globalization;

namespace KeypadGrid
{
	public class KeyPadGridViewModel : INotifyPropertyChanged
	{
		public ICommand OperandCommand { private set; get; }
		public ICommand OperationCommand { private set; get; }
		public ICommand UnaryOperationCommand { private set; get; }
		public ICommand BackSpaceCommand { private set; get; }
		public ICommand DecimalCommand { private set; get; }

		public Stack<CalulatorStackItem> CalculatorStack { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		private double operand1, operand2;
		public KeyPadGridViewModel()
		{
			CurrentOperand = "0";
			OperandCommand = new Command<object>(AppendOperand);
			OperationCommand = new Command<char>(OpCommand);
			UnaryOperationCommand = new Command<object>(UnaryOpCommand, CanUnary);
			BackSpaceCommand = new Command(BSCommand, CanBS);
			DecimalCommand = new Command(DecCommand, CanDec);

			RaisePropertyChanged("CurrentOperand");

		}

		public double FontSize
		{
			get
			{
				double retval = 5.0;
				switch (CurrentOperand.Length)
				{
					case (1):
					case (2):
					case (3):
					case (4):
					case (5):
					case (6):
					case (7):
						retval = 45.0;
						break;
					case (8):
					case (9):
						retval = 35.0;
					
						break;				
					
					case (10):
					case (11):
					case (12):
						retval = 30.0;
						break;
					

					case (13):	
					case (14):	

						retval = 20.0;				
						break;
					case (15):	case (16):	
					case (17):	
					case (18):
					case (19):
					case (20):
						retval = 15.0;
						RaisePropertyChanged("FontSize");	break;					
					default:
						retval = 10.0;
						break;

					

						
				}
				return retval;

			}

		

		}
		string currentOperand = "0";
	    public string CurrentOperand
		{
			private set
			{
				CultureInfo ci = CultureInfo.CurrentCulture;
				decimal d;
				if (decimal.TryParse(value, out d))
					currentOperand = d.ToString("G",ci);
				else
					currentOperand = "0";
				if (value.Contains(".") && !currentOperand.Contains("."))

					currentOperand += ".";
				if (currentOperand.Length > 0 && currentOperand[0] == '.')
					currentOperand = currentOperand.Insert(0, "0");
				RaisePropertyChanged("CurrentOperand");
				RaisePropertyChanged("FontSize");


			}
			 get { return currentOperand; }
		}

		bool CanUnary(object arg)
		{
			return CurrentOperand != "0";
		}

		bool CanDec(object arg)
		{
			return !this.CurrentOperand.Contains(".");
		}

		void DecCommand(object obj)
		{
			
				AppendOperand(".");
		}

		void BSCommand(object obj)
		{
			CurrentOperand = CurrentOperand.Substring(0, CurrentOperand.Length - 1);
			if (CurrentOperand == "-" || CurrentOperand == "-0" || CurrentOperand.Length == 0)
				CurrentOperand = "0";
			RaisePropertyChanged("CurrentOperand");
			
		}

		bool CanBS(object arg)
		{
			return this.CurrentOperand.Length >= 1 && CurrentOperand != "0" && !CurrentOperand.ToUpper().Contains("E");
		}
		const string unaryOps = "pct,plusminus";
		void UnaryOpCommand(object op)
		{
			var newChar = op as string;
			if (newChar != null && unaryOps.Contains(newChar))
			{
				switch (newChar)
				{
					case ("pct"):
						{
							float f = float.Parse(CurrentOperand);
							CurrentOperand = (f / 100.0).ToString();
							RaisePropertyChanged("CurrentOperand");
							break;
						}
					case ("plusminus"):
						{
							if (CurrentOperand.Length >= 1 & CurrentOperand != "0")
							{
								if (this.CurrentOperand.Contains("-"))
								{
									CurrentOperand = CurrentOperand.Remove(0, 1);
								}
								else
								{
									CurrentOperand = CurrentOperand.Insert(0, "-");
								}
								RaisePropertyChanged("CurrentOperand");
							}
							break;
						}
				}
			}
		}

		void OpCommand(char obj)
		{
			
		}
		const string digits = "0123456789.";
		void AppendOperand(object operandChar)
		{
			var newChar = operandChar as string;
			if (currentOperand.ToLower().Contains("e"))
			{
				operandChar = newChar;
				
			}else
			if (newChar != null && digits.Contains(newChar))
			{
				if (currentOperand == "0")
				{
					if (newChar == ".")
						currentOperand = "0.";
					else
					  currentOperand =  newChar;
				}
				else	
					currentOperand = currentOperand+ newChar;
				
			}
			RaisePropertyChanged("CurrentOperand");
			RaisePropertyChanged("FontSize");
			RefreshCanExecutes();
		}

	


		protected void RaisePropertyChanged(string propName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propName));
			}
			RefreshCanExecutes();
		}

		internal void RestoreState(IDictionary<string, object> properties)
		{
			
		}
		void RefreshCanExecutes()
		{
			if (BackSpaceCommand != null)
				((Command)this.BackSpaceCommand			).ChangeCanExecute();
			if (OperandCommand != null)
				((Command)this.OperandCommand			).ChangeCanExecute();
			if (DecimalCommand != null)
				((Command)this.DecimalCommand			).ChangeCanExecute();
			if (OperationCommand != null)
				((Command)this.OperationCommand			).ChangeCanExecute();
			if (UnaryOperationCommand != null)
				((Command)this.UnaryOperationCommand	).ChangeCanExecute();
		}


	}
}
