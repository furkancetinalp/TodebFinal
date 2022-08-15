﻿using DAL.EFBase;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface definition of Messages class
    public interface IMessageRepository: IEFBaseRepository<Message>
    {
    }
}
