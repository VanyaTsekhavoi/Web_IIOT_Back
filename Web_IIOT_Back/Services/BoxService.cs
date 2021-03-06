﻿using Controllers_Layer;
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
        private IInitControllersLayerService initControllersLayerService;


        public BoxService(IInitControllersLayerService initControllersLayerService)
        {
            this.initControllersLayerService = initControllersLayerService;

            Boxes = new List<Box>();

            for (int i = 1; i < 3; i++)
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

        /// <summary>
        /// Return Box with the id=boxId and set properties from Logic.cs
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public Box GetBox(int boxId)
        {
            var box = Boxes.SingleOrDefault(x => x.Id == boxId);
            try
            {
                box.Temperature = double.Parse(initControllersLayerService.StartLogic.firstBoxTemperature);
                box.Pressure = double.Parse(initControllersLayerService.StartLogic.firstBoxPressure);
                box.Humidity = double.Parse(initControllersLayerService.StartLogic.firstBoxHumidity);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("Connectivity error - " + boxId);
                //Console.WriteLine(ex);
            }

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
