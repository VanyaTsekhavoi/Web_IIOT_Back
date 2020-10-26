using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Services
{
    public interface IPeripheryService
    {
        List<Periphery> GetAllPeripheries();

        Periphery GetPeriphery(int peripheryId);

        Periphery CreatePeriphery();

        Periphery UpdatePeriphery(int peripheryId);

        Periphery DeletePeriphery(int peripheryId);
    }
}
