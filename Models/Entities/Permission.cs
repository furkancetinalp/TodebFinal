using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    //Defining user permissions by enum
    public enum Permission
    {
        AddBill =1,
        GetSpecificBill=2,
        GetAllBills,
        AssignFeeBills,
        AssignElectricityBills,
        AssignWaterBills,
        AssignGasBills,
        GetByBillType,
        GetBillByHouseNumber=9,
        GetByBTypeAndMonth=10,
        GetTotalAmountOfDebtByMonthAndBill,
        DeleteBill,

        AddHouse,
        GetAllHouses,
        GetByHouseNo,
        UpdateHouse,
        DeleteHouse,

        SendMeesageToAdmin=18,
        GetAllMessages,
        MarkMessagesAsRead,
        GetUnreadMessages,

        MakePayment=22,
        PaymentByHouseNo,
        AllPayments,

        Register,
        GetAllUsers,
        UserByIdentityNumber,
        UpdateUser,
        DeleteUser,
        GetUserByHouseId,

    }
}
