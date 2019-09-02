using System;

namespace RealLifeScratch
{
    public class FunctionPointersHelper
    {
        public int vResistor;
        public Action<int> FunctionToRun;
        public bool IsEnd = false;
        public FunctionPointersHelper(int ResistorValue,Action<int> Function)
        {
            vResistor = ResistorValue;
            FunctionToRun = Function;
            if(Function == null) IsEnd = true;
        }
    }
}
