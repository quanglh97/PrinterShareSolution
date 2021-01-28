﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Enums
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
        OrderSendFile, //yeu cau gui file 0
        OrderPrintFile, //yeu cau in file 1
        ReceiveFile, //nhan file          2
        PrintFile //in file               3
    }
}
