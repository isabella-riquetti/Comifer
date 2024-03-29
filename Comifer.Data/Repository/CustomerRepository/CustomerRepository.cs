﻿using Comifer.Data.Context;
using Comifer.Data.Models;
using Comifer.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly ComiferContext _context;

        public CustomerRepository(ComiferContext context) : base(context)
        {
            _context = context;
        }
    }
}
