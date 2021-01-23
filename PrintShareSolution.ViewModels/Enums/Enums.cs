using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Enums
{
    public enum Status
    {
        InActive,
        Active,
        AnyActive
    }
    public enum ActionOrder
    {
        SendFile,
        PrintFile
    }
    public enum ActionHistory
    {
        OrderSendFile,
        OrderPrintFile,
        ReceiveFile,
        PrintFile
    }
}
