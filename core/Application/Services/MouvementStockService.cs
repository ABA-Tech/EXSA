using Domain.Models;
using Domain.Services;
using Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MouvementStockService : AppService<MouvementStock>, IMouvementStockService
    {
        public MouvementStockService(IMouvementStockStore repository) : base(repository)
        {
        }
    }
}
