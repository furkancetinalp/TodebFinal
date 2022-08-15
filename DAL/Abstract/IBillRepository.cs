using DAL.EFBase;
using DTO.Bill;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface definition of Bill class
    public interface IBillRepository:IEFBaseRepository<Bill>
    {
    }
}
