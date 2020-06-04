using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Services
{
    public class BoxService : IBoxService
    {
        private readonly List<Box> Boxes;

        public BoxService()
        {
            Boxes = new List<Box>();

            for (int i = 0; i < 2; i++)
            {
                Boxes.Add(new Box()
                {
                    Id = i,
                    Temperature = 30,
                    Humidity = 40,
                    Pressure = 100,
                    Relay = false,
                    VoltageSupply = false
                });
            }
        }

        public List<Box> GetAllBoxes()
        {
            return Boxes;
        }

        public Box GetBox(int boxId)
        {
            var box = Boxes.SingleOrDefault(x => x.Id == boxId);
            return box;
        }

        public Box CreateBox()
        {
            throw new NotImplementedException();
        }

        public Box UpdateBox(int boxId)
        {
            throw new NotImplementedException();
        }

        public Box DeleteBox(int boxId)
        {
            throw new NotImplementedException();
        }
    }
}
