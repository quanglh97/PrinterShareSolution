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

    public enum Result
    {
        NotDone, //chua thuc thien yeu cau
        Success, //thuc hien thanh cong
        CancelPrint,  //tu choi thuc hien yeu cau
        CancelReceive,
    }
    public enum Duplex
    {
        OneFace,
        TwoFaceVertical,
        TwoFaceHorizontal,
    }
}
