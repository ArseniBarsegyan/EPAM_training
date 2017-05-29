using System;

namespace AtsCompany.Classes
{
    public class AnswerEventArgs : EventArgs
    {
        public readonly int number1;
        public readonly int number2;
        public readonly string message;

        public AnswerEventArgs(int number1, int number2, string message)
        {
            this.number1 = number1;
            this.number2 = number2;
            this.message = message;
        }

        public AnswerEventArgs(int number2, string message)
        {
            this.number2 = number2;
            this.message = message;
        }
    }
}
