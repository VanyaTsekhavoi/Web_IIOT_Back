using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Services
{
    public interface IBoxService
    {
        List<Box> GetAllBoxes();

        Box GetBox(int boxId);

        Box CreateBox();

        Box UpdateBox(int boxId);

        Box DeleteBox(int boxId);
    }
}
