using System;
namespace KeypadGrid
{
	
	public enum E_BINARY_OPERATION { addition, subtract, divide, multiply }
	public enum E_UNARY_OPERATION { equal, plusminus }
	public class CalulatorStackItem
	{
		public CalulatorStackItem()
		{
		}

	}


	public class Operation : CalulatorStackItem
	{
		public E_BINARY_OPERATION OpType { get; set; }
	}

	public class Operand : CalulatorStackItem
	{
		public float Value { get; set; }
	}
}
